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
    public class PatientDemographicsController : ApiController
    {
        private static readonly IPatientMDRepository _patients = new PatientMDRepository();
        private static readonly IEncounterReport encrepo = new EncounterReportModel();
       
        //[HttpGet]
        //public IQueryable<PatientMD> Get()
        //{
        //    //List< PatientMD> model = new List< PatientMD>();
        //    //model = (from contact in _patients.GetAll()).ToList();
        //    //return model;
        //    return _patients.GetAll().AsQueryable();
        //}
        [HttpGet]
        public IEnumerable<PatientMD> GetPatient()
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                 userId = session["UserId"].ToString();
                // IEnumerable<PatientMD> patientmd = _patients.GetByUserId(id);
                //return patientmd;
                 IEnumerable<PatientMD> pmd = _patients.GetByUserId(userId);
                //if (pmd == null)
                //{
                //    throw new HttpResponseException(HttpStatusCode.NotFound);
                //}
                
            return pmd;
           // return _patients.GetAll();
        }
        [HttpPost]
        public PatientMD AddPatDemo(PatientMD patientmd)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            PatientMD patmd = _patients.AddPatDemo(patientmd);
            EncounterReport erp = new EncounterReport();
            erp.RefId = patmd._id;
            erp.PatientId = patmd.UserId;
            erp.UpdateBy = userId;
            erp.Action = "Add new PatientMD";
            var encreports = encrepo.AddEncReport(erp);
            return patmd;
        }

        [HttpPut]
        public PatientMD UpdatePatient(PatientMD patientmd)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            PatientMD patmd = _patients.UpdatePatient(patientmd);
            EncounterReport erp = new EncounterReport();
            erp.RefId = patmd._id;
            erp.PatientId = patmd.UserId;
            erp.UpdateBy = userId;
            erp.Action = "update PatientMD";
            var encreports = encrepo.AddEncReport(erp);
            return patmd;
        }
        public string userId { get; set; }
    }
}
