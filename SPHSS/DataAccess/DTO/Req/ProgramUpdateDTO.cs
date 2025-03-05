using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Req
{
    public class ProgramUpdateDTO
    {
        public string ProgramName { get; set; } = null!;
        public String DateStart { get; set; }
        public String? DateEnd { get; set; }
        public int SlotId { get; set; }
    }
}
