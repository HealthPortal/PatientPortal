using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface IPhysicianRepository
    {
        IEnumerable<MyPhysician> GetAll();
        IEnumerable<MyPhysician> GetByUserId(string UserId);
        MyPhysician GetById(int Physicianid);
        MyPhysician AddPhy(MyPhysician physician);
        void DeletePatient(string userid);
        MyPhysician UpdatePatient(MyPhysician physician);
    }
}
