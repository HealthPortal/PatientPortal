using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface IPhysicianDetailsRepository
    {
        IEnumerable<PhysicianImage> GetAll();
        //IEnumerable<PhysicianImage> GebyUserId(string PatientId);
        IEnumerable<PhysicianImage> GetbyId(string userid);
    }
}
