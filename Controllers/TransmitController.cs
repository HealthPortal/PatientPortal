using HealthPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Web.Mvc;
using RazorPDF;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web.UI;
using iTextSharp.text.html.simpleparser;
using System.Xml;
using iTextSharp.text.xml;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Xml.Linq;

namespace HealthPortal.Controllers
{
    public class TransmitController : Controller
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

        //
        // GET: /Transmit/

        public string filenamepdf { get; set; }



        public ActionResult MailPdfAttach(string encountertype, string RecipientEmail)
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
                filenamepdf = "In_AMB_pdf.pdf";
            }
            else if (encttype == "I")
            {
                Pdfviewmodel.Reporttitle = "Inpatient Patient Summary Report for";
                filenamepdf = "Inpatient_pdf.pdf";
            }
            else if (encttype == "A")
            {
                Pdfviewmodel.Reporttitle = "Ambulatory Patient Summary Report for";
                filenamepdf = "Ambulatory_pdf.pdf";
            }
            else if (encttype == "T")
            {
                encttype = "I";
                filenamepdf = "Inp_Transferpdf.pdf";
                Pdfviewmodel.Reporttitle = "Inpatient Transfer Report for";
            }
            else if (encttype == "R")
            {
                encttype = "A";
                filenamepdf = "Amb_Transferpdf.pdf";
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



            var stream = generatepdf(Pdfviewmodel, filenamepdf);
            var ms = new MemoryStream(stream.ToArray());
            var attachment = new Attachment(ms, filenamepdf, "Application/pdf");
            var from = "myhealthcare26nov@gmail.com";
            var to = RecipientEmail.ToString();
            MailMessage mm = new MailMessage(from, to)
            {
                Subject = "Patient Summary Report",
                IsBodyHtml = true,
                Body = "Patient Report"
            };
            mm.Attachments.Add(attachment);
            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("myhealthcare26nov@gmail.com",
                                                    "pooja123456")
            };
            smtp.Send(mm);

            EncounterReport erp = new EncounterReport();
            erp.RefId ="Report";
            erp.PatientId = userId;
            erp.UpdateBy = userId;
            erp.Action = "Transmit Report";
            var encreports = encrepo.AddEncReport(erp);

            return View();

        }



        public MemoryStream generatepdf(PDFViewModel Pdfviewmodel, object filename)
        {

            var document = new Document();
            // var output = new FileStream(Server.MapPath("/PDFFiles/"+filename),FileMode.Create);
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);

            document.Open();
            var title = Pdfviewmodel.Reporttitle;
            var patname = "";
            if (Pdfviewmodel.PatientMDS != null)
            {
                var patmd = new PatientMD();
                patmd = Pdfviewmodel.PatientMDS.FirstOrDefault();
                patname = patmd.PatientName;
            }
            var para = new Paragraph();
            para.Add(new Chunk(title));
            para.Add(new Chunk(" "));
            para.Add(new Chunk(patname));
            document.Add(para);
            var boldTableFont = FontFactory.GetFont("", 13, Font.BOLD);
            if (Pdfviewmodel.PatientMDS != null)
            {
                document.Add(new Paragraph(new Chunk("Patient Demographics", boldTableFont)));
                var pattable = new PdfPTable(4);
                pattable.WidthPercentage = 100;
                pattable.HorizontalAlignment = 0;
                pattable.SpacingBefore = 10;
                pattable.SpacingAfter = 10;
                pattable.DefaultCell.Border = 4;
                pattable.SetWidths(new int[] { 8, 3, 4, 4 });
                pattable.DefaultCell.BorderWidth = 1;
                pattable.DefaultCell.BorderWidthBottom = 1;
                pattable.DefaultCell.BorderWidthLeft = 1;
                pattable.DefaultCell.BorderWidthRight = 1;
                pattable.DefaultCell.BorderWidthTop = 1;
                PdfPCell cell = new PdfPCell(new Phrase("Patient Name"));
                cell.BackgroundColor = new CMYKColor(55, 32, 0, 53);
                pattable.AddCell(cell);
                PdfPCell cell2 = new PdfPCell(new Phrase("Sex"));
                cell2.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                pattable.AddCell(cell2);
                PdfPCell cell3 = new PdfPCell(new Phrase("Date Of Birth"));
                cell3.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                pattable.AddCell(cell3);
                PdfPCell cell4 = new PdfPCell(new Phrase("Race"));
                cell4.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                pattable.AddCell(cell4);

                foreach (var patient in Pdfviewmodel.PatientMDS)
                {
                    pattable.AddCell(new Phrase(patient.PatientName));
                    pattable.AddCell(new Phrase(patient.Sex));
                    pattable.AddCell(new Phrase(patient.DateOfBirth));
                    pattable.AddCell(new Phrase(patient.Race));

                }
                document.Add(pattable);
            }
            if (Pdfviewmodel.MyPhysicians != null)
            {
                document.Add(new Paragraph(new Chunk("Care Team", boldTableFont)));
                var caretable = new PdfPTable(2);
                caretable.WidthPercentage = 100;
                caretable.HorizontalAlignment = 0;
                caretable.SpacingBefore = 10;
                caretable.SpacingAfter = 10;
                caretable.DefaultCell.BorderWidthBottom = 1;
                caretable.DefaultCell.BorderWidthLeft = 1;
                caretable.DefaultCell.BorderWidthRight = 1;
                caretable.DefaultCell.BorderWidthTop = 1;

                caretable.SetWidths(new int[] { 3, 8 });
                PdfPCell cell = new PdfPCell(new Phrase("Provider's Name"));
                cell.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                caretable.AddCell(cell);
                PdfPCell cell1 = new PdfPCell(new Phrase("Provider's office contact information"));
                cell1.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                caretable.AddCell(cell1);

                foreach (var careteam in Pdfviewmodel.MyPhysicians)
                {
                    caretable.AddCell(new Phrase(careteam.Name));
                    var provideroffice = new Phrase(careteam.PrimaryPhone);
                    provideroffice.Add(new Phrase(careteam.HospitalName));
                    provideroffice.Add(new Phrase(careteam.StreetAddress));
                    provideroffice.Add(new Phrase(careteam.StreetAddress2));
                    provideroffice.Add(new Phrase(careteam.Locality));
                    provideroffice.Add(new Phrase(careteam.Region));
                    provideroffice.Add(new Phrase(careteam.PostalCode));
                    caretable.AddCell(provideroffice);

                }
                document.Add(caretable);
            }

            foreach (var enci in Pdfviewmodel.EncounterInfos)
            {

                if (enci.EncounterType.Contains("I"))
                {

                    if (Pdfviewmodel.EncounterInfos != null)
                    {


                        document.Add(new Paragraph(new Chunk("Encounter Information", boldTableFont)));
                        var encinfotable = new PdfPTable(3);
                        encinfotable.WidthPercentage = 100;
                        encinfotable.HorizontalAlignment = 0;
                        encinfotable.SpacingBefore = 10;
                        encinfotable.SpacingAfter = 10;
                        encinfotable.DefaultCell.BorderWidthBottom = 1;
                        encinfotable.DefaultCell.BorderWidthLeft = 1;
                        encinfotable.DefaultCell.BorderWidthRight = 1;
                        encinfotable.DefaultCell.BorderWidthTop = 1;
                        encinfotable.SetWidths(new int[] { 4, 4, 6 });
                        PdfPCell cell = new PdfPCell(new Phrase("Admission Date"));
                        cell.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                        encinfotable.AddCell(cell);
                        PdfPCell cell1 = new PdfPCell(new Phrase("Discharge Date"));
                        cell1.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                        encinfotable.AddCell(cell1);

                        PdfPCell cell3 = new PdfPCell(new Phrase("Admission and Discharge Location"));
                        cell3.BackgroundColor = new CMYKColor(55, 32, 0, 53);
                        encinfotable.AddCell(cell3);


                        foreach (var encinfo in Pdfviewmodel.EncounterInfos)
                        {
                            encinfotable.AddCell(new Phrase(encinfo.AdmissionDate));
                            encinfotable.AddCell(new Phrase(encinfo.DischargeDate));
                            encinfotable.AddCell(new Phrase(encinfo.Admissiondischargelocation));
                        }
                        document.Add(encinfotable);
                    }
                }
            }

            if (Pdfviewmodel.SocialHistories != null)
            {
                document.Add(new Paragraph(new Chunk("Social History", boldTableFont)));
                var socialtable = new PdfPTable(3);
                socialtable.WidthPercentage = 100;
                socialtable.HorizontalAlignment = 0;
                socialtable.SpacingBefore = 10;
                socialtable.SpacingAfter = 10;
                socialtable.DefaultCell.BorderWidthBottom = 1;
                socialtable.DefaultCell.BorderWidthLeft = 1;
                socialtable.DefaultCell.BorderWidthRight = 1;
                socialtable.DefaultCell.BorderWidthTop = 1;
                socialtable.SetWidths(new int[] { 4, 4, 6 });
                PdfPCell cell = new PdfPCell(new Phrase("Social History Item"));
                cell.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                socialtable.AddCell(cell);
                PdfPCell cell2 = new PdfPCell(new Phrase("Description"));
                cell2.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                socialtable.AddCell(cell2);

                PdfPCell cell3 = new PdfPCell(new Phrase("SNOMED-CT"));
                cell3.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                socialtable.AddCell(cell3);


                foreach (var sochistory in Pdfviewmodel.SocialHistories)
                {
                    socialtable.AddCell(new Phrase(sochistory.SocialHistoryItem));
                    socialtable.AddCell(new Phrase(sochistory.Description));
                    socialtable.AddCell(new Phrase(sochistory.SNOMEDCT));
                }
                document.Add(socialtable);

            }

            if (Pdfviewmodel.MedicationAllergies != null)
            {
                document.Add(new Paragraph(new Chunk("Medication Allergies", boldTableFont)));
                var medallergytable = new PdfPTable(4);
                medallergytable.WidthPercentage = 100;
                medallergytable.HorizontalAlignment = 0;
                medallergytable.SpacingBefore = 10;
                medallergytable.SpacingAfter = 10;
                medallergytable.DefaultCell.BorderWidthBottom = 1;
                medallergytable.DefaultCell.BorderWidthLeft = 1;
                medallergytable.DefaultCell.BorderWidthRight = 1;
                medallergytable.DefaultCell.BorderWidthTop = 1;
                medallergytable.SetWidths(new int[] { 8, 3, 4, 4 });

                PdfPCell cell = new PdfPCell(new Phrase("Allergen"));
                cell.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                medallergytable.AddCell(cell);

                PdfPCell cell1 = new PdfPCell(new Phrase("RxNorm"));
                cell1.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                medallergytable.AddCell(cell1);

                PdfPCell cell2 = new PdfPCell(new Phrase("Reaction"));
                cell2.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                medallergytable.AddCell(cell2);

                PdfPCell cell3 = new PdfPCell(new Phrase("Status"));
                cell3.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                medallergytable.AddCell(cell3);


                foreach (var medallergy in Pdfviewmodel.MedicationAllergies)
                {
                    medallergytable.AddCell(new Phrase(medallergy.Allergen));
                    medallergytable.AddCell(new Phrase(medallergy.RxNormCode));
                    medallergytable.AddCell(new Phrase(medallergy.Reaction));
                    medallergytable.AddCell(new Phrase(medallergy.Status));
                }
                document.Add(medallergytable);

            }

            if (Pdfviewmodel.Medications != null)
            {

                document.Add(new Paragraph(new Chunk("Medication", boldTableFont)));
                var medtable = new PdfPTable(5);
                medtable.WidthPercentage = 100;
                medtable.HorizontalAlignment = 0;
                medtable.SpacingBefore = 10;
                medtable.SpacingAfter = 10;
                medtable.DefaultCell.BorderWidthBottom = 1;
                medtable.DefaultCell.BorderWidthLeft = 1;
                medtable.DefaultCell.BorderWidthRight = 1;
                medtable.DefaultCell.BorderWidthTop = 1;
                medtable.SetWidths(new int[] { 7, 3, 5, 3, 2 });
                PdfPCell cell = new PdfPCell(new Phrase("Medication/Drug Name"));
                cell.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                medtable.AddCell(cell);
                PdfPCell cell1 = new PdfPCell(new Phrase("RxNorm"));
                cell1.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                medtable.AddCell(cell1);
                PdfPCell cell2 = new PdfPCell(new Phrase("SIG"));
                cell2.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                medtable.AddCell(cell2);
                PdfPCell cell3 = new PdfPCell(new Phrase("Date Of Prescription"));
                cell3.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                medtable.AddCell(cell3);
                PdfPCell cell4 = new PdfPCell(new Phrase("Prescription Status"));
                cell4.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                medtable.AddCell(cell4);


                foreach (var meds in Pdfviewmodel.Medications)
                {
                    medtable.AddCell(new Phrase(meds.MedicationsName));
                    medtable.AddCell(new Phrase(meds.StandardCode));
                    medtable.AddCell(new Phrase(meds.SIG));
                    medtable.AddCell(new Phrase(meds.DateOfPrescription));
                    medtable.AddCell(new Phrase(meds.Status));
                }
                document.Add(medtable);

            }

            if (Pdfviewmodel.Problems != null)
            {

                document.Add(new Paragraph(new Chunk("Problems", boldTableFont)));
                var probtable = new PdfPTable(4);
                probtable.WidthPercentage = 100;
                probtable.HorizontalAlignment = 0;
                probtable.SpacingBefore = 10;
                probtable.SpacingAfter = 10;
                probtable.DefaultCell.BorderWidthBottom = 1;
                probtable.DefaultCell.BorderWidthLeft = 1;
                probtable.DefaultCell.BorderWidthRight = 1;
                probtable.DefaultCell.BorderWidthTop = 1;
                probtable.SetWidths(new int[] { 4, 4, 4, 4 });
                PdfPCell cell = new PdfPCell(new Phrase("Problem Name/Description"));
                cell.BackgroundColor = new CMYKColor(55, 32, 0, 53);
                probtable.AddCell(cell);
                PdfPCell cell2 = new PdfPCell(new Phrase("SNOMED-CT"));
                cell2.BackgroundColor = new CMYKColor(55, 32, 0, 53);
                probtable.AddCell(cell2);
                PdfPCell cell3 = new PdfPCell(new Phrase("Problem Reported Date"));
                cell3.BackgroundColor = new CMYKColor(55, 32, 0, 53);
                probtable.AddCell(cell3);
                PdfPCell cell4 = new PdfPCell(new Phrase("Problem Status"));
                cell4.BackgroundColor = new CMYKColor(55, 32, 0, 53);
                probtable.AddCell(cell4);


                foreach (var probs in Pdfviewmodel.Problems)
                {
                    probtable.AddCell(new Phrase(probs.ProblemCause));
                    probtable.AddCell(new Phrase(probs.StandardCode));
                    probtable.AddCell(new Phrase(probs.ProblemReportedDate));
                    probtable.AddCell(new Phrase(probs.Status));

                }
                document.Add(probtable);

            }

            if (Pdfviewmodel.ProcedureDetails != null)
            {

                document.Add(new Paragraph(new Chunk("Procedures", boldTableFont)));
                var medtable = new PdfPTable(5);
                medtable.WidthPercentage = 100;
                medtable.HorizontalAlignment = 0;
                medtable.SpacingBefore = 10;
                medtable.SpacingAfter = 10;
                medtable.DefaultCell.BorderWidthBottom = 1;
                medtable.DefaultCell.BorderWidthLeft = 1;
                medtable.DefaultCell.BorderWidthRight = 1;
                medtable.DefaultCell.BorderWidthTop = 1;
                medtable.SetWidths(new int[] { 6, 3, 4, 3, 3 });
                PdfPCell cell = new PdfPCell(new Phrase("Procedure"));
                cell.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                medtable.AddCell(cell);
                PdfPCell cell1 = new PdfPCell(new Phrase("SNOMED-CT"));
                cell1.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                medtable.AddCell(cell1);
                PdfPCell cell2 = new PdfPCell(new Phrase("Date Of Procedure"));
                cell2.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                medtable.AddCell(cell2);
                PdfPCell cell4 = new PdfPCell(new Phrase("BodySite"));
                cell4.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                medtable.AddCell(cell4);
                PdfPCell cell3 = new PdfPCell(new Phrase("BodySite SNOMED-CT"));
                cell3.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                medtable.AddCell(cell3);


                foreach (var procdet in Pdfviewmodel.ProcedureDetails)
                {
                    medtable.AddCell(new Phrase(procdet.ProcedureName));
                    medtable.AddCell(new Phrase(procdet.SNOMEDCT));
                    medtable.AddCell(new Phrase(procdet.ProcedureDate));
                    medtable.AddCell(new Phrase(""));
                    medtable.AddCell(new Phrase(""));
                }
                document.Add(medtable);

            }

            foreach (var vit in Pdfviewmodel.VitalSigns)
            {

                if (vit.EncounterType.Contains("I"))
                {
                    if (Pdfviewmodel.VitalSigns != null)
                    {

                        document.Add(new Paragraph(new Chunk("Vital Signs", boldTableFont)));
                        var vialsigntable = new PdfPTable(2);
                        vialsigntable.WidthPercentage = 100;
                        vialsigntable.HorizontalAlignment = 0;
                        vialsigntable.SpacingBefore = 10;
                        vialsigntable.SpacingAfter = 10;
                        vialsigntable.DefaultCell.BorderWidthBottom = 1;
                        vialsigntable.DefaultCell.BorderWidthLeft = 1;
                        vialsigntable.DefaultCell.BorderWidthRight = 1;
                        vialsigntable.DefaultCell.BorderWidthTop = 1;
                        vialsigntable.SetWidths(new int[] { 4, 4 });
                        PdfPCell cell = new PdfPCell(new Phrase("Vital Type"));
                        cell.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                        vialsigntable.AddCell(cell);
                        PdfPCell cell1 = new PdfPCell(new Phrase("Value"));
                        cell1.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                        vialsigntable.AddCell(cell1);


                        foreach (var vitals in Pdfviewmodel.VitalSigns)
                        {
                            vialsigntable.AddCell(new Phrase("Height"));
                            vialsigntable.AddCell(new Phrase(vitals.Height.ToString()));
                            vialsigntable.AddCell(new Phrase("Weight"));
                            vialsigntable.AddCell(new Phrase(vitals.Weight.ToString()));
                            vialsigntable.AddCell(new Phrase("Blood Pressure"));
                            vialsigntable.AddCell(new Phrase(vitals.BloodPressure));
                            vialsigntable.AddCell(new Phrase("Body Mass Index(BMI)"));
                            vialsigntable.AddCell(new Phrase(vitals.BMI.ToString()));
                        }
                        document.Add(vialsigntable);
                    }
                }
            }

            if (Pdfviewmodel.LabResults != null)
            {

                document.Add(new Paragraph(new Chunk("Laboratory Test and Values/Results", boldTableFont)));
                var labtable = new PdfPTable(4);
                labtable.WidthPercentage = 100;
                labtable.HorizontalAlignment = 0;
                labtable.SpacingBefore = 10;
                labtable.SpacingAfter = 10;
                labtable.DefaultCell.BorderWidthBottom = 1;
                labtable.DefaultCell.BorderWidthLeft = 1;
                labtable.DefaultCell.BorderWidthRight = 1;
                labtable.DefaultCell.BorderWidthTop = 1;
                labtable.SetWidths(new int[] { 8, 3, 4, 4 });
                PdfPCell cell = new PdfPCell(new Phrase("Orderable Test Name"));
                cell.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                labtable.AddCell(cell);
                PdfPCell cell1 = new PdfPCell(new Phrase("LOINC Code"));
                cell1.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                labtable.AddCell(cell1);
                PdfPCell cell2 = new PdfPCell(new Phrase("Test Result Value"));
                cell2.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                labtable.AddCell(cell2);
                PdfPCell cell3 = new PdfPCell(new Phrase("Date Performed"));
                cell3.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                labtable.AddCell(cell3);


                foreach (var labresult in Pdfviewmodel.LabResults)
                {
                    labtable.AddCell(new Phrase(labresult.OrderableTestName));
                    labtable.AddCell(new Phrase(labresult.LONICCode));
                    labtable.AddCell(new Phrase(labresult.TestResultValue));
                    labtable.AddCell(new Phrase(labresult.DatePerformed));
                }
                document.Add(labtable);

            }


            foreach (var vac in Pdfviewmodel.Vaccinations)
            {

                if (vac.EncounterType.Contains("I"))
                {
                    if (Pdfviewmodel.Vaccinations != null)
                    {


                        document.Add(new Paragraph(new Chunk("Immunizations", boldTableFont)));
                        var vaccinetable = new PdfPTable(4);
                        vaccinetable.WidthPercentage = 100;
                        vaccinetable.HorizontalAlignment = 0;
                        vaccinetable.SpacingBefore = 10;
                        vaccinetable.SpacingAfter = 10;
                        vaccinetable.DefaultCell.BorderWidthBottom = 1;
                        vaccinetable.DefaultCell.BorderWidthLeft = 1;
                        vaccinetable.DefaultCell.BorderWidthRight = 1;
                        vaccinetable.DefaultCell.BorderWidthTop = 1;
                        vaccinetable.SetWidths(new int[] { 8, 3, 4, 4 });
                        PdfPCell cell = new PdfPCell(new Phrase("Vaccine"));
                        cell.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                        vaccinetable.AddCell(cell);
                        PdfPCell cell1 = new PdfPCell(new Phrase("CVXCode"));
                        cell1.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                        vaccinetable.AddCell(cell1);
                        PdfPCell cell2 = new PdfPCell(new Phrase("Date"));
                        cell2.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                        vaccinetable.AddCell(cell2);
                        PdfPCell cell3 = new PdfPCell(new Phrase("Status"));
                        cell3.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                        vaccinetable.AddCell(cell3);

                        foreach (var vaccine in Pdfviewmodel.Vaccinations)
                        {
                            vaccinetable.AddCell(new Phrase(vaccine.VaccineName));
                            vaccinetable.AddCell(new Phrase(vaccine.CVXCode));
                            vaccinetable.AddCell(new Phrase(vaccine.DateAdministered));
                            vaccinetable.AddCell(new Phrase(vaccine.VaccineStatus));
                        }
                        document.Add(vaccinetable);
                    }
                }
            }


            if (Pdfviewmodel.CarePlans != null)
            {
                var newpara = new Paragraph();
                newpara.Add(new Chunk("Care Plan", boldTableFont));
                newpara.Add(new Chunk("(Vendor supplied data that includes care plan goals and instructions is permitted.)"));
                document.Add(newpara);
                var careplantable = new PdfPTable(2);
                careplantable.WidthPercentage = 100;
                careplantable.HorizontalAlignment = 0;
                careplantable.SpacingBefore = 10;
                careplantable.SpacingAfter = 10;
                careplantable.DefaultCell.BorderWidthBottom = 1;
                careplantable.DefaultCell.BorderWidthLeft = 1;
                careplantable.DefaultCell.BorderWidthRight = 1;
                careplantable.DefaultCell.BorderWidthTop = 1;
                careplantable.SetWidths(new int[] { 4, 6 });
                PdfPCell cell = new PdfPCell(new Phrase("Goal/Instructions"));
                cell.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                careplantable.AddCell(cell);
                PdfPCell cell3 = new PdfPCell(new Phrase("Instructions"));
                cell3.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                careplantable.AddCell(cell3);

                foreach (var cares in Pdfviewmodel.CarePlans)
                {
                    careplantable.AddCell(new Phrase("Goal"));
                    careplantable.AddCell(new Phrase(cares.Goal + ",SNOMED-CT:" + cares.SNOMEDCT));
                    careplantable.AddCell(new Phrase("Instructions"));
                    careplantable.AddCell(new Phrase(cares.Instructions));
                }
                document.Add(careplantable);

            }

            if (Pdfviewmodel.EncounterDisgnos != null)
            {
                foreach (var encdi in Pdfviewmodel.EncounterInfos)
                {
                    if (encdi.EncounterType.Contains("I"))
                    {
                        document.Add(new Paragraph(new Chunk("Encounter Diagnosis", boldTableFont)));
                        var encdiagtable = new PdfPTable(4);
                        encdiagtable.WidthPercentage = 100;
                        encdiagtable.HorizontalAlignment = 0;
                        encdiagtable.SpacingBefore = 10;
                        encdiagtable.SpacingAfter = 10;
                        encdiagtable.DefaultCell.BorderWidthBottom = 1;
                        encdiagtable.DefaultCell.BorderWidthLeft = 1;
                        encdiagtable.DefaultCell.BorderWidthRight = 1;
                        encdiagtable.DefaultCell.BorderWidthTop = 1;
                        encdiagtable.SetWidths(new int[] { 8, 3, 4, 4 });
                        PdfPCell cell = new PdfPCell(new Phrase("Diagnosis"));
                        cell.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                        encdiagtable.AddCell(cell);
                        PdfPCell cell1 = new PdfPCell(new Phrase("SNOMED-CT"));
                        cell1.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                        encdiagtable.AddCell(cell1);
                        PdfPCell cell2 = new PdfPCell(new Phrase("Start Date"));
                        cell2.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                        encdiagtable.AddCell(cell2);
                        PdfPCell cell3 = new PdfPCell(new Phrase("Status"));
                        cell3.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                        encdiagtable.AddCell(cell3);

                        foreach (var encdiag in Pdfviewmodel.EncounterDisgnos)
                        {
                            encdiagtable.AddCell(new Phrase(encdiag.Diagnosis));
                            encdiagtable.AddCell(new Phrase(encdiag.SNOMEDCT));
                            encdiagtable.AddCell(new Phrase(encdiag.StartDate));
                            encdiagtable.AddCell(new Phrase(encdiag.Status));
                        }
                        document.Add(encdiagtable);
                    }
                }
            }

            if (Pdfviewmodel.CognFunStatus != null)
            {
                foreach (var cog in Pdfviewmodel.CognFunStatus)
                {
                    if (cog.EncounterType.Contains("I"))
                    {

                        document.Add(new Paragraph(new Chunk("Cognitive and Functional Status", boldTableFont)));
                        var cogfuntable = new PdfPTable(4);
                        cogfuntable.WidthPercentage = 100;
                        cogfuntable.HorizontalAlignment = 0;
                        cogfuntable.SpacingBefore = 10;
                        cogfuntable.SpacingAfter = 10;
                        cogfuntable.DefaultCell.BorderWidthBottom = 1;
                        cogfuntable.DefaultCell.BorderWidthLeft = 1;
                        cogfuntable.DefaultCell.BorderWidthRight = 1;
                        cogfuntable.DefaultCell.BorderWidthTop = 1;
                        cogfuntable.SetWidths(new int[] { 8, 3, 4, 4 });
                        PdfPCell cell = new PdfPCell(new Phrase("Description"));
                        cell.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                        cogfuntable.AddCell(cell);
                        PdfPCell cell1 = new PdfPCell(new Phrase("SNOMED-CT"));
                        cell1.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                        cogfuntable.AddCell(cell1);
                        PdfPCell cell2 = new PdfPCell(new Phrase("Reported Date"));
                        cell2.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                        cogfuntable.AddCell(cell2);
                        PdfPCell cell3 = new PdfPCell(new Phrase("Status"));
                        cell3.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                        cogfuntable.AddCell(cell3);

                        foreach (var cogfunstatus in Pdfviewmodel.CognFunStatus)
                        {
                            cogfuntable.AddCell(new Phrase(cogfunstatus.Description));
                            cogfuntable.AddCell(new Phrase(cogfunstatus.SNOMEDCT));
                            cogfuntable.AddCell(new Phrase(cogfunstatus.ReportedDate));
                            cogfuntable.AddCell(new Phrase(cogfunstatus.Status));
                        }
                        document.Add(cogfuntable);
                    }
                }
            }
            if (Pdfviewmodel.Reasonhospitals != null)
            {
                foreach (var resn in Pdfviewmodel.Reasonhospitals)
                {
                    if (resn.EncounterType.Contains("I"))
                    {

                        document.Add(new Paragraph(new Chunk("Reason For Hospitalization", boldTableFont)));
                        var reasontable = new PdfPTable(1);
                        reasontable.WidthPercentage = 100;
                        reasontable.HorizontalAlignment = 0;
                        reasontable.SpacingBefore = 10;
                        reasontable.SpacingAfter = 10;
                        reasontable.DefaultCell.BorderWidthBottom = 1;
                        reasontable.DefaultCell.BorderWidthLeft = 1;
                        reasontable.DefaultCell.BorderWidthRight = 1;
                        reasontable.DefaultCell.BorderWidthTop = 1;
                        reasontable.SetWidths(new int[] { 10 });
                        PdfPCell cell = new PdfPCell(new Phrase("Discharge Instructions"));
                        cell.BackgroundColor = new CMYKColor(55, 32, 0, 53);

                        reasontable.AddCell(cell);

                        foreach (var reason in Pdfviewmodel.Reasonhospitals)
                        {
                            reasontable.AddCell(new Phrase(reason.Reason));
                        }
                        document.Add(reasontable);
                    }
                }
            }


            if (Pdfviewmodel.EncounterInfos != null)
            {
                foreach (var enc in Pdfviewmodel.EncounterInfos)
                {

                    if (enc.EncounterType.Contains("I"))
                    {
                        document.Add(new Paragraph(new Chunk("Discharge Instructions", boldTableFont)));
                        var enhosdischarge = new PdfPTable(1);
                        enhosdischarge.WidthPercentage = 100;
                        enhosdischarge.HorizontalAlignment = 0;
                        enhosdischarge.SpacingBefore = 10;
                        enhosdischarge.SpacingAfter = 10;
                        enhosdischarge.DefaultCell.BorderWidthBottom = 1;
                        enhosdischarge.DefaultCell.BorderWidthLeft = 1;
                        enhosdischarge.DefaultCell.BorderWidthRight = 1;
                        enhosdischarge.DefaultCell.BorderWidthTop = 1;
                        enhosdischarge.SetWidths(new int[] { 10 });
                        PdfPCell cell = new PdfPCell();
                        var admtext = "You were admitted to ";

                        foreach (var encinn in Pdfviewmodel.EncounterInfos)
                        {

                            if (encinn.EncounterType.Contains("I"))
                            {
                                var admdislocation = encinn.Admissiondischargelocation;
                                var txt = admdislocation + " on " + encinn.AdmissionDate + " with a diagnosis of ";
                                var rea = "";
                                foreach (var reason in Pdfviewmodel.Reasonhospitals)
                                {
                                    rea = reason.Reason + ".";
                                }
                                var elm = "You underwent a biopsy of the sinus mass and were treated with iv antibiotics. You tolerated the procedure without complications and your condition improved. You were discharged from";
                                var admd = encinn.Admissiondischargelocation + " on " + encinn.DischargeDate + " with instructions to follow up with ";
                                var phyname = "";
                                foreach (var care in Pdfviewmodel.MyPhysicians)
                                {
                                    if (care.EncounterType.Contains("I"))
                                    {
                                        phyname = care.Name;
                                    }
                                }
                                var elm1 = "Should you have any questions prior to discharge, please contact a member of your healthcare team. If you have left the hospital and have  any questions, please contact your primary care physician.";

                                cell.AddElement(new Phrase(admtext + txt + rea + elm + admd + phyname + elm1));
                            }

                        }

                        enhosdischarge.AddCell(cell);
                        PdfPCell cell1 = new PdfPCell();
                        var elm2 = "1.Take all medications as prescribed 2. No heavy lifting, or nose blowing 3. If you experience any of the following symptoms, call your primary care physician or return to the Emergency Room. * Chest pain * Shortness of breath * Dizziness or light-headedness * Intractable nausea or vomiting * High fever * Uncontrollable bleeding * Pain or redness at the site of any previous intravenous catheter * Any other unusual 4.Schedule a follow up appointment with your primary care physician in one week";
                        cell1.AddElement(new Phrase(elm2));
                        enhosdischarge.AddCell(cell1);


                        document.Add(enhosdischarge);
                    }
                }

            }




            document.Close();

            return output;
        }



        public ActionResult MailxmlAttach(string encountertype1, string RecipientEmail)
        {

            userId = Session["UserId"].ToString();
            var Pdfviewmodel = new PDFViewModel();
            var encttype = encountertype1.ToString();
            if (encttype == "P")
            {
                if (userId != "a8a56c1d-203c-4c53-9d95-972dec7a2ef5" || userId != "f40ca8e9-dc6c-44de-9936-266249a7a201" || userId != "a24e6d62-414d-4434-bbe3-b3eec3a026e7")
                {
                    encttype = "I,A";
                }
                Pdfviewmodel.Reporttitle = "Inpatient and Ambulatory report for";
                filenamepdf = "In_AMB_xml.xml";
            }
            else if (encttype == "I")
            {
                Pdfviewmodel.Reporttitle = "Inpatient Patient Summary Report for";
                filenamepdf = "Inpatient_xml.xml";
            }
            else if (encttype == "A")
            {
                Pdfviewmodel.Reporttitle = "Ambulatory Patient Summary Report for";
                filenamepdf = "Ambulatory_xml.xml";
            }
            else if (encttype == "T")
            {
                encttype = "I";
                filenamepdf = "Inp_Transferxml.xml";
                Pdfviewmodel.Reporttitle = "Inpatient Transfer Report for";
            }
            else if (encttype == "R")
            {
                encttype = "A";
                filenamepdf = "Amb_Transferxml.xml";
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

            var stream = generatexml(Pdfviewmodel, filenamexml);
            var ms = new MemoryStream(stream.ToArray());
            var attachment = new Attachment(ms, filenamepdf, "text/xml");
            var from = "myhealthcare26nov@gmail.com";
            var to = RecipientEmail.ToString();
            MailMessage mm = new MailMessage(from, to)
            {
                Subject = "Patient Summary Report",
                IsBodyHtml = true,
                Body = "Patient Report"
            };
            mm.Attachments.Add(attachment);
            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("myhealthcare26nov@gmail.com",
                                                    "pooja123456")
            };
            smtp.Send(mm);
            return View();

        }

        private MemoryStream generatexml(PDFViewModel Pdfviewmodel, string filenamepdf)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-16", "yes"),

              new XElement("ClinicalDocument", new XElement("realmCode", new XAttribute("Code", "US")), new XElement("title", Pdfviewmodel.Reporttitle)));

            foreach (var patientmd in Pdfviewmodel.PatientMDS)
            {
                doc.Element("ClinicalDocument").Add(
                    new XElement("PatientDetails",
                        new XElement("PatientName", patientmd.PatientName),
                        new XElement("DateOfBirth", patientmd.DateOfBirth),
                        new XElement("Sex", patientmd.Sex),
                        new XElement("Race", patientmd.Race),
                        new XElement("PreferredLanguage", patientmd.PreferredLanguage),
                        new XElement("Height", patientmd.Height),
                        new XElement("Weight", patientmd.Weight)
                        )
                    );

                if (Pdfviewmodel.MyPhysicians.Any())
                {
                    foreach (var myphysician in Pdfviewmodel.MyPhysicians)
                    {
                        doc.Element("ClinicalDocument").Add(
                            new XElement("PhysicianDetails",
                                new XElement("ProvidersName", myphysician.Name),
                                new XElement("Providersofficecontactinformation", myphysician.PrimaryPhone, myphysician.HospitalName, myphysician.StreetAddress, myphysician.StreetAddress2, myphysician.Locality, myphysician.Region, myphysician.PostalCode)
                                )
                            );
                    }
                }
                if (Pdfviewmodel.EncounterInfos.Any())
                {
                    foreach (var encinfo in Pdfviewmodel.EncounterInfos)
                    {
                        doc.Element("ClinicalDocument").Add(
                            new XElement("EncounterInformation",
                                new XElement("AdmissionDate", encinfo.AdmissionDate),
                                new XElement("DischargeDate", encinfo.DischargeDate),
                                new XElement("AdmissionandDischargeLocation", encinfo.Admissiondischargelocation)
                                )
                            );
                    }
                }
                if (Pdfviewmodel.SocialHistories.Any())
                {
                    foreach (var sochist in Pdfviewmodel.SocialHistories)
                    {
                        doc.Element("ClinicalDocument").Add(
                            new XElement("SocialHistory",
                                new XElement("SocialHistoryItem", sochist.SocialHistoryItem),
                                new XElement("Description", sochist.Description),
                                new XElement("SNOMED-CT", sochist.SNOMEDCT)
                                )
                            );
                    }
                }
                if (Pdfviewmodel.MedicationAllergies.Any())
                {
                    foreach (var medallrgs in Pdfviewmodel.MedicationAllergies)
                    {
                        doc.Element("ClinicalDocument").Add(
                            new XElement("MedicationAllergies",
                                new XElement("Allergen", medallrgs.Allergen),
                                new XElement("RxNorm", medallrgs.RxNormCode),
                                new XElement("Reaction", medallrgs.Reaction),
                                new XElement("Status", medallrgs.Status)
                                )
                            );
                    }
                }
                if (Pdfviewmodel.Medications.Any())
                {
                    foreach (var meds in Pdfviewmodel.Medications)
                    {
                        doc.Element("ClinicalDocument").Add(
                                new XElement("Medication",
                                    new XElement("MedicationDrugName", meds.MedicationsName),
                                    new XElement("RxNorm", meds.StandardCode),
                                    new XElement("Reaction", meds.SIG),
                                    new XElement("DateOfPrescription", meds.DateOfPrescription),
                                    new XElement("Status", meds.Status)
                                    )
                                );
                    }
                }
                if (Pdfviewmodel.Problems.Any())
                {
                    foreach (var probs in Pdfviewmodel.Problems)
                    {
                        doc.Element("ClinicalDocument").Add(
                                new XElement("Problems",
                                    new XElement("ProblemCause", probs.ProblemCause),
                                    new XElement("SNOMED-CT", probs.StandardCode),
                                    new XElement("ProblemReportedDate", probs.ProblemReportedDate),
                                    new XElement("Status", probs.Status)
                                    )
                                );
                    }
                }
                if (Pdfviewmodel.ProcedureDetails.Any())
                {
                    foreach (var procdet in Pdfviewmodel.ProcedureDetails)
                    {
                        doc.Element("ClinicalDocument").Add(
                                new XElement("Procedures",
                                    new XElement("Procedure", procdet.ProcedureName),
                                    new XElement("SNOMED-CT", procdet.SNOMEDCT),
                                    new XElement("DateOfProcedure", procdet.ProcedureDate),
                                    new XElement("BodySite", ""),
                                    new XElement("BodySiteSNOMED-CT", "")
                                    )
                                );
                    }
                }
                if (Pdfviewmodel.VitalSigns.Any())
                {
                    foreach (var vital in Pdfviewmodel.VitalSigns)
                    {
                        doc.Element("ClinicalDocument").Add(
                                new XElement("VitalSigns",
                                    new XElement("VitalType",
                                    new XElement("Height", vital.Height),
                                    new XElement("Weight", vital.Weight),
                                    new XElement("BloodPressure", vital.BloodPressure),
                                    new XElement("BodyMassIndex", vital.BMI)
                                    ))
                                );
                    }
                }
                if (Pdfviewmodel.LabResults.Any())
                {
                    foreach (var labs in Pdfviewmodel.LabResults)
                    {
                        doc.Element("ClinicalDocument").Add(
                                new XElement("LabResults",
                                    new XElement("OrderableTestName", labs.OrderableTestName),
                                    new XElement("LONICCode", labs.LONICCode),
                                    new XElement("TestResultValue", labs.TestResultValue),
                                    new XElement("DatePerformed", labs.DatePerformed)
                                    )
                                );
                    }
                }
                if (Pdfviewmodel.Vaccinations.Any())
                {
                    foreach (var vaccine in Pdfviewmodel.Vaccinations)
                    {
                        doc.Element("ClinicalDocument").Add(
                                new XElement("Immunizations",
                                    new XElement("Vaccine", vaccine.VaccineName),
                                    new XElement("CVXCode", vaccine.CVXCode),
                                    new XElement("DateAdministered", vaccine.DateAdministered),
                                    new XElement("VaccineStatus", vaccine.VaccineStatus)
                                    )
                                );
                    }
                }
                if (Pdfviewmodel.CarePlans.Any())
                {
                    foreach (var careplan in Pdfviewmodel.CarePlans)
                    {
                        doc.Element("ClinicalDocument").Add(
                                new XElement("CarePlans",
                                    new XElement("GoalInstructions", new XAttribute("Goal", careplan.Goal),
                                    new XAttribute("SNOMEDCT", careplan.SNOMEDCT)),
                                    new XElement("Instructions", careplan.Instructions)
                                    )
                                );
                    }
                }
                if (Pdfviewmodel.EncounterDisgnos.Any())
                {
                    foreach (var encdiag in Pdfviewmodel.EncounterDisgnos)
                    {
                        doc.Element("ClinicalDocument").Add(
                                new XElement("EncounterDiagnosis",
                                    new XElement("Diagnosis", encdiag.Diagnosis),
                                    new XElement("SNOMEDCT", encdiag.SNOMEDCT),
                                    new XElement("StartDate", encdiag.StartDate),
                                    new XElement("Status", encdiag.Status)
                                    )
                                );
                    }
                }
                if (Pdfviewmodel.CognFunStatus.Any())
                {
                    foreach (var cogfunstatus in Pdfviewmodel.CognFunStatus)
                    {
                        doc.Element("ClinicalDocument").Add(
                                new XElement("CognitiveandFunctionalStatus",
                                    new XElement("Description", cogfunstatus.Description),
                                    new XElement("SNOMEDCT", cogfunstatus.SNOMEDCT),
                                    new XElement("ReportedDate", cogfunstatus.ReportedDate),
                                    new XElement("Status", cogfunstatus.Status)
                                    )
                                );
                    }
                }
                if (Pdfviewmodel.Reasonhospitals.Any())
                {
                    foreach (var reason in Pdfviewmodel.Reasonhospitals)
                    {
                        doc.Element("ClinicalDocument").Add(
                                new XElement("DischargeInstructions",
                                    new XElement("ReasonForHospitalization", reason.Reason)
                                    )
                                );
                    }
                }


            }

            MemoryStream ms = new MemoryStream();
            doc.Save(ms);
            return ms;
        }

        public string userId { get; set; }



        public string filenamexml { get; set; }
    }
}
