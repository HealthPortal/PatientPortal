using HealthPortal.Models;
using RazorPDF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace HealthPortal.Controllers
{
    public class CCDAViewController : Controller
    {
        //
        // GET: /CCDAView/

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


        public ActionResult Index(string encountertype)
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


            // return PDFView(Pdfviewmodel);
            return XmlView(Pdfviewmodel);

        }

        private ActionResult XmlView(PDFViewModel Pdfviewmodel)
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

            return new XmlResult(doc);
            //MemoryStream ms = new MemoryStream();
            //doc.Save(ms);
            //return File(new MemoryStream(ms.ToArray()),"text/xml","Hello.xml");
        }

        public ActionResult DownloadXml(string encountertype)
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
                filename = "In_AMB_xml.xml";
            }
            else if (encttype == "I")
            {
                Pdfviewmodel.Reporttitle = "Inpatient Patient Summary Report for";
                filename = "Inpatient_xml.xml";
            }
            else if (encttype == "A")
            {
                Pdfviewmodel.Reporttitle = "Ambulatory Patient Summary Report for";
                filename = "Ambulatory_xml.xml";
            }
            else if (encttype == "T")
            {
                encttype = "I";
                filename = "Inp_Transferxml.xml";
                Pdfviewmodel.Reporttitle = "Inpatient Transfer Report for";
            }
            else if (encttype == "R")
            {
                encttype = "A";
                filename = "Amb_Transferxml.xml";
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


            return XmlDownload(Pdfviewmodel, filename);
        }

        private ActionResult XmlDownload(PDFViewModel Pdfviewmodel, string filename)
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
            return File(new MemoryStream(ms.ToArray()), "text/xml", filename);
        }


        public string userId { get; set; }

        public string filename { get; set; }
    }
}
