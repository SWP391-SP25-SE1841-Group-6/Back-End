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
        public DateOnly Date { get; set; }
        public DateTime DateCreated { get; set; }
        public bool? IsDeleted { get; set; }
        public int SlotId { get; set; }
        public string? GoogleMeetLink { get; set; }
        public int PsychologistId { get; set; }
        public int Capacity { get; set; }
        public int CurrentNumber { get; set; }

    }
}
