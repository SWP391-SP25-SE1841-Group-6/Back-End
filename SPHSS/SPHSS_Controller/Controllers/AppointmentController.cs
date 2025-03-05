﻿using DataAccess.DTO.Req;
using DataAccess.DTO.Res;
using DataAccess.Repo;
using DataAccess.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace SPHSS_Controller.Controllers
{
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAppointments()
        {
            try
            {
                var appointments = await _appointmentService.GetAllAppointments();
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetAppointmentById")]
        public async Task<IActionResult> GetAppointmentById(int appointmentId)
        {
            try
            {
                var appointment = await _appointmentService.GetAppointmentById(appointmentId);

                if (appointment == null)
                {
                    return NotFound(new { message = "Appointment not found!" });
                }

                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("CreateAppointment")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentCreateDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest(new { Message = "Dữ liệu không hợp lệ!" });
                }
                var appointment = await _appointmentService.CreateAppointment(dto.StudentID, dto.SlotID, dto.Date);
                return Ok(new
                {
                    Message = "Đặt lịch thành công!",
                    AppointmentId = appointment.AppointmentId,
                    StudentId = appointment.StudentId,
                    PsychologistId = appointment.PsychologistId,
                    SlotId = appointment.SlotId,
                    Date = appointment.Date.ToString("yyyy-MM-dd"),
                    DateCreated = appointment.DateCreated
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("DeleteAppointmentById")]
        public async Task<IActionResult> DeleteAppointment(int appointmentId)
        {
            var result = await _appointmentService.DeleteAppointment(appointmentId);

            if (!result)
                return NotFound(new { message = "Appointment not found!" });

            return Ok(new { message = "Appointment deleted successfully." });
        }

        [HttpPut("UpdateAppointmentById")]
        public async Task<IActionResult> UpdateAppointment(int appointmentId, [FromBody] AppointmentUpdateDTO dto)
        {
            try
            {
                var updatedAppointment = await _appointmentService.UpdateAppointment(appointmentId, dto);
                return Ok(updatedAppointment);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
