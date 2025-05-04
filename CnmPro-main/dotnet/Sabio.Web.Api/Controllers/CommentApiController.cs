

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Domain.Comments;
using Sabio.Models.Requests.Comments;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentApiController : BaseApiController
    {
        private ICommentService _service = null;
        private IAuthenticationService<int> _authService = null;
        public CommentApiController(ICommentService service
          , IAuthenticationService<int> authService
          , ILogger<CommentApiController> logger) : base(logger)
        {
            _service = service;
            _authService = authService;
        }

        [HttpPost]
        public ActionResult<ItemResponse<int>> Create(CommentAddRequest request)
        {
            {
                int userId = _authService.GetCurrentUserId();
                ObjectResult result = null;
                try
                {
                    int id = _service.AddComment(request, userId);
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
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public ActionResult<ItemResponse<Comment>> Get(int id)
        {
            int iCode = 200;
            BaseResponse response = null;
            try
            {
                Comment user = _service.GetComment(id);
                if (user == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Comment> { Item = user };
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
        [HttpGet("paginate")]
        public ActionResult<ItemResponse<Paged<Comment>>> GetCommentsPaginated(int pageIndex, int pageSize)
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                Paged<Comment> page = _service.GetCommentsPaginated(pageIndex, pageSize);
                if (page == null)
                {
                    code = 404;
                    response = new ErrorResponse("App Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Paged<Comment>> { Item = page };
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

        [AllowAnonymous]
        [HttpGet("replies/paginate")]
        public ActionResult<ItemResponse<Paged<Comment>>> GetRepliesPaginated(int pageIndex, int pageSize)
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                Paged<Comment> page = _service.GetRepliesPaginated(pageIndex, pageSize);
                if (page == null)
                {
                    code = 404;
                    response = new ErrorResponse("App Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Paged<Comment>> { Item = page };
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

        [AllowAnonymous]
        [HttpGet("createdBy/paginate")] 
        public ActionResult<ItemResponse<Comment>> GetCreatedByPaginate(int pageIndex, int pageSize) 
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                int userId = _authService.GetCurrentUserId();
                Paged<Comment> page = _service.GetByCreatedBy(userId, pageIndex, pageSize);
                if (page == null)
                {
                    code = 404;
                    response = new ErrorResponse("App Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Paged<Comment>> { Item = page };
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

        [AllowAnonymous]
        [HttpGet("entityId/{entityId:int}/entityTypeId/{entityTypeId:int}")]
        public ActionResult<ItemResponse<Comment>> GetByEntityId(int EntityId, int EntityTypeId, int pageIndex, int pageSize)
        {
            int iCode = 200;
            BaseResponse response = null;
            try
            {
                Paged<Comment> comment = _service.GetByEntityId(EntityId, EntityTypeId, pageIndex, pageSize);
                if (comment == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Paged<Comment>> { Item = comment };
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

        [HttpGet("parentId/{parentId:int}")]
        public ActionResult<ItemResponse<Comment>> GetByParentId(int ParentId)
        {
            int iCode = 200;
            BaseResponse response = null;
            try
            {
                Comment comment = _service.GetByParentId(ParentId);
                if (comment == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Comment> { Item = comment };
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

        [HttpPut("update/{id:int}")]
        public ActionResult<SuccessResponse> Update(CommentUpdateRequest request)
        {
            int userId = _authService.GetCurrentUserId();
            int iCode = 200;
            BaseResponse response = null;
            try
            {
                if (userId == request.UserId)
                {

                _service.UpdateComment(request, userId);
                response = new SuccessResponse();
                }
                else
                {
                    response = new ErrorResponse("unauthorized user");
                    iCode = 401;
                }
            }
            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(iCode, response);
        } 
        [HttpDelete("delete/{id:int}")]
        public ActionResult<SuccessResponse> DeleteComment(int id) 
        {
            int iCode = 200;
            BaseResponse response = null;
            try
            {
                _service.DeleteComment(id);
                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(iCode, response);
        }
    }
}
