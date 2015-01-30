using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    public class EncounterReportViewModel
    {
        public string PatientName { get; set; }
        public string DOB { get; set; }
        public Int32 TotalOutside { get; set; }
        public Int32 TotalWithin { get; set; }
        public string Gender { get; set; }

        public string Date { get; set; }

        public string ActionResults { get; set; }

        public string UpdatedBy { get; set; }
    }
}
