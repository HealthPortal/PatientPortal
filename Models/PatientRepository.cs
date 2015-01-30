using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace HealthPortal.Models
{
    public class PatientRepository:IPatientRepository
    {
        
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<Patient> _patients;

        public PatientRepository()
            : this("")
        {

        }
        public PatientRepository(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:amit123@ds027519.mongolab.com:27519/patient";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("patient", SafeMode.True);
            _patients = _database.GetCollection<Patient>("newpatient");

        }
        public IEnumerable<Patient> GetAll()
        {
            return _patients.FindAllAs<Patient>();
            //return _patients.FindAll().ToList();
        }

        public IEnumerable<Patient> GetByUserId(string UserId)
        {
            IMongoQuery query = Query.EQ("UserId", UserId);
            return _patients.Find(query).AsEnumerable();

        }



        public Patient GetById(int patientid)
        {
            throw new NotImplementedException();
        }
    }
}