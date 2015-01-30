using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace HealthPortal.Models
{
    public class DiagnosisModel:IDiagnosisRepository
    {
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<Diagnosi> _Diagnosi;

        public DiagnosisModel()
            : this("")
        {

        }
        public DiagnosisModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _Diagnosi = _database.GetCollection<Diagnosi>("Diagnosis");

        }

        public IEnumerable<Diagnosi> GetAll()
        {
            return _Diagnosi.FindAll().AsEnumerable();
        }


        public IEnumerable<Diagnosi> GebyUserId(string PatientId)
        {
            IMongoQuery query = Query.EQ("PatientId", PatientId);
            return _Diagnosi.Find(query).AsEnumerable();
        }

    }
}