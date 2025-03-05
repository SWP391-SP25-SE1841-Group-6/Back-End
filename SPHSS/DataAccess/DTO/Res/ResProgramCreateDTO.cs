using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Res
{
    public class ResProgramCreateDTO
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; } = null!;
        public DateOnly DateStart { get; set; }
        public DateOnly? DateEnd { get; set; }
        public DateTime DateCreated { get; set; }
        public bool? IsDeleted { get; set; }
        public int SlotId { get; set; }

    }
}
