using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HealthPortal.Models;

namespace HealthPortal.Models
{
    public class DemographicsController : ApiController
    {
        IdemographicsRepository repository = new DemographicsModel();
        // GET api/values
        [HttpGet]
        public IEnumerable<Demographic> Get()
        {
            return repository.GetAll();
        }
        [HttpGet]
        public IEnumerable<Demographic> Get(string PatientId)
        {
            IEnumerable<Demographic> patdemo = repository.GebyUserId(PatientId);
            return patdemo;
        }

        [HttpPost]
        public Demographic AddDemo(Demographic demographic)
        {
            //var session = HttpContext.Current.Session;
            //if (session["NewPatient"] != null)
            //    physician.UserId = session["NewPatient"].ToString();
            Demographic demo = repository.AddDemo(demographic);
            return demo;
        }

    }
}
