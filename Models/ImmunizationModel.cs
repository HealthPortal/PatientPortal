using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace HealthPortal.Models
{
    public class ImmunizationModel:IImmunizationsRepository
    {
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<Immunization> _Immunization;

        public ImmunizationModel()
            : this("")
        {

        }
        public ImmunizationModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _Immunization = _database.GetCollection<Immunization>("Immunizations");

        }

        public IEnumerable<Immunization> GetbyUserId(string PatientId)
        {
            IMongoQuery query = Query.EQ("PatientId", PatientId);
            return _Immunization.Find(query).AsEnumerable();
        }


        public IEnumerable<Immunization> GetAll()
        {
            return _Immunization.FindAll();
        }

        public Immunization AddImmu(Immunization immunization)
        {
            Immunization imn = _Immunization.FindAll().OrderBy(p => p.ImmunizationID).Last();

            immunization._id = ObjectId.GenerateNewId().ToString();
            Int32 immuid = imn.ImmunizationID + 1;
            immunization.ImmunizationID = immuid;

            _Immunization.Insert(immunization);

            return immunization;
        }

        public void DeletePatient(string userid)
        {
            _Immunization.Remove(Query.EQ("PatientId", userid));
        }

        public Immunization UpdatePatient(Immunization immunization)
        {
            var immuexist = GetbyUserId(immunization.PatientId);

            Immunization immu = immuexist.Where(p => p._id == immunization._id).First();
            immu.DATE=immunization.DATE;
            immu.NAME=immunization.NAME;
            immu.PROVIDER=immunization.PROVIDER;
            immu.INSTRUCTIONS=immunization.INSTRUCTIONS;
            immu.TYPE=immunization.TYPE;
            immu.Dose=immunization.Dose;
            immu.Rate=immunization.Rate;
                         
           _Immunization.Save(immu);

            return immu;
        }
    }
}