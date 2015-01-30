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
   public class ProcedureDetail
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public Int32 Proceduredetailsid { get; set; }
        public string ProcedureName { get; set; }
        public string ProcedureDate { get; set; }
        public string SNOMEDCT { get; set; }
        public string UserId { get; set; }
        public string EncounterType { get; set; }
    }
}
