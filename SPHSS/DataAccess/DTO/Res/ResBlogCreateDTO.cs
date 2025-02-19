using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Res
{
    public class ResBlogCreateDTO
    {
        public int? BlogId { get; set; }
        public string? BlogName { get; set; }
        public int? CreatorId { get; set; }
        public string? ContentDescription { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool? IsDeleted { get; set; }

    }
}
