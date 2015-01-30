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
    public class VaccinationsController : ApiController
    {
        private static readonly IVaccinationRepository repository = new VaccinationModel();
        private static readonly IEncounterReport encrepo = new EncounterReportModel();
       
        // GET api/values
        [HttpGet]
        public IEnumerable<Vaccination> Get()
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            IEnumerable<Vaccination> vaccine = repository.GetByUserId(userId);
            return vaccine;
            
            //return repository.GetAll();
        }

        // GET api/values/5

        [HttpPost]
        public Vaccination AddVaccine(Vaccination vaccine)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            Vaccination vacc =repository.AddVaccine(vaccine);
            EncounterReport erp = new EncounterReport();
            erp.RefId = vacc._id;
            erp.PatientId = vacc.UserId;
            erp.UpdateBy = userId;
            erp.Action = "Add new Vaccination";
            var encreports = encrepo.AddEncReport(erp);
            return vacc;
        }

        [HttpPut]
        public Vaccination UpdatePatient(Vaccination vaccine)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            Vaccination vacc = repository.UpdatePatient(vaccine);
            EncounterReport erp = new EncounterReport();
            erp.RefId = vacc._id;
            erp.PatientId = vacc.UserId;
            erp.UpdateBy = userId;
            erp.Action = "update Vaccination";
            var encreports = encrepo.AddEncReport(erp);
            return vacc;
        }
        public string userId { get; set; }
    }
}
