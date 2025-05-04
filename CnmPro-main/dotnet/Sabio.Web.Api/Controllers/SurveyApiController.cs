using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Domain.SurveyQuestions;
using Sabio.Models.Requests.SurveyQuestions;
using Sabio.Models.Domain.Surveys;
using Sabio.Models.Requests.Surveys;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/surveys")]
    [ApiController]
    public class SurveyApiController : BaseApiController
    {
        private ISurveyService _service = null;
        private IAuthenticationService<int> _authService = null;

        public SurveyApiController(ISurveyService service
            , ILogger<SurveyApiController> logger
            , IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _authService = authService;
        }
        #region - Submit User Complete Survey -
        [HttpPost("submit")]
        public ActionResult<ItemsResponse<int>> Submit(SurveyInstanceSubmitRequest model)
        {
            int userId = _authService.GetCurrentUserId();
            ObjectResult result = null;
            try
            {
                int id = _service.Submit(model, userId);
                ItemResponse<int> response = new ItemResponse<int>() { Item = id };
                result = Created201(response);
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex.ToString());
                ErrorResponse response = new ErrorResponse(ex.Message);
                result = StatusCode(500, response);
            }
            return result;
        }
        #endregion

        #region Survey Instance
        #region Instance Add/Update
        [HttpPost("instances")]
        public ActionResult<ItemResponse<int>> CreateInstance(SurveyInstanceAddRequest model)
        {
            int userId = _authService.GetCurrentUserId();

            ObjectResult result = null;

            try
            {
                int id = _service.AddInstance(model, userId);
                ItemResponse<int> response = new ItemResponse<int>() { Item = id };
                result = Created201(response);

            }

            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                ErrorResponse response = new ErrorResponse(ex.Message);

                result = StatusCode(500, response);
            }
            return result;
        }
        [HttpPut("instances/{id:int}")]
        public ActionResult<SuccessResponse> UpdateInstance(SurveyInstanceUpdateRequest model)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                _service.UpdateInstance(model);
                response = new SuccessResponse();

            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(code, response);
        }
        #endregion
        #region Instance Get
        [HttpGet("instances/{id:int}")]
        public ActionResult<ItemResponse<SurveyInstance>> GetInstanceById(int id)
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                SurveyInstance surveyInstance = _service.GetInstance(id);
                if (surveyInstance == null)
                {
                    code = 404;
                    response = new ErrorResponse("Instance not found.");
                }
                else
                {
                    response = new ItemResponse<SurveyInstance>() { Item = surveyInstance };
                }

            }
            catch (Exception ex)
            {
                code = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(code, response);

        }
        [HttpGet("instances")]
        public ActionResult<ItemsResponse<SurveyInstance>> GetAllInstances()
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                List<SurveyInstance> surveyInstances = _service.GetAllInstances();
                if (surveyInstances == null)
                {
                    code = 404;
                    response = new ErrorResponse("Resource not found");
                }
                else
                {
                    response = new ItemsResponse<SurveyInstance>() { Items = surveyInstances };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Error: ${ex.Message}");
            }
            return StatusCode(code, response);
        }
        [HttpGet("instances/paginate")]
        public ActionResult<ItemResponse<Paged<SurveyInstance>>> GetAllInstancesPagination(int pageIndex, int pageSize)
        {
            ActionResult result = null;
            try
            {
                Paged<SurveyInstance> paged = _service.GetAllInstancesPagination(pageIndex, pageSize);
                if (paged == null)
                {
                    result = NotFound404(new ErrorResponse("Records not found."));
                }
                else
                {
                    ItemResponse<Paged<SurveyInstance>> response = new ItemResponse<Paged<SurveyInstance>>() { Item = paged };
                    result = Ok200(response);
                }
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex.ToString());
                result = StatusCode(500, new ErrorResponse($"Generic Error: ${ex.Message}"));
            }
            return result;
        }
        [HttpGet("instances/paginate/{id:int}")]
        public ActionResult<ItemResponse<Paged<SurveyInstance>>> GetAllInstancesByIdPagination(int pageIndex, int pageSize, int id)
        {
            ActionResult result = null;
            try
            {
                Paged<SurveyInstance> paged = _service.GetAllInstancesByIdPagination(pageIndex, pageSize, id);
                if (paged == null)
                {
                    result = NotFound404(new ErrorResponse("Records not found."));
                }
                else
                {
                    ItemResponse<Paged<SurveyInstance>> response = new ItemResponse<Paged<SurveyInstance>>() { Item = paged };
                    result = Ok200(response);
                }
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex.ToString());
                result = StatusCode(500, new ErrorResponse($"Generic Error: ${ex.Message}"));
            }
            return result;
        }
        [HttpGet("instances/search")]
        public ActionResult<ItemResponse<Paged<SurveyInstance>>> SearchInstances(int pageIndex, int pageSize, string query)
        {
            ActionResult result = null;
            try
            {
                Paged<SurveyInstance> paged = _service.SearchInstances(pageIndex, pageSize, query);
                if (paged == null)
                {
                    result = NotFound404(new ErrorResponse("Records not found."));
                }
                else
                {
                    ItemResponse<Paged<SurveyInstance>> response = new ItemResponse<Paged<SurveyInstance>>() { Item = paged };
                    result = Ok200(response);
                }
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex.ToString());
                result = StatusCode(500, new ErrorResponse($"Generic Error: ${ex.Message}"));
            }
            return result;
        }
        #endregion
        #region Insights
        [HttpGet("insights/{id:int}")]
        public ActionResult<ItemResponse<SurveyInsight>> GetInsightById(int id)
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                SurveyInsight surveyInsight = _service.GetSurveyInsight(id);
                if (surveyInsight == null)
                {
                    code = 404;
                    response = new ErrorResponse("Instance not found.");

                }
                else
                {
                    response = new ItemResponse<SurveyInsight>() { Item = surveyInsight };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(code, response);
        }
        [HttpGet("insights")]
        public ActionResult<ItemsResponse<SurveyInsight>> GetAllInsights()
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                List<SurveyInsight> surveyInsights = _service.GetAllSurveyInsights();
                if (surveyInsights == null)
                {
                    code = 404;
                    response = new ErrorResponse("Resource not found");
                }
                else
                {
                    response = new ItemsResponse<SurveyInsight>() { Items = surveyInsights };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Error: ${ex.Message}");
            }
            return StatusCode(code, response);
        }
        [HttpGet("insights/paginate")]
        public ActionResult<ItemResponse<Paged<SurveyInsight>>> GetAllInsightsPagination(int pageIndex, int pageSize)
        {
            ActionResult result = null;
            try
            {
                Paged<SurveyInsight> paged = _service.GetAllInsightsPagination(pageIndex, pageSize);
                if (paged == null)
                {
                    result = NotFound404(new ErrorResponse("Records not found."));
                }
                else
                {
                    ItemResponse<Paged<SurveyInsight>> response = new ItemResponse<Paged<SurveyInsight>>() { Item = paged };
                    result = Ok200(response);
                }
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex.ToString());
                result = StatusCode(500, new ErrorResponse($"Generic Error: ${ex.Message}"));
            }
            return result;
        }
        [HttpGet("insights/search")]
        public ActionResult<ItemResponse<Paged<SurveyInsight>>> SearchInsights(int pageIndex, int pageSize, string query)
        {
            ActionResult result = null;
            try
            {
                Paged<SurveyInsight> paged = _service.SearchInsights(pageIndex, pageSize, query);
                if (paged == null)
                {
                    result = NotFound404(new ErrorResponse("Records not found."));
                }
                else
                {
                    ItemResponse<Paged<SurveyInsight>> response = new ItemResponse<Paged<SurveyInsight>>() { Item = paged };
                    result = Ok200(response);
                }
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex.ToString());
                result = StatusCode(500, new ErrorResponse($"Generic Error: ${ex.Message}"));
            }
            return result;
        }
        #endregion
        #endregion

        #region Survey Question Answer Option
        [HttpPost("answeroptions")]
        public ActionResult<ItemResponse<int>> CreateQuestionAnswerOption(SurveyQuestionAnswerOptionAddRequest model)
        {
            int userId = _authService.GetCurrentUserId();
            ObjectResult result = null;
            try
            {
                int id = _service.AddQuestionAnswerOption(model, userId);
                ItemResponse<int> response = new ItemResponse<int>() { Item = id };
                result = Created201(response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                ErrorResponse response = new ErrorResponse(ex.Message);
                result = StatusCode(500, response);
            }
            return result;
        }
        [HttpPut("answeroptions/{id:int}")]
        public ActionResult<SuccessResponse> UpdateQuestionAnswerOption(SurveyQuestionAnswerOptionUpdateRequest model)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                _service.UpdateQuestionAnswerOption(model);
                response = new SuccessResponse();

            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(code, response);
        }
        [HttpDelete("answeroptions/{id:int}")]
        public ActionResult<SuccessResponse> DeleteQuestionAnswerOption(int id)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                _service.DeleteQuestionAnswerOption(id);
                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(code, response);
        }
        #endregion

        #region - SurveyAnswers Delete (SurveyAnswers) -
        [HttpDelete("answers/{id:int}")]
        public ActionResult<SuccessResponse> DeleteSurveyAnswer(int id)
        {
            int code = 200;
            BaseResponse response = null; //do not declare an instance.

            try
            {

                _service.DeleteSurveyAnswer(id);

                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(code, response);
        }
        #endregion

        #region - Survey(Answers) Update -
        [HttpPut("answers/{id:int}")]
        public ActionResult<ItemResponse<int>> UpdateSurveyAnswers(SurveyAnswersUpdateRequest model)
        {

            int code = 200;
            BaseResponse response = null;

            try
            {

                _service.UpdateSurveyAnswers(model);

                response = new SuccessResponse();
            }
            catch (Exception ex)
            {

                code = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(code, response);
        }
        #endregion

        #region - Survey(Answers) AddSurveyAnswer -
        [HttpPost("answers")]
        public ActionResult<ItemsResponse<int>> AddSurveyAnswers(SurveyAnswersAddRequest model)
        {
            ObjectResult result = null;
            try
            {

                int id = _service.AddSurveyAnswers(model);

                ItemResponse<int> response = new ItemResponse<int>() { Item = id };

                result = Created201(response);
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex.ToString());
                ErrorResponse response = new ErrorResponse(ex.Message);

                result = StatusCode(500, response);
            }
            return result;
        }
        #endregion

        #region - Survey Search Paginated -
        [HttpGet("search")]
        public ActionResult<ItemResponse<Paged<Survey>>> SearchPaginated(int pageIndex, int pageSize, string query)
        {
            int code = 200;
            BaseResponse response = null;//do not declare an instance.

            try
            {
                Paged<Survey> page = _service.SearchPaginatedSurveys(pageIndex, pageSize, query);

                if (page == null)
                {
                    code = 404;
                    response = new ErrorResponse("App Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Paged<Survey>> { Item = page };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
                base.Logger.LogError(ex.ToString());
            }


            return StatusCode(code, response);

        }
        #endregion

        #region - Survey Update -
        [HttpPut("{id:int}")]
        public ActionResult<ItemResponse<int>> UpdateSurvey(SurveyUpdateRequest model)
        {

            int code = 200;
            BaseResponse response = null;

            try
            {
                int userId = _authService.GetCurrentUserId();
                _service.UpdateSurvey(model);

                response = new SuccessResponse();
            }
            catch (Exception ex)
            {

                code = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(code, response);
        }
        #endregion

        #region - Survey Add/Create -
        [HttpPost]
        public ActionResult<ItemsResponse<int>> AddSurvey(SurveyAddRequest model)
        {
            int userId = _authService.GetCurrentUserId();

            ObjectResult result = null;
            try
            {

                int id = _service.AddSurvey(model, userId);

                ItemResponse<int> response = new ItemResponse<int>() { Item = id };

                result = Created201(response);
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex.ToString());
                ErrorResponse response = new ErrorResponse(ex.Message);

                result = StatusCode(500, response);
            }
            return result;
        }
        #endregion

        #region - Survey GetByCreatedBy -
        [HttpGet("paginate/{createdBy:int}")]
        public ActionResult<ItemResponse<Paged<Survey>>> GetByCreatedBySurveys(int pageIndex,
    int pageSize, int createdBy)
        {
            int code = 200;
            BaseResponse response = null;//do not declare an instance.

            try
            {
                Paged<Survey> page = _service.GetByCreatedBySurveys(pageIndex, pageSize, createdBy);

                if (page == null)
                {
                    code = 404;
                    response = new ErrorResponse("App Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Paged<Survey>> { Item = page };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
                base.Logger.LogError(ex.ToString());
            }


            return StatusCode(code, response);

        }
        #endregion

        #region - Survey GetAll(Paginated) -
        [HttpGet("paginate")]
        public ActionResult<ItemResponse<Paged<Survey>>> GetAllSurveys(int pageIndex, int pageSize)
        {
            int code = 200;
            BaseResponse response = null;//do not declare an instance.

            try
            {
                Paged<Survey> page = _service.GetAllSurveys(pageIndex, pageSize);

                if (page == null)
                {
                    code = 404;
                    response = new ErrorResponse("Surveys not found.");
                }
                else
                {
                    response = new ItemResponse<Paged<Survey>> { Item = page };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
                base.Logger.LogError(ex.ToString());
            }


            return StatusCode(code, response);

        }
        #endregion

        #region - Survey GetById -
        [HttpGet("{id:int}")]
        public ActionResult<ItemResponse<Survey>> GetByIdSurvey(int Id)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                Survey survey = _service.GetByIdSurvey(Id);
                if (survey == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("Survey Not Found.");

                }
                else
                {
                    iCode = 200;
                    response = new ItemResponse<Survey> { Item = survey };

                }
            }
            catch (SqlException sqlEx)
            {
                iCode = 500;
                // i have some other clean up code, or logging to do
                response = new ErrorResponse($"SqlException Error: ${sqlEx.Message}");
                base.Logger.LogError(sqlEx.ToString());

            }
            catch (ArgumentException argEx)
            {
                // i have some other clean up code, or logging to do
                iCode = 500;
                response = new ErrorResponse($"ArgumentException Error: ${argEx.Message}");
            }

            catch (Exception ex)
            {
                // ????
                iCode = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Error: ${ex.Message}");
            }

            return StatusCode(iCode, response);
        }
        #endregion

        #region - Survey GetById W/ Questions & Answers -
        [HttpGet("details/{id:int}")]
        public ActionResult<ItemResponse<SurveyWithDetails>> GetByIdDetailsSurvey(int Id)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                SurveyWithDetails survey = _service.GetByIdDetailsSurvey(Id);
                if (survey == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("Survey Not Found.");

                }
                else
                {
                    iCode = 200;
                    response = new ItemResponse<SurveyWithDetails> { Item = survey };

                }
            }
            catch (SqlException sqlEx)
            {
                iCode = 500;
                // i have some other clean up code, or logging to do
                response = new ErrorResponse($"SqlException Error: ${sqlEx.Message}");
                base.Logger.LogError(sqlEx.ToString());

            }
            catch (ArgumentException argEx)
            {
                // i have some other clean up code, or logging to do
                iCode = 500;
                response = new ErrorResponse($"ArgumentException Error: ${argEx.Message}");
            }

            catch (Exception ex)
            {
                // ????
                iCode = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Error: ${ex.Message}");
            }

            return StatusCode(iCode, response);
        }
        #endregion

        #region SurveyQuestions
        [HttpPost("questions")]
        public ActionResult<ItemResponse<int>> CreateSurveyQuestion(SurveyQuestionAddRequest model)
        {
            int code = 200;
            BaseResponse response = null; ;

            try
            {
                int userId = _authService.GetCurrentUserId();

                int id = _service.CreateSurveyQuestion(model, userId);
                response = new ItemResponse<int>() { Item = id };
            }
            catch (Exception ex)
            {
                code = 500;
                Logger.LogError(ex.ToString());
                response = new ErrorResponse(ex.Message);
            }


            return StatusCode(code, response);
        }
        [HttpPost("questions/questionwithoptions")]
        public ActionResult<ItemResponse<int>> CreateSurveyQuestionWithOptions(SurveyQuestionWithOptionsRequest model)
        {
            int code = 200;
            BaseResponse response = null; ;

            try
            {
                int userId = _authService.GetCurrentUserId();

                int id = _service.CreateSurveyQuestionWithAnswerOptions(model, userId);
                response = new ItemResponse<int>() { Item = id };
            }
            catch (Exception ex)
            {
                code = 500;
                Logger.LogError(ex.ToString());
                response = new ErrorResponse(ex.Message);
            }


            return StatusCode(code, response);
        }

        [HttpPut("questions/{id:int}")]
        public ActionResult<SuccessResponse> UpdateSurveyQuestion(SurveyQuestionUpdateRequest model)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                int userId = _authService.GetCurrentUserId();

                _service.UpdateSurveyQuestion(model, userId);
                response = new SuccessResponse();

            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(code, response);
        }

    } }
#endregion