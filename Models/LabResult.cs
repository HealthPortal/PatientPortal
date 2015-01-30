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
    public class LabResult
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public Int32 LabId { get; set; }
        public string UserId { get; set; }
        public string OrderableTestName { get; set; }
        public string LONICCode { get; set; }
        public string TestResultValue { get; set; }
        public string DatePerformed { get; set; }
        public string EncounterType { get; set; }
    }
}