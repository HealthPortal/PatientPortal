using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace HealthPortal.Models
{
    public class AdmissionModel:IAdmissionsRepository
    {
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<Admission> _Admission;

        public AdmissionModel()
            : this("")
        {

        }
        public AdmissionModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _Admission = _database.GetCollection<Admission>("Admissions");

        }

        public IEnumerable<Admission> GetAll()
        {
            return _Admission.FindAll().AsEnumerable();
        }



        public Admission GetbyId(int id)
        {
            IMongoQuery query = Query.EQ("AdmissionsId", id);
            return _Admission.Find(query).FirstOrDefault();
        }


        public IEnumerable<Admission> GebyUserId(string PatientId)
        {
            IMongoQuery query = Query.EQ("PatientId", PatientId);
            return _Admission.Find(query).AsEnumerable();
        }
    }
}