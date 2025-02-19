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
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _blogService.GetAllBlog();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var result = await _blogService.DeleteBlog(id);

            if (!result.Success)
                return NotFound(new { success = false, message = result.Message });

            return Ok(new { success = true, message = result.Message });
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog([FromBody] BlogCreateDTO dto)
        {
            var result = await _blogService.CreateBlog(dto);

            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new { success = true, data = result.Data, message = result.Message });
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetBlogById(int id)
        {
            var result = await _blogService.GetBlogById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(BlogUpdateDTO blog, int id)
        {
            var result = await _blogService.Update(blog, id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        //[Authorize]
        //[HttpPost]
        //public async Task<IActionResult> CreateBlog([FromBody] BlogCreateDTO dto)
        //{
        //    // Lấy CreatorId từ JWT Token
        //    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        //    if (userIdClaim == null)
        //        return Unauthorized(new { success = false, message = "User not authenticated" });

        //    int creatorId = int.Parse(userIdClaim.Value); // Chuyển về số nguyên

        //    var result = await _blogService.CreateBlog(dto, creatorId);

        //    if (!result.Success)
        //        return BadRequest(new { success = false, message = result.Message });

        //    return Ok(new { success = true, data = result.Data, message = result.Message });
        //}

    }
}
