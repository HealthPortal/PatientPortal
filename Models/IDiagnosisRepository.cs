using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface IDiagnosisRepository
    {
        IEnumerable<Diagnosi> GetAll();
        IEnumerable<Diagnosi> GebyUserId(string UserId);
    }
}
