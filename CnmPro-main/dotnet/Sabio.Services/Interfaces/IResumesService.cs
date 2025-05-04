using Sabio.Models;
using Sabio.Models.Domain.Resumes;
using Sabio.Models.Requests.Resumes;
using System.Data;

namespace Sabio.Services.Interfaces
{
    public interface IResumesService
    {
        int AddV2(ResumeAddRequestV2 model, int userId);
        Resume GetById(int id);
        Paged<Resume> GetAllByPage(int pageIndex, int pageSize);
        int Add(ResumeAddRequest model, int userId);
        void Update(ResumeUpdateRequest model, int userId);
        void Delete(int id);
        int AddResumeView(ResumeViewAddRequest model);
        ResumeView GetResumeViewById(int id);
        Paged<ResumeView> GetResumeViewsByResumeId(int id, int pageIndex, int pageSize);
        public int GetResumeViewCount(int id);
    }
}