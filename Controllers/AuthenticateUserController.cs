using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HealthPortal.Models;
using System.Web.Security;
using System.Net.Mail;
using System.Net;
namespace HealthPortal.Controllers
{
    public class AuthenticateUserController : Controller
    {
        private WallEntities db = new WallEntities();
        //
        // GET: /AuthenticateUser/
        [HttpPost]
        public ActionResult AuthenticateAccess(AuthenticationUser authmodel,string[] type)
        {
            if (authmodel != null && type != null)
            {
                if (Session["UserId"] != null)
                {
                    userId = Session["UserId"].ToString();
                }
                authmodel.UserAdmin = userId;
                //string emailid = authmodel.EmailId.ToString();
                string username = Membership.GetUserNameByEmail(authmodel.EmailId);
                MembershipUser user = Membership.GetUser(username);
                authmodel.UserAccess = user.ProviderUserKey.ToString();
                authmodel.UserAdminName = Session["UserName"].ToString();
                authmodel.Verification = "Unverify";
                foreach (var tp in type)
                {
                    if (authmodel.AccessType != null)
                    {
                        authmodel.AccessType = authmodel.AccessType + "," + tp.ToString();
                    }
                    else
                    {
                        authmodel.AccessType = tp.ToString();
                    }
                }
                if (authmodel.UserAccess != null)
                {
                    var usercheck = db.AuthenticationUsers.ToList().Last();
                    if (usercheck == null)
                    {
                        authmodel.Authuserid = 1;
                    }
                    else
                    {
                        authmodel.Authuserid = usercheck.Authuserid + 1;
                    }
                    var useraccss = authmodel.UserAccess;
                    var verifyurl = "http://localhost:56393/Verification/Verify?useradminid=" + authmodel.UserAdmin + "" + "&Useraccess=" + useraccss;
                    var unverifyurl = "http://localhost:56393/Verification/Verify?useradminid=" + authmodel.UserAdmin;
                    db.AuthenticationUsers.Add(authmodel);
                    db.SaveChanges();

                    ViewBag.Message = "Verify link sended to " + authmodel.EmailId;

                    string useradminname = Session["UserName"].ToString();
                    MembershipUser useradmin = Membership.GetUser(useradminname);
                    var from = useradmin.Email.ToString();
                    var to = authmodel.EmailId.ToString();
                    MailMessage mm = new MailMessage(from, to)
                    {
                        Subject = useradminname + " has granted you access to their Patient Portal account -- accept or deny?",
                        IsBodyHtml = true,
                        Body = "<html><head></head><body>" +  useradminname + "<" + from + ">" + " Has granted you" + "<" + to + ">" + " access to see their patient portal.\n" 
                        + "To accept this request click on link below:\n" 
                        + "<a href="+ verifyurl + ">Click here to accept access.</a>\n" 
                        + " To reject this request click on link below:\n"
                        + "<a href=" + unverifyurl + ">Click here to deny access.</a>\n"
                        + "</body></html>"
                    };

                    SmtpClient smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        Credentials = new NetworkCredential("myhealthcare26nov@gmail.com",
                                                            "pooja123456")
                    };
                    smtp.Send(mm);
                    ViewBag.Message = "Verification mail send successful";

                }
                else
                {
                    ViewBag.Message = "There is No user has this EmailId";

                }
            }
            else 
            {
                ViewBag.Message = "Please enter emailid";
            }
            return View(); 
        }
        
        [HttpGet]
        public JsonResult GetAccessUser()
        {
            GetAccessUsers accusers = new GetAccessUsers();
            if (Session["UserId"] != null)
            {
                userId = Session["UserId"].ToString();
            }
            List<AuthenticationUser> atuser=new List<AuthenticationUser>();
            atuser = accusers.GetAccUsers(userId);
            if (atuser != null)
            {
                return new JsonResult { Data = atuser, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                return new JsonResult { Data = null, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }


        [HttpDelete]
        public ActionResult RemoveAccessUser(string UserAccessId)
        {
            if (Session["UserId"] != null)
            {
                userId = Session["UserId"].ToString();
            }
            
            AuthenticationUser autuser = db.AuthenticationUsers.Where(a => a.UserAccess == UserAccessId && a.UserAdmin == userId).FirstOrDefault();
            if(autuser!=null)
            {
                db.AuthenticationUsers.Remove(autuser);
                db.SaveChanges();
             }
            else if (autuser == null)
            {
                ViewBag.Message = "User Not Found";
           
            }
           
            return View();
        }
        public string userId { get; set; }
    }
}
