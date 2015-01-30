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
   public class Immunization
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public Int32 ImmunizationID { get; set; }
        public string DATE { get; set; }
        public string NAME { get; set; }
        public string PROVIDER { get; set; }
        public string INSTRUCTIONS { get; set; }
        public string TYPE { get; set; }
        public string Dose { get; set; }
        public string Rate { get; set; }
        public string PatientId { get; set; }

    }
}
