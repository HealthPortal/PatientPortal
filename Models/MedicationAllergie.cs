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
    public class MedicationAllergie
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public Int32 MedicationAllergiesid { get; set; }
        public string Allergen { get; set; }
        public string Reaction { get; set; }
        public string Status { get; set; }
        public string RxNormCode { get; set; }
        public string UserId { get; set; }
        public string EncounterType { get; set; }
    }
}
