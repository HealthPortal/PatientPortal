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
   public class Demographic
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public Int32 DemoId { get; set; }
        public string Name { get; set; }
        public string NextOfKin { get; set; }
        public string Address { get; set; }
        public string PrimaryPhone { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public string Age { get; set; }
        public string PatientId { get; set; }

    }
}
