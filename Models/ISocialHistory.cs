using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface ISocialHistory
    {
        IEnumerable<SocialHistory> GetAll();
        SocialHistory GetById(int careplainid);
        IEnumerable<SocialHistory> GetByUserId(string UserId);
        void DeletePatient(string userid);
        SocialHistory AddHistory(SocialHistory SocialHistory);

        SocialHistory UpdatePatient(SocialHistory SocialHistory);
    }
}
