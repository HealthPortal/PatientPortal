using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthPortal.Models
{
    public class PDFViewModel
    {
        public IEnumerable<PatientMD> PatientMDS { get; set; }
        public IEnumerable<MyPhysician> MyPhysicians { get; set; }
        public IEnumerable<EncounterInformation> EncounterInfos { get; set; }
        public IEnumerable<VitalSign> VitalSigns { get; set; }
        public IEnumerable<MedicationAllergie> MedicationAllergies { get; set; }
        public IEnumerable<Problem> Problems { get; set; }
        public IEnumerable<Medication> Medications { get; set; }
        public IEnumerable<Vaccination> Vaccinations { get; set; }
        public IEnumerable<ProcedureDetail> ProcedureDetails { get; set; }
        public IEnumerable<CarePlan> CarePlans { get; set; }
        public IEnumerable<SocialHistory> SocialHistories { get; set; }
        public IEnumerable<LabResult> LabResults { get; set; }
        public IEnumerable<EncounterDiagnosis> EncounterDisgnos { get; set; }
        public IEnumerable<ReasonForHospitalization> Reasonhospitals { get; set; }
        public IEnumerable<CognitiveAndFunctionalStatus> CognFunStatus { get; set; }
        public IEnumerable<Immunization> Immunizations { get; set; }
        public IEnumerable<InsuranceBilling> InsuranceBillings { get; set; }
        public IEnumerable<Appointment>Appointments{ get; set; }

        public string Reporttitle { get; set; }




    }
}