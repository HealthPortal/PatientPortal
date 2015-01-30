using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface ILabRepository
    {

        IEnumerable<Lab> GetAll();
        IEnumerable<Lab> GebyUserId(string PatientId);
    }
}
