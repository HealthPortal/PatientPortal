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
    public class MedicationsController : ApiController
    {
        private static readonly IMedicationRepository repository = new MedicationModel();
        private static readonly IEncounterReport encrepo = new EncounterReportModel();
      
        // GET api/values
        //[HttpGet]
        //public IEnumerable<Medication> Get()
        //{
        //    return repository.GetAll();
        //}
        [HttpGet]
        public IEnumerable<Medication> Get()
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            IEnumerable<Medication> meds = repository.GetByUserId(userId);
            return meds;
        }

        // GET api/values/5


        [HttpPost]
        public Medication AddMed(Medication medication)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            Medication med = repository.AddMed(medication);
            EncounterReport erp = new EncounterReport();
            erp.RefId = med._id;
            erp.PatientId = med.UserId;
            erp.UpdateBy = userId;
            erp.Action = "Add new Medication";
            var encreports = encrepo.AddEncReport(erp);
            return med;
        }
        [HttpPut]
        public Medication UpdatePatient(Medication Medicat)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            Medication med = repository.UpdatePatient(Medicat);
            EncounterReport erp = new EncounterReport();
            erp.RefId = med._id;
            erp.PatientId = med.UserId;
            erp.UpdateBy = userId;
            erp.Action = "update Medication";
            var encreports = encrepo.AddEncReport(erp);
            return med;
        }
        public string userId { get; set; }
    }
}
