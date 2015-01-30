using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface ICognitiveFunctionalStatus
    {
        IEnumerable<CognitiveAndFunctionalStatus> GetAll();
        CognitiveAndFunctionalStatus GetById(int careplainid);
        IEnumerable<CognitiveAndFunctionalStatus> GetByUserId(string UserId);
        void DeletePatient(string userid);
        CognitiveAndFunctionalStatus AddCogFunStat(CognitiveAndFunctionalStatus CogFunStat);

        CognitiveAndFunctionalStatus UpdatePatient(CognitiveAndFunctionalStatus cognfstat);
    }
}
