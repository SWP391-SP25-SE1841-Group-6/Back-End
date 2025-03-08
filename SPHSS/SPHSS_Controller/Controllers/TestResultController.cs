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

        public TestResultController(ITestResultService testResultService)
        {
            _testResultService = testResultService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]TestResultCreateDTO testResult)
        {
            var result = await _testResultService.AddTestResultAsync(testResult);
            return Ok(result);
        }


        [HttpGet("student/{studentId}/test/{testId}")]
        public async Task<IActionResult> GetTestResultByStudent(int studentId, int testId)
        {
            var result = await _testResultService.GetTestResultByStudentAsync(studentId, testId);
            return Ok(result);
        }

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetTestResultsByStudent(int studentId)
        {
            var result = await _testResultService.GetTestResultsByStudentAsync(studentId);
            return Ok(result);
        }
    }
}
