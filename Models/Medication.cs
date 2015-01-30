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
   public class Medication
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public Int32 Medicationsid { get; set; }
        public string MedicationsName { get; set; }
        public string DosageForm { get; set; }
        public string SIG { get; set; }
        public string Status { get; set; }
        public string DateOfPrescription { get; set; }
        public string StandardCode { get; set; }
        public string UserId { get; set; }
        public string EncounterType { get; set; }
    }
}
