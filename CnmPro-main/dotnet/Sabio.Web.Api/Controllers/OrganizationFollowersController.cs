using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Domain.Organizations;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/followers")]
    [ApiController]
    public class OrganizationFollowersController : BaseApiController
    {
        private IOrganizationsFollowersServices _service = null;
        private IAuthenticationService<int> _authService = null;

        public OrganizationFollowersController(IOrganizationsFollowersServices service, ILogger<OrganizationFollowersController> logger,
            IAuthenticationService<int> authenticationService) : base(logger)
        {
            _service = service;
            _authService = authenticationService;
        }

        [HttpPost]
        public  ActionResult<SuccessResponse> Create(int orgId)
        {
            int code = 201;
            BaseResponse response = null;

            try
            {
                int userId = _authService.GetCurrentUserId();
                _service.Add(userId, orgId);
                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(code, response);
        }

        [AllowAnonymous]
        [HttpGet("organization/{organizationId:int}")]
        public ActionResult<ItemResponse<Paged<UserProfileBase>>> GetByOrganizationId(int organizationId, int pageIndex, int pageSize)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                Paged<UserProfileBase> paged = _service.GetByOrganizationId( organizationId,  pageIndex, pageSize);

                if (paged == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Paged<UserProfileBase>> { Item = paged };
                }
            }
            catch (Exception ex)
            {
                iCode = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Errors: {ex.Message}");
            }
            return StatusCode(iCode, response);
        }

        [AllowAnonymous]
        [HttpGet("current")]
        public ActionResult<ItemResponse<Paged<OrganizationBase>>> GetOrgByUserId(int pageIndex, int pageSize)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                int userId = _authService.GetCurrentUserId();
                Paged<OrganizationBase> paged = _service.GetOrgByUserId(userId, pageIndex, pageSize);

                if (paged == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Paged<OrganizationBase>> { Item = paged };
                }
            }
            catch (Exception ex)
            {
                iCode = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Errors: {ex.Message}");
            }
            return StatusCode(iCode, response);
        }

        [HttpDelete("{organizationId:int}")]
        public ActionResult<SuccessResponse> DeleteOrgFolById(int organizationId)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                int userId = _authService.GetCurrentUserId();
                _service.DeleteOrgFolById(organizationId, userId);
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
}
