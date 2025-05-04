using Microsoft.AspNetCore.Mvc;
using Sabio.Models;
using Sabio.Models.Domain.ShareStory;
using Sabio.Models.Requests.ShareStory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services.Interfaces
{
    public interface IShareStoryService
    {
        int Add(ShareStoryAddRequest model, int userId);
        void Update(ShareStoryUpdateRequest model, int ShareStoryId, int userId);
        ShareStory GetStoryById(int id);
        void Delete(int id);
        Paged<ShareStory> GetAll(int pageIndex, int pageSize);


    }
}
