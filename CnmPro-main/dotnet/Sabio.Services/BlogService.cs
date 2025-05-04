using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Requests.Blogs;
using Sabio.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace Sabio.Services
{
    public class BlogService : IBlogService
    {
        IDataProvider _data = null;
        IUserProfileMapper _profileMapper = null;
        ILookUpService _lookUp = null;

        public BlogService(IDataProvider data , IUserProfileMapper profileMapper, ILookUpService lookUp)
        {
            _data = data;
            _profileMapper = profileMapper;
            _lookUp = lookUp;
        }
        
        public void Delete(int id)
        {
            string procName = "[dbo].[Blogs_Delete_ById]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@Id", id);

            }, returnParameters: null);
        }

        public int AddBlog(BlogAddRequest model, int userId)
        {
            int Id = 0;

            string procName = "[dbo].[Blogs_Insert]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                AddCommonParams(model, col);
                col.AddWithValue("@AuthorId", userId);

                SqlParameter IdOut = new SqlParameter("@Id", SqlDbType.Int);
                IdOut.Direction = ParameterDirection.Output;

                col.Add(IdOut);

            }, returnParameters: delegate (SqlParameterCollection returnCollection)
            {
                object oId = returnCollection["@Id"].Value;
                int.TryParse(oId.ToString(), out Id);
                Console.WriteLine("");
            });
            return Id;
        }

        public void Update(BlogUpdateRequest model, int userId)
        {

            string procName = "[dbo].[Blogs_Update]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                AddCommonParams(model, col);
                col.AddWithValue("@AuthorId", userId);

                col.AddWithValue("@Id", model.Id);

            }, returnParameters: null);
        }

        public Blog GetById(int id)
        {

            string procName = "[dbo].[Blogs_Select_ById]";

            Blog blog = null;

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Id", id);

            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                blog = MapSingleBlog(reader, ref startingIndex);
            }
            );

            return blog;
        }

        public Paged<Blog> GetByAuthor(int authorId, int pageIndex, int pageSize)
        {
            Paged<Blog> pagedList = null;
            List<Blog> list = null;
            int totalCount = 0;

            _data.ExecuteCmd(
                "[dbo].[Blogs_Select_ByCreatedBy]"
                , (param) =>

                {
                    param.AddWithValue("@AuthorId", authorId);
                    param.AddWithValue("@PageIndex", pageIndex);
                    param.AddWithValue("@PageSize", pageSize);                
                },
                (reader, recordSetIndex) =>
                {
                    int startingIndex = 0;
                    Blog blog = MapSingleBlog(reader, ref startingIndex);
                    
                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(startingIndex++);
                    }

                    if (list == null)
                    {
                        list = new List<Blog>();
                    }

                    list.Add(blog);
                });
            if (list != null)
            {
                pagedList = new Paged<Blog>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }

        public Paged<Blog> GetAll(int pageIndex, int pageSize)
        {
            Paged<Blog> pagedList = null;
            List<Blog> list = null;
            int totalCount = 0;

            _data.ExecuteCmd(
                "[dbo].[Blogs_SelectAll_V3]"
                , (param) =>

                {
                    param.AddWithValue("@PageIndex", pageIndex);
                    param.AddWithValue("@PageSize", pageSize);
                },
                (reader, recordSetIndex) =>
                {
                    int startingIndex = 0;
                    Blog blog = MapSingleBlog(reader, ref startingIndex);
                    
                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(startingIndex++);
                    }

                    if (list == null)
                    {
                        list = new List<Blog>();
                    }

                    list.Add(blog);
                });
            if (list != null)
            {
                pagedList = new Paged<Blog>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }

        public Paged<Blog> SearchBySubject(string query, int pageIndex, int pageSize)
        {
            Paged<Blog> pagedList = null;
            List<Blog> list = null;
            int totalCount = 0;

            _data.ExecuteCmd(
                "[dbo].[Blogs_Select_BlogCategory]"
                , (param) =>

                {
                    param.AddWithValue("@Query", query);
                    param.AddWithValue("@PageIndex", pageIndex);
                    param.AddWithValue("@PageSize", pageSize);
                },
                (reader, recordSetIndex) =>
                {
                    int startingIndex = 0;
                    Blog blog = MapSingleBlog(reader, ref startingIndex);
                    
                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(startingIndex++);
                    }

                    if (list == null)
                    {
                        list = new List<Blog>();
                    }

                    list.Add(blog);
                });
            if (list != null)
            {
                pagedList = new Paged<Blog>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }

        private Blog MapSingleBlog(IDataReader reader, ref int startingIndex)
        {
            Blog blog = new Blog();
            blog.BlogType = new LookUp();


            blog.Id = reader.GetSafeInt32(startingIndex++);
            blog.Title = reader.GetSafeString(startingIndex++);
            blog.Subject = reader.GetSafeString(startingIndex++);
            blog.Content = reader.GetSafeString(startingIndex++);
            blog.IsPublished = reader.GetSafeBool(startingIndex++);
            blog.ImageUrl = reader.GetSafeString(startingIndex++);
            blog.DateCreated = reader.GetSafeDateTime(startingIndex++);
            blog.DateModified = reader.GetSafeDateTime(startingIndex++);
            blog.DatePublish = reader.GetSafeDateTime(startingIndex++);
            blog.BlogType = _lookUp.MapSingleLookUp(reader, ref startingIndex);
            blog.Author = _profileMapper.Map(reader, ref startingIndex);


            return blog;
        }

        private static void AddCommonParams(BlogAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@Title", model.Title);
            col.AddWithValue("@Subject", model.Subject);
            col.AddWithValue("@BlogTypeId", model.BlogTypeId);
            col.AddWithValue("@Content", model.Content);
            col.AddWithValue("@IsPublished", model.IsPublished);
            col.AddWithValue("@ImageUrl", model.ImageUrl);
            col.AddWithValue("@DatePublish", model.DatePublish);
        }
    }
}
