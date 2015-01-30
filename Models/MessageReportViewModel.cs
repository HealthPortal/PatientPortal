using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthPortal.Models
{
    public class MessageReportViewModel
    {
        public Int32 NumWithin { get; set; }
        public Int32 NumOutside { get; set; }

        public string PatientName { get; set; }

        public string DOB { get; set; }

        public int TotalOutside { get; set; }

        public int TotalWithin { get; set; }

        public string Gender { get; set; }
    }
}