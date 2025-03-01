using BusinessObject;
using DataAccess.Repo.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repo
{
    public class AppointmentRepo : IAppointmentRepo
    {
        private readonly SphssContext _context;

        public AppointmentRepo(SphssContext context)
        {
            _context = context;
        }

        public Appointment CreateAppointment(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
            return appointment;
        }

        //public List<Appointment> GetAppointmentsByDate(DateTime date)
        //{
        //    return _context.Appointments
        //    .Where(a => a.DateStart == date)
        //    .Include(a => a.Slots)
        //    .Include(a => a.Psychologist)
        //    .ToList();
        //}
    }
}
