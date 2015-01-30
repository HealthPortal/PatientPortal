using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface IProcedureRepository
    {
        IEnumerable<ProcedureDetail> GetAll();
        ProcedureDetail GetById(int procedureid);
        IEnumerable<ProcedureDetail> GetByUserId(string UserId);
        void DeletePatient(string userid);
        ProcedureDetail AddProcDetail(ProcedureDetail procdetail);

        ProcedureDetail UpdatePatient(ProcedureDetail procdetail);
    }
}
