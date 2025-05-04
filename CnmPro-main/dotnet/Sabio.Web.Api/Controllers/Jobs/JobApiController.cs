using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Domain.Jobs;
using Sabio.Models.Interfaces;
using Sabio.Models.Requests.Jobs;
using Sabio.Services;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Collections.Generic;

namespace Sabio.Web.Api.Controllers.Jobs
{
    [Route("api/jobs")]
    [ApiController]
    public class JobApiController : BaseApiController
    {

        private IJobService _service = null;
        private IAuthenticationService<int> _authService = null;
        public JobApiController(
                IJobService service
               , ILogger<PingApiController> logger
               , IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _authService = authService;
        }
        [HttpPost]
        public ActionResult<ItemResponse<int>> Create(JobAddRequest model)
        {
            int userId = _authService.GetCurrentUserId();

            ObjectResult result = null;

            try
            {
                int id = _service.AddJob(model, userId);
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
        public ActionResult<SuccessResponse> Update(JobUpdateRequest model)
        {
            int code = 200;
            BaseResponse response = null;
            int userId = _authService.GetCurrentUserId();

            try
            {
                _service.UpdateV2(model, userId);
                response = new SuccessResponse();

            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(code, response);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<SuccessResponse> Delete(int id)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                _service.DeleteJobById(id);
                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(code, response);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<ItemResponse<Paged<Job>>> GetAll(int pageIndex, int pageSize)
        {
            ActionResult result = null;
            try
            {
                Paged<Job> paged = _service.SelectAll(pageIndex, pageSize);
                if (paged == null)
                {
                    result = NotFound404(new ErrorResponse("Records Not Found"));
                }
                else
                {
                    ItemResponse<Paged<Job>> response = new ItemResponse<Paged<Job>>();
                    response.Item = paged;
                    result = Ok200(response);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                result = StatusCode(500, new ErrorResponse(ex.Message.ToString()));
            }
            return result;
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public ActionResult<ItemResponse<Job>> GetById(int id)
        {
            int iCode = 200;
            BaseResponse response = null;
            try
            {
                Job job = _service.GetJobById(id);

                if (job == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Job> { Item = job };
                }
            }
            catch (Exception ex)
            {
                iCode = 500;
                Logger.LogError(ex.ToString());
                response = new ErrorResponse("$Generic Error: ${ex.Message}");
            }
            return StatusCode(iCode, response);
        }

        [HttpGet("organization/{id:int}")]
        [AllowAnonymous]
        public ActionResult<ItemsResponse<JobBase>> GetByOrganization(int id)
        {
            int iCode = 200;
            BaseResponse response = null;
            try
            {
                List<JobBase> job = _service.GetJobByOrganization(id);

                if (job == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemsResponse<JobBase> { Items = job };
                }
            }
            catch (Exception ex)
            {
                iCode = 500;
                Logger.LogError(ex.ToString());
                response = new ErrorResponse("$Generic Error: ${ex.Message}");
            }
            return StatusCode(iCode, response);
        }

        [HttpGet("createdby")]
        public ActionResult<ItemResponse<Paged<Job>>> GetByCreatedBy(int createdBy, int pageIndex, int pageSize)
        {
            ActionResult result = null;
            try
            {
                Paged<Job> paged = _service.SelectJobByCreatedBy(createdBy, pageIndex, pageSize);
                if (paged == null)
                {
                    result = NotFound404(new ErrorResponse("Records Not Found"));
                }
                else
                {
                    ItemResponse<Paged<Job>> response = new ItemResponse<Paged<Job>>();
                    response.Item = paged;
                    result = Ok200(response);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                result = StatusCode(500, new ErrorResponse(ex.Message.ToString()));
            }
            return result;
        }
        [HttpGet("search")]
        [AllowAnonymous]
        public ActionResult<ItemsResponse<Paged<Job>>> GetSearchedJobs(int pageIndex, int pageSize, string query)
        {

            ActionResult result = null;
            try
            {
                Paged<Job> paged = _service.GetSearchedJobs(pageIndex, pageSize, query);
                if (paged == null)
                {

                    result = NotFound404(new ErrorResponse("Records Not Found"));
                }
                else
                {
                    ItemResponse<Paged<Job>> response = new ItemResponse<Paged<Job>>();
                    response.Item = paged;
                    result = Ok200(response);
                }
            }
            catch (Exception ex)
            {

                Logger.LogError(ex.ToString());
                result = StatusCode(500, new ErrorResponse(ex.Message.ToString()));

            }


            return result;
        }

        [HttpGet("search/location")]
        [AllowAnonymous]
        public ActionResult<ItemsResponse<Paged<Job>>> GetSearchedJobsByLocation(int pageIndex, int pageSize, string query)
        {

            ActionResult result = null;
            try
            {
                Paged<Job> paged = _service.GetSearchedJobsByLocation(pageIndex, pageSize, query);
                if (paged == null)
                {

                    result = NotFound404(new ErrorResponse("Records Not Found"));
                }
                else
                {
                    ItemResponse<Paged<Job>> response = new ItemResponse<Paged<Job>>();
                    response.Item = paged;
                    result = Ok200(response);
                }
            }
            catch (Exception ex)
            {

                Logger.LogError(ex.ToString());
                result = StatusCode(500, new ErrorResponse(ex.Message.ToString()));

            }


            return result;
        }


        [HttpGet("search/radius")]
        [AllowAnonymous]
        public ActionResult<ItemsResponse<Paged<JobSearchResult>>> GetJobsWithDistanceRadius(int pageIndex, int pageSize, string query, double latParam, double longParam, int distance)
        {

            ActionResult result = null;
            try
            {
                Paged<JobSearchResult> paged = _service.GetSearchedJobsWithLocationRadius(pageIndex, pageSize, query, latParam, longParam, distance);
                if (paged == null)
                {

                    result = NotFound404(new ErrorResponse("Records Not Found"));
                }
                else
                {
                    ItemResponse<Paged<JobSearchResult>> response = new ItemResponse<Paged<JobSearchResult>>();
                    response.Item = paged;
                    result = Ok200(response);
                }
            }
            catch (Exception ex)
            {

                Logger.LogError(ex.ToString());
                result = StatusCode(500, new ErrorResponse(ex.Message.ToString()));

            }


            return result;
        }

    }
}

