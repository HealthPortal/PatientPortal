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
    public class MessagesDetail
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public Int32 MessageID { get; set; }
        public string MessageDateTime { get; set; }
        public Int32 MessageSequenceNumber { get; set; }
        public string MessageText { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public string Unread { get; set; }
        public string UserId { get; set; }
    
    }
}
