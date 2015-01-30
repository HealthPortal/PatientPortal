using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HealthPortal.Models;

namespace HealthPortal.Models
{
    public class PatientDetailsController : ApiController
    {
        IPatientdetailRepository repository = new PatientDetailsModel();

        [HttpGet]
        public IEnumerable<PatientDetail> Get(string PatientId)
        {
            IEnumerable<PatientDetail> patdemo = repository.GebyUserId(PatientId);
            return patdemo;
        }

    }
}
