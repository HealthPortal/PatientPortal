using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface IPhyAllergyRepository
    {
        IEnumerable<Allergy> GetAll();
        IEnumerable<Allergy> GebyUserId(string PatientId);
    }
}
