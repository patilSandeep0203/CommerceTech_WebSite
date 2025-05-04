using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Requests.Licenses;
using Sabio.Models.Requests.UserProfiles;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Collections.Generic;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/userprofiles")]
    [ApiController]
    public class UserProfileApiController : BaseApiController
    {
        private IUserProfilesService _service = null;
        private IAuthenticationService<int> _authService = null;
        public UserProfileApiController(IUserProfilesService service,
                                  ILogger<UserProfileApiController> logger,
                                  IAuthenticationService<int> authService) : base(logger)
        {
            _service = service; 
            _authService = authService;
        }

        [HttpPost("")]
        public ActionResult<ItemResponse<int>> CreateV2(UserProfileAddRequestV2 model)
        {
            int userId = _authService.GetCurrentUserId();
            ObjectResult result = null;

            try
            {
                int id = _service.AddV2(model, userId);
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

        [HttpPut("{id:int}")]
        public ActionResult<ItemResponse<SuccessResponse>> UpdateV2(UserProfileUpdateRequestV2 model)
        {
            int userId = _authService.GetCurrentUserId();
            BaseResponse response = null;
            int code = 200;

            try
            {
                _service.UpdateV2(model, userId);
                response = new SuccessResponse();

            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                response = new ErrorResponse(ex.Message);
                code = 500;

            }

            return StatusCode(code, response);
        }

        [HttpGet("current")]
        public ActionResult<ItemResponse<UserProfile>> GetByCurrentId()
        {
            int iCode = 200;
            BaseResponse response = null;
            int userId = _authService.GetCurrentUserId();

            try
            {
                UserProfile aUser = _service.GetByCurrentId(userId);

                if (aUser == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemResponse<UserProfile> { Item = aUser };
                }
            }
            catch (Exception ex)
            {

                iCode = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Error: {ex.Message}");
            }

            return StatusCode(iCode, response);
        }

        [HttpGet("dashboard")]
        public ActionResult<ItemResponse<UserDashboard>> GetUserDashboard()
        {
            int iCode = 200;
            BaseResponse response = null;
            int userId = _authService.GetCurrentUserId();

            try
            {
                UserDashboard dashboard = _service.GetDashboard(userId);

                if (dashboard == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemResponse<UserDashboard> { Item = dashboard };
                }
            }
            catch (Exception ex)
            {

                iCode = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Error: {ex.Message}");
            }

            return StatusCode(iCode, response);
        }

        #region Admin Controller
        [HttpPut("admin/{id:int}/status/{statusId:int}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<SuccessResponse> DeleteById(int id, int statusId)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                _service.DeleteById(id);

                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(iCode, response);
        }

        [HttpGet("admin/")]
        [Authorize(Roles = "Admin")]
        public ActionResult<ItemResponse<Paged<UserProfile>>> GetAll(int pageIndex, int pageSize)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                Paged<UserProfile> list = _service.GetAll(pageIndex, pageSize);

                if (list == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("App Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Paged<UserProfile>> { Item = list };
                }
            }
            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse(ex.Message);
                Logger.LogError(ex.ToString());
            }

            return StatusCode(iCode, response);
        }

        [HttpGet("admin/pagination")]
        [Authorize(Roles = "Admin")]
        public ActionResult<ItemResponse<Paged<UserProfile>>> SearchPaginate(int pageIndex, int pageSize, int userId)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                Paged<UserProfile> list = _service.Pagination(pageIndex, pageSize, userId);

                if (list == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("App Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Paged<UserProfile>> { Item = list };
                }
            }
            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse(ex.Message);
                Logger.LogError(ex.ToString());
            }

            return StatusCode(iCode, response);
        }

        #endregion

    }
}
