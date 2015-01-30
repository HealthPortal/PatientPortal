using HealthPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace HealthPortal.Controllers
{
    public class InsuranceBillingController : ApiController
    {
        private static readonly IInsuranceBillRepository _insbilling = new InsuranceBillRepository();
        private static readonly IEncounterReport encrepo = new EncounterReportModel();

        [HttpGet]
        public IEnumerable<InsuranceBilling> GetInsBill()
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();

            IEnumerable<InsuranceBilling> ins = _insbilling.GetByUserId(userId);
            return ins;

        }

        [HttpPost]
        public InsuranceBilling AddInsBill(InsuranceBilling insbill)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            InsuranceBilling reashosp = _insbilling.AddInsBill(insbill);
            EncounterReport erp = new EncounterReport();
            erp.RefId = reashosp._id;
            erp.PatientId = reashosp.UserId;
            erp.UpdateBy = userId;
            erp.Action = "Add new ReasonForHospitalization";
            var encreports = encrepo.AddEncReport(erp);
            return reashosp;
        }



        [HttpPut]
        public InsuranceBilling UpdateInsBill(InsuranceBilling insbill)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            InsuranceBilling reasons = _insbilling.UpdateInsBill(insbill);
            EncounterReport erp = new EncounterReport();
            erp.RefId = reasons._id;
            erp.PatientId = reasons.UserId;
            erp.UpdateBy = userId;
            erp.Action = "update ReasonForHospitalization";
            var encreports = encrepo.AddEncReport(erp);
            return reasons;
        }

        public string userId { get; set; }
    }
}
