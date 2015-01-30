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
   public class Vaccination
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public Int32 Vaccinationid { get; set; }
        public string VaccineName { get; set; }
        public string VaccineStatus { get; set; }
        public string DateAdministered { get; set; }
        public string CVXCode { get; set; }
        public string UserId { get; set; }
        public string EncounterType { get; set; }
    }
}
