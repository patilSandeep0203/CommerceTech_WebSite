using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Domain.User;
using Sabio.Models.Requests;
using Sabio.Models.Requests.Email;
using Sabio.Models.Requests.User;
using System.Threading.Tasks;
using Twilio.Rest.Verify.V2.Service;

namespace Sabio.Services
{
    public interface IUserService
    {
        VerificationResource SendVerification(string phoneNumber);
        Task<bool> CheckVerification(TwoFactorAuthenticationRequest model);
        int Create(UserAddRequest userModel);

        void InsertToken(string token, int userId, int tokenType);

        void ConfirmUser(string token);

        bool LogInAsync(string email, string password);

        Task<bool> LogInTest(string email, string password, int id, string[] roles = null);

        bool VerifyEmail(string email);

        int GetByEmail(string email);

        void UpdatePassword(UserUpdatePasswordRequest model);

        void DeleteToken(string token);

        UserV2 HasProfile(int userId, UserBase user);

        #region Facebook
        int AddThirdParty(UserThirdPartyAddRequest model, int tokenType);
        Task<bool> FacebookLogInAsync(string email);
        #endregion
    }
}