using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    public interface IEncounterReport
    {
        IEnumerable<EncounterReport> GetByUserId(string patientid);
        IEnumerable<EncounterReport> GetAll();
        void DeletePatient(string patientid);
        EncounterReport AddEncReport(EncounterReport EncReport);
    }
}
