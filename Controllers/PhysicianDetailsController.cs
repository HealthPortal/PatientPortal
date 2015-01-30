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
    public class PhysicianDetailsController : ApiController
    {
        IPhysicianDetailsRepository repository = new PhysicianDetailsModel();
        [HttpGet]
        public IEnumerable<PhysicianImage> Get()
        {
            var session = HttpContext.Current.Session;
            if (session["UserId"] != null)
                physicianId = session["UserId"].ToString();
            IEnumerable<PhysicianImage> patdemo = repository.GetbyId(physicianId);
            return patdemo;
        }
        
        //[HttpGet]
        //public PhysicianImage GetbyId(int id)
        //{
        //    PhysicianImage phyimg = repository.GetbyId(id);
        //    return phyimg;
        //}


        public string physicianId { get; set; }
    }
}
