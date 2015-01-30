using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface IAdmissionsRepository
    {
        IEnumerable<Admission> GetAll();
        IEnumerable<Admission> GebyUserId(string PatientId);
        Admission GetbyId(int id);
    }
}
