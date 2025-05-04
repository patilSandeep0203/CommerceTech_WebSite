using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Domain.Events;
using Sabio.Models.Interfaces;
using Sabio.Models.Requests.Events;
using Sabio.Services;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Collections.Generic;

namespace Sabio.Web.Api.Controllers.Events
{
    [Route("api/events")]
    [ApiController]
    public class EventApiController : BaseApiController
    {
        private IEventService _service = null;
        private IAuthenticationService<int> _authService = null;
        public EventApiController(
                IEventService service
               , ILogger<PingApiController> logger
               , IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _authService = authService;
        }
        [HttpPost]
        public ActionResult<ItemResponse<int>> Create(EventAddRequest model)
        {
            int userId = _authService.GetCurrentUserId();

            ObjectResult result = null;

            try
            {
                int id = _service.AddEvent(model, userId);
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
        public ActionResult<SuccessResponse> Update(EventUpdateRequest model, int userId)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                _service.Update(model);
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
                _service.DeleteEventById(id);
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
        public ActionResult<ItemResponse<Paged<Event>>> GetAll(int pageIndex, int pageSize)
        {
            ActionResult result = null;
            try
            {
                Paged<Event> paged = _service.SelectAll(pageIndex, pageSize);
                if (paged == null)
                {
                    result = NotFound404(new ErrorResponse("Records Not Found"));
                }
                else
                {
                    ItemResponse<Paged<Event>> response = new ItemResponse<Paged<Event>>();
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
        public ActionResult<ItemResponse<Event>> GetById(int id)
        {
            int iCode = 200;
            BaseResponse response = null;
            try
            {
                Event events = _service.GetEventById(id); 

                if (events == null) 
                {
                    iCode = 404;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Event> { Item = events }; 
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
        public ActionResult<ItemsResponse<EventBase>> GetByOrganization(int id)
        {
            int iCode = 200;
            BaseResponse response = null;
            try
            {
                List<EventBase> events = _service.GetEventByOrganization(id); 

                if (events == null) 
                {
                    iCode = 404;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemsResponse<EventBase> { Items = events }; 
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
        public ActionResult<ItemResponse<Paged<Event>>> GetByCreatedBy(int createdBy, int pageIndex, int pageSize)
        {
            ActionResult result = null;
            try
            {
                Paged<Event> paged = _service.SelectEventByCreatedBy(createdBy, pageIndex, pageSize);
                if (paged == null)
                {
                    result = NotFound404(new ErrorResponse("Records Not Found"));
                }
                else
                {
                    ItemResponse<Paged<Event>> response = new ItemResponse<Paged<Event>>();
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
        public ActionResult<ItemsResponse<Paged<Event>>> GetSearchedEvents(int pageIndex, int pageSize, string query)
        {

            ActionResult result = null;
            try
            {
                Paged<Event> paged = _service.GetSearchedEvents(pageIndex, pageSize, query);
                if (paged == null)
                {

                    result = NotFound404(new ErrorResponse("Records Not Found"));
                }
                else
                {
                    ItemResponse<Paged<Event>> response = new ItemResponse<Paged<Event>>();
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
        public ActionResult<ItemsResponse<Paged<Event>>> GetSearchedEventsByLocation(int pageIndex, int pageSize, string query)
        {

            ActionResult result = null;
            try
            {
                Paged<Event> paged = _service.GetSearchedEventsByLocation(pageIndex, pageSize, query);
                if (paged == null)
                {

                    result = NotFound404(new ErrorResponse("Records Not Found"));
                }
                else
                {
                    ItemResponse<Paged<Event>> response = new ItemResponse<Paged<Event>>();
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
        public ActionResult<ItemsResponse<Paged<EventSearchResult>>> GetEventsWithDistanceRadius(int pageIndex, int pageSize, string query, double latParam, double longParam, int distance)
        {

            ActionResult result = null;
            try
            {
                Paged<EventSearchResult> paged = _service.GetSearchedEventsWithLocationRadius(pageIndex, pageSize, query, latParam, longParam, distance);
                if (paged == null)
                {

                    result = NotFound404(new ErrorResponse("Records Not Found"));
                }
                else
                {
                    ItemResponse<Paged<EventSearchResult>> response = new ItemResponse<Paged<EventSearchResult>>();
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
