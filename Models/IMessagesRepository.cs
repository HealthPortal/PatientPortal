using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface IMessagesRepository
    {
        IEnumerable<MessagesDetail> GetAll();
        MessagesDetail GetById(int MessageID);
        IEnumerable<MessagesDetail> GetByUserId(string UserId);
    }
}
