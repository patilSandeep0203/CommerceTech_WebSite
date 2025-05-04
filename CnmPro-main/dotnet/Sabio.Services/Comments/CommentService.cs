using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain.Comments;
using Sabio.Models.Requests.Comments;
using Sabio.Services.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Sabio.Services.Comments
{
    public class CommentService : ICommentService
    {
        IDataProvider _data = null;
        public CommentService(IDataProvider data)
        {
            _data = data;
        }

        #region -- Create/Add
        public int AddComment(CommentAddRequest request, int userId)
        {
            int id = 0;
            string procName = "[dbo].[Comments_Insert]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                AddCommonParams(request, col);
                col.AddWithValue("@CreatedBy", userId);
                SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                idOut.Direction = ParameterDirection.Output;
                col.Add(idOut);
            },
            returnParameters: delegate (SqlParameterCollection returnCollection)
            {
                object oId = returnCollection["@Id"].Value;
                int.TryParse(oId.ToString(), out id);
            });
            return id;
        }
        #endregion

        #region -- GetById
        public Comment GetComment(int id)
        {
            string procName = "[dbo].[Comments_Select_ById]";

            Comment comment = null;

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Id", id);

            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                comment = MapSingleComment(reader, ref startingIndex);
            }
            );
            return comment;
        }
        #endregion

        #region -- GetByCreatedBy
        public Paged<Comment> GetByCreatedBy(int userId, int pageIndex, int pageSize)
        {
            Paged<Comment> pagedResult = null;
            List<Comment> result = null;
            int totalCount = 0;
            string procName = "[dbo].[Comments_Select_ByCreatedBy]";
            _data.ExecuteCmd(
                procName,
                inputParamMapper: delegate (SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@PageIndex", pageIndex);
                    parameterCollection.AddWithValue("@PageSize", pageSize);
                    parameterCollection.AddWithValue("@CreatedBy", userId);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    int startingIndex = 0;
                    Comment comment = MapSingleComment(reader, ref startingIndex);
                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(startingIndex++);
                    }
                    if (result == null)
                    {
                        result = new List<Comment>();
                    }
                    result.Add(comment);
                }
            );
            if (result != null)
            {
                pagedResult = new Paged<Comment>(result, pageIndex, pageSize, totalCount);
            }
            return pagedResult;
        }

        #endregion

        #region -- GetByEntityId
        public Paged<Comment> GetByEntityId(int EntityId, int EntityTypeId, int pageIndex, int pageSize)
        {
            Paged<Comment> pagedList = null;
            List<Comment> list = null;
            int totalCount = 0;
            string procName = "[dbo].[Comments_Select_ByEntityId]";
            _data.ExecuteCmd(procName, delegate (SqlParameterCollection parameterCollection)
            {
                parameterCollection.AddWithValue("@EntityId", EntityId);
                parameterCollection.AddWithValue("@EntityTypeId", EntityTypeId);
                parameterCollection.AddWithValue("@PageIndex", pageIndex);
                parameterCollection.AddWithValue("@PageSize", pageSize);
            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                Comment comment = MapSingleComment(reader, ref startingIndex);
                if (totalCount == 0)
                {
                totalCount = reader.GetSafeInt32(startingIndex++);
                }

                if (list == null)
                {
                    list = new List<Comment>();
                }
                list.Add(comment);
            }
           );
            if (list != null)
            {
                pagedList = new Paged<Comment>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }
        #endregion

        #region -- GetByParentId
        public Comment GetByParentId(int ParentId)
        {
            string procName = "[dbo].[Comments_Select_ByParentId]";
            Comment comment = null;
            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@ParentId", ParentId);
            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                comment = MapSingleComment(reader, ref startingIndex);
            }
            );
            return comment;
        }
        #endregion

        #region -- GetAllPaginated
        public Paged<Comment> GetCommentsPaginated(int pageIndex, int pageSize)
        {
            Paged<Comment> pagedList = null;
            List<Comment> list = null;
            int totalCount = 0;
            string procName = "[dbo].[Comments_SelectAllV2]";
            _data.ExecuteCmd(
           procName,
           inputParamMapper: delegate (SqlParameterCollection parameterCollection)
           {
               parameterCollection.AddWithValue("@PageIndex", pageIndex);
               parameterCollection.AddWithValue("@PageSize", pageSize);
           },
           singleRecordMapper: delegate (IDataReader reader, short set)
           {
               int startingIndex = 0;
               Comment comment = MapSingleComment(reader, ref startingIndex);
               if (totalCount == 0)
               {
               totalCount = reader.GetSafeInt32(startingIndex++);
               }
               if (list == null)
               {
                   list = new List<Comment>();
               }
               list.Add(comment);
           }
           );
            if (list != null)
            {
                pagedList = new Paged<Comment>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }
        #endregion

        #region -- GetRepliesPaginated
        public Paged<Comment> GetRepliesPaginated(int pageIndex, int pageSize)
        {
            Paged<Comment> pagedList = null;
            List<Comment> list = null;
            int totalCount = 0;
            string procName = "[dbo].[Comments_Select_Replies]";
            _data.ExecuteCmd(
           procName,
           inputParamMapper: delegate (SqlParameterCollection parameterCollection)
           {
               parameterCollection.AddWithValue("@PageIndex", pageIndex);
               parameterCollection.AddWithValue("@PageSize", pageSize);
           },
           singleRecordMapper: delegate (IDataReader reader, short set)
           {
               int startingIndex = 0;
               Comment comment = MapSingleComment(reader, ref startingIndex);
               totalCount = reader.GetSafeInt32(startingIndex++);
               if (list == null)
               {
                   list = new List<Comment>();
               }
               list.Add(comment);
           }
           );
            if (list != null)
            {
                pagedList = new Paged<Comment>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }
        #endregion

        #region -- Update
        public void UpdateComment(CommentUpdateRequest request, int UserId)
        {
            string procName = "[dbo].[Comments_Update]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@Id", request.Id);
                col.AddWithValue("@IsDeleted", request.IsDeleted);
                col.AddWithValue("@Text", request.Text);
                col.AddWithValue("@UserId", UserId);
            },
            returnParameters: null);
        }
        #endregion

        #region -- Delete
        public void DeleteComment(int id)
        {
            string procName = "[dbo].[Comments_Delete_ById]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@Id", id);
                },
                returnParameters: null);
        }
        #endregion

        private static void AddCommonParams(CommentAddRequest request, SqlParameterCollection col)
        {
            col.AddWithValue("@Text", request.Text);
            col.AddWithValue("@ParentId", request.ParentId);
            col.AddWithValue("@EntityTypeId", request.EntityTypeId);
            col.AddWithValue("@EntityId", request.EntityId);
            col.AddWithValue("@IsDeleted", request.IsDeleted);

        }
        private static Comment MapSingleComment(IDataReader reader, ref int startingIndex)
        {
            Comment acomment = new Comment();
            acomment.Id = reader.GetSafeInt32(startingIndex++);
            acomment.Subject = reader.GetSafeString(startingIndex++);
            acomment.Text = reader.GetSafeString(startingIndex++);
            acomment.ParentId = reader.GetSafeInt32(startingIndex++);
            acomment.EntityTypeId = reader.GetSafeInt32(startingIndex++);
            acomment.EntityId = reader.GetSafeInt32(startingIndex++);
            acomment.DateCreated = reader.GetSafeDateTime(startingIndex++);
            acomment.DateModified = reader.GetSafeDateTime(startingIndex++);
            acomment.CreatedBy = reader.GetSafeInt32(startingIndex++);
            acomment.FirstName = reader.GetSafeString(startingIndex++);
            acomment.LastName = reader.GetSafeString(startingIndex++);
            acomment.AvatarUrl = reader.GetSafeString(startingIndex++);
            acomment.IsDeleted = reader.GetSafeBool(startingIndex++);
            acomment.Replies = reader.DeserializeObject<List<Comment>>(startingIndex++);
            return acomment;
        }
    }
}
