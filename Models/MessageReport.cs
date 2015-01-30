using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    public class MessageReport
    {
        //public MessageReport()
        //{
        //    Messagedate = DateTime.Now;
        //}

        //private DateTime date;

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public Int32 MessageReportId { get; set; }
        //[BsonElement("Messagedate")]
        //public DateTime Messagedate
        //{
        //    get { return date.ToLocalTime(); }
        //    set { date = value; }
        //}
        public string SentBy { get; set; }
        public string SentTo { get; set; }
        public DateTime Messagedate { get; set; }
    }
}
