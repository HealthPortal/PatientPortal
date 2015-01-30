using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface IMedicationRepository
    {
        IEnumerable<Medication> GetAll();
        Medication GetById(string refid);
        IEnumerable<Medication> GetByUserId(string UserId);
        void DeletePatient(string userid);
        Medication AddMed(Medication medication);

        Medication UpdatePatient(Medication Medicat);
    }
}
