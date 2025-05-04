using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Requests.Blogs;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Collections.Generic;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/blogs")]
    [ApiController]
    public class BlogApiController : BaseApiController
    {
        private IBlogService _service = null;
        private IAuthenticationService<int> _authService = null;
        public BlogApiController(IBlogService service
            , ILogger<PingApiController> logger
            , IAuthenticationService<int> authService) : base(logger)
        {
            _service = (IBlogService)service;
            _authService = authService;
        }

        [HttpPost]
        public ActionResult<ItemResponse<int>> Create(BlogAddRequest model)
        {
            int userId = _authService.GetCurrentUserId();

            ObjectResult result = null;

            try
            {
                int id = _service.AddBlog(model, userId);
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
        public ActionResult<SuccessResponse> Update(BlogUpdateRequest model, int userId)
        {
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
        [HttpGet]
        public ActionResult<ItemResponse<Paged<Blog>>> GetAll(int pageIndex, int pageSize)
        {
            ActionResult result = null;
            try
            {
                Paged<Blog> paged = _service.GetAll(pageIndex, pageSize);
                if (paged == null)
                {
                    result = NotFound404(new ErrorResponse("Records Not Found"));
                }
                else
                {
                    ItemResponse<Paged<Blog>> response = new ItemResponse<Paged<Blog>>();
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

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public ActionResult<ItemResponse<Blog>> GetById(int id)
        {
            int iCode = 200;
            BaseResponse response = null;
            try
            {
                Blog blog = _service.GetById(id);

                if (blog == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Blog> { Item = blog };
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

        [AllowAnonymous]
        [HttpGet("author")]
        public ActionResult<ItemResponse<Paged<Blog>>> GetByAuthor(int authorId, int pageIndex, int pageSize)
        {
            ActionResult result = null;
            try
            {
                Paged<Blog> paged = _service.GetByAuthor(authorId, pageIndex, pageSize);
                if (paged == null)
                {
                    result = NotFound404(new ErrorResponse("Records Not Found"));
                }
                else
                {
                    ItemResponse<Paged<Blog>> response = new ItemResponse<Paged<Blog>>();
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


        [AllowAnonymous]
        [HttpGet("search")]
        public ActionResult<ItemResponse<Paged<Blog>>> GetBySubject(string query, int pageIndex, int pageSize)
        {
            ActionResult result = null;
            try
            {
                Paged<Blog> paged = _service.SearchBySubject(query, pageIndex, pageSize);
                if (paged == null)
                {
                    result = NotFound404(new ErrorResponse("Records Not Found"));
                }
                else
                {
                    ItemResponse<Paged<Blog>> response = new ItemResponse<Paged<Blog>>();
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
