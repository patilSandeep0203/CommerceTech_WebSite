using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sabio.Models.Domain.Comments
{
    public class Comment
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public int ParentId { get; set; }
        public int EntityTypeId { get; set; }
        public string EntityType { get; set; }
        public int EntityId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int CreatedBy { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarUrl { get; set; }
        public bool IsDeleted { get; set; }
        public List<Comment> Replies { get; set; }

    }
}
