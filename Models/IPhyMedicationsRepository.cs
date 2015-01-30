using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface IPhyMedicationsRepository
    {

        IEnumerable<PhyMedication> GetAll();
        IEnumerable<PhyMedication> GebyUserId(string PatientId);
    }
}
