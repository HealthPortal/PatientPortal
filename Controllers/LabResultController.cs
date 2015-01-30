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
    public class LabResultController : ApiController
    {
        private static readonly ILabResult _labresults = new LabResultModel();
        private static readonly IEncounterReport encrepo = new EncounterReportModel();

        [HttpGet]
        public IEnumerable<LabResult> Get()
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            IEnumerable<LabResult> labs = _labresults.GetByUserId(userId);
            return labs;
        }

        [HttpPost]
        public LabResult AddLabRes(LabResult LabRes)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            LabResult labres = _labresults.AddLabRes(LabRes);
            EncounterReport erp = new EncounterReport();
            erp.RefId = labres._id;
            erp.PatientId = labres.UserId;
            erp.UpdateBy = userId;
            erp.Action = "Add new LabResult";
            var encreports = encrepo.AddEncReport(erp);
            return labres;
        }
        [HttpPut]
        public LabResult UpdatePatient(LabResult labresult)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            LabResult labsr = _labresults.UpdatePatient(labresult);
            EncounterReport erp = new EncounterReport();
            erp.RefId = labsr._id;
            erp.PatientId = labsr.UserId;
            erp.UpdateBy = userId;
            erp.Action = "update LabResult";
            var encreports = encrepo.AddEncReport(erp);
            return labsr;
        }

        public string userId { get; set; }
    }
}
