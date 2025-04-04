using BusinessObject.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Req
{
    public class ProgramCreateDTO
    {
        public string ProgramName { get; set; } = null!;
        public String Date { get; set; }
        public int SlotId { get; set; }
        public int Capacity { get; set; }
        public TypeEnum Type { get; set; }
    }
}
