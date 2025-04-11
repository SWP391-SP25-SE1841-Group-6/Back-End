using DataAccess.DTO.Res;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service.IService
{
    public interface ISlotService
    {
        Task<ResFormat<IEnumerable<ResSlotDTO>>> GetAllSlot();
    }
}
