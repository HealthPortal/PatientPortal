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
    public class ProceduresController : ApiController
    {
        private static readonly IProcedureRepository repository = new ProcedureModel();
        private static readonly IEncounterReport encrepo = new EncounterReportModel();
       
        // GET api/values
        [HttpGet]
        public IEnumerable<ProcedureDetail> Get()
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            IEnumerable<ProcedureDetail> procedure = repository.GetByUserId(userId);
            return procedure;
            //return repository.GetAll();
        }

        // GET api/values/5
        [HttpPost]
        public ProcedureDetail AddProcDetail(ProcedureDetail procdetail)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            ProcedureDetail procdet = repository.AddProcDetail(procdetail);
            EncounterReport erp = new EncounterReport();
            erp.RefId = procdet._id;
            erp.PatientId = procdet.UserId;
            erp.UpdateBy = userId;
            erp.Action = "Add new ProcedureDetail";
            var encreports = encrepo.AddEncReport(erp);
            return procdet;
        }

        [HttpPut]
        public ProcedureDetail UpdatePatient(ProcedureDetail procdetail)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            ProcedureDetail procdet = repository.UpdatePatient(procdetail);
            EncounterReport erp = new EncounterReport();
            erp.RefId = procdet._id;
            erp.PatientId = procdet.UserId;
            erp.UpdateBy = userId;
            erp.Action = "update ProcedureDetail";
            var encreports = encrepo.AddEncReport(erp);
            return procdet;
        }
        public string userId { get; set; }
    }
}
