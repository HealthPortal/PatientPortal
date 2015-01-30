using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface IEncounterInformation
    {
        IEnumerable<EncounterInformation> GetAll();
        EncounterInformation GetById(int careplainid);
        IEnumerable<EncounterInformation> GetByUserId(string UserId);
        void DeletePatient(string userid);
        EncounterInformation AddEncInfo(EncounterInformation EncInfo);

        EncounterInformation UpdatePatient(EncounterInformation encinfo);
    }
}
