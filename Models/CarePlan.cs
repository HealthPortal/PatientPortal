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
    public class CarePlan
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public Int32 CarePlanid { get; set; }
        public string Goal { get; set; }
        public string Instructions { get; set; }
        public string SNOMEDCT { get; set; }
        public string UserId { get; set; }
        public string EncounterType { get; set; }
    }
}
