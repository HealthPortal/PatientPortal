using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HealthPortal.Models;

namespace HealthPortal.Models
{
    public class PhyProceduresController : ApiController
    {
        IPhyProceduresRepository repository = new PhyProceduresModel();
        // GET api/values
        [HttpGet]
        public IEnumerable<Procedure> Get()
        {
            return repository.GetAll();
        }
        [HttpGet]
        public IEnumerable<Procedure> Get(string PatientId)
        {
            IEnumerable<Procedure> patdemo = repository.GebyUserId(PatientId);
            return patdemo;
        }


    }
}
