using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace HealthPortal.Models
{
    public class DemographicsModel:IdemographicsRepository
    {
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<Demographic> _Demographic;

        public DemographicsModel()
            : this("")
        {

        }
        public DemographicsModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _Demographic = _database.GetCollection<Demographic>("Demographics");

        }

        public IEnumerable<Demographic> GebyUserId(string PatientId)
        {
            IMongoQuery query = Query.EQ("PatientId", PatientId);
            return _Demographic.Find(query).AsEnumerable();
        }

        public IEnumerable<Demographic> GetAll()
        {
            return _Demographic.FindAll();
        }

        public Demographic AddDemo(Demographic demographic)
        {
            Demographic demo = _Demographic.FindAll().OrderBy(p => p.DemoId).Last();

            demographic._id = ObjectId.GenerateNewId().ToString();
            Int32 demoid = demo.DemoId + 1;
            demographic.DemoId = demoid;
          
            _Demographic.Insert(demographic);
            return demographic;
        }
    }
}