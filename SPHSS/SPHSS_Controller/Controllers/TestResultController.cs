using DataAccess.DTO.Req;
using DataAccess.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SPHSS_Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestResultController : ControllerBase
    {
        private readonly ITestResultService _testResultService;
        private readonly IAccountService _accountService;

        public TestResultController(ITestResultService testResultService, IAccountService accountService)
        {
            _testResultService = testResultService;
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]TestResultCreateDTO testResult)
        {
            var user = await _accountService.GetAcccountByTokenAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _testResultService.AddTestResultAsync(testResult, user.AccId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


        [HttpGet("test/{testId}")]
        public async Task<IActionResult> GetTestResultByStudent(int testId)
        {
            var user = await _accountService.GetAcccountByTokenAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
            var result = await _testResultService.GetTestResultByStudentAsync(user.AccId, testId);
            return Ok(result);
        }
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetTestResultsByStudentId(int studentId)
        {
            var result = await _testResultService.GetTestResultsByStudentIdAsync(studentId);
            if (result == null || !result.Success)
            {
                return NotFound(result?.Message ?? "Failed to retrieve test results.");
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTestResultsByStudent()
        {
            var user = await _accountService.GetAcccountByTokenAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
            var result = await _testResultService.GetTestResultsByStudentAsync(user.AccId);
            return Ok(result);
        }
    }
}
