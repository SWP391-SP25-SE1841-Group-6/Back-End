using DataAccess.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.DTO.Req
{
    public class AppointmentCreateDTO
    {
        public int StudentID { get; set; }
        public int SlotID { get; set; }
        public string Date { get; set; }
    }
}
