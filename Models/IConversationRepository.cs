using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface IConversationRepository
    {
       IEnumerable<ConversationsDetail> GetAll();
       ConversationsDetail GetbyId(int id);
    }
}
