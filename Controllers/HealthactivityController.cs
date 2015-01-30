using HealthPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HealthPortal.Controllers
{
    public class HealthactivityController : Controller
    {
        //
        // GET: /Healthactivity/
        private static readonly IEncounterReport encrepo = new EncounterReportModel();
        private static readonly IMedicationRepository _meds = new MedicationModel();
        private static readonly ILabResult _labresults = new LabResultModel();
        private static readonly IAppointment _appointments = new AppointmentRepository();

        [HttpPost]
        public ActionResult HealthActs()
        {
            if (Session["UserId"] != null)
                userId = Session["UserId"].ToString();
            IEnumerable<EncounterReport> encs = encrepo.GetByUserId(userId).OrderByDescending(date => date._id).AsEnumerable();
            List<HealthActivityModel> healthmodels = new List<HealthActivityModel>();
            foreach (var enctrs in encs)
            {
                HealthActivityModel hmodel = new HealthActivityModel();
                if (enctrs.Action.Contains("Medication"))
                {
                    Medication med = _meds.GetById(enctrs.RefId);
                    hmodel.Medication = med;
                }
                else if (enctrs.Action.Contains("ReasonForHospitalization"))
                {
                    Appointment app = _appointments.GetById(enctrs.RefId);
                    Guid useid = new Guid(app.UserId);
                    MembershipUser pat = Membership.GetUser(useid);
                    AppointmentNew appnew = new AppointmentNew();
                    appnew._id = app._id;
                    appnew.Address = app.Address;
                    appnew.AppointmentsID = app.AppointmentsID;
                    appnew.CenterName = app.CenterName;
                    appnew.Reason = app.Reason;
                    appnew.Date = app.Date;
                    appnew.Time = app.Time;
                    appnew.EncounterType = app.EncounterType;
                    appnew.Physician = app.Physician;
                    appnew.Patient = pat.UserName;
                    appnew.UserId = app.UserId;

                    hmodel.AppointmentNew = appnew;
                }
                else if (enctrs.Action.Contains("LabResult"))
                {
                    LabResult lab = _labresults.GetById(enctrs.RefId);
                    hmodel.LabResult = lab;
                }
                //else if (enctrs.Action.Contains("Medication"))
                //{

                //}

                if (hmodel != null)
                {
                    healthmodels.Add(hmodel);
                }

            }
            return Json(healthmodels);
        }


        public string userId { get; set; }
    }
}
