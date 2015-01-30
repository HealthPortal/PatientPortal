using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    public class EncounterReport
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public Int32 EncounterId { get; set; }
        public DateTime Encounterdate { get; set; }
        public string Action { get; set; }
        public string PatientId { get; set; }
        public string UpdateBy { get; set; }
        public string RefId { get; set; }
    }
}
