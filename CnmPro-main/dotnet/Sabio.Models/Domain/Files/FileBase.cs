using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sabio.Models.Domain.User;

namespace Sabio.Models.Domain.Files
{
    public class FileBase : FileResponse
    {
        public LookUp FileType { get; set; }

        public int CreatedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
