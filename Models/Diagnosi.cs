using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;


namespace HealthPortal.Models
{
   public class Diagnosi
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public Int32 DiagnosisId { get; set; }
        public string Cause { get; set; }
        public string Date { get; set; }
        public string AdmittingDept { get; set; }
        public string Facility { get; set; }
        public string Doctor { get; set; }
        public string DischargeReport { get; set; }
        public string PatientId { get; set; }

    }
}
