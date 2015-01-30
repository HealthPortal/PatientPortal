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
    public class EncounterInformation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public Int32 EncounterID { get; set; }
        public string UserId { get; set; }
        public string AdmissionDate { get; set; }
        public string DischargeDate { get; set; }
        public string Admissiondischargelocation { get; set; }
        public string EncounterType { get; set; }
    }
}