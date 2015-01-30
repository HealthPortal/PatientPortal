using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HealthPortal.Models;
using System.Web;

namespace HealthPortal.Models
{
    public class MessagesController : ApiController
    {
        private static readonly IMessagesRepository repository = new MessagesModel();
        // GET api/values
        public IEnumerable<MessagesDetail> Get()
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                userId = session["UserId"].ToString();
            IEnumerable<MessagesDetail> msgs = repository.GetByUserId(userId);
            return msgs;


            //return repository.GetAll();
        }
        public string userId { get; set; }
    }
}
