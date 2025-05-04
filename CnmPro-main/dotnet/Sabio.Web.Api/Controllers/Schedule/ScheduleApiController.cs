using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Domain.Schedules;
using Sabio.Models.Requests.Schedules;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;

namespace Sabio.Web.Api.Controllers
{

    [Route("api/schedules")]
    [ApiController]
    public class ScheduleApiController : BaseApiController
    {
        private IScheduleService _service = null;
        private IAuthenticationService<int> _authService = null;
        public ScheduleApiController(IScheduleService service
            , ILogger<ScheduleApiController> logger
            , IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _authService = authService;
        }

        [HttpDelete("delete/{id:int}")]
        public ActionResult<SuccessResponse> Delete(int id)
        {
            int code = 201;
            BaseResponse response = null;

            try
            {
                _service.DeleteById(id);
                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                Logger.LogError(ex.ToString());
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(code, response);
        }

        [HttpGet("current")]
        public ActionResult<Paged<Schedule>> GetByCreatedBy(int pageIndex, int pageSize)
        {
            int code = 201;
            BaseResponse response = null;

            try
            {
                int userId = _authService.GetCurrentUserId();
                Paged<Schedule> schedules = _service.GetByCreatedBy(pageIndex, pageSize, userId);
                if (schedules == null)
                {
                    code = 404;
                    response = new ErrorResponse("App Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Paged<Schedule>> { Item = schedules };
                }

            }
            catch (Exception ex)
            {
                code = 500;
                Logger.LogError(ex.ToString());
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(code, response);
        }

        [HttpPut("{id:int}")]
        public ActionResult<SuccessResponse> Update(ScheduleUpdateRequest model)
        {
            int code = 201;
            BaseResponse response = null;
            try
            {
                int userId = _authService.GetCurrentUserId();
                _service.Update(model, userId);
                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                Logger.LogError(ex.ToString());
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(code, response);
        }

        [HttpPost]
        public ActionResult<ItemResponse<int>> Create(ScheduleAddRequest model)
        {
            ObjectResult result =  null;

            try
            {
                int userId = _authService.GetCurrentUserId();
                int id = _service.Insert(model, userId);

                ItemResponse<int> response = new ItemResponse<int> { Item = id};

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

        [HttpGet("{id:int}")]
        public ActionResult<ItemResponse<Schedule>> GetById(int id)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                Schedule schedule = _service.GetById(id);
                
                if(schedule  == null)
                {
                    code = 404;

                    response = new ErrorResponse("App Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Schedule> { Item = schedule };
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

        [HttpGet]
        public ActionResult<ItemResponse<Paged<Schedule>>> GetAll(int pageIndex, int pageSize)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                Paged<Schedule> schedule = _service.GetAll(pageIndex, pageSize);
                
                if(schedule  == null)
                {
                    code = 404;

                    response = new ErrorResponse("App Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Paged<Schedule>> { Item = schedule };
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
    }
}
