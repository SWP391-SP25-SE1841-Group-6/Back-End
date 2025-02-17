using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Req
{
    public class BlogCreateDTO
    {
        public string? BlogName { get; set; }
        public string? ContentDescription { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
