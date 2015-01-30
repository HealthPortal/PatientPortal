using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HealthPortal.Models;

namespace HealthPortal.Models
{
    public class LabsController : ApiController
    {
        ILabRepository repository = new LabsModel();
        // GET api/values
        [HttpGet]
        public IEnumerable<Lab> Get()
        {
            return repository.GetAll();
        }


        [HttpGet]
        public IEnumerable<Lab> Get(string PatientId)
        {
            IEnumerable<Lab> patdemo = repository.GebyUserId(PatientId);
            return patdemo;
        }

    }
}
