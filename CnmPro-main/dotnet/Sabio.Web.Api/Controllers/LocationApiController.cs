using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Requests.Locations;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Collections.Generic;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/locations")]
    [ApiController]
    public class LocationApiController : BaseApiController
    {
        private ILocationService _locationService = null;
        private IAuthenticationService<int> _authService = null;

        public LocationApiController(ILocationService locationService, ILogger<IUserService> logger, IAuthenticationService<int> authService) : base(logger)
        {
            _locationService = locationService;
            _authService = authService;
        }

        #region GetByI
        [HttpGet("{id:int}")]
        public ActionResult<ItemResponse<Location>> GetById(int id)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                Location aLocation = _locationService.GetById(id);

                if (aLocation == null)
                {
                    code = 404;
                    response = new ErrorResponse("Application resource not found.");
                }
                else
                {
                    response = new ItemResponse<Location>() { Item = aLocation };
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
        #endregion

        #region Get All Paginated
        [HttpGet]
        public ActionResult<ItemResponse<Paged<Location>>> GetAll(int pageIndex, int pageSize)
        {
            ActionResult result = null;
            try
            {
                Paged<Location> paged = _locationService.GetAll(pageIndex, pageSize);
                if (paged == null)
                {
                    result = NotFound404(new ErrorResponse("Records Not Found"));
                }
                else
                {
                    ItemResponse<Paged<Location>> response = new ItemResponse<Paged<Location>>();
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
        #endregion

        #region GetByAuthor
        [HttpGet("author")]
        public ActionResult<ItemResponse<Paged<Location>>> GetByAuthor(int authorId, int pageIndex, int pageSize)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                Paged<Location> paged = _locationService.GetByAuthor(authorId, pageIndex, pageSize);
                if (paged == null)
                {
                    response = new ErrorResponse("Records Not Found");
                    code = 404;
                }
                else
                {
                    response = new ItemResponse<Paged<Location>>() { Item = paged };
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                response =  new ErrorResponse(ex.Message);
                code = 500;
            }

            return StatusCode(code, response);
        }
        #endregion

        #region Get By Geo
        [HttpGet("geo")]
        public ActionResult<ItemsResponse<Location>> GetByGeo(double lat, double lng, int distance)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                List<Location> listOfLocations = _locationService.GetByGeo(lat, lng, distance);

                if (listOfLocations == null)
                {
                    code = 404;
                    response = new ErrorResponse("Application resource not found.");
                }
                else
                {
                    response = new ItemsResponse<Location>() { Items = listOfLocations };
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
        #endregion

        #region Create New Record
        [HttpPost]
        public ActionResult<ItemResponse<int>> Create(LocationAddRequest model)
        {
            int userId = _authService.GetCurrentUserId();

            ObjectResult result = null;

            try
            {
                int id = _locationService.Add(model, userId);
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
        #endregion

        #region Update existing Record
        [HttpPut("{id:int}")]
        public ActionResult<SuccessResponse> Update(LocationUpdateRequest model)
        {
            int code = 200;
            int userId = _authService.GetCurrentUserId();
            BaseResponse response = null;

            try
            {
                _locationService.Update(model, userId);
                response = new SuccessResponse();

            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(code, response);
        }
        #endregion

        #region Delete Record
        [HttpDelete("delete/{id:int}")]
        public ActionResult<SuccessResponse> Delete(int id)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                _locationService.Delete(id);
                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(code, response);
        }
        #endregion


      
    }
}
