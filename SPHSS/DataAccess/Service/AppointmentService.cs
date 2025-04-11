using BusinessObject;
using BusinessObject.Enum;
using DataAccess.DTO.Req;
using DataAccess.DTO.Res;
using DataAccess.Repo.IRepo;
using DataAccess.Service.IService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccess.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepo _appointmentRepo;
        private readonly SphssContext _context;
        private readonly IConfiguration _configuration;

        public AppointmentService(IAppointmentRepo appointmentRepo, SphssContext context, IConfiguration configuration)
        {
            _appointmentRepo = appointmentRepo;
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<ResAppointmentCreateDTO>> GetAllAppointments()
        {
            var appointments = await _context.Appointments
                .Where(ap => !ap.IsDeleted)
                .Select(ap => new ResAppointmentCreateDTO
                {
                    AppointmentId = ap.AppointmentId,
                    StudentId = ap.StudentId,
                    PsychologistId = ap.PsychologistId,
                    SlotId = ap.SlotId,
                    Date = ap.Date.ToString("yyyy-MM-dd"),
                    DateCreated = ap.DateCreated,
                    GoogleMeetLink = ap.GoogleMeetLink
                })
                .ToListAsync();
            return appointments;
        }

        public async Task<List<ResAppointmentCreateDTO>> GetAppointmentsByStudentId(int studentId)
        {
            var appointments = await _context.Appointments
                .Where(ap => !ap.IsDeleted && ap.StudentId == studentId)
                .Select(ap => new ResAppointmentCreateDTO
                {
                    AppointmentId = ap.AppointmentId,
                    StudentId = ap.StudentId,
                    PsychologistId = ap.PsychologistId,
                    SlotId = ap.SlotId,
                    Date = ap.Date.ToString("yyyy-MM-dd"),
                    DateCreated = ap.DateCreated,
                    GoogleMeetLink = ap.GoogleMeetLink
                })
                .ToListAsync();
            return appointments;
        }

        public async Task<List<ResAppointmentCreateDTO>> GetAppointmentsByPsychologistId(int psychologistId)
        {
            var appointments = await _context.Appointments
                .Where(ap => !ap.IsDeleted && ap.PsychologistId == psychologistId)
                .Select(ap => new ResAppointmentCreateDTO
                {
                    AppointmentId = ap.AppointmentId,
                    StudentId = ap.StudentId,
                    PsychologistId = ap.PsychologistId,
                    SlotId = ap.SlotId,
                    Date = ap.Date.ToString("yyyy-MM-dd"),
                    DateCreated = ap.DateCreated,
                    GoogleMeetLink = ap.GoogleMeetLink
                })
                .ToListAsync();
            return appointments;
        }

        public async Task<ResAppointmentCreateDTO?> GetAppointmentById(int appointmentId)
        {
            var appointment = await _context.Appointments
                .Where(ap => ap.AppointmentId == appointmentId && !ap.IsDeleted)
                .Select(ap => new ResAppointmentCreateDTO
                {
                    AppointmentId = ap.AppointmentId,
                    StudentId = ap.StudentId,
                    PsychologistId = ap.PsychologistId,
                    SlotId = ap.SlotId,
                    Date = ap.Date.ToString("yyyy-MM-dd"),
                    DateCreated = ap.DateCreated,
                    GoogleMeetLink = ap.GoogleMeetLink
                })
                .FirstOrDefaultAsync();
            return appointment;
        }

        public async Task<Appointment> CreateAppointment(int studentId, int slotId, string dateString)
        {
            if (!DateOnly.TryParse(dateString, out DateOnly date))
            {
                throw new Exception("Ngày không hợp lệ! Định dạng đúng: yyyy-MM-dd");
            }
            if (date <= DateOnly.FromDateTime(DateTime.Now))
            {
                throw new Exception("Ngày phải từ ngày mai trở đi!");
            }

            var availablePsychologists = await _context.Accounts
                .Where(a => a.Role == RoleEnum.Psychologist && a.IsActivated == true && a.IsApproved == true)
                .Where(a => 
                !_context.Appointments.Any(ap => ap.PsychologistId == a.AccId && ap.IsDeleted == true && ap.SlotId == slotId && ap.Date == date) && 
                !_context.Programs.Any(pr => pr.PsychologistId == a.AccId && pr.IsDeleted == true && pr.SlotId == slotId && pr.Date == date)) 
                .ToListAsync();
            if (!availablePsychologists.Any())
            {
                throw new Exception("Không còn psychologist trống trong slot này!");
            }
            var selectedPsychologist = availablePsychologists
                .OrderBy(p => _context.Appointments.Count(ap => ap.PsychologistId == p.AccId))
                .ThenBy(p => p.AccId)
                .First();

            string zoomMeetingLink = await CreateZoomMeeting();
            var appointment = new Appointment
            {
                StudentId = studentId,
                PsychologistId = selectedPsychologist.AccId,
                SlotId = slotId,
                Date = date,
                DateCreated = DateTime.Now,
                GoogleMeetLink = zoomMeetingLink
            };
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        private async Task<string> CreateZoomMeeting()
        {
            string zoomApiUrl = "https://api.zoom.us/v2/users/me/meetings";
            string accessToken = await GetZoomAccessToken();
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var meetingData = new
            {             
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
                throw new Exception($"❌ Lỗi lấy access token: {responseContent}");
            }
            using var doc = JsonDocument.Parse(responseContent);
            return doc.RootElement.GetProperty("access_token").GetString();
        }

        public async Task<bool> DeleteAppointment(int appointmentId)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(ap => ap.AppointmentId == appointmentId);
            if (appointment == null)
                return false;
            appointment.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ResAppointmentCreateDTO?> UpdateAppointment(int appointmentId, AppointmentUpdateDTO dto)
        {
            if (!DateOnly.TryParse(dto.Date, out DateOnly newDate))
            {
                throw new Exception("Ngày không hợp lệ! Định dạng đúng: yyyy-MM-dd");
            }
            var appointment = await _context.Appointments
                .Where(ap => ap.AppointmentId == appointmentId && ap.IsDeleted == false)
                .FirstOrDefaultAsync();
            if (appointment == null)
            {
                throw new Exception("Appointment không tồn tại!");
            }
            var availablePsychologist = await _context.Accounts
                .Where(a => a.Role == RoleEnum.Psychologist && a.IsActivated == true && a.IsApproved == true)
            .Where(a => 
                !_context.Appointments.Any(ap => ap.PsychologistId == a.AccId && ap.SlotId == dto.SlotId && ap.Date == newDate) && 
                !_context.Programs.Any(pr => pr.PsychologistId == a.AccId && pr.SlotId == dto.SlotId && pr.Date == newDate)) 
                .OrderBy(a => _context.Appointments.Count(ap => ap.PsychologistId == a.AccId))
                .ThenBy(a => a.AccId) 
                .FirstOrDefaultAsync();
            if (availablePsychologist == null)
            {
                throw new Exception("Không có psychologist trống trong ngày và slot này!");
            }
            appointment.Date = newDate;
            appointment.SlotId = dto.SlotId;
            appointment.PsychologistId = availablePsychologist.AccId;
            await _context.SaveChangesAsync();
            return new ResAppointmentCreateDTO
            {
                AppointmentId = appointment.AppointmentId,
                StudentId = appointment.StudentId,
                PsychologistId = appointment.PsychologistId,
                SlotId = appointment.SlotId,
                Date = appointment.Date.ToString("yyyy-MM-dd"),
                DateCreated = appointment.DateCreated,
                GoogleMeetLink = appointment.GoogleMeetLink
            };
        }
    }
}
