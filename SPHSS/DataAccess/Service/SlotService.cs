using AutoMapper;
using BusinessObject;
using DataAccess.DTO.Res;
using DataAccess.Repo.IRepo;
using DataAccess.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class SlotService : ISlotService
    {
        private readonly IBaseRepo<Slot> _slotRepo;
        private readonly IMapper _mapper;

        public SlotService(IBaseRepo<Slot> slotRepo, IMapper mapper)
        {
            _slotRepo = slotRepo;
            _mapper = mapper;
        }

        public async Task<ResFormat<IEnumerable<ResSlotDTO>>> GetAllSlot()
        {
            var res = new ResFormat<IEnumerable<ResSlotDTO>>();
            try
            {
                var list = await _slotRepo.FindAsync(b => (bool)!b.IsDeleted);
                var resList = _mapper.Map<IEnumerable<ResSlotDTO>>(list);

                res.Success = true;
                res.Data = resList;
                res.Message = "Retrieved successfully";
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Retrieved failed: {ex.Message}";
            }
            return res;
        }
    }
}
