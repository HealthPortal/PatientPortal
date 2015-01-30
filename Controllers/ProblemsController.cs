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
    public class ProblemsController : ApiController
    {
        private static readonly IProblemRepository _problems = new ProblemModel();
        private static readonly IEncounterReport encrepo = new EncounterReportModel();
       
        // GET api/values
        [HttpGet]
        public IEnumerable<Problem> Get()
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            IEnumerable<Problem> prob = _problems.GetByUserId(userId);
            return prob;
            //return repository.GetAll();
        }

        // GET api/values/5

        [HttpPost]
        public Problem AddProb(Problem prob)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
           Problem problem = _problems.AddProb(prob);
           EncounterReport erp = new EncounterReport();
           erp.RefId = problem._id;
           erp.PatientId = problem.UserId;
           erp.UpdateBy = userId;
           erp.Action = "Add new Problem";
           var encreports = encrepo.AddEncReport(erp);
            return problem;
        }

        [HttpPut]
        public Problem UpdatePatient(Problem prob)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            Problem problem = _problems.UpdatePatient(prob);
            EncounterReport erp = new EncounterReport();
            erp.RefId = problem._id;
            erp.PatientId = problem.UserId;
            erp.UpdateBy = userId;
            erp.Action = "update Problem";
            var encreports = encrepo.AddEncReport(erp);
            return problem;
        }
        public string userId { get; set; }
    }
}
