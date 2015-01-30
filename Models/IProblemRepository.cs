using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface IProblemRepository
    {
        IEnumerable<Problem> GetAll();
        Problem GetById(int Problemid);
        IEnumerable<Problem> GetByUserId(string UserId);
        void DeletePatient(string userid);
        Problem AddProb(Problem prob);

        Problem UpdatePatient(Problem prob);
    }
}
