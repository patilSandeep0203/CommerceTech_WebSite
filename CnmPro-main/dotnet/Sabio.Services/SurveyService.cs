using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Domain.Surveys;
using Sabio.Models.Requests.Surveys;
using Sabio.Models.Domain.SurveyQuestions;
using Sabio.Models.Requests.SurveyQuestions;
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
    public class SurveyService : ISurveyService
    {
        private IDataProvider _data;
        private ILookUpService _lookUp;
        public SurveyService(IDataProvider dataProvider, ILookUpService LookUp)
        {
            _data = dataProvider;
            _lookUp = LookUp;
            
        }
        #region - Survey Submission -
        public int Submit(SurveyInstanceSubmitRequest model, int userId)
        {
            int id = 0;
            string procName = "[dbo].[Survey_Submit]";
            
            DataTable dt = new DataTable();
          
            
             dt = MapAnswersToTable(model.Questions);
            
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@SurveyId", model.SurveyId);
                    col.AddWithValue("@StatusId", model.StatusId);
                    col.AddWithValue("@UserId", userId);
                    col.AddWithValue("@batchAnswers", dt);
                   
                    SqlParameter idOut = new SqlParameter("@InstanceId", SqlDbType.Int);
                    idOut.Direction = ParameterDirection.Output;
                    col.Add(idOut);
                },
            returnParameters: delegate (SqlParameterCollection returnCollection)
            {
                object oId = returnCollection["@InstanceId"].Value;
                int.TryParse(oId.ToString(), out id);
            });
            return id;
        }
        #endregion
        private static DataTable MapAnswersToTable(List<SurveyAnswersAddRequest> answersToMap)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("QuestionId", typeof(int));
            dt.Columns.Add("AnswerOptionId", typeof(int));
            dt.Columns.Add("Answer", typeof(string));
            dt.Columns.Add("AnswerNumber", typeof(int));

                foreach (SurveyAnswersAddRequest singleAnswer in answersToMap)
                {
                    DataRow dr = dt.NewRow();
                    int startingIndex = 0;
                    dr.SetField(startingIndex++, singleAnswer.QuestionId);
                    dr.SetField(startingIndex++, singleAnswer.AnswerOptionId);
                    dr.SetField(startingIndex++, singleAnswer.Answer);
                    dr.SetField(startingIndex++, singleAnswer.AnswerNumber);
                    dt.Rows.Add(dr);
                }
            
            return dt;
        }

        #region Survey Instance Services
        public Paged<SurveyInstance> GetAllInstancesByIdPagination(int pageIndex, int pageSize, int Id)
        {
            Paged<SurveyInstance> pagedResult = null;
            List<SurveyInstance> result = null;
            int totalCount = 0;
            string procName = "[dbo].[SurveyInstances_SelectAllByIdPaginate]";
            _data.ExecuteCmd(procName,
             inputParamMapper: delegate (SqlParameterCollection col)
             {
                 col.AddWithValue("@PageIndex", pageIndex);
                 col.AddWithValue("@PageSize", pageSize);
                 col.AddWithValue("@Id", Id);
             },
             singleRecordMapper: delegate (IDataReader reader, short set)
             {
                 int startingIndex = 0;
                 SurveyInstance oneSurveyInstance = MapSingleInstance(reader, ref startingIndex);
                 if (totalCount == 0)
                 {
                     totalCount = reader.GetInt32(startingIndex++);
                 }
                 if (result == null)
                 {
                     result = new List<SurveyInstance>();
                 }
                 result.Add(oneSurveyInstance);
             });
            if (result != null)
            {
                pagedResult = new Paged<SurveyInstance>(result, pageIndex, pageSize, totalCount);
            }
            return pagedResult;
        }
        public Paged<SurveyInstance> GetAllInstancesPagination(int pageIndex, int pageSize)
        {
            Paged<SurveyInstance> pagedResult = null;
            List<SurveyInstance> result = null;
            int totalCount = 0;
            string procName = "[dbo].[SurveyInstances_SelectAllPaginateV2]";
            _data.ExecuteCmd(procName,
             inputParamMapper: delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@PageIndex", pageIndex);
                    col.AddWithValue("@PageSize", pageSize);
                },
             singleRecordMapper: delegate (IDataReader reader, short set)
             {
                 int startingIndex = 0;
                 SurveyInstance oneSurveyInstance = MapSingleInstance(reader, ref startingIndex);
                 if (totalCount == 0)
                 {
                     totalCount = reader.GetInt32(startingIndex++);
                 }
                 if (result == null)
                 {
                     result = new List<SurveyInstance>();
                 }
                 result.Add(oneSurveyInstance);
             });
            if (result != null)
            {
                pagedResult = new Paged<SurveyInstance>(result, pageIndex, pageSize, totalCount);
            }
            return pagedResult;
        }
        public Paged<SurveyInstance> SearchInstances(int pageIndex, int pageSize, string query)
        {
            Paged<SurveyInstance> pagedResult = null;
            List<SurveyInstance> result = null;
            int totalCount = 0;
            string procName = "[dbo].[SurveyInstances_SearchPaginateV2]";
            _data.ExecuteCmd(procName,
             inputParamMapper: delegate (SqlParameterCollection col)
             {
                 col.AddWithValue("@PageIndex", pageIndex);
                 col.AddWithValue("@PageSize", pageSize);
                 col.AddWithValue("@Query", query);

             },
             singleRecordMapper: delegate (IDataReader reader, short set)
             {
                 int startingIndex = 0;
                 SurveyInstance oneSurveyInstance = MapSingleInstance(reader, ref startingIndex);
                 if (totalCount == 0)
                 {
                     totalCount = reader.GetInt32(startingIndex++);
                 }
                 if (result == null)
                 {
                     result = new List<SurveyInstance>();
                 }
                 result.Add(oneSurveyInstance);
             });
            if (result != null)
            {
                pagedResult = new Paged<SurveyInstance>(result, pageIndex, pageSize, totalCount);
            }
            return pagedResult;
        }
        public Paged<SurveyInsight> SearchInsights(int pageIndex, int pageSize, string query)
        {
            Paged<SurveyInsight> pagedResult = null;
            List<SurveyInsight> result = null;
            int totalCount = 0;
            string procName = "[dbo].[SurveyInstances_SearchBySurvey]";
            _data.ExecuteCmd(procName,
             inputParamMapper: delegate (SqlParameterCollection col)
             {
                 col.AddWithValue("@PageIndex", pageIndex);
                 col.AddWithValue("@PageSize", pageSize);
                 col.AddWithValue("@Query", query);

             },
             singleRecordMapper: delegate (IDataReader reader, short set)
             {
                 int startingIndex = 0;
                 SurveyInsight oneSurveyInsight = MapSingleInsight(reader, ref startingIndex);
                 if (totalCount == 0)
                 {
                     totalCount = reader.GetInt32(startingIndex++);
                 }
                 if (result == null)
                 {
                     result = new List<SurveyInsight>();
                 }
                 result.Add(oneSurveyInsight);
             });
            if (result != null)
            {
                pagedResult = new Paged<SurveyInsight>(result, pageIndex, pageSize, totalCount);
            }
            return pagedResult;
        }
        public List<SurveyInstance> GetAllInstances()
        {
            string procName = "[dbo].[SurveyInstances_SelectAllV2]";
            List<SurveyInstance> surveyInstances = null;
            _data.ExecuteCmd(procName,
                inputParamMapper: null,
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    int startingIndex = 0;
                    SurveyInstance oneSurveyInstance = MapSingleInstance(reader, ref startingIndex);
                    if (surveyInstances == null)
                    {
                        surveyInstances = new List<SurveyInstance>();
                    }
                    surveyInstances.Add(oneSurveyInstance);
                });
            return surveyInstances;
        }
        public SurveyInstance GetInstance(int id)
        {
            string procName = "[dbo].[SurveyInstances_SelectByInstanceIdV2]";
            SurveyInstance oneSurveyInstance = null;
            _data.ExecuteCmd(procName,
                delegate (SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Id", id);
                }, delegate (IDataReader reader, short set)
                {
                    int startingIndex = 0;
                    oneSurveyInstance = MapSingleInstance(reader, ref startingIndex);
                }
                );
            return oneSurveyInstance;
        }
        public int AddInstance(SurveyInstanceAddRequest model, int userId)
        {
            int Id = 0;

            string procName = "[dbo].[SurveyInstances_Insert]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@SurveyId", model.SurveyId);
                col.AddWithValue("@StatusId", model.StatusId);
                col.AddWithValue("@UserId", userId);

                SqlParameter IdOut = new SqlParameter("@Id", SqlDbType.Int);
                IdOut.Direction = ParameterDirection.Output;

                col.Add(IdOut);

            }, returnParameters: delegate (SqlParameterCollection returnCollection)
            {
                object oId = returnCollection["@Id"].Value;
                int.TryParse(oId.ToString(), out Id);
            });
            return Id;
        }
        public void UpdateInstance(SurveyInstanceUpdateRequest model)
        {

            string procName = "[dbo].[SurveyInstances_Update]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@Id", model.Id);
                col.AddWithValue("@StatusId", model.StatusId);
            }, returnParameters: null);
        }
        public SurveyInsight GetSurveyInsight(int id)
        {
            string procName = "[dbo].[SurveyInstances_SelectBySurveyId]";
            SurveyInsight oneSurveyInsight = null;
            _data.ExecuteCmd(procName,
                delegate (SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Id", id);
                }, delegate (IDataReader reader, short set)
                {
                    int startingIndex = 0;
                    oneSurveyInsight = MapSingleInsight(reader, ref startingIndex);
                }
                );
            return oneSurveyInsight;
        }
        public List<SurveyInsight> GetAllSurveyInsights()
        {
            string procName = "[dbo].[SurveyInstances_SelectAllBySurvey]";
            List<SurveyInsight> surveyInsights = null;
            _data.ExecuteCmd(procName,
                inputParamMapper: null,
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    int startingIndex = 0;
                    SurveyInsight oneSurveyInsight = MapSingleInsight(reader, ref startingIndex);
                    if (surveyInsights == null)
                    {
                        surveyInsights = new List<SurveyInsight>();
                    }
                    surveyInsights.Add(oneSurveyInsight);
                });
            return surveyInsights;
        }
        public Paged<SurveyInsight> GetAllInsightsPagination(int pageIndex, int pageSize)
        {
            Paged<SurveyInsight> pagedResult = null;
            List<SurveyInsight> result = null;
            int totalCount = 0;
            string procName = "[dbo].[SurveyInstances_SelectAllBySurveyPaginate]";
            _data.ExecuteCmd(procName,
             inputParamMapper: delegate (SqlParameterCollection col)
             {
                 col.AddWithValue("@PageIndex", pageIndex);
                 col.AddWithValue("@PageSize", pageSize);
             },
             singleRecordMapper: delegate (IDataReader reader, short set)
             {
                 int startingIndex = 0;
                 SurveyInsight oneSurveyInsight = MapSingleInsight(reader, ref startingIndex);
                 if (totalCount == 0)
                 {
                     totalCount = reader.GetInt32(startingIndex++);
                 }
                 if (result == null)
                 {
                     result = new List<SurveyInsight>();
                 }
                 result.Add(oneSurveyInsight);
             });
            if (result != null)
            {
                pagedResult = new Paged<SurveyInsight>(result, pageIndex, pageSize, totalCount);
            }
            return pagedResult;
        }
        private SurveyInsight MapSingleInsight(IDataReader reader, ref int startingIndex)
        {
            SurveyInsight oneSurveyInsight = new SurveyInsight();
            oneSurveyInsight.Id = reader.GetSafeInt32(startingIndex++);
            oneSurveyInsight.Name = reader.GetSafeString(startingIndex++);
            oneSurveyInsight.InstanceCount = reader.GetSafeInt32(startingIndex++);
            oneSurveyInsight.Description = reader.GetSafeString(startingIndex++);
            oneSurveyInsight.Logo = reader.GetSafeString(startingIndex++);
            oneSurveyInsight.DateCreated = reader.GetSafeDateTime(startingIndex++);
            oneSurveyInsight.CreatorUserId = reader.GetSafeInt32(startingIndex++);
            oneSurveyInsight.CreatorFirstName = reader.GetSafeString(startingIndex++);
            oneSurveyInsight.CreatorLastName = reader.GetSafeString(startingIndex++);
            oneSurveyInsight.InsightData = reader.DeserializeObject<List<SurveyInsightDataItem>>(startingIndex++);
            return oneSurveyInsight;


        }
        private SurveyInstance MapSingleInstance(IDataReader reader, ref int startingIndex)
        {
            SurveyInstance oneSurveyInstance = new SurveyInstance();
            oneSurveyInstance.Id = reader.GetSafeInt32(startingIndex++);
            oneSurveyInstance.UserId = reader.GetSafeInt32(startingIndex++);
            oneSurveyInstance.FirstName = reader.GetSafeString(startingIndex++);
            oneSurveyInstance.LastName = reader.GetSafeString(startingIndex++);
            oneSurveyInstance.SurveyId = reader.GetSafeInt32(startingIndex++);
            oneSurveyInstance.SurveyName = reader.GetSafeString(startingIndex++);
            oneSurveyInstance.CompanyLogo = reader.GetSafeString(startingIndex++);
            oneSurveyInstance.SurveyType = _lookUp.MapSingleLookUp(reader, ref startingIndex);
            oneSurveyInstance.InstanceStatus = _lookUp.MapSingleLookUp(reader, ref startingIndex);
            oneSurveyInstance.DateCreated = reader.GetSafeDateTime(startingIndex++);
            oneSurveyInstance.SurveyData = reader.DeserializeObject<List<SurveyDataItem>>(startingIndex++);
            return oneSurveyInstance;
        }
        #endregion

        #region Survey Question Answer Option Services
        public int AddQuestionAnswerOption(SurveyQuestionAnswerOptionAddRequest model, int userId)
        {
            int Id = 0;
            string procName = "[dbo].[SurveyQuestionAnswerOptions_Insert]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                AddCommonQuestionAnswerOptionParams(model, col);
                col.AddWithValue("@CreatedBy", userId);

                SqlParameter IdOut = new SqlParameter("@Id", SqlDbType.Int);
                IdOut.Direction = ParameterDirection.Output;

                col.Add(IdOut);

            }, returnParameters: delegate (SqlParameterCollection returnCol)
            {
                object oId = returnCol["@Id"].Value;
                int.TryParse(oId.ToString(), out Id);
            });
            return Id;
        }
        public void UpdateQuestionAnswerOption(SurveyQuestionAnswerOptionUpdateRequest model)
        {
            string procName = "[dbo].[SurveyQuestionAnswerOptions_Update]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
        {
                col.AddWithValue("@Id", model.Id);
                AddCommonQuestionAnswerOptionParams(model, col);

            }, returnParameters: null);
        }
        public void DeleteQuestionAnswerOption(int id)
        {
            string procName = "[dbo].[SurveyQuestionAnswerOptions_Delete]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@Id", id);

            }, returnParameters: null);
        }

        private static void AddCommonQuestionAnswerOptionParams(SurveyQuestionAnswerOptionAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@Text", model.Text);
            col.AddWithValue("@Value", model.Value);
            col.AddWithValue("@AdditionalInfo", model.AdditionalInfo);
            col.AddWithValue("@SortOrder", model.SortOrder);
        }
        #endregion

        #region - Survey Answers -
        public void DeleteSurveyAnswer(int id)
        {
            string procName = "[dbo].[SurveyAnswers_DELETE]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection parameterCollection)
            {
                parameterCollection.AddWithValue("@Id", id);

            }, returnParameters: null);
        }

        public void UpdateSurveyAnswers(SurveyAnswersUpdateRequest model)
        {
            string procName = "[dbo].[SurveyAnswers_Update]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@Id", model.Id);
                AddCommonParamsSurveyAnswers(model, col);
            }, returnParameters: null);
        }

        public int AddSurveyAnswers(SurveyAnswersAddRequest model)
        {
            int id = 0;

            string procName = "[dbo].[SurveyAnswers_Insert]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
            {
                AddCommonParamsSurveyAnswers(model, col);

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

        private static void AddCommonParamsSurveyAnswers(SurveyAnswersAddRequest model, SqlParameterCollection col)
        {
            
            col.AddWithValue("@QuestionId", model.QuestionId);
            col.AddWithValue("@AnswerOptionId", model.AnswerOptionId);
            col.AddWithValue("@Answer", model.Answer);
            col.AddWithValue("@AnswerNumber", model.AnswerNumber);
        }
        #endregion

        #region - Surveys -
        
        public void UpdateSurvey(SurveyUpdateRequest model)
        {
            string procName = "[dbo].Surveys_Update";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@Id", model.Id);
                AddCommonParamsSurvey(model, col);


            }, returnParameters: null);
        }

        public int AddSurvey(SurveyAddRequest model, int userId)
        {
            int id = 0;

            string procName = "[dbo].[Surveys_Insert]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    AddCommonParamsSurvey(model, col);
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

        private static void AddCommonParamsSurvey(SurveyAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@Name", model.Name);
            col.AddWithValue("@Description", model.Description);
            col.AddWithValue("@StatusId", model.StatusId);
            col.AddWithValue("@SurveyTypeId", model.SurveyTypeId);
            col.AddWithValue("@CompanyLogo", model.CompanyLogo);
        }

        public Paged<Survey> SearchPaginatedSurveys(int pageIndex, int pageSize, string query)
        {
            Paged<Survey> pagedResult = null;

            List<Survey> result = null;

            int totalCount = 0;

            _data.ExecuteCmd(
                "[dbo].[Surveys_Search]",
                inputParamMapper: delegate (SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@PageIndex", pageIndex);
                    parameterCollection.AddWithValue("@PageSize", pageSize);
                    parameterCollection.AddWithValue("@Query", query);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    int index = 0;

                    Survey survey = MapSingleSurvey(reader, ref index);

                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(index++);
                    }


                    if (result == null)
                    {
                        result = new List<Survey>();
                    }

                    result.Add(survey);
                }

            );
            if (result != null)
            {
                pagedResult = new Paged<Survey>(result, pageIndex, pageSize, totalCount);
            }

            return pagedResult;

        }

        public Paged<Survey> GetByCreatedBySurveys(int pageIndex, int pageSize, int createdBy)
        {
            Paged<Survey> pagedResult = null;

            List<Survey> result = null;

            int totalCount = 0;

            _data.ExecuteCmd(
                "[dbo].[Surveys_SelectByCreatedBy]",
                inputParamMapper: delegate (SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@PageIndex", pageIndex);
                    parameterCollection.AddWithValue("@PageSize", pageSize);
                    parameterCollection.AddWithValue("@CreatedBy", createdBy);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    int index = 0;

                    Survey survey = MapSingleSurvey(reader, ref index);


                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(index++);
                    }


                    if (result == null)
                    {
                        result = new List<Survey>();
                    }

                    result.Add(survey);
                }

            );
            if (result != null)
            {
                pagedResult = new Paged<Survey>(result, pageIndex, pageSize, totalCount);
            }

            return pagedResult;

        }

        public Paged<Survey> GetAllSurveys(int pageIndex, int pageSize)
        {
            Paged<Survey> pagedResult = null;

            List<Survey> result = null;

            int totalCount = 0;

            _data.ExecuteCmd(
                "[dbo].[Surveys_SelectAllV2]",
                inputParamMapper: delegate (SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@PageIndex", pageIndex);
                    parameterCollection.AddWithValue("@PageSize", pageSize);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    int index = 0;
                    Survey survey = MapSingleSurvey(reader, ref index);

                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(index++);
                    }


                    if (result == null)
                    {
                        result = new List<Survey>();
                    }

                    result.Add(survey);
                }

            );
            if (result != null)
            {
                pagedResult = new Paged<Survey>(result, pageIndex, pageSize, totalCount);
            }

            return pagedResult;
        }

        public Survey GetByIdSurvey(int Id)
        {
            string procName = "[dbo].[Surveys_Select_ById]";

            Survey survey = null;
            int startingIndex = 0;

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection parameterCollection)
            {
                parameterCollection.AddWithValue("@Id", Id);
            }, delegate (IDataReader reader, short set)
            {
                survey = MapSingleSurvey(reader, ref startingIndex);
            });
            return survey;
        }

        private Survey MapSingleSurvey(IDataReader reader, ref int startingIndex)
        {
            Survey survey = new Survey();
            survey.Id = reader.GetSafeInt32(startingIndex++);
            survey.Name = reader.GetSafeString(startingIndex++);
            survey.Description = reader.GetSafeString(startingIndex++);
            survey.Status = _lookUp.MapSingleLookUp(reader, ref startingIndex);
            survey.SurveyType = _lookUp.MapSingleLookUp(reader, ref startingIndex);
            survey.CreatedBy = reader.GetSafeInt32(startingIndex++);
            survey.CreatorFirstName = reader.GetSafeString(startingIndex++);
            survey.CreatorLastName = reader.GetSafeString(startingIndex++);
            survey.CompanyLogo = reader.GetSafeString(startingIndex++);
            survey.DateCreated = reader.GetSafeDateTime(startingIndex++);
            survey.DateModified = reader.GetSafeDateTime(startingIndex++);


            return survey;
        }

        public SurveyWithDetails GetByIdDetailsSurvey(int Id)
        {
            string procName = "dbo.[Surveys_SelectById_DetailsV2]";

            SurveyWithDetails survey = null;
            int startingIndex = 0;

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection parameterCollection)
            {
                parameterCollection.AddWithValue("@Id", Id);
            }, delegate (IDataReader reader, short set)
            {
                survey = MapSingleSurveyQA(reader, ref startingIndex);
            });
            return survey;
        }
        private SurveyWithDetails MapSingleSurveyQA(IDataReader reader, ref int startingIndex)
        {
            SurveyWithDetails asurvey = new SurveyWithDetails();

            asurvey.Id = reader.GetSafeInt32(startingIndex++);
            asurvey.Name = reader.GetSafeString(startingIndex++);
            asurvey.Description = reader.GetSafeString(startingIndex++);
            asurvey.Status = _lookUp.MapSingleLookUp(reader, ref startingIndex);
            asurvey.SurveyType = _lookUp.MapSingleLookUp(reader, ref startingIndex);
            asurvey.CreatedBy = reader.GetSafeInt32(startingIndex++);
            asurvey.CompanyLogo = reader.GetSafeString(startingIndex++);
            asurvey.QandA = reader.DeserializeObject<List<QuestionsWithOptions>>(startingIndex++);
            asurvey.DateCreated = reader.GetSafeDateTime(startingIndex++);
            asurvey.DateModified = reader.GetSafeDateTime(startingIndex++);
            return asurvey;
        }
        #endregion

        #region SurveyQuestions
        public int CreateSurveyQuestionWithAnswerOptions(SurveyQuestionWithOptionsRequest model, int userId)
        {
            int id = 0;
            DataTable dt = new DataTable();
            dt = MapAnswerOptionsToTable(model.AnswerOptions);

            string procName = "[dbo].[SurveyQuestionWithBatchAnswerOptions_Insert]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                AddCommonParamsSurveyQuestion(model, col);
                col.AddWithValue(@"UserId", userId);
                col.AddWithValue("@batchAnswerOptions", dt);
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
        private static DataTable MapAnswerOptionsToTable(List<SurveyQuestionAnswerOptionAddRequest> answerOptionsToMap)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Text", typeof(string));
            dt.Columns.Add("Value", typeof(string));
            dt.Columns.Add("AdditionalInfo", typeof(string));
            dt.Columns.Add("SortOrder", typeof(int));

            foreach (SurveyQuestionAnswerOptionAddRequest singleAnswerOption in answerOptionsToMap)
            {
                DataRow dr = dt.NewRow();
                int startingIndex = 0;
                dr.SetField(startingIndex++, singleAnswerOption.Text);
                dr.SetField(startingIndex++, singleAnswerOption.Value);
                dr.SetField(startingIndex++, singleAnswerOption.AdditionalInfo);
                dr.SetField(startingIndex++, singleAnswerOption.SortOrder);
                dt.Rows.Add(dr);
            }

            return dt;
        }
        public int CreateSurveyQuestion(SurveyQuestionAddRequest model, int userId)
        {
            int id = 0;

            string procName = "[dbo].[SurveyQuestions_Insert]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                AddCommonParamsSurveyQuestion(model, col);
                col.AddWithValue(@"UserId", userId);

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

        public void UpdateSurveyQuestion(SurveyQuestionUpdateRequest model, int userId)
        {
            string procName = "[dbo].[SurveyQuestions_Update]";

            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                AddCommonParamsSurveyQuestion(model, col);
                col.AddWithValue("@Id", model.Id);
                col.AddWithValue("@UserId", userId);
            },
            returnParameters: null);
        }

        private static void AddCommonParamsSurveyQuestion(SurveyQuestionAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@SurveyId", model.SurveyId);
            col.AddWithValue("@Question", model.Question);
            col.AddWithValue("@HelpText", model.HelpText);
            col.AddWithValue("@IsRequired", model.IsRequired);
            col.AddWithValue("@IsMultipleAllowed", model.IsMultipleAllowed);
            col.AddWithValue("@QuestionTypeId", model.QuestionTypeId);
            col.AddWithValue("@StatusId", model.StatusId);
            col.AddWithValue("@SortOrder", model.SortOrder);
        }
    }
}
#endregion