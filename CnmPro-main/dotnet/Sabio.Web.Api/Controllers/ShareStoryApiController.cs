using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Domain.ShareStory;
using Sabio.Models.Requests.ShareStory;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Collections.Generic;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/sharestory")]
    [ApiController]
    public class ShareStoryApiController : BaseApiController
    {
        private IShareStoryService _service = null;
        private IAuthenticationService<int> _authService = null;
        public ShareStoryApiController(IShareStoryService service,
            ILogger<PingApiController> logger,
            IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _authService = authService;
        }
        [HttpPost]
        public ActionResult<ItemResponse<int>> Create(ShareStoryAddRequest model)
        {

            ObjectResult result = null;

            try
            {
                int userId = _authService.GetCurrentUserId();
                int id = _service.Add(model, userId);

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
        [HttpPut("{id:int}")]
        public ActionResult<SuccessResponse> Update(ShareStoryUpdateRequest model, int Id)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                int userId = _authService.GetCurrentUserId();
                _service.Update(model, Id, userId);
                response = new SuccessResponse();
            }

            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse($"Generic Error {ex.Message}");
            }

            return StatusCode(code, response);
        }

        [AllowAnonymous]
        [HttpGet("paginate")]
        public ActionResult<ItemResponse<Paged<ShareStory>>> GetAll(int pageIndex, int pageSize)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                Paged<ShareStory> page = _service.GetAll(pageIndex, pageSize);

                if (page == null)
                {
                    code = 404;
                    response = new ErrorResponse("No files  found.");
                }

                else
                {
                    response = new ItemResponse<Paged<ShareStory>> { Item = page };
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

        [HttpDelete("{id:int}")]
        public ActionResult<SuccessResponse> Delete(int id)
        {

            int code = 200;
            BaseResponse response = null;

            try
            {
                _service.Delete(id);

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
        [HttpGet("{id:int}")]
        public ActionResult<ItemResponse<ShareStory>> GetById(int id)
        {
            int code = 200;
            BaseResponse response = null;


            try
            {
                ShareStory story = _service.GetStoryById(id);

                if (story == null)
                {
                    code = 404;
                    response = new ErrorResponse("Reservation Not Found");
                }
                else
                {
                    response = new ItemResponse<ShareStory> { Item = story };
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
    }
}

