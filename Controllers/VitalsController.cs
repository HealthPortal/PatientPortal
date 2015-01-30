using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HealthPortal.Models;
using System.Web;

namespace HealthPortal.Models
{
    public class VitalsController : ApiController
    {
       private static readonly IVitalsRepository _Vitals = new VitalsModel();
       private static readonly IEncounterReport encrepo = new EncounterReportModel();
       
        // GET api/values
        [HttpGet]
        public IEnumerable<VitalSign> Get()
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            IEnumerable<VitalSign> vitals = _Vitals.GetByUserId(userId);
            return vitals;
           // return repository.GetAll();
        }

        
        [HttpPost]
        public VitalSign AddPatient(VitalSign vsign)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            VitalSign vitals = _Vitals.AddVitals(vsign);
            EncounterReport erp = new EncounterReport();
            erp.RefId = vitals._id;
            erp.PatientId = vitals.UserId;
            erp.UpdateBy = userId;
            erp.Action = "Add new Vitals";
            var encreports = encrepo.AddEncReport(erp);
            return vitals;
        }

        [HttpPut]
        public VitalSign UpdatePatient(VitalSign vsign)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            VitalSign vitals = _Vitals.UpdatePatient(vsign);
            EncounterReport erp = new EncounterReport();
            erp.RefId = vitals._id;
            erp.PatientId = vitals.UserId;
            erp.UpdateBy = userId;
            erp.Action = "update Vitals";
            var encreports = encrepo.AddEncReport(erp);
            return vitals;
        }
        public string userId { get; set; }
    }
}
