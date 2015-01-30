using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Controllers
{
   public class AppointmentNew
    {
        public string _id { get; set; }
        public Int32 AppointmentsID { get; set; }
        public string Physician { get; set; }
        public string CenterName { get; set; }
        public string Date { get; set; }
        public string Address { get; set; }
        public string Reason { get; set; }
        public string Time { get; set; }
        public string UserId { get; set; }
        public string EncounterType { get; set; }
        public string Patient { get; set; }
    }
}
