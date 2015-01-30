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
    public class MyPhysician
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public Int32 Physicianid { get; set; }
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string StreetAddress2 { get; set; }
        public string Locality { get; set; }
        public string Region { get; set; }
        public Int32 PostalCode { get; set; }
        public string Country { get; set; }
        public string PrimaryPhone { get; set; }
        public string Department { get; set; }
        public string HospitalName { get; set; }
        public string UserId { get; set; }
        public string PhyUserId { get; set; }
        public string EncounterType { get; set; }
    }
}