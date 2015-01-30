using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface IMyDayRepository
    {
        IEnumerable<MyDay> GetAll();
        MyDay GetbyId(int id);
        MyDay GetRemovePatient(string id);
    }
}
