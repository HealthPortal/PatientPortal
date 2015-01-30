using HealthPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealthPortal.Controllers
{
    public class VerificationController : Controller
    {
        //
        // GET: /Verification/

        public ActionResult Verify(string useradminid,string Useraccess)
        {
            if (Useraccess != null)
            { 
                using(WallEntities db=new WallEntities())
                {
                    AuthenticationUser authuser = db.AuthenticationUsers.Where(a => a.UserAccess == Useraccess && a.UserAdmin == useradminid).FirstOrDefault();
                    authuser.Verification = "Verify";
                    db.SaveChanges();
                }
            }

            return View();
        }

    }
}
