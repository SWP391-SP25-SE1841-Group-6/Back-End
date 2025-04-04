using DataAccess.DTO.Req;
using DataAccess.Service;
using DataAccess.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace SPHSS_Controller.Controllers
{
    [Route("api/programs")]
    [ApiController]
    public class ProgramController : Controller
    {
        private readonly IProgramService _programService;
        private readonly IAccountService _accountService;

        public ProgramController(IProgramService programService, IAccountService accountService)
        {
            _programService = programService;
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPrograms()
        {
            var programs = await _programService.GetAllPrograms();
            return Ok(programs);
        }

        [HttpPost("CreateProgram")]
        public async Task<IActionResult> CreateProgram([FromBody] ProgramCreateDTO dto)
        {
            try
            {
                var program = await _programService.CreateProgram(dto);
                return Ok(program);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("DeleteProgramById")]
        public async Task<IActionResult> DeleteProgram(int programId)
        {
            var result = await _programService.DeleteProgram(programId);

            if (!result)
            {
                return NotFound(new { message = "Chương trình không tồn tại!" });
            }

            return Ok(new { message = "Xóa chương trình thành công!" });
        }

        [HttpPut("UpdateProgramById")]
        public async Task<IActionResult> UpdateProgram(int programId, [FromBody] ProgramUpdateDTO dto)
        {
            try
            {
                var updatedProgram = await _programService.UpdateProgram(programId, dto);

                if (updatedProgram == null)
                {
                    return NotFound(new { message = "Chương trình không tồn tại hoặc đã bị xóa!" });
                }

                return Ok(updatedProgram);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetProgramById")]
        public async Task<IActionResult> GetProgramById(int programId)
        {
            var program = await _programService.GetProgramById(programId);

            if (program == null)
            {
                return NotFound(new { message = "Chương trình không tồn tại hoặc đã bị xóa!" });
            }

            return Ok(program);
        }

        [HttpGet("GetProgramByStudentId")]
        public async Task<IActionResult> GetProgramByStudentId(int studentId)
        {
            var user = await _accountService.GetAcccountByTokenAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var program = await _programService.GetProgramByStudentId(user.AccId);

            if (!program.Any())
            {
                return NotFound(new { message = "Bạn chưa tham gia chương trình nào cả!" });
            }

            return Ok(program);
        }

        [HttpPost("RegisterProgram")]
        public async Task<IActionResult> RegisterProgram([FromBody] ProgramSignupDTO request)
        {
            var user = await _accountService.GetAcccountByTokenAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
            try
            {
                var result = await _programService.RegisterProgram(user.AccId, request.ProgramId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}
