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
    public class CognFunctStatusController : ApiController
    {
        private static readonly ICognitiveFunctionalStatus _cogfuncstatus = new CognitiveFunctionalModel();
        private static readonly IEncounterReport encrepo = new EncounterReportModel();
      
        [HttpPost]
        public CognitiveAndFunctionalStatus AddCogFunStat(CognitiveAndFunctionalStatus CogFunStat)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            CognitiveAndFunctionalStatus cogfunst= _cogfuncstatus.AddCogFunStat(CogFunStat);
            EncounterReport erp = new EncounterReport();
            erp.RefId = cogfunst._id;
            erp.PatientId = cogfunst.UserId;
            erp.UpdateBy = userId;
            erp.Action = "Add new CognitiveAndFunctionalStatus";
            var encreports = encrepo.AddEncReport(erp);
            return cogfunst;
        }

        [HttpPut]
        public CognitiveAndFunctionalStatus UpdatePatient(CognitiveAndFunctionalStatus cognfstat)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            CognitiveAndFunctionalStatus cogfs = _cogfuncstatus.UpdatePatient(cognfstat);
            EncounterReport erp = new EncounterReport();
            erp.RefId = cogfs._id;
            erp.PatientId = cogfs.UserId;
            erp.UpdateBy = userId;
            erp.Action = "update CognitiveAndFunctionalStatus";
            var encreports = encrepo.AddEncReport(erp);
            return cogfs;
        }

        public string userId { get; set; }
    }
}
