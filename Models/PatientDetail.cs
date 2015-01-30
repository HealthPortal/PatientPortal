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
   public class PatientDetail
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public Int32 PhyId { get; set; }
        public string PatientName { get; set; }
        public string summary { get; set; }
        public string Age { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public string DateOfBirth { get; set; }
        public string Time { get; set; }
        public string Hvalue { get; set; }
        public string Wvalue { get; set; }
        public string PatientId { get; set; }

    }
}
