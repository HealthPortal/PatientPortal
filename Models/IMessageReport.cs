using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    public interface IMessageReport
    {
        IEnumerable<MessageReport> GetAll();
        IEnumerable<MessageReport> GetByUserId(string patientid);
        void DeletePatient(string patientid);
        MessageReport AddMessageReport(MessageReport MsgReport);
    }
}
