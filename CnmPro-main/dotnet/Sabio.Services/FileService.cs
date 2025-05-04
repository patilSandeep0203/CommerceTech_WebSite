using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.AppSettings;
using Sabio.Models.Domain;
using Sabio.Models.Domain.Files;
using Sabio.Models.Domain.User;
using Sabio.Models.Requests.Files;
using Sabio.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace Sabio.Services
{
    public class FileService : IFileService
    {
        private IDataProvider _data = null;
        private AWSStorageConfig _amazonKeys;
        private ILookUpService _lookUp = null;

        public FileService(IDataProvider data, IOptions<AWSStorageConfig> amazonKeys, ILookUpService lookUp)
        {
            _data = data;
            _amazonKeys = amazonKeys.Value;
            _lookUp = lookUp;
        }

        public async Task<List<FileResponse>> UploadFilesAsync(List<IFormFile> files, int userId)
        {
            List<FileResponse> responsePackage = null;

            foreach (IFormFile file in files)

            {
                string fileType = GetFileType(file.ContentType);
                string keyName = Guid.NewGuid() + "_" + file.FileName;

                using (var client = new AmazonS3Client(_amazonKeys.AccessKey, _amazonKeys.Secret, RegionEndpoint.GetBySystemName(_amazonKeys.BucketRegion)))
                {
                    int id = 0;

                    TransferUtility fileTransferUtility = new TransferUtility(client);

                    await fileTransferUtility.UploadAsync(file.OpenReadStream(), _amazonKeys.BucketName, keyName);

                    string acceptedFile = Path.GetExtension(file.FileName);

                    FileAddRequest model = new FileAddRequest();

                    string url = $"{_amazonKeys.Domain}{keyName}";

                    model.Url = url;
                    model.Name = file.FileName;
                    model.FileType = acceptedFile.ToString();

                    id = AddFile(model, userId);

                    FileResponse response = new FileResponse()
                    {
                        Id = id,
                        Name = file.FileName,
                        Url = url
                    };

                    if (responsePackage == null)
                    {
                        responsePackage = new List<FileResponse>();
                    }

                    responsePackage.Add(response);
                }
            }
            return responsePackage;
        }

        public int AddFile(FileAddRequest model, int userId)
        {
            int id = 0;

            string procName = "[dbo].[Files_InsertV2]";
            _data.ExecuteNonQuery(procName,
               inputParamMapper: delegate (SqlParameterCollection col)
               {
                   col.AddWithValue("@Name", model.Name);
                   col.AddWithValue("@Url", model.Url);
                   col.AddWithValue("@FileType", model.FileType);
                   col.AddWithValue("@CreatedBy", userId);

                   SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                   idOut.Direction = ParameterDirection.Output;

                   col.Add(idOut);
               }, returnParameters: delegate (SqlParameterCollection returnCollection)
               {
                   object objId = returnCollection["@Id"].Value;
                   int.TryParse(objId.ToString(), out id);

                   Console.WriteLine("");
               });

            return id;
        }

        public Paged<FileBase> GetAll(int pageIndex, int pageSize)
        {
            string procName = "[dbo].[Files_SelectAll]";
            Paged<FileBase> pagedList = null;
            List<FileBase> list = null;
            int totalCount = 0;

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                AddCommonParams(pageIndex, pageSize, paramCollection);
            },
                delegate (IDataReader reader, short set)
                {
                    int startingIndex = 0;
                    FileBase file = MapSingleFile(reader, ref startingIndex);

                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(startingIndex++);
                    }

                    if (list == null)
                    {
                        list = new List<FileBase>();
                    }

                    list.Add(file);
                });
            if (list != null)
            {
                pagedList = new Paged<FileBase>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }

        public Paged<FileBase> GetByFileTypeId(int typeId, int pageIndex, int pageSize)
        {
            string procName = "[dbo].[Files_Select_ByFileTypeId_Paginated]";
            Paged<FileBase> pagedList = null;
            List<FileBase> list = null;
            int totalCount = 0;

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@TypeId", typeId);
                AddCommonParams(pageIndex, pageSize, paramCollection);
            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                FileBase file = MapSingleFile(reader, ref startingIndex);

                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(startingIndex++);
                }

                if (list == null)
                {
                    list = new List<FileBase>();
                }

                list.Add(file);
            });
            if (list != null)
            {
                pagedList = new Paged<FileBase>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }

        public Paged<FileBase> GetByUserId(int userId, int pageIndex, int pageSize)
        {
            string procName = "[dbo].[Files_Select_ByCreatedBy]";
            Paged<FileBase> pagedList = null;
            List<FileBase> list = null;
            int totalCount = 0;

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@CreatedBy", userId);
                AddCommonParams(pageIndex, pageSize, paramCollection);
            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                FileBase file = MapSingleFile(reader, ref startingIndex);

                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(startingIndex++);
                }

                if (list == null)
                {
                    list = new List<FileBase>();
                }

                list.Add(file);
            });
            if (list != null)
            {
                pagedList = new Paged<FileBase>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }

        public Paged<FileBase> GetByFileIsDeleted(bool isDeleted, int pageIndex, int pageSize)
        {
            string procName = "[dbo].[Files_Select_ByIsDeleted_Paginated]";
            Paged<FileBase> pagedList = null;
            List<FileBase> list = null;
            int totalCount = 0;

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)

            {
                paramCollection.AddWithValue("@IsDeleted", isDeleted);
                AddCommonParams(pageIndex, pageSize, paramCollection);
            },
                delegate (IDataReader reader, short set)
                {
                    int startingIndex = 0;
                    FileBase file = MapSingleFile(reader, ref startingIndex);

                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(startingIndex++);
                    }

                    if (list == null)
                    {
                        list = new List<FileBase>();
                    }

                    list.Add(file);
                });
            if (list != null)
            {
                pagedList = new Paged<FileBase>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }

        public Paged<FileBase> SearchPagination(int pageIndex, int pageSize, string query)
        {
            string procName = "[dbo].[Files_Search_Paginated]";
            Paged<FileBase> pagedList = null;
            List<FileBase> list = null;
            int totalCount = 0;

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                AddCommonParams(pageIndex, pageSize, paramCollection);
                paramCollection.AddWithValue("@Query", query);
            },
               delegate (IDataReader reader, short set)
               {
                   int startingIndex = 0;
                   FileBase aFile = MapSingleFile(reader, ref startingIndex);
                   totalCount = reader.GetSafeInt32(startingIndex);

                   if (list == null)
                   {
                       list = new List<FileBase>();
                   }

                   list.Add(aFile);
               });
            if (list != null)
            {
                pagedList = new Paged<FileBase>(list, pageIndex, pageSize, totalCount);
            }

            return pagedList;
        }

        public void DeleteById(int id, int deleteStatus)
        {
            string procName = "[dbo].[Files_Delete_ById]";

            _data.ExecuteNonQuery(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Id", id);
                paramCollection.AddWithValue("@Delete", deleteStatus);
            });
        }

        private static void AddCommonParams(int pageIndex, int pageSize, SqlParameterCollection paramCollection)
        {
            paramCollection.AddWithValue("@PageIndex", pageIndex);
            paramCollection.AddWithValue("@PageSize", pageSize);
        }

        private FileBase MapSingleFile(IDataReader reader, ref int startingIndex)
        {
            FileBase file = new FileBase();

            file.Id = reader.GetSafeInt32(startingIndex++);
            file.Name = reader.GetSafeString(startingIndex++);
            file.Url = reader.GetSafeString(startingIndex++);
            file.IsDeleted = reader.GetSafeBool(startingIndex++);
            file.FileType = _lookUp.MapSingleLookUp(reader, ref startingIndex);
            file.CreatedBy = reader.GetSafeInt32(startingIndex++);

            return file;
        }

        private static string GetFileType(string formFileType)
        {
            string fileType = null;
            switch (formFileType)
            {
                case "image/png":
                    fileType = "image";
                    break;

                case "image/jpg":
                    fileType = "image";
                    break;

                case "image/jpeg":
                    fileType = "image";
                    break;

                case "image/webp":
                    fileType = "image";
                    break;

                case "image/bmp":
                    fileType = "image";
                    break;

                case "image/gif":
                    fileType = "image";
                    break;

                case "application/pdf":
                    fileType = "pdf";
                    break;

                case "text/plain":
                    fileType = "text";
                    break;

                case "audio/mpeg":
                    fileType = "audio";
                    break;

                default:
                    throw new Exception("File Type Not Supported");
            }
            return fileType;
        }
    }

}
