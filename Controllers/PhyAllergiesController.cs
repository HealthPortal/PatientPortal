using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HealthPortal.Models;

namespace HealthPortal.Models
{
    public class PhyAllergiesController : ApiController
    {
        IPhyAllergyRepository repository = new PhyAllergyModel();
        // GET api/values
        [HttpGet]
        public IEnumerable<Allergy> Get()
        {
            return repository.GetAll();
        }

        [HttpGet]
        public IEnumerable<Allergy> Get(string PatientId)
        {
            IEnumerable<Allergy> patdemo = repository.GebyUserId(PatientId);
            return patdemo;
        }

    }
}
