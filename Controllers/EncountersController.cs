using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HealthPortal.Models;

namespace HealthPortal.Models
{
    public class EncountersController : ApiController
    {
        IEncountersRepository repository = new EncountersModel();
        // GET api/values
        //[HttpGet]
        //public IEnumerable<Encounter> Get()
        //{
        //    return repository.GetAll();
        //}


        [HttpGet]
        public IEnumerable<Encounter> Get(string PatientId)
        {
            IEnumerable<Encounter> patdemo = repository.GebyUserId(PatientId);
            return patdemo;
        }



    }
}
