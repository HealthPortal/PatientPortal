using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HealthPortal.Models;

namespace HealthPortal.Models
{
    public class PhyMedicationsController : ApiController
    {
        IPhyMedicationsRepository repository = new PhyMedicationModel();
        // GET api/values
        [HttpGet]
        public IEnumerable<PhyMedication> Get()
        {
            return repository.GetAll();
        }


        [HttpGet]
        public IEnumerable<PhyMedication> Get(string PatientId)
        {
            IEnumerable<PhyMedication> patdemo = repository.GebyUserId(PatientId);
            return patdemo;
        }


    }
}
