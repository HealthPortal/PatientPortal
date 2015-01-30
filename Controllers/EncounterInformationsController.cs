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
    public class EncounterInformationsController : ApiController
    {
        private static readonly IEncounterInformation _encountinfos = new EncounterInfoModel();
        private static readonly IEncounterReport encrepo = new EncounterReportModel();
      
        [HttpPost]
        public EncounterInformation AddEncInfo(EncounterInformation EncInfo)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            EncounterInformation encinf = _encountinfos.AddEncInfo(EncInfo);
            EncounterReport erp = new EncounterReport();
            erp.RefId = encinf._id;
            erp.PatientId = encinf.UserId;
            erp.UpdateBy = userId;
            erp.Action = "Add new EncounterInformation";
            var encreports = encrepo.AddEncReport(erp);
            return encinf;
        }

        [HttpPut]
        public EncounterInformation UpdatePatient(EncounterInformation encinfo)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            EncounterInformation enci = _encountinfos.UpdatePatient(encinfo);
            EncounterReport erp = new EncounterReport();
            erp.RefId = enci._id;
            erp.PatientId = enci.UserId;
            erp.UpdateBy = userId;
            erp.Action = "update EncounterInformation";
            var encreports = encrepo.AddEncReport(erp);
            return enci;
        }

        public string userId { get; set; }
    }
}
