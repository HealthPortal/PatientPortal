using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using HealthPortal.Models;

namespace HealthPortal.Controllers
{
    public class PatientsController : ApiController
    {
         private static readonly IPatientRepository _Patients = new PatientRepository();
        [HttpGet]
         public IEnumerable<Patient> Get()
        {
            //var session = HttpContext.Current.Session;
            //if (session["UserId"] != null)
            //    userId = session["UserId"].ToString();
            //IEnumerable<Patient> patients = _Patients.GetByUserId(userId);
            //return patients;

            return _Patients.GetAll();
        }

       

        public string userId { get; set; }
    
    }
}
