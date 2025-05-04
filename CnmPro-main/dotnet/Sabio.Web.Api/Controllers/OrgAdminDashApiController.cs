using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Domain.Jobs;
using Sabio.Models.Domain.OrgAdminDashboard;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Collections.Generic;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/orgadmindashboard")]
    [ApiController]
    public class OrgAdminDashApiController : BaseApiController
    {
        private IOrgAdminDashService _service = null;
        private IAuthenticationService<int> _authService = null;

        public OrgAdminDashApiController(IOrgAdminDashService service,
                                      ILogger<PingApiController> logger,
                                      IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _authService = authService;
        }

        [HttpGet("stats/jobtypes/{id:int}")]
        public ActionResult<ItemResponse<JobTypeStats>> GetById(int id)
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                JobTypeStats stats = _service.GetJobTypeStatsByOrg(id);
                if (stats == null)
                {
                    code = 404;
                    response = new ErrorResponse("Application Resource not found.");
                }

                else
                {
                    response = new ItemResponse<JobTypeStats> { Item = stats };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse($"Generic Error: {ex.Message}");
            }

            return StatusCode(code, response);
        }

        [HttpGet("job/status/{id:int}")]
        public ActionResult<ItemResponse<JobsByActiveStatus>> GetByActiveStatus(int id)
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                JobsByActiveStatus jobs = _service.GetJobsByActiveStatus(id);
                if (jobs == null)
                {
                    code = 404;
                    response = new ErrorResponse("Application Resource not found.");
                }

                else
                {
                    response = new ItemResponse<JobsByActiveStatus> { Item = jobs };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse($"Generic Error: {ex.Message}");
            }

            return StatusCode(code, response);
        }


        [HttpGet("jobs/organization/{id:int}")]
        public ActionResult<ItemResponse<Paged<Job>>> GetJobsByOrg(int id, int pageIndex, int pageSize)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                Paged<Job> paged = _service.GetJobsByOrganizationPaginated(id, pageIndex, pageSize);
                if (paged == null)
                {
                    code = 404;
                    response = new ErrorResponse("App Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Paged<Job>> { Item = paged };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse($"Generic Error: {ex.Message}");
            }

            return StatusCode(code, response);
        }

        [HttpGet("proposals/organization/{id:int}")]
        public ActionResult<ItemResponse<Paged<ProposalsByOrg>>> GetProposalsByOrg(int id, int pageIndex, int pageSize)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                Paged<ProposalsByOrg> paged = _service.GetProposalsByOrg(id, pageIndex, pageSize);
                if (paged == null)
                {
                    code = 404;
                    response = new ErrorResponse("App Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Paged<ProposalsByOrg>> { Item = paged };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse($"Generic Error: {ex.Message}");
            }

            return StatusCode(code, response);
        }

        [HttpGet("proposal/weekly")]
        public ActionResult<ItemsResponse<ProposalCountByDay>> GetLast7DaysProposals(int id, int timezoneOffset)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                List<ProposalCountByDay> list = _service.GetLastSevenDaysOfProposals(id, timezoneOffset);
                if (list == null)
                {
                    code = 404;
                    response = new ErrorResponse("App Resource not found.");
                }
                else
                {
                    response = new ItemsResponse<ProposalCountByDay> { Items = list };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse($"Generic Error: {ex.Message}");
            }

            return StatusCode(code, response);

        }

        [HttpGet("proposal/biweekly")]
        public ActionResult<ItemsResponse<ProposalCountByDay>> GetLast15DaysProposals(int id, int timezoneOffset)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                List<ProposalCountByDay> list = _service.GetLast15DaysOfProposals(id, timezoneOffset);
                if (list == null)
                {
                    code = 404;
                    response = new ErrorResponse("App Resource not found.");
                }
                else
                {
                    response = new ItemsResponse<ProposalCountByDay> { Items = list };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse($"Generic Error: {ex.Message}");
            }

            return StatusCode(code, response);

        }

        [HttpGet("proposal/monthly")]
        public ActionResult<ItemsResponse<ProposalCountByDay>> GetLast30DaysProposals(int id, int timezoneOffset)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                List<ProposalCountByDay> list = _service.GetLast30DaysOfProposals(id, timezoneOffset);
                if (list == null)
                {
                    code = 404;
                    response = new ErrorResponse("App Resource not found.");
                }
                else
                {
                    response = new ItemsResponse<ProposalCountByDay> { Items = list };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse($"Generic Error: {ex.Message}");
            }

            return StatusCode(code, response);

        }


    }
}
