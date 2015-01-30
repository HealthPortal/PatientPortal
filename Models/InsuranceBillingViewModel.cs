using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthPortal.Models
{
    public class InsuranceBillingViewModel
    {
        public string patientname { get; set; }
        public IEnumerable<InsuranceBilling> InsuranceBills { get; set; }
    }
}