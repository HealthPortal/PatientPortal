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
    public class DiagnosisController : ApiController
    {
        IDiagnosisRepository repository = new DiagnosisModel();
        // GET api/values
        //[HttpGet]
        //public IEnumerable<Diagnosi> Get()
        //{
        //    var session = HttpContext.Current.Session;
        //    if (session["PatientId"] != null)
        //        patientId = session["PatientId"].ToString();
        //    IEnumerable<Diagnosi> diademo = repository.GebyUserId(patientId);
        //    return diademo;
        //}


        [HttpGet]
        public IEnumerable<Diagnosi> Get(string PatientId)
        {
            IEnumerable<Diagnosi> patdemo = repository.GebyUserId(PatientId);
            return patdemo;
        }

        public string patientId { get; set; }
    }
}
