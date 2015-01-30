using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace HealthPortal.Models
{
    public class PatientDetailsModel:IPatientdetailRepository
    {
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<PatientDetail> _PatientDetail;

        public PatientDetailsModel()
            : this("")
        {

        }
        public PatientDetailsModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _PatientDetail = _database.GetCollection<PatientDetail>("PatientDetails");

        }

        public IEnumerable<PatientDetail> GetAll()
        {
            return _PatientDetail.FindAll().AsEnumerable();
        }



        public PatientDetail GetbyId(int id)
        {
            IMongoQuery query = Query.EQ("PhyId", id);
            return _PatientDetail.Find(query).FirstOrDefault();
        }


        public IEnumerable<PatientDetail> GebyUserId(string PatientId)
        {
            IMongoQuery query = Query.EQ("PatientId", PatientId);
            return _PatientDetail.Find(query).AsEnumerable();
        }

    }
}