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
    public class SocialHistoriesController : ApiController
    {
        private static readonly ISocialHistory _sochist = new SocialHistoryModel();
        private static readonly IEncounterReport encrepo = new EncounterReportModel();
       
        [HttpGet]
        public IEnumerable<SocialHistory> Get()
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            IEnumerable<SocialHistory> sochist = _sochist.GetByUserId(userId);
            return sochist;
        }

        [HttpPost]
        public SocialHistory AddHistory(SocialHistory SocialHistory)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            SocialHistory sochist = _sochist.AddHistory(SocialHistory);
            EncounterReport erp = new EncounterReport();
            erp.RefId = sochist._id;
            erp.PatientId = sochist.UserId;
            erp.UpdateBy = userId;
            erp.Action = "Add new SocialHistory";
            var encreports = encrepo.AddEncReport(erp);
            return sochist;
        }

        [HttpPut]
        public SocialHistory UpdatePatient(SocialHistory SocialHistory)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            SocialHistory sochist = _sochist.UpdatePatient(SocialHistory);
            EncounterReport erp = new EncounterReport();
            erp.RefId = sochist._id;
            erp.PatientId = sochist.UserId;
            erp.UpdateBy = userId;
            erp.Action = "update SocialHistory";
            var encreports = encrepo.AddEncReport(erp);
            return sochist;
        }

        public string userId { get; set; }
    }
}
