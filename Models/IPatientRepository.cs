using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    public interface IPatientRepository
    {
        IEnumerable<Patient> GetAll();
        Patient GetById(int patientid);
        IEnumerable<Patient> GetByUserId(string UserId);
    }
}
