using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Res
{
    public class ResAppointmentCreateDTO
    {
        public int AppointmentId { get; set; }
        public int StudentId { get; set; }
        public int PsychologistId { get; set; }
        public int SlotId { get; set; }
        public string Date { get; set; } 
        public DateTime DateCreated { get; set; }
        public string? GoogleMeetLink { get; set; }
    }
}
