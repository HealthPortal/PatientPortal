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
    public class Allergy
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public Int32 AllergiesId { get; set; }
        public string Name { get; set; }
        public string Severity { get; set; }
        public string Reaction { get; set; }
        public string PatientId { get; set; }

    }
}
