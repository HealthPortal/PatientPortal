using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson.Serialization.Attributes;

namespace HealthPortal.Models
{
    public class InsuranceBilling
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public Int32 InsuranceBillingid { get; set; }
        public string Date { get; set; }
        public string Diagnosis { get; set; }
        public string ICDCode { get; set; }
        public float Charges { get; set; }
        public float Payments { get; set; }
        public float Balance { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string UserId { get; set; }
        public string EncounterType { get; set; }
    }
}