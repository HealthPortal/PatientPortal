using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HealthPortal.Models;
using System.Web;
using System.Globalization;

namespace HealthPortal.Models
{
    public class AllergiesController : ApiController
    {
        private static readonly IAllergyRepository _Allergies = new AllergyModel();
        private static readonly IEncounterReport encrepo = new EncounterReportModel();
      
        // GET api/values
        [HttpGet]
        public IEnumerable<MedicationAllergie> Get()
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            IEnumerable<MedicationAllergie> allergies = _Allergies.GetByUserId(userId);
            return allergies;

            //return repository.GetAll();
        }

        [HttpPost]
        public MedicationAllergie AddAllergie(MedicationAllergie medallerg)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            MedicationAllergie alg = _Allergies.AddAllergie(medallerg);
            EncounterReport erp = new EncounterReport();
            erp.RefId = alg._id;
            erp.PatientId = alg.UserId;
            erp.UpdateBy = userId;
            erp.Action = "Add new MedicationAllergie";
            var encreports = encrepo.AddEncReport(erp);
            return alg;
        }


        [HttpPut]
        public MedicationAllergie UpdatePatient(MedicationAllergie medallerg)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            MedicationAllergie alg = _Allergies.UpdatePatient(medallerg);
            EncounterReport erp = new EncounterReport();
            erp.RefId = alg._id;
            erp.PatientId = alg.UserId;
            erp.UpdateBy = userId;
            erp.Action = "update MedicationAllergie";
            var encreports = encrepo.AddEncReport(erp);
            return alg;
        }
        public string userId { get; set; }
    }
}
