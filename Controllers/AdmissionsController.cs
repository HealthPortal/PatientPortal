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
    public class AdmissionsController : ApiController
    {
        IAdmissionsRepository repository = new AdmissionModel();
        // GET api/values
        //[HttpGet]
        //public IEnumerable<Admission> Get()
        //{
        //    var session = HttpContext.Current.Session;
        //    if (session["PatientId"] != null)
        //        patientId = session["PatientId"].ToString();
        //    IEnumerable<Admission> patdemo = repository.GebyUserId(patientId);
        //    return patdemo;
        //}


        [HttpGet]
        public IEnumerable<Admission> Get(string PatientId)
        {
            IEnumerable<Admission> patdemo = repository.GebyUserId(PatientId);
            return patdemo;
        }

        //[HttpGet]
        //public IEnumerable<Admission> Get()
        //{
        //    return repository.GetAll();
        //}
        //[HttpGet]
        //public Admission GetbyId(int id)
        //{
        //    Admission adm = repository.GetbyId(id);
        //    return adm;
        //}


        public string patientId { get; set; }
    }
}
