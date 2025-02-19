using BusinessObject.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Req
{
    public class AccountRegisterDTO
    {
        public string? AccName { get; set; }

        public string? AccPass { get; set; }

        public string? AccEmail { get; set; }

        public DateTime? Dob { get; set; }

        public bool? Gender { get; set; }
        public RoleEnum Role { get; set; }

    }
}
