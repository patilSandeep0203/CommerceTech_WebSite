using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Domain.User;
using Sabio.Models.Enums;
using Sabio.Models.Requests;
using Sabio.Models.Requests.Email;
using Sabio.Models.Requests.User;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Threading.Tasks;
using Twilio.Rest.Verify.V2.Service;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserApiController : BaseApiController
    {
        private IUserService _userService = null;
        private IEmailServices _emailService = null;
        private IAuthenticationService<int> _authService = null;

        public UserApiController(IUserService service, IEmailServices emailServices, ILogger<IUserService> logger, IAuthenticationService<int> authService) : base(logger)
        {
            _userService = service;
            _authService = authService;
            _emailService = emailServices;
        }

        [HttpPost("login/verify/auth")]
        [AllowAnonymous]
        public async Task<ActionResult<SuccessResponse>> Verify(TwoFactorAuthenticationRequest model)
        {
            int code = 200;
            BaseResponse response = null;
            bool success = false;
            try
            {
                success = await _userService.CheckVerification(model);
                if (success == false)
                {
                    code = 401;
                    response = new ErrorResponse("App resources not found.");
                }
                else
                {
                    response = new SuccessResponse();
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

        [HttpPost()]
        [AllowAnonymous]
        public ActionResult<ItemResponse<int>> Add(UserAddRequest model)
        {

            ObjectResult result = null;

            try
            {
                int id = _userService.Create(model);

                string token = Guid.NewGuid().ToString();

                _userService.InsertToken(token, id, 1);

                ItemResponse<int> response = new ItemResponse<int>() { Item = id };
                _emailService.confirmEmail(model.Email, token);
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

        [HttpPost("login/verify")]
        [AllowAnonymous]
        public ActionResult<SuccessResponse> SendAuthenticationTwoken(TwoFactorToken model)
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                VerificationResource service = _userService.SendVerification(model.PhoneNumber);
            }
            catch (Exception ex)
            {
                code = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(code, response);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult<SuccessResponse> LoginAsync(UserLogin model)
        {
            int code = 200;
            BaseResponse response = null;
            bool success = false;

            try
            {
                success = _userService.LogInAsync(model.Email, model.Password);

                if (success == false)
                {
                    code = 404;
                    response = new ErrorResponse("Try Again");
                }
                else
                {
                    response = new SuccessResponse();
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

        [HttpPut("confirm/{token}")]
        [AllowAnonymous]
        public ActionResult<SuccessResponse> ConfirmUser(string token)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                _userService.ConfirmUser(token);

                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(code, response);
        }

        [HttpGet("current")]
        public ActionResult<ItemResponse<User>> CurrentUser()
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                IUserAuthData user = _authService.GetCurrentUser();

                if (user == null)
                {
                    code = 404;
                    response = new ErrorResponse("User not found.");
                }
                else
                {
                    response = new ItemResponse<IUserAuthData>() { Item = user };
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


        [HttpGet("currentv2")]
        public ActionResult<ItemResponse<UserV2>> CurrentUserV2()
        {
            int code = 200;
            BaseResponse response = null;
            UserBase user = (UserBase)_authService.GetCurrentUser();

            try
            {
                UserV2 aUser = _userService.HasProfile(user.Id, user);

                if (aUser == null)
                {
                    code = 404;
                    response = new ErrorResponse("User not found.");
                }
                else
                {
                    response = new ItemResponse<UserV2>() { Item = aUser };
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

        [HttpGet("logout")]
        public async Task<ActionResult<SuccessResponse>> Logout()
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                await _authService.LogOutAsync();
                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(code, response);
        }


        [HttpPut("forgotpassword")]
        [AllowAnonymous]
        public ActionResult<SuccessResponse> SendForgotPassEmail(ChangePasswordRequest model)
        {
            int code = 200;
            BaseResponse response;
            int tokenType = 1;
            int id = 0;
            bool isEmailValid = false;

            try
            {
                isEmailValid = _userService.VerifyEmail(model.Email);

                if (!isEmailValid)
                {
                    code = 404;
                    response = new ErrorResponse("The email you have entered does not exist with an account.");
                }
                else
                {
                    id = _userService.GetByEmail(model.Email);
                    string newToken = Guid.NewGuid().ToString();
                    _userService.InsertToken(newToken, id, tokenType);
                    _emailService.SendResetPasswordEmail(model.Email, newToken);

                    response = new SuccessResponse();
                }
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse(ex.Message);
                code = 500;
            }
            return StatusCode(code, response);
        }


        [HttpPut("changepassword")]
        [AllowAnonymous]
        public ActionResult<SuccessResponse> ResetPassword(UserUpdatePasswordRequest model)
        {
            int code = 200;
            BaseResponse response = null;
            model.Id = _userService.GetByEmail(model.Email);

            try
            {
                _userService.UpdatePassword(model);

                _userService.DeleteToken(model.Token);

                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(code, response);
        }

        #region Facebook
        [HttpPost("facebook/register")]
        [AllowAnonymous]
        public ActionResult<ItemResponse<int>> CreateFacebookUser(UserThirdPartyAddRequest model)
        {

            ObjectResult result = null;

            try
            {
                int tokenType = (int)TokenType.Facebook;
                int id = _userService.AddThirdParty(model, tokenType);
           
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

        [HttpPost("login/facebook")]
        [AllowAnonymous]
        public async Task<ActionResult<SuccessResponse>> FacebookLoginAsync(FacebookLogin model)
        {
            int code = 200;
            BaseResponse response = null;
            bool success = false;

            try
            {
                success = await _userService.FacebookLogInAsync(model.Email);

                if (success == false)
                {
                    code = 404;
                    response = new ErrorResponse("Try Again");
                }
                else
                {
                    response = new SuccessResponse();
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
        #endregion
    }
}
