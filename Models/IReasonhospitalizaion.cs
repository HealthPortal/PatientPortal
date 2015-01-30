using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface IReasonhospitalizaion
    {
        IEnumerable<ReasonForHospitalization> GetAll();
        ReasonForHospitalization GetById(int careplainid);
        IEnumerable<ReasonForHospitalization> GetByUserId(string UserId);
        void DeletePatient(string userid);
        ReasonForHospitalization AddReasHosp(ReasonForHospitalization ReasHospt);
        ReasonForHospitalization UpdateReason(ReasonForHospitalization ReasHospt);
    }
}
