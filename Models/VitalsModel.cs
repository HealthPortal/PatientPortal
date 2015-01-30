using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Web.Security;

namespace HealthPortal.Models
{
    public class VitalsModel:IVitalsRepository
    {
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<VitalSign> _vitals;

        public VitalsModel()
            : this("")
        {

        }
        public VitalsModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _vitals = _database.GetCollection<VitalSign>("VitalSign");

        }
        public IEnumerable<VitalSign> GetAll()
        {

            return _vitals.FindAll();
        }

        public IEnumerable<VitalSign> GetByUserId(string UserId)
        {
            IMongoQuery query = Query.EQ("UserId", UserId);
            return _vitals.Find(query).AsEnumerable();

        }
        public VitalSign AddVitals(VitalSign vsign)
        {
            VitalSign vitals = _vitals.FindAll().OrderBy(p => p.VitalsId).Last();

            vsign._id = ObjectId.GenerateNewId().ToString();
            Int32 newvitid = vitals.VitalsId + 1;
            vsign.VitalsId = newvitid;
            if (vsign.EncounterType == "P")
            { vsign.EncounterType = "I,A"; }
            _vitals.Insert(vsign);
            return vsign;
        }



        public VitalSign GetById(int Vitalsid)
        {
            throw new NotImplementedException();
        }

        public void DeletePatient(string userid)
        {
            _vitals.Remove(Query.EQ("UserId", userid));
        }

        public VitalSign UpdatePatient(VitalSign Vitalsign)
        {
            var vitalexist = GetByUserId(Vitalsign.UserId);

            if (Vitalsign.EncounterType == "P")
            {
                if (Vitalsign.UserId != "a8a56c1d-203c-4c53-9d95-972dec7a2ef5" || Vitalsign.UserId == "b6c625f5-653a-429f-b134-5b4d128ce4e8" || Vitalsign.UserId == "a24e6d62-414d-4434-bbe3-b3eec3a026e7")
                {
                    Vitalsign.EncounterType = "I,A";
                }

            }
            VitalSign vitals = vitalexist.Where(p => p._id == Vitalsign._id).First();
            vitals.BMI=Vitalsign.BMI;
            vitals.BloodPressure=Vitalsign.BloodPressure;
            vitals.Height=Vitalsign.Height;
            vitals.Weight=Vitalsign.Weight;
            vitals.RecordedDate=Vitalsign.RecordedDate;

            _vitals.Save(vitals);

            return vitals;
        }
    }
}