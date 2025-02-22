using DataAccess.DTO.Req;
using DataAccess.DTO.Res;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service.IService
{
    public interface IBlogService
    {
        Task<ResFormat<IEnumerable<ResBlogCreateDTO>>> GetAllBlog();
        Task<ResFormat<ResBlogCreateDTO>> GetBlogById(int id);
        Task<ResFormat<bool>> DeleteBlog(int id);
        Task<ResFormat<ResBlogCreateDTO>> CreateBlog(BlogCreateDTO dto);
        Task<ResFormat<ResBlogCreateDTO>> Update(BlogUpdateDTO blog, int id);
        Task<ResFormat<bool>> ApproveBlog(int id);
    }
}
