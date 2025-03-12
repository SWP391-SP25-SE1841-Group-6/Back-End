using AutoMapper;
using BusinessObject;
using DataAccess.DTO.Req;
using DataAccess.DTO.Res;
using DataAccess.Repo.IRepo;
using DataAccess.Service.IService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class BlogService : IBlogService
    {

        private readonly IBaseRepo<Blog> _blogRepo;
        private readonly IMapper _mapper;

        public BlogService(IBaseRepo<Blog> blogRepo, IMapper mapper)
        {
            _blogRepo = blogRepo;
            _mapper = mapper;
        }

        public async Task<ResFormat<IEnumerable<ResBlogCreateDTO>>> GetAllBlog()
        {
            var res = new ResFormat<IEnumerable<ResBlogCreateDTO>>();
            try
            {
                var list = await _blogRepo.FindAsync(b => (bool)!b.IsDeleted && b.IsApproved == true); // Chỉ lấy blog chưa bị xóa và được approved
                var resList = _mapper.Map<IEnumerable<ResBlogCreateDTO>>(list);

                res.Success = true;
                res.Data = resList;
                res.Message = "Retrieved successfully";
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Retrieved failed: {ex.Message}";
            }
            return res;
        }

        public async Task<ResFormat<ResBlogCreateDTO>> GetBlogById(int id)
        {
            var res = new ResFormat<ResBlogCreateDTO>();
            try
            {
                var blog = await _blogRepo.GetByIdAsync(id);
                if (blog == null)
                {
                    res.Success = false;
                    res.Message = "Blog not found";
                    return res;
                }

                var blogDto = _mapper.Map<ResBlogCreateDTO>(blog);
                res.Success = true;
                res.Data = blogDto;
                res.Message = "Retrieved successfully";
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Retrieved failed: {ex.Message}";
            }
            return res;
        }

        public async Task<ResFormat<bool>> DeleteBlog(int id)
        {
            var res = new ResFormat<bool>();
            try
            {
                var blog = await _blogRepo.GetByIdAsync(id);
                if (blog == null)
                {
                    res.Success = false;
                    res.Message = "Blog not found";
                    return res;
                }
                blog.IsDeleted = true; // Đánh dấu blog là đã xóa
                _blogRepo.Update(blog); // Cập nhật trạng thái

                res.Success = true;
                res.Data = true;
                res.Message = "Blog marked as deleted";
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Data = false;
                res.Message = $"Delete failed: {ex.Message}";
            }
            return res;
        }

        public async Task<ResFormat<ResBlogCreateDTO>> CreateBlog(BlogCreateDTO dto, int id)
        {
            var res = new ResFormat<ResBlogCreateDTO>();
            try
            {
                var blog = _mapper.Map<Blog>(dto);
                blog.CreatorId = id;
                blog.IsApproved = false; //Đảm bảo chưa được approved
                blog.IsDeleted = false; // Đảm bảo blog mới không bị ẩn
                blog.DateCreated = DateTime.Now;

                await _blogRepo.AddAsync(blog);
                var createdBlog = _mapper.Map<ResBlogCreateDTO>(blog);

                res.Success = true;
                res.Data = createdBlog;
                res.Message = "Blog created successfully";
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Create failed: {ex.Message}";
            }
            return res;
        }

        public async Task<ResFormat<ResBlogCreateDTO>> Update(BlogUpdateDTO blog, int id)
        {
            var res = new ResFormat<ResBlogCreateDTO>();

            try
            {
                var list = await _blogRepo.GetAllAsync();
                if(list.Any(a => a.BlogId == id && a.IsDeleted == false && a.IsApproved == true))
                {
                    var currentBlog = list.FirstOrDefault(a => a.BlogId == id);
                    currentBlog.BlogName = blog.BlogName;
                    currentBlog.ContentDescription = blog.ContentDescription;
                    _blogRepo.Update(currentBlog);
                    var blogUpdate = _mapper.Map<ResBlogCreateDTO>(currentBlog);
                    res.Success = true;
                    res.Data = blogUpdate;
                    res.Message = "Success";
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Message = "No blog with this Id";
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Update failed: {ex.Message}";
            }

            return res;
        }

        public async Task<ResFormat<bool>> ApproveBlog(int id)
        {
            var res = new ResFormat<bool>();
            try
            {

                var list = await _blogRepo.GetAllAsync();
                if (list.Any(a => a.BlogId == id && a.IsDeleted == false && a.IsApproved == false))
                {
                    var blog = list.FirstOrDefault(a => a.BlogId == id);
                    blog.IsApproved = true;
                    _blogRepo.Update(blog);
                    res.Success = true;
                    res.Data = true;
                    res.Message = "Success";
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Message = "No blog with this Id";
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Failed :{ex.Message}";
                return res;
            }
        }

        //public async Task<ResFormat<ResBlogCreateDTO>> CreateBlog(BlogCreateDTO dto, int creatorId)
        //{
        //    var res = new ResFormat<ResBlogCreateDTO>();
        //    try
        //    {
        //        var blog = _mapper.Map<Blog>(dto);
        //        blog.CreatorId = creatorId; // Gán người tạo từ token
        //        blog.IsDeleted = false;
        //        blog.IsApproved = false;

        //        await _blogRepo.AddAsync(blog);
        //        var createdBlog = _mapper.Map<ResBlogCreateDTO>(blog);

        //        res.Success = true;
        //        res.Data = createdBlog;
        //        res.Message = "Blog created successfully";
        //    }
        //    catch (Exception ex)
        //    {
        //        res.Success = false;
        //        res.Message = $"Create failed: {ex.Message}";
        //    }
        //    return res;
        //}


    }
}
