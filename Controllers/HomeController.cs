using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HealthPortal.Models;
using HealthPortal.Controllers;
using System.Web.Security;
using Newtonsoft.Json;

namespace HealthPortal.Models
{
    [HandleError]
    public class HomeController : Controller
    {
        //[CustomAuthorize]
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to My Health Care Site!";
            return RedirectToAction("LogIn", "Account");
            //return View();
        }
        //[CustomAuthorize]
        public ActionResult About()
        {
            return View();
        }

        //[CustomAuthorize]
        
        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            ViewBag.Message = "SelectOperation";
            return View();
        }

        //[CustomAuthorize]
        
        [Authorize(Roles = "Physician")]
        public ActionResult Physician()
        {
            var Data_session = new LogOnModel();
            //var Data_session = new SessionModel();
            try
            {
                if ((Object)Session["UserName"] != null)
                    Data_session.sess_val = Session["UserName"].ToString();
                else
                    return RedirectToAction("LogIn", "Account");
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
            return View(Data_session);

            //return View();
        }

        //[CustomAuthorize]
         
        [Authorize(Roles = "Patient")]
        public ActionResult Patient()
        {
            //if (Session["UserId"] == Session["UseradminId"]) 
            //{
            //    ViewBag.admin = "admin";
            //}
            //else
            //{
            //    ViewBag.admin = "user";
            //    ViewBag.adminid = Session["UseradminId"].ToString();
            //    ViewBag.adminname = Session["UserAdminName"].ToString();
            //}
            //if ((Object)Session["UserId"] != null)
            //{
            //    Session["UserId"] = Session["UserId"];
            //}
            var Data_session = new LogOnModel();
            try
            {
                if ((Object)Session["UserName"] != null)
                    Data_session.sess_val = Session["UserName"].ToString();

                else
                    return RedirectToAction("LogIn", "Account");
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
            return View(Data_session);

           
        }
        [Authorize(Roles = "Patient,Physician")]
        public ActionResult AccessModels()
        {
            if (Session["UserId"] != null)
            {
                useradminaccess = Session["UserId"].ToString();
                //ViewBag.Useradmin = useradminaccess;
                 ViewBag.Message = "Useradmin";
                 ViewBag.Username = Session["UserName"].ToString();
                 ViewBag.Userid = useradminaccess;
        
                //MembershipUser user = Membership.GetUser(useradminaccess);
                //string email = user.Email;
                //ViewBag.Username = Membership.GetUserNameByEmail(email);
                //ViewBag.Userid = useradminaccess;
        
            }
            //WallEntities db = new WallEntities();

            //List<AuthenticationUser> atuser = new List<AuthenticationUser>();
            //atuser = db.AuthenticationUsers.Where(a => a.UserAccess == useradminaccess).ToList();
            return View();
        }

        public JsonResult AccessModeluser()
        {
            GetAccessUsers accusers = new GetAccessUsers();
            
        if(Session["UserId"]!=null)
        {
            useradminaccess = Session["UserId"].ToString();

        }
        WallEntities db = new WallEntities();

        List<AuthenticationUser> atuser = new List<AuthenticationUser>();
        atuser = accusers.GetAccessAccept(useradminaccess);
        if (atuser != null)
        {
            return new JsonResult { Data = atuser, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        else
        {
            return new JsonResult { Data = null, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        } 

        }

        public ActionResult ShowAccessUser(string userid, string username)
        {
            if (Session["UserId"] != null && Session["UserName"] != null)
            {
                Session.Clear();
                Session["UserId"] = userid;
                Session["UserName"] = username;
            }
            return View();
        }

        //public ActionResult AddPatient(string newpatient)
        //{
        //    ViewBag.NewPatientid = newpatient;
        //    return RedirectToAction("AddPatientDetails", "Home");
        //}
         [Authorize(Roles = "Admin")]
        public ActionResult AddPatientDetails()
        {
            if (Session["NewPatientid"] != null)
            {
                ViewBag.NewPatientid = Session["NewPatientid"];
            }
            //else
            //{
            //    ViewBag.NewPatientid = Session["NewPatientid"];
            //}
            ViewBag.NewPatientid = ViewBag.NewPatientid;
            return View();
        }
        //public JsonResult ShowDropdownUser()
        //{
        //    GetAccessUsers accusers = new GetAccessUsers();

        //    if (Session["UseradminId"] != null)
        //    {
        //        useradminaccess = Session["UseradminId"].ToString();

        //    }
        //    WallEntities db = new WallEntities();

        //    List<AuthenticationUser> atuser = new List<AuthenticationUser>();
        //    atuser = accusers.GetAccessAccept(useradminaccess);
        //    if (atuser != null)
        //    {
        //        return new JsonResult { Data = atuser, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //    }
        //    else
        //    {
        //        return new JsonResult { Data = null, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //    } 

        //}
        public string useradminaccess { get; set; }
    }
}
