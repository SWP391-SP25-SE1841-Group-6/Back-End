using BusinessObject;
using DataAccess.DTO.Req;
using DataAccess.DTO.Res;
using DataAccess.Service.IService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class ProgramService : IProgramService
    {
        private readonly SphssContext _context;

        public ProgramService(SphssContext context)
        {
            _context = context;
        }

        public async Task<ResProgramCreateDTO> CreateProgram(ProgramCreateDTO dto)
        {
            if (!DateOnly.TryParse(dto.DateStart, out DateOnly dateStart))
            {
                throw new Exception("Ngày bắt đầu không hợp lệ! Định dạng đúng: yyyy-MM-dd");
            }

            // Kiểm tra ngày bắt đầu phải từ hôm nay trở đi
            if (dateStart < DateOnly.FromDateTime(DateTime.Now))
            {
                throw new Exception("Ngày bắt đầu phải từ hôm nay trở đi!");
            }

            DateOnly? dateEnd = null;
            if (!string.IsNullOrEmpty(dto.DateEnd))
            {
                if (!DateOnly.TryParse(dto.DateEnd, out DateOnly parsedDateEnd))
                {
                    throw new Exception("Ngày kết thúc không hợp lệ! Định dạng đúng: yyyy-MM-dd");
                }
                // Kiểm tra ngày bắt đầu phải trước ngày kết thúc
                if (dateStart > parsedDateEnd)
                {
                    throw new Exception("Ngày bắt đầu phải trước ngày kết thúc!");
                }
                dateEnd = parsedDateEnd;
            }

            var slotExists = await _context.Slots.AnyAsync(s => s.SlotId == dto.SlotId);
            if (!slotExists)
            {
                throw new Exception("Slot không tồn tại!");
            }

            var program = new Program
            {
                ProgramName = dto.ProgramName,
                DateStart = dateStart,
                DateEnd = dateEnd,
                SlotId = dto.SlotId,
                DateCreated = DateTime.Now,
                IsDeleted = false
            };

            _context.Programs.Add(program);
            await _context.SaveChangesAsync();

            return new ResProgramCreateDTO
            {
                ProgramId = program.ProgramId,
                ProgramName = program.ProgramName,
                DateStart = program.DateStart,
                DateEnd = program.DateEnd,
                DateCreated = program.DateCreated,
                IsDeleted = program.IsDeleted,
                SlotId = program.SlotId
            };
        }

        public async Task<List<ResProgramCreateDTO>> GetAllPrograms()
        {
            var programs = await _context.Programs
                .Where(p => p.IsDeleted == false)
                .Select(p => new ResProgramCreateDTO
                {
                    ProgramId = p.ProgramId,
                    ProgramName = p.ProgramName,
                    DateStart = p.DateStart,
                    DateEnd = p.DateEnd,
                    DateCreated = p.DateCreated,
                    IsDeleted = p.IsDeleted,
                    SlotId = p.SlotId
                })
                .ToListAsync();

            return programs;
        }

        public async Task<bool> DeleteProgram(int programId)
        {
            var program = await _context.Programs.FindAsync(programId);

            if (program == null)
            {
                return false; // Không tìm thấy chương trình
            }

            program.IsDeleted = true; // Chuyển trạng thái thành đã xóa
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ResProgramCreateDTO?> UpdateProgram(int programId, ProgramUpdateDTO dto)
        {
            var program = await _context.Programs.FindAsync(programId);

            if (program == null || program.IsDeleted == true)
            {
                return null; // Không tìm thấy hoặc đã bị xóa
            }

            // Kiểm tra DateStart hợp lệ
            if (!DateOnly.TryParse(dto.DateStart, out DateOnly dateStart))
            {
                throw new Exception("Ngày bắt đầu không hợp lệ! Định dạng đúng: yyyy-MM-dd");
            }

            // Kiểm tra DateEnd hợp lệ (nếu có)
            DateOnly? dateEnd = null;
            if (!string.IsNullOrEmpty(dto.DateEnd))
            {
                if (!DateOnly.TryParse(dto.DateEnd, out DateOnly parsedDateEnd))
                {
                    throw new Exception("Ngày kết thúc không hợp lệ! Định dạng đúng: yyyy-MM-dd");
                }
                dateEnd = parsedDateEnd;
            }

            // Kiểm tra ngày bắt đầu phải >= hôm nay
            if (dateStart < DateOnly.FromDateTime(DateTime.UtcNow))
            {
                throw new Exception("Ngày bắt đầu phải từ hôm nay trở đi!");
            }

            // Kiểm tra ngày bắt đầu phải <= ngày kết thúc (nếu có)
            if (dateEnd.HasValue && dateStart > dateEnd.Value)
            {
                throw new Exception("Ngày bắt đầu phải trước hoặc bằng ngày kết thúc!");
            }

            // Kiểm tra SlotId có tồn tại không
            var slotExists = await _context.Slots.AnyAsync(s => s.SlotId == dto.SlotId);
            if (!slotExists)
            {
                throw new Exception("Slot không tồn tại!");
            }

            // Cập nhật thông tin
            program.ProgramName = dto.ProgramName;
            program.DateStart = dateStart;
            program.DateEnd = dateEnd;
            program.SlotId = dto.SlotId;

            await _context.SaveChangesAsync();

            return new ResProgramCreateDTO
            {
                ProgramId = program.ProgramId,
                ProgramName = program.ProgramName,
                DateStart = program.DateStart,
                DateEnd = program.DateEnd,
                DateCreated = program.DateCreated,
                IsDeleted = program.IsDeleted,
                SlotId = program.SlotId
            };
        }

        public async Task<ResProgramCreateDTO?> GetProgramById(int programId)
        {
            var program = await _context.Programs
                .Where(p => p.ProgramId == programId && p.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (program == null)
            {
                return null; // Không tìm thấy hoặc đã bị xóa
            }

            return new ResProgramCreateDTO
            {
                ProgramId = program.ProgramId,
                ProgramName = program.ProgramName,
                DateStart = program.DateStart,
                DateEnd = program.DateEnd,
                DateCreated = program.DateCreated,
                IsDeleted = program.IsDeleted,
                SlotId = program.SlotId
            };
        }

        public async Task<ResProgramSignupDTO> RegisterProgram(int studentId, int programId)
        {
            // Kiểm tra xem chương trình có tồn tại và chưa bị xóa không
            var program = await _context.Programs
                .FirstOrDefaultAsync(p => p.ProgramId == programId && p.IsDeleted == false);

            if (program == null)
            {
                throw new Exception("Chương trình không tồn tại hoặc đã bị xóa!");
            }

            // Kiểm tra xem sinh viên có tồn tại không
            var student = await _context.Accounts.FirstOrDefaultAsync(a => a.AccId == studentId);
            if (student == null)
            {
                throw new Exception("Sinh viên không tồn tại!");
            }

            // Kiểm tra xem đã đăng ký chương trình này chưa
            bool alreadyRegistered = await _context.ProgramSignups
                .AnyAsync(ps => ps.StudentId == studentId && ps.ProgramId == programId);
            if (alreadyRegistered)
            {
                throw new Exception("Sinh viên đã đăng ký chương trình này!");
            }

            // Đăng ký chương trình
            var programSignup = new ProgramSignup
            {
                StudentId = studentId,
                ProgramId = programId,
                DateAdded = DateTime.Now
            };

            _context.ProgramSignups.Add(programSignup);
            await _context.SaveChangesAsync();

            // Trả về thông tin đăng ký theo ResProgramSignupDTO
            return new ResProgramSignupDTO
            {
                StudentId = studentId,
                ProgramId = programId,
                ProgramName = program.ProgramName,
                DateAdded = programSignup.DateAdded
            };
        }


    }
}
