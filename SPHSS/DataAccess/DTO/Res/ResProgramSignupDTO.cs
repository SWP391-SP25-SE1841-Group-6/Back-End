using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Res
{
    public class ResProgramSignupDTO
    {
        public int StudentId { get; set; }
        public int ProgramId { get; set; }
        public string ProgramName { get; set; } = null!;
        public DateTime DateAdded { get; set; }
    }
}
