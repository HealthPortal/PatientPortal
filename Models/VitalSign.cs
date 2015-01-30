using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace HealthPortal.Models
{
    public class VitalSign
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public Int32 VitalsId { get; set; }
        public Int32 Height { get; set; }
        public Int32 Weight { get; set; }
        public string BloodPressure { get; set; }
        public float BMI { get; set; }
        public string RecordedDate { get; set; }
        public string UserId { get; set; }
        public string EncounterType { get; set; }
    }
}