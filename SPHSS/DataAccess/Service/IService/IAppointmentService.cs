using BusinessObject;
using DataAccess.DTO.Req;
using DataAccess.DTO.Res;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service.IService
{
    public interface IAppointmentService
    {
        Task<Appointment> CreateAppointment(int studentId, int slotId, string date);
        Task<List<ResAppointmentCreateDTO>> GetAllAppointments();
        Task<ResAppointmentCreateDTO?> GetAppointmentById(int appointmentId);
        Task<bool> DeleteAppointment(int appointmentId);
        Task<ResAppointmentCreateDTO?> UpdateAppointment(int appointmentId, AppointmentUpdateDTO dto);
        Task<List<ResAppointmentCreateDTO>> GetAppointmentsByStudentId(int studentId);
        Task<List<ResAppointmentCreateDTO>> GetAppointmentsByPsychologistId(int psychologistId);
    }
}
