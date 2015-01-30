using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface IVitalsRepository
    {
        IEnumerable<VitalSign> GetAll();
        VitalSign GetById(int Vitalsid);
        IEnumerable<VitalSign> GetByUserId(string UserId);
        void DeletePatient(string userid);
        VitalSign AddVitals(VitalSign vsign);

        VitalSign UpdatePatient(VitalSign vsign);
    }
}
