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
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

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

            string zoomMeetingLink = await CreateZoomMeeting(dto.ProgramName);

            var program = new Program
            {
                ProgramName = dto.ProgramName,
                DateStart = dateStart,
                DateEnd = dateEnd,
                SlotId = dto.SlotId,
                DateCreated = DateTime.Now,
                IsDeleted = false,
                GoogleMeetLink = zoomMeetingLink
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
                SlotId = program.SlotId,
                GoogleMeetLink = zoomMeetingLink
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
                    SlotId = p.SlotId,
                    GoogleMeetLink = p.GoogleMeetLink
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

        public async Task<List<ResProgramCreateDTO>> GetProgramByStudentId(int studentId)
        {
            var programs = await _context.ProgramSignups
                .Where(ps => ps.StudentId == studentId)
                .Join(_context.Programs,
                    ps => ps.ProgramId,
                    p => p.ProgramId,
                    (ps, p) => new { Program = p })
                .Where(p => p.Program.IsDeleted == false)
                .Select(p => new ResProgramCreateDTO
                {
                    ProgramId = p.Program.ProgramId,
                    ProgramName = p.Program.ProgramName,
                    DateStart = p.Program.DateStart,
                    DateEnd = p.Program.DateEnd,
                    DateCreated = p.Program.DateCreated,
                    IsDeleted = p.Program.IsDeleted,
                    SlotId = p.Program.SlotId
                })
                .ToListAsync();

            return programs;
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
            var student = await _context.Accounts.FirstOrDefaultAsync(a => a.AccId == studentId && a.IsActivated == true && a.IsApproved == true);
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

        private async Task<string> CreateZoomMeeting(string topic)
        {
            string zoomApiUrl = "https://api.zoom.us/v2/users/me/meetings";
            string accessToken = await GetZoomAccessToken(); 

            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var meetingData = new
            {
                topic = topic,
                type = 2,
                start_time = DateTime.UtcNow.AddMinutes(10).ToString("yyyy-MM-ddTHH:mm:ssZ"),
                duration = 60,
                timezone = "UTC",
                settings = new
                {
                    host_video = true,
                    participant_video = true,
                    join_before_host = false
                }
            };

            var response = await client.PostAsJsonAsync(zoomApiUrl, meetingData);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Lỗi khi tạo Zoom Meeting: {await response.Content.ReadAsStringAsync()}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(jsonResponse);
            return doc.RootElement.GetProperty("join_url").GetString();
        }

        private async Task<string> GetZoomAccessToken()
        {
            string clientId = "QVsE_mlrQ8GCJS_PxVtmcg";
            string clientSecret = "X90kcFaL64z7GJPhiA8HhMVRPF4Rpbi4";
            string accountId = "wrcDrlfCRBOQkSDB0VEoVw";

            using HttpClient client = new HttpClient();
            var authBytes = Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}");
            string authBase64 = Convert.ToBase64String(authBytes);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authBase64);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new FormUrlEncodedContent(new[]
            {
        new KeyValuePair<string, string>("grant_type", "account_credentials"),
        new KeyValuePair<string, string>("account_id", accountId)
    });

            Console.WriteLine("🔍 Sending request to Zoom OAuth...");
            Console.WriteLine($"🔹 Auth Header: Basic {authBase64}");
            Console.WriteLine($"🔹 Account ID: {accountId}");

            HttpResponseMessage response = await client.PostAsync("https://zoom.us/oauth/token", content);
            string responseContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"🔍 Response Code: {response.StatusCode}");
            Console.WriteLine($"🔹 Response Body: {responseContent}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"❌ Lỗi lấy access token: {responseContent}");
            }

            using var doc = JsonDocument.Parse(responseContent);
            return doc.RootElement.GetProperty("access_token").GetString();
        }

    }
}
