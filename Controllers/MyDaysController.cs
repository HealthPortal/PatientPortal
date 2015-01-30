using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HealthPortal.Models;

namespace HealthPortal.Models
{
    public class MyDaysController : ApiController
    {
        IMyDayRepository repository = new MyDayModel();
        // GET api/values
        [HttpGet]
        public IEnumerable<MyDay> Get()
        {
            return repository.GetAll();
        }
        [HttpGet]
        public MyDay GetbyId(int id)
        {
            MyDay myday = repository.GetbyId(id);
            return myday;
        }

    }
}
