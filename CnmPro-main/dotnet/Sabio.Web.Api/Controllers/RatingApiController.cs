using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Requests.Rating;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/ratings")]
    [ApiController]
    public class RatingApiController : BaseApiController
    {
        private IRatingsService _service = null;
        private IAuthenticationService<int> _authService = null;

        public RatingApiController(IRatingsService service, ILogger<PingApiController> logger, IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _authService = authService;
        }

        #region GetById Api Controller
        [HttpGet("{id:int}")]
        public ActionResult<ItemResponse<Rating>> GetById(int id)
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                Rating course = _service.Get(id);
                if (course == null)
                {
                    code = 404;
                    response = new ErrorResponse("Application Resource not found");
                }
                else
                {
                    response = new ItemResponse<Rating> { Item = course };
                }
            }
            catch (SqlException sqlEx)
            {
                code = 500;

                response = new ErrorResponse($"SqlException Error: {sqlEx.Message}");
                base.Logger.LogError(sqlEx.ToString());
            }
            catch (ArgumentException argEx)
            {
                code = 500;

                response = new ErrorResponse($"ArgumentException Error: {argEx.Message}");
            }

            catch (Exception ex)
            {
                code = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Error: {ex.Message}");

            }

            return StatusCode(code, response);
        }
        #endregion

        [HttpGet("by/comment")]
        public ActionResult<ItemResponse<RatingByComment>> GetRatingByComment(int commentId)
        {
            int code = 200;
            BaseResponse response = null;
            int createdBy = _authService.GetCurrentUserId();
            try
            {
                RatingByComment comment = _service.GetRatingByComment(commentId, createdBy);
                if (comment == null)
                {
                    code = 404;
                    response = new ErrorResponse("Application Resource not found");
                }
                else
                {
                    response = new ItemResponse<RatingByComment> { Item = comment };
                }
            }
            catch (SqlException sqlEx)
            {
                code = 500;

                response = new ErrorResponse($"SqlException Error: {sqlEx.Message}");
                base.Logger.LogError(sqlEx.ToString());
            }
            catch (ArgumentException argEx)
            {
                code = 500;

                response = new ErrorResponse($"ArgumentException Error: {argEx.Message}");
            }

            catch (Exception ex)
            {
                code = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Error: {ex.Message}");

            }

            return StatusCode(code, response);
        }
        [HttpGet("by/createdby")]
        public ActionResult<ItemResponse<RatingCreatedBy>> GetRatingCreatedBy(int entityId, int entityTypeId)
        {
            int code = 200;
            BaseResponse response = null;
            int createdBy = _authService.GetCurrentUserId();
            try
            {
                RatingCreatedBy created = _service.GetRatingCreatedBy(entityId, entityTypeId, createdBy);
                if (created == null)
                {
                    code = 404;
                    response = new ErrorResponse("Application Resource not found");
                }
                else
                {
                    response = new ItemResponse<RatingCreatedBy> { Item = created };
                }
            }
            catch (SqlException sqlEx)
            {
                code = 500;

                response = new ErrorResponse($"SqlException Error: {sqlEx.Message}");
                base.Logger.LogError(sqlEx.ToString());
            }
            catch (ArgumentException argEx)
            {
                code = 500;

                response = new ErrorResponse($"ArgumentException Error: {argEx.Message}");
            }

            catch (Exception ex)
            {
                code = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Error: {ex.Message}");

            }

            return StatusCode(code, response);
        }


        #region GetPage(SelectAll Paginate) Api Controller
        [HttpGet("paginate")]
        public ActionResult<ItemResponse<Paged<Rating>>> GetPage(int pageIndex, int pageSize)
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                Paged<Rating> page = _service.GetPage(pageIndex, pageSize);

                if (page == null)
                {
                    code = 404;
                    response = new ErrorResponse("App Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Paged<Rating>> { Item = page };
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

        #region Author/CreatedBy Api Controller
        [HttpGet("createdby")]
        public ActionResult<ItemResponse<Paged<Rating>>> GetByAuthor(int pageIndex, int pageSize, int createdBy)
        {
            int code = 200;
            BaseResponse response = null;//do not declare an instance.

            try
            {
                Paged<Rating> page = _service.GetByAuthor(pageIndex, pageSize, createdBy);

                if (page == null)
                {
                    code = 404;
                    response = new ErrorResponse("App Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Paged<Rating>> { Item = page };
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

        #region Select ByEntityId
        [HttpGet("by/entityid")]
        public ActionResult<ItemResponse<Paged<Rating>>> GetByEntityId(int pageIndex, int pageSize, int entityId, int entityTypeId)
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                Paged<Rating> page = _service.GetByEntityId(pageIndex, pageSize, entityId, entityTypeId);

                if (page == null)
                {
                    code = 404;
                    response = new ErrorResponse("App Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Paged<Rating>> { Item = page };
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

        #region Summary Entity
        [HttpGet("average/rating")]
        public ActionResult<ItemResponse<AvgRating>> GetAvgRating(int entityId, int entityTypeId)
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                AvgRating rating = _service.GetAvgRating(entityId, entityTypeId);
                if (rating == null)
                {
                    code = 404;
                    response = new ErrorResponse("Application Resource not found");
                }
                else
                {
                    response = new ItemResponse<AvgRating> { Item = rating };
                }
            }
            catch (SqlException sqlEx)
            {
                code = 500;

                response = new ErrorResponse($"SqlException Error: {sqlEx.Message}");
                base.Logger.LogError(sqlEx.ToString());
            }
            catch (ArgumentException argEx)
            {
                code = 500;

                response = new ErrorResponse($"ArgumentException Error: {argEx.Message}");
            }

            catch (Exception ex)
            {
                code = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Error: {ex.Message}");
            }
            return StatusCode(code, response);
        } 
        #endregion
        #region ADD/Create Api Controllers

        [HttpPost]
        public ActionResult<ItemResponse<int>> Create(RatingAddRequest model)
        {
            {
                int userId = _authService.GetCurrentUserId();
                ObjectResult result = null;

                try
                {
                    int id = _service.Add(model, userId);
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
        }
        #endregion
        #region Rating Delete
        [HttpDelete("{id:int}")]
        public ActionResult<SuccessResponse> Delete(int id)
        {
            int userId = _authService.GetCurrentUserId();
            int code = 200;
            BaseResponse response = null;

            try
            {
                _service.Delete(id, userId);

                response = new SuccessResponse();
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
        #region Update
        [HttpPut("{id:int}")]
        public ActionResult<SuccessResponse> Update(RatingUpdateRequest model)
        {
            {
                int userId = _authService.GetCurrentUserId();
                int code = 200;
                BaseResponse response = null;

                try
                {
                    _service.Update(model, userId);
                    response = new SuccessResponse();
                }
                catch (Exception ex)
                {
                    code = 500;
                    response = new ErrorResponse(ex.Message);
                }

                return StatusCode(code, response);
            }
        }
        #endregion
    }
}


