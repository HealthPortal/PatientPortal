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
    public class PhysiciansController : ApiController
    {

        private static readonly IPhysicianRepository _physicians = new PhysicianModel();
        private static readonly IEncounterReport encrepo = new EncounterReportModel();
       
        [HttpGet]
        public IEnumerable<MyPhysician> GetPatient()
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            IEnumerable<MyPhysician> phy = _physicians.GetByUserId(userId);
            return phy;
            //return _physicians.GetAll();
        }
        [HttpPost]
        public MyPhysician AddPatient(MyPhysician physician)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            MyPhysician phy = _physicians.AddPhy(physician);
            EncounterReport erp = new EncounterReport();
            erp.RefId = phy._id;
            erp.PatientId = phy.UserId;
            erp.UpdateBy = userId;
            erp.Action = "Add new MyPhysician";
            var encreports = encrepo.AddEncReport(erp);
            return phy;
        }

        [HttpPut]
        public MyPhysician UpdatePatient(MyPhysician phys)
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            MyPhysician myphys = _physicians.UpdatePatient(phys);
            EncounterReport erp = new EncounterReport();
            erp.RefId = myphys._id;
            erp.PatientId = myphys.UserId;
            erp.UpdateBy = userId;
            erp.Action = "update MyPhysician";
            var encreports = encrepo.AddEncReport(erp);
            return myphys;
        }

        public string userId { get; set; }
    }
}
