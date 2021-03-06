﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;


namespace HealthPortal.Models
{
    public class MyDay
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public Int32 MyDayId { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Time { get; set; }
        public string UserId { get; set; }
        public string PatientId { get; set; }


    }
}
