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
using Microsoft.Extensions.Configuration;

namespace DataAccess.Service
{
    public class ProgramService : IProgramService
    {
        private readonly SphssContext _context;
        private readonly IConfiguration _configuration;

        public ProgramService(SphssContext context, IConfiguration IConfiguration)
        {
            _context = context;
            _configuration = IConfiguration;
        }

        public async Task<ResProgramCreateDTO> CreateProgram(ProgramCreateDTO dto)
        {
            if (!DateOnly.TryParse(dto.Date, out DateOnly date))
            {
                throw new Exception("Ngày bắt đầu không hợp lệ! Định dạng đúng: yyyy-MM-dd");
            }

            // Kiểm tra ngày bắt đầu phải từ hôm nay trở đi
            if (date < DateOnly.FromDateTime(DateTime.Now))
            {
                throw new Exception("Ngày bắt đầu phải từ hôm nay trở đi!");
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
                DateCreated = DateTime.Now,
                Date = date,
                IsDeleted = false,
                SlotId = dto.SlotId,            
                GoogleMeetLink = zoomMeetingLink,
                Capacity = dto.Capacity,
                CurrentNumber = 0,
                PsychologistId = 2 //them code o day
            };
            _context.Programs.Add(program);
            await _context.SaveChangesAsync();

            return new ResProgramCreateDTO
            {
                ProgramId = program.ProgramId,
                ProgramName = program.ProgramName,
                Date = program.Date,
                DateCreated = program.DateCreated,
                IsDeleted = program.IsDeleted,
                SlotId = program.SlotId,
                GoogleMeetLink = zoomMeetingLink,
                Capacity = program.Capacity,
                PsychologistId = program.PsychologistId,
                CurrentNumber = program.CurrentNumber
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
                    Date = p.Date,
                    DateCreated = p.DateCreated,
                    IsDeleted = p.IsDeleted,
                    SlotId = p.SlotId,
                    GoogleMeetLink = p.GoogleMeetLink,
                    PsychologistId = p.PsychologistId,
                    Capacity = p.Capacity,
                    CurrentNumber = p.CurrentNumber
                })
                .ToListAsync();

            return programs;
        }

        public async Task<bool> DeleteProgram(int programId)
        {
            var program = await _context.Programs.FindAsync(programId);

            if (program == null)
            {
                return false; 
            }
            program.IsDeleted = true; 
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ResProgramCreateDTO?> UpdateProgram(int programId, ProgramUpdateDTO dto)
        {
            var program = await _context.Programs.FindAsync(programId);

            if (program == null || program.IsDeleted == true)
            {
                throw new Exception("Program đã bị xóa hoặc không tồn tại!");
            }

            if (!DateOnly.TryParse(dto.Date, out DateOnly date))
            {
                throw new Exception("Ngày bắt đầu không hợp lệ! Định dạng đúng: yyyy-MM-dd");
            }

            if (date < DateOnly.FromDateTime(DateTime.UtcNow))
            {
                throw new Exception("Ngày bắt đầu phải từ hôm nay trở đi!");
            }

            var slotExists = await _context.Slots.AnyAsync(s => s.SlotId == dto.SlotId);
            if (!slotExists)
            {
                throw new Exception("Slot không tồn tại!");
            }

            program.ProgramName = dto.ProgramName;
            program.Date = date;
            program.SlotId = dto.SlotId;
            program.Capacity = dto.Capacity;
            program.PsychologistId = program.PsychologistId;
            program.GoogleMeetLink = program.GoogleMeetLink;

            await _context.SaveChangesAsync();

            return new ResProgramCreateDTO
            {
                ProgramId = program.ProgramId,
                ProgramName = program.ProgramName,
                Date = program.Date,
                DateCreated = program.DateCreated,
                IsDeleted = program.IsDeleted,
                SlotId = program.SlotId,
                Capacity = program.Capacity,
                PsychologistId = program.PsychologistId,
                GoogleMeetLink = program.GoogleMeetLink,
                CurrentNumber = program.CurrentNumber
            };
        }

        public async Task<ResProgramCreateDTO?> GetProgramById(int programId)
        {
            var program = await _context.Programs
                .Where(p => p.ProgramId == programId && p.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (program == null)
            {
                return null; 
            }

            return new ResProgramCreateDTO
            {
                ProgramId = program.ProgramId,
                ProgramName = program.ProgramName,
                Date = program.Date,
                DateCreated = program.DateCreated,
                IsDeleted = program.IsDeleted,
                SlotId = program.SlotId,
                Capacity = program.Capacity,
                GoogleMeetLink = program.GoogleMeetLink,
                PsychologistId = program.PsychologistId,
                CurrentNumber = program.CurrentNumber
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
                    Date = p.Program.Date,
                    DateCreated = p.Program.DateCreated,
                    IsDeleted = p.Program.IsDeleted,
                    SlotId = p.Program.SlotId,
                    PsychologistId = p.Program.PsychologistId,
                    GoogleMeetLink = p.Program.GoogleMeetLink
                })
                .ToListAsync();

            return programs;
        }


        public async Task<ResProgramSignupDTO> RegisterProgram(int studentId, int programId)
        {
            var program = await _context.Programs
                .FirstOrDefaultAsync(p => p.ProgramId == programId && p.IsDeleted == false);
            if (program == null)
            {
                throw new Exception("Chương trình không tồn tại hoặc đã bị xóa!");
            }
            var student = await _context.Accounts.FirstOrDefaultAsync(a => a.AccId == studentId && a.IsActivated == true && a.IsApproved == true);
            if (student == null)
            {
                throw new Exception("Sinh viên không tồn tại!");
            }
            bool alreadyRegistered = await _context.ProgramSignups
                .AnyAsync(ps => ps.StudentId == studentId && ps.ProgramId == programId);
            if (alreadyRegistered)
            {
                throw new Exception("Sinh viên đã đăng ký chương trình này!");
            }
            var programSignup = new ProgramSignup
            {
                StudentId = studentId,
                ProgramId = programId,
                DateAdded = DateTime.Now
            };
            _context.ProgramSignups.Add(programSignup);
            await _context.SaveChangesAsync();

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
            string clientId = _configuration["ZoomService:ClientId"];
            string clientSecret = _configuration["ZoomService:ClientSecret"];
            string accountId = _configuration["ZoomService:AccountId"];
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
                throw new Exception($"Lỗi lấy access token: {responseContent}");
            }
            using var doc = JsonDocument.Parse(responseContent);
            return doc.RootElement.GetProperty("access_token").GetString();
        }

    }
}
