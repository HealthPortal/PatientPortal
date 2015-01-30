using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace HealthPortal.Models
{
    public class LabsModel:ILabRepository
    {
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<Lab> _Lab;

        public LabsModel()
            : this("")
        {

        }
        public LabsModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _Lab = _database.GetCollection<Lab>("Labs");

        }

        public IEnumerable<Lab> GebyUserId(string PatientId)
        {
            IMongoQuery query = Query.EQ("PatientId", PatientId);
            return _Lab.Find(query).AsEnumerable();
        }

        public IEnumerable<Lab> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}