using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Requests.Rating;
using Sabio.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services
{
    public class RatingsService : IRatingsService
    {
        IDataProvider _data = null;
        public RatingsService(IDataProvider data)
        {
            _data = data;
        }

        #region Get Methods
        private static Rating MapSingleRating(IDataReader reader, ref int startingIndex)
        {
            Rating rating = new Rating();
            rating.Id = reader.GetSafeInt32(startingIndex++);

            rating.RatingNumber = reader.GetSafeByte(startingIndex++);

            rating.Comment = new LookUp();
            rating.Comment.Id = reader.GetSafeInt32(startingIndex++);
            rating.Comment.Name = reader.GetSafeString(startingIndex++);
            rating.EntityType = new LookUp();
            rating.EntityType.Id = reader.GetSafeInt32(startingIndex++);
            rating.EntityType.Name = reader.GetSafeString(startingIndex++);
            rating.EntityId= reader.GetSafeInt32(startingIndex++);
            rating.DateCreated = reader.GetSafeDateTime(startingIndex++);
            rating.DateModified = reader.GetSafeDateTime(startingIndex++);          
            rating.CreatedById = reader.GetSafeInt32(startingIndex++);       
            rating.IsDeleted = reader.GetSafeBool(startingIndex++);

            return rating;
        }
        private static RatingCreatedBy MapSingleRatingCreatedBy(IDataReader reader, ref int startingIndex)
        {
            RatingCreatedBy ratingCreatedBy = new RatingCreatedBy();

            ratingCreatedBy.Id = reader.GetSafeInt32(startingIndex++);
            ratingCreatedBy.Rating = reader.GetSafeByte(startingIndex++);
            ratingCreatedBy.EntityTypeId = reader.GetSafeInt32(startingIndex++);
            ratingCreatedBy.EntityId = reader.GetSafeInt32(startingIndex++);

            return ratingCreatedBy;
        }
        private static void AddCommonParameter(SqlParameterCollection parameterCollection, int entityId, int entityTypeId)
        {
            parameterCollection.AddWithValue("@EntityId", entityId);
            parameterCollection.AddWithValue("@EntityTypeId", entityTypeId);
        }
        public Rating Get(int id)
        {
            Rating rating = null;

            string procName = "[dbo].[Ratings_Select_ById]";

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Id", id);
            },

            delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                rating = MapSingleRating(reader, ref startingIndex);
            }
            );
            return rating;
        }

        public RatingCreatedBy GetRatingCreatedBy(int entityId, int entityTypeId, int createdBy)
        {
            RatingCreatedBy ratingCreatedBy = null;

            string procName = "[dbo].[Ratings_CreatedBy]";

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@entityId", entityId);
                paramCollection.AddWithValue("@entityTypeId", entityTypeId);
                paramCollection.AddWithValue("@createdBy", createdBy);
            },

            delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                ratingCreatedBy = MapSingleRatingCreatedBy(reader, ref startingIndex);
                
            }
            );
            return ratingCreatedBy;
        }
        public RatingByComment GetRatingByComment(int commentId, int createdBy)
        {
            RatingByComment comment = new RatingByComment();

            string procName = "[dbo].[Ratings_Select_ByCommentId]";

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@commentId",commentId);
                paramCollection.AddWithValue("@createdBy", createdBy);
            },

            delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                comment.Id = reader.GetSafeInt32(startingIndex++);
                comment.Rating = reader.GetSafeByte(startingIndex++);
                comment.CommentId = reader.GetSafeInt32(startingIndex++);
            });
            return comment;
        }
        
        public Paged<Rating> GetPage(int pageIndex, int pageSize)
        {
            Paged<Rating> pagedResult = null;

            List<Rating> result = null;

            int totalCount = 0;

            _data.ExecuteCmd(
            "[dbo].[Ratings_SeletAll]",
                inputParamMapper: delegate (SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@PageIndex", pageIndex);
                    parameterCollection.AddWithValue("@PageSize", pageSize);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    Rating rating = new Rating();
                    int index = 0;
                    rating = MapSingleRating(reader, ref index);
                    if (totalCount == 0)
                    {
                        Console.WriteLine(index);
                        totalCount = reader.GetSafeInt32(index);
                    }

                    if (result == null)
                    {
                        result = new List<Rating>();
                    }

                    result.Add(rating);
                }

            );
            if (result != null)
            {
                pagedResult = new Paged<Rating>(result, pageIndex, pageSize, totalCount);
            }

            return pagedResult;
        }

        public Paged<Rating> GetByAuthor(int pageIndex, int pageSize, int createdBy)
        {
            Paged<Rating> pagedResult = null;

            List<Rating> result = null;

            int totalCount = 0;

            _data.ExecuteCmd(
            "[dbo].[Ratings_Select_ByCreatedBy]",
                inputParamMapper: delegate (SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@PageIndex", pageIndex);
                    parameterCollection.AddWithValue("@PageSize", pageSize);
                    parameterCollection.AddWithValue("@CreatedBy", createdBy);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    Rating rating = new Rating();
                    int index = 0;
                    rating = MapSingleRating(reader, ref index);

                    if (totalCount == 0)
                    {
                        Console.WriteLine(index);
                        totalCount = reader.GetSafeInt32(index);
                    }

                    if (result == null)
                    {
                        result = new List<Rating>();
                    }

                    result.Add(rating);
                }

            );
            if (result != null)
            {
                pagedResult = new Paged<Rating>(result, pageIndex, pageSize, totalCount);
            }

            return pagedResult;
        }
        public Paged<Rating> GetByEntityId(int pageIndex, int pageSize, int entityId, int entityTypeId)
        {
            Paged<Rating> pagedResult = null;

            List<Rating> result = null;

            int totalCount = 0;

            _data.ExecuteCmd(
            "[dbo].[Ratings_Select_ByEntityId]",
                inputParamMapper: delegate (SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@PageIndex", pageIndex);
                    parameterCollection.AddWithValue("@PageSize", pageSize);
                    AddCommonParameter(parameterCollection, entityId, entityTypeId);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    Rating rating = new Rating();
                    int index = 0;
                    rating = MapSingleRating(reader, ref index);
                    if (totalCount == 0)
                    {
                        Console.WriteLine(index);
                        totalCount = reader.GetSafeInt32(index);
                    }

                    if (result == null)
                    {
                        result = new List<Rating>();
                    }

                    result.Add(rating);
                }

            );
            if (result != null)
            {
                pagedResult = new Paged<Rating>(result, pageIndex, pageSize, totalCount);
            }

            return pagedResult;
        }
        public AvgRating GetAvgRating(int entityId, int entityTypeId)
        {
            AvgRating avg_rating = new AvgRating();

            string procName = "[dbo].[Rating_SelectSummary_ByEntityId]";

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection parameterCollection)
            {
                AddCommonParameter(parameterCollection, entityId, entityTypeId);
            },

            delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                avg_rating.Average = reader.GetSafeDecimal(startingIndex++);
            });

            return avg_rating;
        }

        #endregion

        #region Add and Update Methods
        private static void AddCommonParams(RatingAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@Rating", model.Rating);
            col.AddWithValue("@CommentId", model.CommentId);
            col.AddWithValue("@IsDeleted ", model.IsDeleted);
        }
        private static void AddCommonParams1(RatingAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@EntityTypeId ", model.EntityTypeId);
            col.AddWithValue("@EntityId", model.EntityId);
        }
        public int Add(RatingAddRequest model, int userId)
        {
            int Id = 0;
            string procName = "[dbo].[Ratings_Insert]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    AddCommonParams(model, col);
                    AddCommonParams1(model, col);
                    col.AddWithValue("@CreatedBy", userId);
                    SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                    idOut.Direction = ParameterDirection.Output;

                    col.Add(idOut);
                },
            returnParameters: delegate (SqlParameterCollection returnCollection)
            {
                object oId = returnCollection["@Id"].Value;

                int.TryParse(oId.ToString(), out Id);

            });
            return Id;
        }
        public void Update(RatingUpdateRequest model, int userId)
        {
            string procName = "[dbo].[Ratings_Update]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                   AddCommonParams(model, col);
                    col.AddWithValue("@Id", model.Id);
                    col.AddWithValue("@UserId", userId);
                },
            returnParameters: null);
        }
        #endregion
        #region Delete Method
        public void Delete(int Id, int userId)
        {
            string procName = "[dbo].[Ratings_Delete_ById]";

            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Id", Id);
                paramCollection.AddWithValue("@UserId", userId);
            },
            returnParameters: null
            );
        } 
        #endregion
    }
}
