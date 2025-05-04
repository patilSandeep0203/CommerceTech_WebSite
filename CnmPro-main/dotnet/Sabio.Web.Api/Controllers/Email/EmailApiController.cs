﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sabio.Web.Models.Responses;
using Sabio.Models.Domain;
using Sabio.Web.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Sabio.Services;
using System;
using System.Threading.Tasks;
using Sabio.Services.Interfaces;
using Sabio.Models.Requests.Email;
using SendGrid.Helpers.Mail;

namespace Sabio.Web.Api.Controllers.Email
{
    [Route("api/emails")]
    [ApiController]
    public class EmailApiController : BaseApiController
    {
        private IEmailServices _service = null;


        public EmailApiController(IEmailServices service,
                                  ILogger<PingApiController> logger) : base(logger)
        {
            _service = service;
        }

        // HTTP Test
        [HttpPost("test")]
        public ActionResult<SuccessResponse> testEmail(EmailAddRequest model)
        {
            int code = 200;
            BaseResponse response;

            try
            {
                _service.SendContactUsEmail(model);
                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse(ex.Message);
                code = 500;
            }
            return StatusCode(code, response);
        }

        [HttpPost("newsletter")]
        public async Task<ActionResult<SuccessResponse>> NewsSubscriptionConfirmation(EmailAddRequest model)
        {
            int code = 200;
            BaseResponse response;

            try
            {
                await _service.NewsSubscriptionConfirmation(model);

                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse(ex.Message);
                code = 500;
            }
            return StatusCode(code, response);
        }

        [HttpPost("approvedEmail")]
        public async Task<ActionResult<SuccessResponse>> SendDeniedProposalEmail(EmailAddRequest model)
        {
            int code = 200;
            BaseResponse response;

            try
            {
                await _service.SendApprovedProposalEmail(model);

                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse(ex.Message);
                code = 500;
            }
            return StatusCode(code, response);
        }

        [HttpPost("deniedEmail")]
        public async Task<ActionResult<SuccessResponse>> SendApprovedProposalEmail(EmailAddRequest model)
        {
            int code = 200;
            BaseResponse response;

            try
            {
                await _service.SendDeniedProposalEmail(model);
                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse(ex.Message);
                code = 500;
            }
            return StatusCode(code, response);
        }
    }
}
