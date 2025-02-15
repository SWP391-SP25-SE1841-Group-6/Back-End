using DataAccess.DTO.Req;
using DataAccess.Service;
using DataAccess.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SPHSS_Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionTypeController : ControllerBase
    {
        private readonly IQuestionTypeService _questionTypeService;
        public QuestionTypeController(IQuestionTypeService questionTypeService)
        {
            _questionTypeService = questionTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _questionTypeService.GetAllQuestionType();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(QuestionTypeCreateDTO questionType)
        {
            var result = await _questionTypeService.CreateQuestionType(questionType);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _questionTypeService.DeleteQuestionType(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var result = await _questionTypeService.GetQuestionTypeById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(QuestionTypeCreateDTO questionType, int id)
        {
            var result = await _questionTypeService.UpdateQuestionType(questionType, id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
