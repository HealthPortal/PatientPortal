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
    public class CognitiveAndFunctionalStatus
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public Int32 CognitiveStatusId { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public string SNOMEDCT { get; set; }
        public string ReportedDate { get; set; }
        public string Status { get; set; }
        public string EncounterType { get; set; }
    }
}