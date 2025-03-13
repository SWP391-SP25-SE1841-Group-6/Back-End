using DataAccess.DTO.Req;
using DataAccess.Service;
using DataAccess.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SPHSS_Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly IAccountService _accountService;
        public BlogController(IBlogService blogService, IAccountService accountService)
        {
            _blogService = blogService;
            _accountService = accountService;
        }
        [HttpGet("GetAllBlog")]
        public async Task<IActionResult> Get()
        {
            var result = await _blogService.GetAllBlog();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("DeleteBlog")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var result = await _blogService.DeleteBlog(id);

            if (!result.Success)
                return NotFound(new { success = false, message = result.Message });

            return Ok(new { success = true, message = result.Message });
        }

        [HttpPost("CreateBlog")]
        public async Task<IActionResult> CreateBlog([FromBody] BlogCreateDTO dto)
        {
            var user = await _accountService.GetAcccountByTokenAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _blogService.CreateBlog(dto,user.AccId);

            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new { success = true, data = result.Data, message = result.Message });
        }

        [HttpGet("GetBlogById")]
        public async Task<IActionResult> GetBlogById(int id)
        {
            var result = await _blogService.GetBlogById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut("UpdateBlog")]
        public async Task<IActionResult> Update(BlogUpdateDTO blog, int id)
        {
            var user = await _accountService.GetAcccountByTokenAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
            var result = await _blogService.Update(blog, id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut("ApproveBlog")]
        public async Task<IActionResult> Approve(int id)
        {
            var result = await _blogService.ApproveBlog(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

    }
}
