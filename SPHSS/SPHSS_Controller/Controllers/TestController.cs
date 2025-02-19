using DataAccess.DTO.Req;
using DataAccess.Service;
using DataAccess.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SPHSS_Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;
        public TestController(ITestService testService)
        {
            _testService = testService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(TestCreateDTO test)
        {
            var result = await _testService.CreateTest(test);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
