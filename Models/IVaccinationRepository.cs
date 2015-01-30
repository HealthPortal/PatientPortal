using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface IVaccinationRepository
    {
        IEnumerable<Vaccination> GetAll();
        Vaccination GetById(int vaccineid);
        IEnumerable<Vaccination> GetByUserId(string UserId);
        void DeletePatient(string userid);
        Vaccination AddVaccine(Vaccination vaccine);

        Vaccination UpdatePatient(Vaccination vaccine);
    }
}
