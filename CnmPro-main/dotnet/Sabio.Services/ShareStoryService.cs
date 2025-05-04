using Microsoft.AspNetCore.Authentication;
using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Domain.Files;
using Sabio.Models.Domain.ShareStory;
using Sabio.Models.Domain.User;
using Sabio.Models.Requests;
using Sabio.Models.Requests.Files;
using Sabio.Models.Requests.ShareStory;
using Sabio.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services
{
    public class ShareStoryService : IShareStoryService
    {
        IDataProvider _data = null;
        IUserProfileMapper _profileMapper = null;

        public ShareStoryService(IDataProvider data, IUserProfileMapper profileMapper)
        {
            _data = data;
            _profileMapper = profileMapper;
        }
        public int Add(ShareStoryAddRequest model, int id)
        {
            int ShareStoryId = 0;

            string procName = "[dbo].[ShareStory_Insert]";
            DataTable mapFieldIds = null;
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                if (mapFieldIds == null)
                {
                    mapFieldIds = MappingFiles(model.FileIds);
                }
                AddCommonParams(model, col, id);
                col.AddWithValue("@ListFileIds", mapFieldIds);


                SqlParameter ShareStoryIdOut = new SqlParameter("@ShareStoryId", SqlDbType.Int);
                ShareStoryIdOut.Direction = ParameterDirection.Output;

                col.Add(ShareStoryIdOut);

            }, returnParameters: delegate (SqlParameterCollection returnCollection)
            {
                object oId = returnCollection["@ShareStoryId"].Value;
                int.TryParse(oId.ToString(), out ShareStoryId);
            });

            return ShareStoryId;
        }
        public void Update(ShareStoryUpdateRequest model, int ShareStoryId, int userId)
        {
            string procName = "[dbo].[ShareStory_Update]";
            DataTable mapFieldIds = null;
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                if (mapFieldIds == null)
                {
                    mapFieldIds = MappingFiles(model.FileIds);
                }
                AddCommonParams(model, col, userId);
                col.AddWithValue("@ShareStoryId", ShareStoryId);
                col.AddWithValue("@ListFileIds", mapFieldIds);

            }, returnParameters: null);
        }
        public ShareStory GetStoryById(int id)
        {
            string procName = "[dbo].[ShareStory_SelectById]";

            ShareStory story = null;

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Id", id);

            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                story = MapSingleStories(reader, ref startingIndex);

            });
            return story;
        }
        public void Delete(int id)
        {
            string procName = "[dbo].[ShareStory_DeleteById]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@Id", id);
                },
                returnParameters: null);
        }
        public Paged<ShareStory> GetAll(int pageIndex, int pageSize)
        {
            string procName = "[dbo].[ShareStory_SelectAll]";
            List<ShareStory> files = null;
            Paged<ShareStory> pagedList = null;
            int totalCount = 0;
            _data.ExecuteCmd(procName,
                delegate (SqlParameterCollection inputParams)
                {
                    inputParams.AddWithValue("@PageIndex", pageIndex);
                    inputParams.AddWithValue("@PageSize", pageSize);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    int startingIndex = 0;
                    ShareStory story = MapSingleStories(reader, ref startingIndex);
                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(startingIndex++);
                    }
                    if (files == null)
                    {
                        files = new List<ShareStory>();
                    }
                    files.Add(story);
                }
            );

            if (files != null)
            {
                pagedList = new Paged<ShareStory>(files, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }
        private static DataTable MappingFiles(List<int> FileIdsToMap)
        {
            DataTable dt = null;

            if (FileIdsToMap != null)
            {
                dt = new DataTable();
                dt.Columns.Add("Id", typeof(Int32));

                foreach (int singleId in FileIdsToMap)
                {
                    DataRow dr = dt.NewRow();
                    int startingIndex = 0;
                    dr.SetField(startingIndex++, singleId);

                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        private static void AddCommonParams(ShareStoryAddRequest model, SqlParameterCollection col, int userId)
        {
            col.AddWithValue("Name", model.Name);
            col.AddWithValue("Email", model.Email);
            col.AddWithValue("Story", model.Story);
            col.AddWithValue("CreatedBy", userId);
        }
        private static ShareStory MapSingleStories(IDataReader reader, ref int startingIndex)
        {
            ShareStory model = new ShareStory();

            model.Id = reader.GetSafeInt32(startingIndex++);
            model.Name = reader.GetSafeString(startingIndex++);
            model.Email = reader.GetSafeString(startingIndex++);
            model.Story = reader.GetSafeString(startingIndex++);
            model.CreatedBy = reader.GetSafeInt32(startingIndex++);
            model.File = reader.DeserializeObject<List<FileBase>>(startingIndex++);

            return model;
        }
        private static ShareStoryFile MapSingleStoryFile(IDataReader reader, ref int startingIndex)
        {
            ShareStoryFile model = new ShareStoryFile();
            model.FileId = reader.GetSafeInt32(startingIndex++);
            model.ShareStoryId = reader.GetSafeInt32(startingIndex++);

            return model;
        }
    }
}

