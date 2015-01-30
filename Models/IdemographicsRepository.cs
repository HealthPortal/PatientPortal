using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface IdemographicsRepository
    {
        IEnumerable<Demographic> GetAll();
        IEnumerable<Demographic> GebyUserId(string PatientId);

        Demographic AddDemo(Demographic demographic);
    }
}
