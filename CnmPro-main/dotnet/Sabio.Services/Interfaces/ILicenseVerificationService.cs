using Sabio.Models.Domain.LicenseVerification;
using Sabio.Models.Requests.LicenseVerification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services
{
    public interface ILicenseVerificationService
    {
        UserLicense SelectbyUserLicenseId(int Id);
        List<UserLicense> SelectbyUnexpiredLicenseType(int LicenseTypeId, int DateExpires);
        int Create(LicenseVerificationAddRequest model);
        void Update(LicenseVerificationUpdateRequest model, int id);
    }
}
