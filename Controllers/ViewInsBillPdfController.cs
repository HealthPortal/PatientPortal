using HealthPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealthPortal.Controllers
{
    public class ViewInsBillPdfController : Controller
    {
        private static readonly IInsuranceBillRepository _insbilling = new InsuranceBillRepository();
        //
        // GET: /ViewInsBillPdf/
        [HttpGet]
        public ActionResult InsBillPdf()
        {
            InsuranceBillingViewModel model = new InsuranceBillingViewModel();
            model.patientname = Session["UserName"].ToString();
            var userid = Session["UserId"].ToString();
            model.InsuranceBills = _insbilling.GetByUserId(userid);
            return PartialView(model);
        }

    }
}
