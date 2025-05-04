using Microsoft.AspNetCore.Http;
using Sabio.Models;
using Sabio.Models.Domain.Files;
using Sabio.Models.Requests.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services.Interfaces
{
    public interface IFileService
    {
        Task<List<FileResponse>> UploadFilesAsync(List<IFormFile> files, int userId);

        int AddFile(FileAddRequest model, int userId);

        public Paged<FileBase> GetAll(int pageIndex, int pageSize);

        Paged<FileBase> GetByFileTypeId(int typeId, int pageIndex, int pageSize);

        public Paged<FileBase> GetByUserId(int userId, int pageIndex, int pageSize);

        public Paged<FileBase> GetByFileIsDeleted(bool isDeleted, int pageIndex, int pageSize);

        public Paged<FileBase> SearchPagination(int pageIndex, int pageSize, string query);

        public void DeleteById(int id, int deleteStatus);

    }

}
