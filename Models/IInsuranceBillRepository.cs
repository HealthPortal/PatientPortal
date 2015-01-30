using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface IInsuranceBillRepository
    {

        IEnumerable<InsuranceBilling> GetAll();
        //PatientMD GetByUserId(string UserId);
        IEnumerable<InsuranceBilling> GetByUserId(string UserId);
        InsuranceBilling GetById(int PatientID);
        void DeletePatient(string userid);

        InsuranceBilling AddInsBill(InsuranceBilling insbln);

        InsuranceBilling UpdateInsBill(InsuranceBilling patientmd);

    }
}
