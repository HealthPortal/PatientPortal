using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace HealthPortal.Models
{
    public class ReasonHospitalModel:IReasonhospitalizaion
    {
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<ReasonForHospitalization> _reasonhospital;

        public ReasonHospitalModel()
            : this("")
        {

        }
        public ReasonHospitalModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _reasonhospital = _database.GetCollection<ReasonForHospitalization>("ReasonForHospitalization");

        }
        public IEnumerable<ReasonForHospitalization> GetAll()
        {

            return _reasonhospital.FindAll();
        }

        public IEnumerable<ReasonForHospitalization> GetByUserId(string UserId)
        {
            IMongoQuery query = Query.EQ("UserId", UserId);
            return _reasonhospital.Find(query).AsEnumerable();

        }

        public ReasonForHospitalization AddReasHosp(ReasonForHospitalization ReasHos)
        {
            ReasonForHospitalization reashosp = _reasonhospital.FindAll().OrderBy(p => p.ReasonId).Last();

            ReasHos._id = ObjectId.GenerateNewId().ToString();
            Int32 reasonid = reashosp.ReasonId + 1;
            ReasHos.ReasonId = reasonid;
            if (ReasHos.EncounterType == "P")
            { ReasHos.EncounterType = "I,A"; }
            _reasonhospital.Insert(ReasHos);
            return ReasHos;
        }

        public ReasonForHospitalization GetById(int careplainid)
        {
            throw new NotImplementedException();
        }

        public void DeletePatient(string userid)
        {
            _reasonhospital.Remove(Query.EQ("UserId", userid));
        }


        public ReasonForHospitalization UpdateReason(ReasonForHospitalization Reason)
        {
            var reasexist = GetByUserId(Reason.UserId);

            if (Reason.EncounterType == "P")
            {
                if (Reason.UserId != "a8a56c1d-203c-4c53-9d95-972dec7a2ef5" || Reason.UserId == "b6c625f5-653a-429f-b134-5b4d128ce4e8" || Reason.UserId == "a24e6d62-414d-4434-bbe3-b3eec3a026e7")
                {
                    Reason.EncounterType = "I,A";
                }
                 
            }
            ReasonForHospitalization reas = reasexist.Where(p => p._id == Reason._id).First();
            reas.Reason = Reason.Reason;
            reas.EncounterType = Reason.EncounterType;

            _reasonhospital.Save(reas);

            return reas;
        }
    }
}