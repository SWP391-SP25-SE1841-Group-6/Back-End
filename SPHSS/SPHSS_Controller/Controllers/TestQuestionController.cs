using DataAccess.DTO.Req;
using DataAccess.Service;
using DataAccess.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SPHSS_Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestQuestionController : ControllerBase
    {
        private readonly ITestQuestionService _testQuestionService;
        public TestQuestionController(ITestQuestionService testQuestionService)
        {
            _testQuestionService = testQuestionService;
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TestQuestionAddDTO testQuestion)
        {
            var result = await _testQuestionService.AddTestQuestion(testQuestion);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(TestQuestionAddDTO testQuestion)
        {
            var result = await _testQuestionService.RemoveTestQuestion(testQuestion);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
