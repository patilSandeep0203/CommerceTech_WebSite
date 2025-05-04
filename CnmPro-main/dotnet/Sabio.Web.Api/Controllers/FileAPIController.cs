using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Domain.Files;
using Sabio.Models.Requests.Files;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FileAPIController : BaseApiController
    {
        private IFileService _service = null;
        private ILookUpService _lookUpService = null;
        private IAuthenticationService<int> _authService = null;

        public FileAPIController(
            IFileService service,
             ILookUpService lookUpService,
            ILogger<PingApiController> logger,
            IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _authService = authService;
            _lookUpService = lookUpService;
        }

        [HttpPost]
        public async Task<ActionResult<ItemsResponse<List<FileResponse>>>> UploadFiles(List<IFormFile> files)
        {
            int responseCode = 200;
            BaseResponse result = null;
            int userId = _authService.GetCurrentUserId();

            try
            {
                List<FileResponse> responsePackage = await _service.UploadFilesAsync(files, userId);

                if (responsePackage == null)
                {
                    responseCode = 404;
                    result = new ErrorResponse("Application Resource Not Found.");
                }
                else
                {
                    result = new ItemsResponse<FileResponse> { Items = responsePackage };
                }
            }
            catch (Exception ex)
            {
                responseCode = 500;
                base.Logger.LogError(ex.ToString());
                ErrorResponse response = new ErrorResponse($"Generic Error: ${ex.Message}");
            }
            return StatusCode(responseCode, result);
        }

        [HttpPost("files")]
        public ActionResult<ItemResponse<int>> AddFile(FileAddRequest model)
        {
            int iCode = 200;
            BaseResponse response = null;
            int userId = _authService.GetCurrentUserId();

            try
            {
                _service.AddFile(model, userId);
                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                iCode = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Error: ${ex.Message}");
            }
            return StatusCode(iCode, response);
        }

        [HttpGet("paginate")]
        public ActionResult<ItemResponse<Paged<FileBase>>> GetAll(int pageIndex, int pageSize)
        {
            ActionResult result = null;
            try
            {
                Paged<FileBase> paged = _service.GetAll(pageIndex, pageSize);
                if (paged == null)
                {
                    result = NotFound404(new ErrorResponse("Records Not Found"));
                }
                else
                {
                    ItemResponse<Paged<FileBase>> response = new ItemResponse<Paged<FileBase>>();
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

        [HttpGet("{typeId:int}")]
        public ActionResult<ItemResponse<Paged<FileBase>>> GetByFileTypeId(int typeId, int pageIndex, int pageSize)
        {
            ActionResult result = null;
            try
            {
                Paged<FileBase> paged = _service.GetByFileTypeId(typeId, pageIndex, pageSize);
                if (paged == null)
                {
                    result = NotFound404(new ErrorResponse("Records Not Found"));
                }
                else
                {
                    ItemResponse<Paged<FileBase>> response = new ItemResponse<Paged<FileBase>>();
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

        [HttpGet("user/current")]
        public ActionResult<ItemResponse<Paged<FileBase>>> GetByUserId(int pageIndex, int pageSize)
        {
            int userId = _authService.GetCurrentUserId();
            ActionResult result = null;

            try
            {
                Paged<FileBase> paged = _service.GetByUserId(userId, pageIndex, pageSize);
                if (paged == null)
                {
                    result = NotFound404(new ErrorResponse("Records Not Found"));
                }
                else
                {
                    ItemResponse<Paged<FileBase>> response = new ItemResponse<Paged<FileBase>>();
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

        [HttpGet("deleted")]
        public ActionResult<ItemResponse<Paged<FileBase>>> GetByFileIsDeleted(bool isDeleted, int pageIndex, int pageSize)
        {
            ActionResult result = null;
            try
            {
                Paged<FileBase> paged = _service.GetByFileIsDeleted(isDeleted, pageIndex, pageSize);
                if (paged == null)
                {
                    result = NotFound404(new ErrorResponse("Records Not Found"));
                }
                else
                {
                    ItemResponse<Paged<FileBase>> response = new ItemResponse<Paged<FileBase>>();
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
        public ActionResult<ItemResponse<Paged<FileBase>>> SearchPaginate(int pageIndex, int pageSize, string query)
        {
            int code = 200;
            BaseResponse response = null;//do not declare an instance.

            try
            {
                Paged<FileBase> page = _service.SearchPagination(pageIndex, pageSize, query);

                if (page == null)
                {
                    code = 404;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Paged<FileBase>> { Item = page };
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

        [HttpPut("{id:int}/{deleteStatus:int}")]
        public ActionResult<SuccessResponse> DeleteById(int id, int deleteStatus)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                _service.DeleteById(id, deleteStatus);
                response = new SuccessResponse();
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
