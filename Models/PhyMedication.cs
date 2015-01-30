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
    public class PhyMedication
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public Int32 MedicationsId { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public string Refills { get; set; }
        public string Prescriber { get; set; }
        public string Instructions { get; set; }
        public string Type { get; set; }
        public string Doseage { get; set; }
        public string Rate { get; set; }
        public string PatientId { get; set; }

    }
}
