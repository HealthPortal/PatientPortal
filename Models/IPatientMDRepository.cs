using HealthPortal.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
   public interface IPatientMDRepository
    {
        IEnumerable<PatientMD> GetAll();
        //PatientMD GetByUserId(string UserId);
        IEnumerable<PatientMD> GetByUserId(string UserId);
        PatientMD GetById(int PatientID);
        void DeletePatient(string userid);

        PatientMD AddPatDemo(PatientMD patientmd);

        PatientMD UpdatePatient(PatientMD patientmd);
        PatientMD updatePersonalinfo(UpdateInfo upinfo, string userid);
    }
}
