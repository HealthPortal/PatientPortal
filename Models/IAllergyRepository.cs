using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface IAllergyRepository
    {
        IEnumerable<MedicationAllergie> GetAll();
        MedicationAllergie GetById(int allergyid);
        IEnumerable<MedicationAllergie> GetByUserId(string UserId);
        void DeletePatient(string userid);
        MedicationAllergie AddAllergie(MedicationAllergie medallerg);

        MedicationAllergie UpdatePatient(MedicationAllergie medallerg);
    }
}
