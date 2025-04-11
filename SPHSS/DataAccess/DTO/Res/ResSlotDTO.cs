using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Res
{
    public class ResSlotDTO
    {
        public int SlotId { get; set; }
        public TimeOnly TimeStart { get; set; }
        public TimeOnly TimeEnd { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
