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
    public class ReasonForHospitalController : ApiController
    {
        private static readonly IReasonhospitalizaion _reasonhospital = new ReasonHospitalModel();
        private static readonly IEncounterReport encrepo = new EncounterReportModel();
       
        [HttpPost]
        public ReasonForHospitalization AddReasHosp(ReasonForHospitalization ReasHospt)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            ReasonForHospitalization reashosp = _reasonhospital.AddReasHosp(ReasHospt);
            EncounterReport erp = new EncounterReport();
            erp.RefId = reashosp._id;
            erp.PatientId = reashosp.UserId;
            erp.UpdateBy = userId;
            erp.Action = "Add new ReasonForHospitalization";
            var encreports = encrepo.AddEncReport(erp);
            return reashosp;
        }


        [HttpPut]
        public ReasonForHospitalization UpdateReason(ReasonForHospitalization Reason)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            ReasonForHospitalization reasons = _reasonhospital.UpdateReason(Reason);
            EncounterReport erp = new EncounterReport();
            erp.RefId = reasons._id;
            erp.PatientId = reasons.UserId;
            erp.UpdateBy = userId;
            erp.Action = "update ReasonForHospitalization";
            var encreports = encrepo.AddEncReport(erp);
            return reasons;
        }

        public string userId { get; set; }
    }
}
