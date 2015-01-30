using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace HealthPortal.Models
{
    public class EncountersModel:IEncountersRepository
    {
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<Encounter> _Encounter;

        public EncountersModel()
            : this("")
        {

        }
        public EncountersModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _Encounter = _database.GetCollection<Encounter>("Encounters");

        }

        public IEnumerable<Encounter> GebyUserId(string PatientId)
        {
            IMongoQuery query = Query.EQ("PatientId", PatientId);
            return _Encounter.Find(query).AsEnumerable();
        }



        public IEnumerable<Encounter> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}