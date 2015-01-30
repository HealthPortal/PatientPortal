using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface IPatientdetailRepository
    {
        IEnumerable<PatientDetail> GetAll();
        IEnumerable<PatientDetail> GebyUserId(string PatientId);
        PatientDetail GetbyId(int id);
    }
}
