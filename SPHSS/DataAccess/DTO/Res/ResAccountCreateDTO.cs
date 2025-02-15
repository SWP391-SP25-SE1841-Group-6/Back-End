using BusinessObject.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Res
{
    public class ResAccountCreateDTO
    {
        public int? AccId { get; set; }
        public string? AccName { get; set; }

        public string? AccPass { get; set; }

        public string? AccEmail { get; set; }

        public DateTime? Dob { get; set; }

        public bool? Gender { get; set; }

        public RoleEnum Role { get; set; }

        public bool? IsActivated { get; set; }

        public bool? IsApproved { get; set; }
    }
}
