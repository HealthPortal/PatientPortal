using HealthPortal.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthPortal.Models
{
    public class HealthActivityModel
    {

        public LabResult LabResult { get; set; }

        public AppointmentNew AppointmentNew { get; set; }

        public Medication Medication { get; set; }
    }
}