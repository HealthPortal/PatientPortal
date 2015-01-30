using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface IEncounterDiagnosis
    {
        IEnumerable<EncounterDiagnosis> GetAll();
        EncounterDiagnosis GetById(int careplainid);
        IEnumerable<EncounterDiagnosis> GetByUserId(string UserId);
        void DeletePatient(string userid);
        EncounterDiagnosis AddEncDiag(EncounterDiagnosis EncDiag);

        EncounterDiagnosis UpdatePatient(EncounterDiagnosis Encdiag);
    }
}
