using HealthPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace HealthPortal.Controllers
{
    public class AppointmentController : ApiController
    {
        private static readonly IAppointment _appointments = new AppointmentRepository();
        private static readonly IEncounterReport encrepo = new EncounterReportModel();

        [HttpGet]
        public IEnumerable<AppointmentNew> Get(string PatientId)
        {
            WallEntities db = new WallEntities();
            IEnumerable<Appointment> app = _appointments.GetByUserId(PatientId);
            List<AppointmentNew> appoint = new List<AppointmentNew>();

            foreach (var a in app)
            {
                Guid userid = new Guid(a.UserId);
                MembershipUser pat = Membership.GetUser(userid);
                AppointmentNew appnew = new AppointmentNew();
                appnew._id = a._id;
                appnew.Address = a.Address;
                appnew.AppointmentsID = a.AppointmentsID;
                appnew.CenterName = a.CenterName;
                appnew.Reason = a.Reason;
                appnew.Date = a.Date;
                appnew.Time = a.Time;
                appnew.EncounterType = a.EncounterType;
                appnew.Physician = a.Physician;
                appnew.Patient = pat.UserName;
                appnew.UserId = a.UserId;
                appoint.Add(appnew);
            }

          //return appoint.AsEnumerable();
           IEnumerable<AppointmentNew> appointnew = appoint.AsEnumerable();
           return appointnew;
        }

        [HttpGet]
        public IEnumerable<Appointment> GetAppointment()
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();

            IEnumerable<Appointment> ins = _appointments.GetByUserId(userId);
            return ins;

        }

        [HttpPost]
        public Appointment AddAppointment(Appointment appoint)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            Appointment app = _appointments.AddAppointment(appoint);
            EncounterReport erp = new EncounterReport();
            erp.RefId = app._id;
            erp.PatientId = app.UserId;
            erp.UpdateBy = userId;
            erp.Action = "Add new ReasonForHospitalization";
            var encreports = encrepo.AddEncReport(erp);
            return app;
        }



        [HttpPut]
        public Appointment UpdateAppointment(Appointment appoint)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            Appointment appnt = _appointments.UpdateAppointment(appoint);
            EncounterReport erp = new EncounterReport();
            erp.RefId = appnt._id;
            erp.PatientId = appnt.UserId;
            erp.UpdateBy = userId;
            erp.Action = "update ReasonForHospitalization";
            var encreports = encrepo.AddEncReport(erp);
            return appnt;
        }

        [HttpDelete]
        public void DeleteAppointmentId(string id)
        {
            _appointments.DeleteAppointmentById(id);
        }

        public string userId { get; set; }
    }
}
