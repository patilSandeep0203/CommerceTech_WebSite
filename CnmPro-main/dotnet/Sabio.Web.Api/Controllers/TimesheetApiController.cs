using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Domain.Timesheet;
using Sabio.Models.Requests.Timesheet;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Collections.Generic;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/timesheets")]
    [ApiController]
    public class TimesheetApiController : BaseApiController
    {
        ITimesheetService _service = null;
        IAuthenticationService<int> _authService = null;
        public TimesheetApiController(ITimesheetService service, IAuthenticationService<int> authService, ILogger<ITimesheetService> logger) : base(logger)
        {
            _service = service;
            _authService = authService;
        }

        [HttpPost]
        public ActionResult<ItemResponse<int>> Create(TimesheetAddRequest model)
        {
            int userId = _authService.GetCurrentUserId();
            int code = 201;
            BaseResponse response = null;

            try
            {
                int id = _service.Create(model, userId);
                response = new ItemResponse<int> { Item = id };
            }
            catch (Exception exception)
            {
                code = 500;
                response = new ErrorResponse(exception.Message);
            }
            return StatusCode(code, response);
        }

        [HttpPut("{id:int}")]
        public ActionResult<SuccessResponse> Update(TimesheetUpdateRequest model)
        {
            int userId = _authService.GetCurrentUserId();
            int code = 200;
            BaseResponse response = null;

            try
            {
                _service.Update(model, userId);
                response = new SuccessResponse();
            }
            catch (Exception exception)
            {
                code = 500;
                response = new ErrorResponse(exception.Message);
            }
            return StatusCode(code, response);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<SuccessResponse> Delete(int Id)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                _service.Delete(Id);
                response = new SuccessResponse();
            }
            catch (Exception exception)
            {
                code = 500;
                response = new ErrorResponse(exception.Message);
            }
            return StatusCode(code, response);
        }

        [HttpGet("user/current/paginate")]
        public ActionResult<ItemResponse<Paged<Timesheet>>> GetAllByUserPagination(int pageIndex, int pageSize)
        {
            int userId = _authService.GetCurrentUserId();
            int code = 200;
            BaseResponse response = null;

            try
            {
                Paged<Timesheet> pagedTimesheets = _service.GetAllByUserPagination(userId, pageIndex, pageSize);
                if (pagedTimesheets == null)
                {
                    code = 404;
                    response = new ErrorResponse("No Records Found");
                }
                else
                {
                    response = new ItemResponse<Paged<Timesheet>> { Item = pagedTimesheets };
                }
            }
            catch (Exception exception)
            {
                code = 500;
                response = new ErrorResponse(exception.Message);
            }
            return StatusCode(code, response);
        }

        [HttpGet("search/filter")]
        public ActionResult<ItemResponse<Paged<Timesheet>>> SearchByFilter(int organizationId, int dateRange, int pageIndex, int pageSize)
        {
            int userId = _authService.GetCurrentUserId();
            int code = 200;
            BaseResponse response = null;

            try
            {
                Paged<Timesheet> list = _service.SearchByFilter(userId, organizationId, dateRange, pageIndex, pageSize);
                if (list == null)
                {
                    code = 404;
                    response = new ErrorResponse("Records not Found");
                }
                else
                {
                    response = new ItemResponse<Paged<Timesheet>> { Item = list };
                }
            }
            catch (Exception exception)
            {
                code = 500;
                response = new ErrorResponse(exception.Message);
            }
            return StatusCode(code, response);
        }

        [HttpGet("search/organization")]
        public ActionResult<ItemResponse<Paged<Timesheet>>> SearchByOrgCurrentUser(int organizationId, int pageIndex, int pageSize)
        {
            int userId = _authService.GetCurrentUserId();
            int code = 200;
            BaseResponse response = null;

            try
            {
                Paged<Timesheet> list = _service.SearchByOrgCurrentUser(userId, organizationId, pageIndex, pageSize);
                if (list == null)
                {
                    code = 404;
                    response = new ErrorResponse("Records not Found");
                }
                else
                {
                    response = new ItemResponse<Paged<Timesheet>> { Item = list };
                }
            }
            catch (Exception exception)
            {
                code = 500;
                response = new ErrorResponse(exception.Message);
            }
            return StatusCode(code, response);
        }

        [HttpGet("total")]
        public ActionResult<ItemResponse<int>> TotalHours()
        {
            int userId = _authService.GetCurrentUserId();
            int code = 200;
            BaseResponse response = null;

            try
            {
                int total = _service.TotalHours(userId);
                response = new ItemResponse<int> { Item = total };
            }
            catch (Exception exception)
            {
                code = 500;
                response = new ErrorResponse(exception.Message);
            }
            return StatusCode(code, response);
        }

        [HttpGet("jobs")]
        public ActionResult<ItemsResponse<UserJob>> TimesheetGetAllJobs()
        {
            int userId = _authService.GetCurrentUserId();
            int code = 200;
            BaseResponse response = null;

            try
            {
                List<UserJob> jobs = _service.GetUserJobs(userId);
                if (jobs == null)
                {
                    code = 404;
                    response = new ErrorResponse("Records not Found");
                }
                else
                {
                    response = new ItemsResponse<UserJob> { Items = jobs };
                }
            }
            catch (Exception exception)
            {
                code = 500;
                response = new ErrorResponse(exception.Message);
            }
            return StatusCode(code, response);
        }

        [HttpGet("organizations")]
        public ActionResult<ItemsResponse<UserOrganization>> UserOrganizations()
        {
            int userId = _authService.GetCurrentUserId();
            int code = 200;
            BaseResponse response = null;

            try
            {
                List<UserOrganization> orgs = _service.UserOrganizations(userId);
                if (orgs == null)
                {
                    code = 404;
                    response = new ErrorResponse("Records not Found");
                }
                else
                {
                    response = new ItemsResponse<UserOrganization> { Items = orgs };
                }
            }
            catch (Exception exception)
            {
                code = 500;
                response = new ErrorResponse(exception.Message);
            }
            return StatusCode(code, response);
        }
    }
}