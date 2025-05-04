using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Domain.AdminDashboard;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Collections.Generic;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/AdminDashboard")]
    [ApiController]
    public class AdminDashApiController : BaseApiController
    {
        private IAdminDashService _service = null;
        private IAuthenticationService<int> _authService = null;

        public AdminDashApiController(IAdminDashService service, 
                                      ILogger<PingApiController> logger,
                                      IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _authService = authService;
        }

        //HTTP Count all the Users
        [HttpGet("")]
        public ActionResult<ItemsResponse<AdminDashStats>> AdmDashboardInfo()
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                List<AdminDashStats> userList = _service.GetAdminDashStats();
                if (userList == null)
                {
                    code = 404;
                    response = new ErrorResponse("There is no users");
                }
                else
                {
                    response = new ItemsResponse<AdminDashStats> { Items = userList };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse($"Exception error is: {ex.Message}");
                base.Logger.LogError(ex.ToString());
            }
            return StatusCode(code, response);
        }

        //HTTP Count all the Users
        [HttpGet("weeks")]
        public ActionResult<ItemsResponse<AdminDashStats>> AdmDashboardInfoWeek()
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                List<AdminDashStats> weekList = _service.GetAdminDashWeekStats();
                if (weekList == null)
                {
                    code = 404;
                    response = new ErrorResponse("There is no users");
                }
                else
                {
                    response = new ItemsResponse<AdminDashStats> { Items = weekList };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse($"Exception error is: {ex.Message}");
                base.Logger.LogError(ex.ToString());
            }
            return StatusCode(code, response);
        }

        //HTTP Chart Users Info Per Week
        [HttpGet("stats/users/week")]
        public ActionResult<ItemsResponse<List<AdminDashStatsPerWeek>>> AdmDashboardUserPerWeek()
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                List<AdminDashStatsPerWeek> userList = _service.GetAdminDashUserWeekStats();
                if (userList == null)
                {
                    code = 404;
                    response = new ErrorResponse("There is no users");
                }
                else
                {
                    response = new ItemsResponse<AdminDashStatsPerWeek> { Items = userList };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse($"Exception error is: {ex.Message}");
                base.Logger.LogError(ex.ToString());
            }
            return StatusCode(code, response);
        }

        //HTTP Chart Users Info Per Two Weeks
        [HttpGet("stats/users/twoweeks")]
        public ActionResult<ItemsResponse<List<AdminDashStatsPerWeek>>> AdmDashboardUserPerTwoWeek()
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                List<AdminDashStatsPerWeek> userList = _service.GetAdminDashUserTwoWeekStats();
                if (userList == null)
                {
                    code = 404;
                    response = new ErrorResponse("There is no users");
                }
                else
                {
                    response = new ItemsResponse<AdminDashStatsPerWeek> { Items = userList };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse($"Exception error is: {ex.Message}");
                base.Logger.LogError(ex.ToString());
            }
            return StatusCode(code, response);
        }

        //HTTP Chart Users Info Per Month
        [HttpGet("stats/users/month")]
        public ActionResult<ItemsResponse<List<AdminDashStatsPerWeek>>> AdmDashboardUserPerMonth()
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                List<AdminDashStatsPerWeek> userList = _service.GetAdminDashUserMonthStats();
                if (userList == null)
                {
                    code = 404;
                    response = new ErrorResponse("There is no users");
                }
                else
                {
                    response = new ItemsResponse<AdminDashStatsPerWeek> { Items = userList };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse($"Exception error is: {ex.Message}");
                base.Logger.LogError(ex.ToString());
            }
            return StatusCode(code, response);
        }

        //HTTP Chart Users Info Per Year
        [HttpGet("stats/users/year")]
        public ActionResult<ItemsResponse<List<AdminDashStatsPerWeek>>> AdmDashboardUserPerYear()
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                List<AdminDashStatsPerWeek> userList = _service.GetAdminDashUserYearStats();
                if (userList == null)
                {
                    code = 404;
                    response = new ErrorResponse("There is no users");
                }
                else
                {
                    response = new ItemsResponse<AdminDashStatsPerWeek> { Items = userList };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse($"Exception error is: {ex.Message}");
                base.Logger.LogError(ex.ToString());
            }
            return StatusCode(code, response);
        }

        //HTTP More Detail Information Table About Organizations
        [HttpGet("stats/organizations/")]
        public ActionResult<ItemsResponse<List<AdminDashOrgTableStats>>> AdmDashboardOrgTable(int pageIndex, int pageSize)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                List<AdminDashOrgTableStats> organizationList = _service.GetAdminDashOrgTable();
                if (organizationList == null)
                {
                    code = 404;
                    response = new ErrorResponse("There is no organizations");
                }
                else
                {
                    response = new ItemsResponse<AdminDashOrgTableStats> { Items = organizationList };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse($"Exception error is: {ex.Message}");
                base.Logger.LogError(ex.ToString());
            }
            return StatusCode(code, response);
        }

    }
}
