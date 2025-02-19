using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Req
{
    public class BlogUpdateDTO
    {
        public string? BlogName { get; set; }
        public string? ContentDescription { get; set; }
    }
}
