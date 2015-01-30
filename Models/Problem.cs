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
    public class Problem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public Int32 Problemsid { get; set; }
        public string ProblemCause { get; set; }
        public string Status { get; set; }
        public string ProblemReportedDate { get; set; }
        public string StandardCode { get; set; }
        public string UserId { get; set; }
        public string EncounterType { get; set; }
    }
}
