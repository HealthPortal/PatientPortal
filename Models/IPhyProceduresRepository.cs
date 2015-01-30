using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface IPhyProceduresRepository
    {
        IEnumerable<Procedure> GetAll();
        IEnumerable<Procedure> GebyUserId(string PatientId);
    }
}
