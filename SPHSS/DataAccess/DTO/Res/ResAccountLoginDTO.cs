using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Res
{
    public class ResAccountLoginDTO
    {
        public int AccId { get; set; }
        public string? AccName { get; set; }

        public string? AccPass { get; set; }

        public string? AccEmail { get; set; }
        public int RoleId { get; set; }
    }
}
