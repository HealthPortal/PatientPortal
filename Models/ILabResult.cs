using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface ILabResult
    {
        IEnumerable<LabResult> GetAll();
        LabResult GetById(string refid);
        IEnumerable<LabResult> GetByUserId(string UserId);
        void DeletePatient(string userid);
        LabResult AddLabRes(LabResult LabRes);

        LabResult UpdatePatient(LabResult labresult);
    }
}
