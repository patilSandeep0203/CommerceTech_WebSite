using Sabio.Models.Domain.Files;
using System.Collections.Generic;

namespace Sabio.Models.Domain.ShareStory
{
    public class ShareStory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Story { get; set; }
        public int CreatedBy { get; set; }
        public List<FileBase> File { get; set; }
        public UserProfile User { get; set; }
        public List<int> FilesIds { get; set; }
        


    }
}
