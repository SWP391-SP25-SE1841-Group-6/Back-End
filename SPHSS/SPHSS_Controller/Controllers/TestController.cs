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
        [HttpPut]
        public async Task<IActionResult> Update(TestUpdateDTO test, int id)
        {
            var result = await _testService.UpdateTest(test, id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _testService.DeleteTest(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _testService.GetAllTest();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetTestById(int id)
        {
            var result = await _testService.GetTestById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
