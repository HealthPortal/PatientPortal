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
    public class CarePlansController : ApiController
    {
        private static readonly ICarePlanRepository repository = new CarePlanModel();
        private static readonly IEncounterReport encrepo = new EncounterReportModel();
        // GET api/values
        //[HttpGet]
        //[Authorize(Roles="Patient")]
        //public IEnumerable<CarePlan> Get()
        //{
        //    return repository.GetAll();
        //}
        [HttpGet]
        public IEnumerable<CarePlan> Get()
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            IEnumerable<CarePlan> care = repository.GetByUserId(userId);
            return care;
        }

        // GET api/values/5


        [HttpPost]
        public CarePlan AddCareplan(CarePlan cplan)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            CarePlan care = repository.AddCareplan(cplan);
            EncounterReport erp = new EncounterReport();
            erp.RefId = care._id;
            erp.PatientId = care.UserId;
            erp.UpdateBy = userId;
            erp.Action = "Add new CarePlan";
            var encreports = encrepo.AddEncReport(erp);
            return care;
        }

        [HttpPut]
        public CarePlan UpdatePatient(CarePlan cplan)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            CarePlan care = repository.UpdatePatient(cplan);
            EncounterReport erp = new EncounterReport();
            erp.RefId = care._id;
            erp.PatientId = care.UserId;
            erp.UpdateBy = userId;
            erp.Action = "update CarePlan";
            var encreports = encrepo.AddEncReport(erp);
            return care;
        }
        public string userId { get; set; }
    }
}
