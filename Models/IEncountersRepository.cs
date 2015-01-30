using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface IEncountersRepository
    {
        IEnumerable<Encounter> GetAll();
        IEnumerable<Encounter> GebyUserId(string PatientId);
    }
}
