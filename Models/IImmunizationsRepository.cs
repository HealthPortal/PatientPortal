using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface IImmunizationsRepository
    {

        IEnumerable<Immunization> GetAll();
        IEnumerable<Immunization> GetbyUserId(string PatientId);
        void DeletePatient(string userid);
        Immunization AddImmu(Immunization Immuni);

        Immunization UpdatePatient(Immunization Immuni);
    }
}
