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
    public class Appointment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public Int32 AppointmentsID { get; set; }
        public string Physician { get; set; }
        public string CenterName { get; set; }
        public string Date { get; set; }
        public string Address { get; set; }
        public string Reason { get; set; }
        public string Time { get; set; }
        public string UserId { get; set; }
        public string EncounterType { get; set; }
    }
}