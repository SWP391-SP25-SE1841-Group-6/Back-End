using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Req
{
    public class AppointmentCreateDTO
    {
        public int StudentID { get; set; }
        public int SlotID { get; set; }
        public String Date { get; set; }
    }
}
