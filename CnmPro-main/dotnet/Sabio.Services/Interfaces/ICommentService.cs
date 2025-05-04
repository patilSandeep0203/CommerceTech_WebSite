using Sabio.Models;
using Sabio.Models.Domain.Comments;
using Sabio.Models.Requests.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services.Interfaces
{
    public interface ICommentService
    {
        int AddComment(CommentAddRequest request, int userId);
        Comment GetComment(int id);
        Paged<Comment> GetByCreatedBy(int userId, int pageIndex, int pageSize);
        Paged<Comment> GetByEntityId(int EntityId, int EntityTypeId, int pageIndex, int pageSize);
        Comment GetByParentId(int ParentId);
        Paged<Comment> GetCommentsPaginated(int pageIndex, int pageSize);
        Paged<Comment> GetRepliesPaginated(int pageIndex, int pageSize);
        void UpdateComment(CommentUpdateRequest request, int userId);
        void DeleteComment(int id);
    }
}
