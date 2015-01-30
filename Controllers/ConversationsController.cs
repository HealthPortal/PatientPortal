using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HealthPortal.Models;

namespace HealthPortal.Models
{
    public class ConversationsController : ApiController
    {
        IConversationRepository repository = new ConversationModel();
        // GET api/values
        [HttpGet]
        public IEnumerable<ConversationsDetail> Get()
        {
            return repository.GetAll();
        }
        [HttpGet]
        public ConversationsDetail GetbyId(int id)
        {
            ConversationsDetail myday = repository.GetbyId(id);
            return myday;
        }

    }
}
