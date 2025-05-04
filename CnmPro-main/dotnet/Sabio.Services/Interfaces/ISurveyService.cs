using Sabio.Models;
using Sabio.Models.Domain.Surveys;
using Sabio.Models.Requests.SurveyQuestions;
using Sabio.Models.Requests.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services.Interfaces
{
    public interface ISurveyService
    {
        int Submit(SurveyInstanceSubmitRequest model, int userId);
        SurveyInstance GetInstance(int id);

        List<SurveyInstance> GetAllInstances();
        int CreateSurveyQuestionWithAnswerOptions(SurveyQuestionWithOptionsRequest model, int userId);

        Paged<SurveyInstance> GetAllInstancesByIdPagination(int pageIndex, int pageSize, int Id);

        Paged<SurveyInsight> SearchInsights(int pageIndex, int pageSize, string query);

        Paged<SurveyInstance> GetAllInstancesPagination(int pageIndex, int pageSize);

        Paged<SurveyInstance> SearchInstances(int pageIndex, int pageSize, string query);

        SurveyInsight GetSurveyInsight(int id);

        List<SurveyInsight> GetAllSurveyInsights();

        Paged<SurveyInsight> GetAllInsightsPagination(int pageIndex, int pageSize);

        int AddInstance(SurveyInstanceAddRequest model, int userId);

        int CreateSurveyQuestion(SurveyQuestionAddRequest model, int userId);

        int AddQuestionAnswerOption(SurveyQuestionAnswerOptionAddRequest model, int userId);

        void UpdateInstance(SurveyInstanceUpdateRequest model);

        void DeleteQuestionAnswerOption(int id);

        void UpdateQuestionAnswerOption(SurveyQuestionAnswerOptionUpdateRequest model);

        void UpdateSurveyQuestion(SurveyQuestionUpdateRequest model, int userId);

        Survey GetByIdSurvey(int id);

        SurveyWithDetails GetByIdDetailsSurvey(int id);

        Paged<Survey> GetAllSurveys(int pageIndex, int pageSize);

        Paged<Survey> GetByCreatedBySurveys(int pageIndex, int pageSize, int createdBy);

        int AddSurvey(SurveyAddRequest model, int userId);

        int AddSurveyAnswers(SurveyAnswersAddRequest model);

        void UpdateSurvey(SurveyUpdateRequest model);

        void UpdateSurveyAnswers(SurveyAnswersUpdateRequest model);

        void DeleteSurveyAnswer(int id);

        Paged<Survey> SearchPaginatedSurveys(int pageIndex, int pageSize, string query);
    }
}
