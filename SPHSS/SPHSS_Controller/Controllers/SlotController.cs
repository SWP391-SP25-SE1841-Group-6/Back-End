using DataAccess.Service;
using DataAccess.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace SPHSS_Controller.Controllers
{
    [Route("api/slots")]
    [ApiController]
    public class SlotController : Controller
    {
        private readonly ISlotService _slotService;

        public SlotController(ISlotService slotService)
        {
            _slotService = slotService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSlot()
        {
            var slots = await _slotService.GetAllSlot();
            return Ok(slots);
        }
    }
}
