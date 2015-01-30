using HealthPortal.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HealthPortal.Controllers
{
    public class ViewReportController : Controller
    {
        //
        // GET: /ViewReport/
         [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            WallEntities wb = new WallEntities();
            //users from usersinrole patient....
            IEnumerable<aspnet_UsersInRoles> users = wb.aspnet_UsersInRoles.Where(p => p.aspnet_Roles.RoleName == "Patient");
            var musers = new List<MembershipUser>();
            foreach (var u in users)
            {
                MembershipUser pat = Membership.GetUser(u.UserId);
                musers.Add(pat);
            }
            List<ExistingPatient> patients = new List<ExistingPatient>();
            foreach (var u in musers)
            {
                if (u != null)
                {
                    var userId = u.ProviderUserKey.ToString();
                    patients.Add(new ExistingPatient()
                    {
                        UserId = userId,
                        PatientName = u.UserName,
                    });
                }
            }
            return View(patients);
        }


         public ActionResult EncounterReport(string patientid, DateTime FromDate, DateTime ToDate)
         {
             
             IEncounterReport encrepo = new EncounterReportModel();
             IEnumerable<EncounterReport> encrepos = encrepo.GetAll().Where(e => e.PatientId == patientid).AsEnumerable();

             IPatientMDRepository _patients = new PatientMDRepository();
             IEnumerable<PatientMD> patient = _patients.GetByUserId(patientid);
             var pat = patient.FirstOrDefault();
            List<EncounterReportViewModel> logs=new List<EncounterReportViewModel>();
             
             foreach (var er in encrepos)
             {
                 DateTime date = er.Encounterdate;
                 if (FromDate <= date && date <= ToDate)
                 {
                     //EncounterReportViewModel log = new EncounterReportViewModel();
                     Guid userid = new Guid(er.UpdateBy);
                     MembershipUser upuser = Membership.GetUser(userid);
                     try
                     {
                         logs.Add(new EncounterReportViewModel()
                         {
                             PatientName = pat.PatientName,
                             Date = date.ToString(),
                             ActionResults = er.Action,
                             UpdatedBy = upuser.UserName
                         });
                     }
                     catch(Exception ex)
                     {

                     }
                     //logs.Add(log);
                 }
                 
             }
           
             return Json(logs);
         }

         public ActionResult MessageReport(string PatientId, DateTime FromDate, DateTime ToDate)
         {

             var totalwithin = 0;
             var totaloutside = 0;
             IEncounterReport encrepo = new EncounterReportModel();
             IEnumerable<EncounterReport> encrepos = encrepo.GetAll().Where(e => e.PatientId == PatientId).AsEnumerable();

             IPatientMDRepository _patients = new PatientMDRepository();
             IEnumerable<PatientMD> patient = _patients.GetByUserId(PatientId);
             var pat = patient.FirstOrDefault();
             foreach (var er in encrepos)
             {
                 DateTime date = er.Encounterdate;
                 if (FromDate <= date && date <= ToDate)
                 {
                     totalwithin++;
                 }
                 else
                 {
                     totaloutside++;
                 }
             }
             var numwithin = 0;
             var numoutside = 0;
             IMessageReport msgrepo = new MessageReportModel();
             IEnumerable<MessageReport> msgrepos = msgrepo.GetAll().Where(e => e.SentTo == PatientId).AsEnumerable();
             foreach (var mr in msgrepos)
             {
                 DateTime dt = mr.Messagedate;
              
                 if(FromDate<=dt && dt<=ToDate)
                 {
                     numwithin++;
                 }
                 else
                 {
                     numoutside++;
                 }
                
             }

             var json = new MessageReportViewModel
             {
                 PatientName =pat.PatientName,
                 DOB= pat.DateOfBirth,
                 TotalOutside = totaloutside,
                 TotalWithin = totalwithin,
                 Gender =pat.Sex,
                 NumWithin = numwithin,
                 NumOutside = numoutside
             };
             return Json(json);
         }

         public ActionResult ViewDownloadTReport(string PatientId, DateTime FromDate, DateTime ToDate)
         {

             var totalwithin = 0;
             var totaloutside = 0;
             IEncounterReport encrepo = new EncounterReportModel();
             IEnumerable<EncounterReport> encrepos = encrepo.GetAll().Where(e => e.PatientId == PatientId).AsEnumerable();

             IPatientMDRepository _patients = new PatientMDRepository();
             IEnumerable<PatientMD> patient = _patients.GetByUserId(PatientId);
             var pat = patient.FirstOrDefault();
             foreach (var er in encrepos)
             {
                 DateTime date = er.Encounterdate;
                 if (FromDate <= date && date <= ToDate)
                 {
                     totalwithin++;
                 }
                 else
                 {
                     totaloutside++;
                 }
             }
             var numwithin = 0;
             var numoutside = 0;
             IEnumerable<EncounterReport> msgrepos = encrepos.Where(mr => mr.Action == "View Report" || mr.Action == "Download Report" || mr.Action == "Transmit Report").AsEnumerable();
             foreach (var mr in msgrepos)
             {
                 DateTime dt = mr.Encounterdate;

                 if (FromDate <= dt && dt <= ToDate)
                 {
                     numwithin++;
                 }
                 else
                 {
                     numoutside++;
                 }

             }

             var json = new MessageReportViewModel
             {
                 PatientName = pat.PatientName,
                 DOB = pat.DateOfBirth,
                 TotalOutside = totaloutside,
                 TotalWithin = totalwithin,
                 Gender = pat.Sex,
                 NumWithin = numwithin,
                 NumOutside = numoutside
             };
             return Json(json);
         }
    }
}
