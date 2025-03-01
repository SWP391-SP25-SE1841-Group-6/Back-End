using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repo.IRepo
{
    public interface IAppointmentRepo
    {
        Appointment CreateAppointment(Appointment appointment);
        //List<Appointment> GetAppointmentsByDate(DateTime date);
    }
}
