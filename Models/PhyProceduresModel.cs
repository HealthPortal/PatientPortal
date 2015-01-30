using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace HealthPortal.Models
{
    public class PhyProceduresModel:IPhyProceduresRepository
    {
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<Procedure> _Procedure;

        public PhyProceduresModel()
            : this("")
        {

        }
        public PhyProceduresModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _Procedure = _database.GetCollection<Procedure>("Procedures");

        }

        public IEnumerable<Procedure> GebyUserId(string PatientId)
        {
            IMongoQuery query = Query.EQ("PatientId", PatientId);
            return _Procedure.Find(query).AsEnumerable();
        }

        public IEnumerable<Procedure> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}