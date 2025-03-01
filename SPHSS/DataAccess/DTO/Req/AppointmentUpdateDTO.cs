using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Req
{
    public class AppointmentUpdateDTO
    {
        public string Date { get; set; }
        public int SlotId { get; set; }
    }
}
