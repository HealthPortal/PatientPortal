using HealthPortal.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    public interface IAppointment
    {
        IEnumerable<Appointment> GetAll();
        Appointment GetById(string refid);
        IEnumerable<Appointment> GetByUserId(string UserId);
        void DeleteAppointmentById(string ID);
        void DeleteAppointment(string userid);

        Appointment AddAppointment(Appointment appoints);

        Appointment UpdateAppointment(Appointment appoints);

    }
}
