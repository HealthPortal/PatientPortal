using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using HealthPortal.Models;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
using HealthPortal.Controllers;

namespace HealthPortal.Models
{
        [HandleError]
        public class AccountController : Controller
        {

            public IFormsAuthenticationService FormsService { get; set; }
            public IMembershipService MembershipService { get; set; }

            protected override void Initialize(RequestContext requestContext)
            {
                if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
                if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

                base.Initialize(requestContext);
            }

            // **************************************
            // URL: /Account/LogIn
            // **************************************

            public ActionResult LogIn()
            {
                return View();
            }

            [HttpPost]
            public ActionResult LogIn(LogOnModel model)
            {
                if (ModelState.IsValid)
                {
                    if (MembershipService.ValidateUser(model.UserName, model.Password))
                    {
                        
                        // webapi.Request.Headers.Add('TokenAuthentication','');
                        FormsService.SignIn(model.UserName, model.RememberMe);
                        if (ModelState.IsValid)
                        {
                            MembershipUser user = Membership.GetUser(model.UserName);
                            //HttpContext.Current.Session = model.UserName;
                            string UserId = user.ProviderUserKey.ToString();
                            Session["UserId"] = UserId;
                            Session["UserName"] = model.UserName;
                            //Session["UseradminId"] = UserId;
                            //Session["UserAdminName"] = model.UserName;
                           // Session.RemoveAll();
                            if( Session["UserId"]==null || Session["UserName"]==null)
                            {
                                //FormsService.SignOut();
                                //return RedirectToAction("Index", "Home");
                                return RedirectToAction("LogOff","Account");
                            }
                           var roles = Roles.GetRolesForUser(model.UserName);
                            if(roles.Contains("Admin"))
                            {
                                return RedirectToAction("Admin","Home");
                            }
                            else if(roles.Contains("Patient"))
                            {
                                return RedirectToAction("AccessModels", "Home");
                            }
                            else if(roles.Contains("Physician"))
                            {
                                return RedirectToAction("Physician","Home");
                            }
                           // return View();
                        }
                        else
                        {
                            Session["UserName"] = model.UserName;
                            return RedirectToAction("Index", "Home");
                        }
            
                    }
                    else
                    {
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    }
                }

                // If we got this far, something failed, redisplay form
                return View(model);
            }

            // **************************************
            // URL: /Account/LogOff
            // **************************************
    
            public ActionResult LogOff()
            {
                
               // Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Session.RemoveAll();
                FormsService.SignOut();

                return RedirectToAction("Index", "Home");
            }

            // **************************************
            // URL: /Account/Register
            // **************************************
            //[Authorize(Roles="Admin")]
            public ActionResult Register()
            {
               // ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
                return View();
            }

            public ActionResult Confirmation()
            {
                return View();
            }

            public ActionResult Welcome()
            {
                return View();
            }


            public ActionResult Verify(string ID)
            {
                if (string.IsNullOrEmpty(ID) || (!Regex.IsMatch(ID, @"[0-9a-f]{8}\-([0-9a-f]{4}\-){3}[0-9a-f]{12}")))
                {
                    TempData["tempMessage"] = "The user account is not valid. Please try clicking the link in your email again.";
                    return View();
                }

                else
                {
                    MembershipUser user = Membership.GetUser(new Guid(ID));
                    
                    if (!user.IsApproved)
                    {
                        user.IsApproved = true;
                        Membership.UpdateUser(user);
                        FormsService.SignIn(user.UserName, false);
                        return RedirectToAction("welcome");
                    }
                    else
                    {
                        FormsService.SignOut();
                        TempData["tempMessage"] = "You have already confirmed your email address... please log in.";
                        return RedirectToAction("LogOn");
                    }
                }
            }
            [HttpPost]
            public ActionResult Register(RegisterModel model)
            {
                if (ModelState.IsValid)
                {
                    if (Session["UserName"] != null)
                    {
                        string username = Session["UserName"].ToString();
                        // Attempt to register the user
                        MembershipCreateStatus createStatus = MembershipService.CreateUser(model.UserName, model.Password, model.Email);

                        if (createStatus == MembershipCreateStatus.Success)
                        {
                            //MembershipService.SendConfirmationEmail(model.UserName);
                            //return RedirectToAction("confirmation");
                            MembershipUser user = Membership.GetUser(model.UserName);
                            if (!user.IsApproved)
                            {
                                user.IsApproved = true;
                                Membership.UpdateUser(user);
                                if (username == "PatAdmin" || username == "RaviBohra" || username == "patadmin" || username == "ravibohra")
                                {
                                    using (WallEntities db = new WallEntities())
                                    {
                                        aspnet_Roles role = db.aspnet_Roles.Where(a => a.RoleName == "Patient").FirstOrDefault();
                                        aspnet_UsersInRoles asproles = new aspnet_UsersInRoles();
                                        Guid userid = (Guid)user.ProviderUserKey;
                                        Guid roleid = (Guid)role.RoleId;
                                        asproles.UserId = userid;
                                        asproles.RoleId = roleid;
                                        db.aspnet_UsersInRoles.Add(asproles);
                                        db.SaveChanges();
                                        ViewBag.NewPatientid = userid.ToString();
                                        Session["NewPatientid"] = userid.ToString();
                                        ViewBag.Message = user.UserName;
                                        ViewBag.Patient = "Patient";
                                        return View();
                                      }
                                }
                                else if (username == "Physician")
                                {
                                    using (WallEntities db = new WallEntities())
                                    {
                                        aspnet_Roles role = db.aspnet_Roles.Where(a => a.RoleName == "Physician").FirstOrDefault();
                                        aspnet_UsersInRoles asproles = new aspnet_UsersInRoles();
                                        Guid userid = (Guid)user.ProviderUserKey;
                                        Guid roleid = (Guid)role.RoleId;
                                        asproles.UserId = userid;
                                        asproles.RoleId = roleid;
                                        db.aspnet_UsersInRoles.Add(asproles);
                                        db.SaveChanges();
                                        ViewBag.Message = user.UserName;
                                        return View();
                                     }
                                }
                                // Roles.AddUserToRole(model.UserName,"Patient");
                                //ViewBag["Message"] = "New User has been created";
                            }
                            //Roles.AddUserToRole(user.UserName, "Patient");

                        }
                        else
                        {
                            ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
                        }
                    }
                }
                // If we got this far, something failed, redisplay form
                ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
                return View(model);
            }

            // **************************************
            // URL: /Account/ChangePassword
            // **************************************
            
            //[Authorize]
            //public ActionResult ChangePassword()
            //{
            //    ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            //    return View();
            //}

            //[Authorize]
            //[HttpPost]
            //public ActionResult ChangePassword(ChangePasswordModel model)
            //{
            //    if (ModelState.IsValid)
            //    {
            //        if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
            //        {
            //            return RedirectToAction("ChangePasswordSuccess");
            //        }
            //        else
            //        {
            //            ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
            //        }
            //    }

            //    // If we got this far, something failed, redisplay form
            //    ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            //    return View(model);
            //}

          
            [Authorize]
            [HttpPost]
            public ActionResult ChangePassword(ChangePasswordModel model)
            {
                var result = new Result();
                if (model !=null)
                {
                    if(model.NewPassword==model.ConfirmPassword)
                    {

                    if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                    {
                        result.Success = "Your Password Has been changed successfully.";
                    }
                    else
                    {
                       result.Error= "The old password is incorrect or the new password is invalid.";
                    }
                    
                    }
                    else
                    {
                        result.Error="new password and repeat password do not match.";
                    }
                }
                else
                {
                    result.Error = "All Password Fields are required..Please Fill all Fields.";
                }
               return Json(result);
            }

            // **************************************
            // URL: /Account/ChangePasswordSuccess
            // **************************************

            //public ActionResult ChangePasswordSuccess()
            //{
            //    return View();
            //}

            [HttpPost]
            public ActionResult PersonalInfo()
            {
                IPatientMDRepository _pat = new PatientMDRepository();
               var userId = Session["UserId"].ToString();
               var username = Session["UserName"].ToString();
               PatientMD user = _pat.GetByUserId(userId).First();
               MembershipUser muser = Membership.GetUser(username);
               var updateinfo = new UpdateInfo();
               updateinfo.PatientName = user.PatientName;
               updateinfo.DOB = user.DateOfBirth;
               updateinfo.Sex = user.Sex;
               updateinfo.EmailId = muser.Email;
               return Json(updateinfo);
            }

            [HttpPost]
            public ActionResult UpdatePersonalInfo(UpdateInfo update)
            {
                var result = new Result();
                if (update != null)
                {
                    IPatientMDRepository _pat = new PatientMDRepository();
                    var userId = Session["UserId"].ToString();
                    var username = Session["UserName"].ToString();

                    var pat = _pat.updatePersonalinfo(update, userId);
                    if (pat != null)
                    {
                        MembershipUser muser = Membership.GetUser(username);
                        muser.Email = update.EmailId;
                        Membership.UpdateUser(muser);
                        result.Success = "Your Personal Information Has been updated..";
                    }
                }
                else
                {
                    result.Error = "Please do Not Put Blank Data.";
                }
                
                return Json(result);
            }


            public ActionResult Contact(Contact contact)
            {
                return View(contact);
            }

            
            public ActionResult SendMail(Contact contact)
            {
                string retValue = "There was an error submitting the form, please try again later.";
                if (!ModelState.IsValid)
                {
                    ViewBag.retValue = retValue;
                    return View();
                }

                if (ModelState.IsValid && contact !=null)
                {
                    var from = "myhealthcare26nov@gmail.com";
                    var to = "donda007@gmail.com";
                    MailMessage mm = new MailMessage(from, to)
                    {
                        Subject = "Contact",
                        IsBodyHtml = true,
                        Body = "First Name :" + contact.FirstName +
                               "Last Name :" + contact.LastName +
                               "Practice Name :" + contact.PracticeName +
                               "Email Address :" + contact.Email +
                               "Comments :" + contact.Comments
                    };
                    
                    SmtpClient smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        Credentials = new NetworkCredential("myhealthcare26nov@gmail.com",
                                                            "pooja123456")
                    };
                     try
                        {
                             smtp.Send(mm);
                            retValue = "Your Request for Contact was submitted successfully. We will contact you shortly.";
                        }
                   
                        catch (Exception)
                        {

                            throw;
                        }
                    
                }
                ViewBag.retValue = retValue;
                return View();
                //return RedirectToAction("Login","Account");
            }

            
        }
    
}
