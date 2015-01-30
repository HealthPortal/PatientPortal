using HealthPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace HealthPortal.Controllers
{
    public class EncounterDiagnosisController : ApiController
    {
        private static readonly IEncounterDiagnosis _EncDiagnosis = new EncounterDiagnosisModel();
        private static readonly IEncounterReport encrepo = new EncounterReportModel();
      
        [HttpPost]
        public EncounterDiagnosis AddEncDiag(EncounterDiagnosis EncDiag)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            EncounterDiagnosis encdiag = _EncDiagnosis.AddEncDiag(EncDiag);
            EncounterReport erp = new EncounterReport();
            erp.RefId = encdiag._id;
            erp.PatientId = encdiag.UserId;
            erp.UpdateBy = userId;
            erp.Action = "Add new EncounterDiagnosis";
            var encreports = encrepo.AddEncReport(erp);
            return encdiag;
        }

        [HttpPut]
        public EncounterDiagnosis UpdatePatient(EncounterDiagnosis Encdiag)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            EncounterDiagnosis encd = _EncDiagnosis.UpdatePatient(Encdiag);
            EncounterReport erp = new EncounterReport();
            erp.RefId = encd._id;
            erp.PatientId = encd.UserId;
            erp.UpdateBy = userId;
            erp.Action = "update EncounterDiagnosis";
            var encreports = encrepo.AddEncReport(erp);
            return encd;
        }

        public string userId { get; set; }
    }
}
