using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace HealthPortal.Models
{
    public class LabResultModel:ILabResult
    {
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<LabResult> _labresult;

        public LabResultModel()
            : this("")
        {

        }
        public LabResultModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _labresult = _database.GetCollection<LabResult>("LabResult");

        }
        public IEnumerable<LabResult> GetAll()
        {

            return _labresult.FindAll();
        }

        public IEnumerable<LabResult> GetByUserId(string UserId)
        {
            IMongoQuery query = Query.EQ("UserId", UserId);
            return _labresult.Find(query).AsEnumerable();

        }

        public LabResult AddLabRes(LabResult LabRes)
        {
            LabResult labres = _labresult.FindAll().OrderBy(p => p.LabId).Last();

            LabRes._id = ObjectId.GenerateNewId().ToString();
            Int32 labid = labres.LabId + 1;
            LabRes.LabId = labid;
            if (LabRes.EncounterType == "P")
            { LabRes.EncounterType = "I,A"; }
            _labresult.Insert(LabRes);
            return LabRes;
        }

        public LabResult GetById(string refid)
        {
            ObjectId newrefid = ObjectId.Parse(refid);
            IMongoQuery query = Query.EQ("_id", newrefid);
            return _labresult.Find(query).FirstOrDefault();
        }

        public void DeletePatient(string userid)
        {
            _labresult.Remove(Query.EQ("UserId", userid));
        }

        public LabResult UpdatePatient(LabResult labsres)
        {
            var labreexist = GetByUserId(labsres.UserId);

            if (labsres.EncounterType == "P")
            {
                if (labsres.UserId != "a8a56c1d-203c-4c53-9d95-972dec7a2ef5" || labsres.UserId == "b6c625f5-653a-429f-b134-5b4d128ce4e8" || labsres.UserId == "a24e6d62-414d-4434-bbe3-b3eec3a026e7")
                {
                    labsres.EncounterType = "I,A";
                }

            }
            LabResult labs = labreexist.Where(p => p._id == labsres._id).First();
            labs.OrderableTestName=labsres.OrderableTestName;
            labs.LONICCode=labsres.LONICCode;
            labs.TestResultValue=labsres.TestResultValue;
            labs.DatePerformed=labsres.DatePerformed;

            _labresult.Save(labs);

            return labs;
        }
    }
}