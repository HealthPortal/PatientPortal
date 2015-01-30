using HealthPortal.Models;
using System.Linq;
using System.Web.Mvc;
using RazorPDF;


namespace HealthPortal.Controllers
{
    public class PDFViewController : Controller
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
         private static readonly IEncounterReport encrepo = new EncounterReportModel();
      

        public ActionResult Index(string encountertype)
        {
            userId = Session["UserId"].ToString();
            var Pdfviewmodel = new PDFViewModel();
            var encttype = encountertype.ToString();
            if (encttype == "P")
            {
                if (userId != "a8a56c1d-203c-4c53-9d95-972dec7a2ef5" || userId == "f40ca8e9-dc6c-44de-9936-266249a7a201" || userId == "a24e6d62-414d-4434-bbe3-b3eec3a026e7")
                {

                    encttype = "I,A";
                }

                Pdfviewmodel.Reporttitle = "Inpatient and Ambulatory report for";

            }
            else if (encttype == "I")
            {
                Pdfviewmodel.Reporttitle = "Inpatient Patient Summary Report for";

            }
            else if (encttype == "A")
            {
                Pdfviewmodel.Reporttitle = "Ambulatory Patient Summary Report for";

            }
            else if (encttype == "T")
            {
                encttype = "I";
                Pdfviewmodel.Reporttitle = "Inpatient Transfer Report for";
            }
            else if (encttype == "R")
            {
                encttype = "A";
                Pdfviewmodel.Reporttitle = "Ambulatory Transfer Report for";
            }
            Pdfviewmodel.PatientMDS = _patients.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();
            Pdfviewmodel.MyPhysicians = _physicians.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();
            Pdfviewmodel.EncounterInfos = _encountinfos.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();
            Pdfviewmodel.VitalSigns = _Vitals.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();
            Pdfviewmodel.MedicationAllergies = _Allergies.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();
            Pdfviewmodel.Problems = _problems.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();
            Pdfviewmodel.Medications = _medications.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();
            Pdfviewmodel.Vaccinations = _vaccinations.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();
            Pdfviewmodel.ProcedureDetails = _procedures.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();
            Pdfviewmodel.CarePlans = _careplans.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();
            Pdfviewmodel.LabResults = _labresults.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();
            Pdfviewmodel.SocialHistories = _socialhistories.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();
            Pdfviewmodel.EncounterDisgnos = _encountdiagnos.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();
            Pdfviewmodel.CognFunStatus = _cogfuncstatus.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();
            Pdfviewmodel.Reasonhospitals = _reasonhospital.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();


            EncounterReport erp = new EncounterReport();
            erp.RefId = "Report";
            erp.PatientId = userId;
            erp.UpdateBy = userId;
            erp.Action = "View Report";
            var encreports = encrepo.AddEncReport(erp);
           
            return PDFView(Pdfviewmodel);


        }
        private ActionResult PDFView(PDFViewModel Pdfviewmodel)
        {

            var pdf = new PdfResult(Pdfviewmodel, "PDFView");
            //return pdf;
            string file = pdf.ToString();
            return pdf;
        }

        public ActionResult DownloadPdf(string encountertype)
        {
            userId = Session["UserId"].ToString();
            var Pdfviewmodel = new PDFViewModel();
            var encttype = encountertype.ToString();
            if (encttype == "P")
            {
                if (userId != "a8a56c1d-203c-4c53-9d95-972dec7a2ef5" || userId != "f40ca8e9-dc6c-44de-9936-266249a7a201" || userId != "a24e6d62-414d-4434-bbe3-b3eec3a026e7")
                {
                    encttype = "I,A";
                }

                Pdfviewmodel.Reporttitle = "Inpatient and Ambulatory report for";
                filename = "In_AMB_pdf.pdf";
            }
            else if (encttype == "I")
            {
                Pdfviewmodel.Reporttitle = "Inpatient Patient Summary Report for";
                filename = "Inpatient_pdf.pdf";
            }
            else if (encttype == "A")
            {
                Pdfviewmodel.Reporttitle = "Ambulatory Patient Summary Report for";
                filename = "Ambulatory_pdf.pdf";
            }
            else if (encttype == "T")
            {
                encttype = "I";
                filename = "Inp_Transferpdf.pdf";
                Pdfviewmodel.Reporttitle = "Inpatient Transfer Report for";
            }
            else if (encttype == "R")
            {
                encttype = "A";
                filename = "Amb_Transferpdf.pdf";
                Pdfviewmodel.Reporttitle = "Ambulatory Transfer Report for";
            }

            Pdfviewmodel.PatientMDS = _patients.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();
            Pdfviewmodel.MyPhysicians = _physicians.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();
            Pdfviewmodel.EncounterInfos = _encountinfos.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();
            Pdfviewmodel.VitalSigns = _Vitals.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();
            Pdfviewmodel.MedicationAllergies = _Allergies.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();
            Pdfviewmodel.Problems = _problems.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();
            Pdfviewmodel.Medications = _medications.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();
            Pdfviewmodel.Vaccinations = _vaccinations.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();
            Pdfviewmodel.ProcedureDetails = _procedures.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();
            Pdfviewmodel.CarePlans = _careplans.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();
            Pdfviewmodel.LabResults = _labresults.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();
            Pdfviewmodel.SocialHistories = _socialhistories.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();
            Pdfviewmodel.EncounterDisgnos = _encountdiagnos.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();
            Pdfviewmodel.CognFunStatus = _cogfuncstatus.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();
            Pdfviewmodel.Reasonhospitals = _reasonhospital.GetByUserId(userId).Where(e => e.EncounterType.Contains(encttype)).AsEnumerable();

            EncounterReport erp = new EncounterReport();
            erp.RefId = "Report";
            erp.PatientId = userId;
            erp.UpdateBy = userId;
            erp.Action = "Download Report";
            var encreports = encrepo.AddEncReport(erp);

            return PDFDownload(Pdfviewmodel, filename);
        }


        public ActionResult PDFDownload(PDFViewModel pdfviewmodel, object filename)
        {
            Response.AddHeader("content-disposition", "attachment; filename=" + filename);

            //var pdf = new PdfResult(pdfviewmodel, "PDFView");
            Response.ContentType = "application/pdf";
            return new PdfResult(pdfviewmodel, "PDFDownload");
        }

        public string userId { get; set; }

        public object filename { get; set; }
    }
}
