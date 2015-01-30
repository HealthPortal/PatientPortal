using HealthPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HealthPortal.Controllers
{
    public class ExistingPatientController : Controller
    {
        private static readonly IPatientMDRepository _patients = new PatientMDRepository();
        private static readonly IPhysicianRepository _physicians = new PhysicianModel();
        private static readonly IEncounterInformation _encountinfos = new EncounterInfoModel();
        private static readonly IVitalsRepository _Vitals = new VitalsModel();
        private static readonly IAllergyRepository _Allergies = new AllergyModel();
        private static readonly IProblemRepository _problems = new ProblemModel();
        private static readonly IMedicationRepository _medications = new MedicationModel();
        private static readonly IVaccinationRepository _vaccinations = new VaccinationModel();
        private static readonly IProcedureRepository _procedures = new ProcedureModel();
        private static readonly ILabResult _labresults = new LabResultModel();
        private static readonly ICarePlanRepository _careplans = new CarePlanModel();
        private static readonly IEncounterDiagnosis _encountdiagnos = new EncounterDiagnosisModel();
        private static readonly ICognitiveFunctionalStatus _cogfuncstatus = new CognitiveFunctionalModel();
        private static readonly IReasonhospitalizaion _reasonhospital = new ReasonHospitalModel();
        private static readonly ISocialHistory _socialhistories = new SocialHistoryModel();
        private static readonly IImmunizationsRepository _immunizations = new ImmunizationModel();
        private static readonly IInsuranceBillRepository _insbilling = new InsuranceBillRepository();
        private static readonly IAppointment _appointments = new AppointmentRepository();

        //
        // GET: /ExistingPatient/
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var username = Session["UserName"].ToString();
            var roles = Roles.GetRolesForUser(username);
            if (roles.Contains("Admin"))
            {
                return RedirectToAction("ExistingPatient");
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ExistingPatient()
        {
            WallEntities wb = new WallEntities();
            //users from usersinrole patient....
            IEnumerable<aspnet_UsersInRoles> users = wb.aspnet_UsersInRoles.Where(p => p.aspnet_Roles.RoleName == "Patient");
            var musers = new List<MembershipUser>();
            foreach (var u in users)
            {
                MembershipUser pat = Membership.GetUser(u.UserId);
                musers.Add(pat);
            }
            List<ExistingPatient> patients = new List<ExistingPatient>();
            foreach (var u in musers)
            {
                if (u != null)
                {
                    var userId = u.ProviderUserKey.ToString();
                    patients.Add(new ExistingPatient()
                    {
                        UserId = userId,
                        PatientName = u.UserName,
                        EmailId = u.Email
                    });
                }
            }
            return View(patients);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditExistingPatient(string Id)
        {
            var userId = Id;
            var Pdfviewmodel = new PDFViewModel();

            Pdfviewmodel.PatientMDS = _patients.GetByUserId(userId).AsEnumerable();
            Pdfviewmodel.MyPhysicians = _physicians.GetByUserId(userId).AsEnumerable();
            Pdfviewmodel.EncounterInfos = _encountinfos.GetByUserId(userId).AsEnumerable();
            Pdfviewmodel.VitalSigns = _Vitals.GetByUserId(userId).AsEnumerable();
            Pdfviewmodel.MedicationAllergies = _Allergies.GetByUserId(userId).AsEnumerable();
            Pdfviewmodel.Problems = _problems.GetByUserId(userId).AsEnumerable();
            Pdfviewmodel.Medications = _medications.GetByUserId(userId).AsEnumerable();
            Pdfviewmodel.Vaccinations = _vaccinations.GetByUserId(userId).AsEnumerable();
            Pdfviewmodel.ProcedureDetails = _procedures.GetByUserId(userId).AsEnumerable();
            Pdfviewmodel.CarePlans = _careplans.GetByUserId(userId).AsEnumerable();
            Pdfviewmodel.LabResults = _labresults.GetByUserId(userId).AsEnumerable();
            Pdfviewmodel.SocialHistories = _socialhistories.GetByUserId(userId).AsEnumerable();
            Pdfviewmodel.EncounterDisgnos = _encountdiagnos.GetByUserId(userId).AsEnumerable();
            Pdfviewmodel.CognFunStatus = _cogfuncstatus.GetByUserId(userId).AsEnumerable();
            Pdfviewmodel.Reasonhospitals = _reasonhospital.GetByUserId(userId).AsEnumerable();
            Pdfviewmodel.Immunizations = _immunizations.GetbyUserId(userId).AsEnumerable();
            Pdfviewmodel.InsuranceBillings = _insbilling.GetByUserId(userId).AsEnumerable();
            Pdfviewmodel.Appointments = _appointments.GetByUserId(userId).AsEnumerable();

            return View(Pdfviewmodel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeletePatient(string UserID, string PatientName)
        {
            var results = new Message();
            try
            {
                MembershipUser user = Membership.GetUser(PatientName);

                if (user != null)
                {
                    var checkdelete = Membership.DeleteUser(PatientName, true);
                    if (checkdelete == true)
                    {
                        try
                        {
                            _patients.DeletePatient(UserID);
                            _physicians.DeletePatient(UserID);
                            _encountinfos.DeletePatient(UserID);
                            _Vitals.DeletePatient(UserID);
                            _Allergies.DeletePatient(UserID);
                            _problems.DeletePatient(UserID);
                            _medications.DeletePatient(UserID);
                            _vaccinations.DeletePatient(UserID);
                            _procedures.DeletePatient(UserID);
                            _careplans.DeletePatient(UserID);
                            _labresults.DeletePatient(UserID);
                            _socialhistories.DeletePatient(UserID);
                            _encountdiagnos.DeletePatient(UserID);
                            _cogfuncstatus.DeletePatient(UserID);
                            _reasonhospital.DeletePatient(UserID);
                            _immunizations.DeletePatient(UserID);
                            results.success = "Suceess: " + PatientName + "'s All Related Records has been Deleted From Portal.";
                        }
                        catch (Exception ex)
                        {
                            results.error = "Error: " + "Some Error Occured While Deleting Patient Record From Portal.May Be If Patient Record No Longer Available";
                        }
                    }
                }
                else
                {
                    results.attention = "Attention: " + PatientName + " Patient Already Deleted.Or No longer Exists.";
                }
            }
            catch (Exception ex)
            {
                results.error = "Error: " + "Some Error Occured While Deleting Patient Record From Portal.May Be If Patient Record No Longer Available";
            }

            return Json(results);
        }




    }
}
