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
    public class ImmunizationsController : ApiController
    {
        private static readonly IImmunizationsRepository repository = new ImmunizationModel();
        private static readonly IEncounterReport encrepo = new EncounterReportModel();
       
        // GET api/values
        [HttpGet]
        public IEnumerable<Immunization> Get()
        {
            return repository.GetAll();
        }


        [HttpGet]
        public IEnumerable<Immunization> Get(string PatientId)
        {
            IEnumerable<Immunization> immu = repository.GetbyUserId(PatientId);
            return immu;
        }

        [HttpPost]
        public Immunization AddImmu(Immunization Immuni)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            Immunization immu = repository.AddImmu(Immuni);
            EncounterReport erp = new EncounterReport();
            erp.RefId = immu._id;
            erp.PatientId = immu.PatientId;
            erp.UpdateBy = userId;
            erp.Action = "Add new Immunization";
            var encreports = encrepo.AddEncReport(erp);
            return immu;
        }

        [HttpPut]
        public Immunization UpdatePatient(Immunization Immuni)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            Immunization immu = repository.UpdatePatient(Immuni);
            EncounterReport erp = new EncounterReport();
            erp.RefId = immu._id;
            erp.PatientId = immu.PatientId;
            erp.UpdateBy = userId;
            erp.Action = "update Immunization";
            var encreports = encrepo.AddEncReport(erp);
            return immu;
        }

        public string userId { get; set; }
    }
}
