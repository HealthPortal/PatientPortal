using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface ICarePlanRepository
    {
        IEnumerable<CarePlan> GetAll();
        CarePlan GetById(int careplainid);
        IEnumerable<CarePlan> GetByUserId(string UserId);
        void DeletePatient(string userid);
        CarePlan AddCareplan(CarePlan cplan);

        CarePlan UpdatePatient(CarePlan cplan);
    }
}
