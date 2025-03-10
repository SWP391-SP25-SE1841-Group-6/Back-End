using DataAccess.DTO.Req;
using DataAccess.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace SPHSS_Controller.Controllers
{
    [Route("api/programs")]
    [ApiController]
    public class ProgramController : Controller
    {
        private readonly IProgramService _programService;

        public ProgramController(IProgramService programService)
        {
            _programService = programService;
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
            var program = await _programService.GetProgramByStudentId(studentId);

            if (program == null)
            {
                return NotFound(new { message = "Chương trình không tồn tại hoặc đã bị xóa!" });
            }

            return Ok(program);
        }

        [HttpPost("RegisterProgram")]
        public async Task<IActionResult> RegisterProgram([FromBody] ProgramSignupDTO request)
        {
            try
            {
                var result = await _programService.RegisterProgram(request.StudentId, request.ProgramId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}
