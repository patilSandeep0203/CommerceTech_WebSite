using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Requests.Blogs;
using System.Collections.Generic;
using System.Data;

namespace Sabio.Services.Interfaces
{
    public interface IBlogService
    {
        int AddBlog(BlogAddRequest model, int userId);
        void Delete(int id);
        Paged<Blog> GetAll(int pageIndex, int pageSize);
        Paged<Blog> GetByAuthor(int authorId, int pageIndex, int pageSize);
        Blog GetById(int id);
        void Update(BlogUpdateRequest model, int userId);
        Paged<Blog> SearchBySubject(string subject, int pageIndex, int pageSize);
    };
}