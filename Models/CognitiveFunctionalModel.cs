using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;


namespace HealthPortal.Models
{
    public class CognitiveFunctionalModel:ICognitiveFunctionalStatus
    {
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<CognitiveAndFunctionalStatus> _cognitiveFunc;

        public CognitiveFunctionalModel()
            : this("")
        {

        }
        public CognitiveFunctionalModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _cognitiveFunc = _database.GetCollection<CognitiveAndFunctionalStatus>("CognitiveAndFunctionalStatus");

        }
        public IEnumerable<CognitiveAndFunctionalStatus> GetAll()
        {

            return _cognitiveFunc.FindAll();
        }

        public IEnumerable<CognitiveAndFunctionalStatus> GetByUserId(string UserId)
        {
            IMongoQuery query = Query.EQ("UserId", UserId);
            return _cognitiveFunc.Find(query).AsEnumerable();

        }
        public CognitiveAndFunctionalStatus AddCogFunStat(CognitiveAndFunctionalStatus CogFunStat)
        {
            CognitiveAndFunctionalStatus cogfunst = _cognitiveFunc.FindAll().OrderBy(p => p.CognitiveStatusId).Last();

            CogFunStat._id = ObjectId.GenerateNewId().ToString();

            Int32 cogid = cogfunst.CognitiveStatusId + 1;
            CogFunStat.CognitiveStatusId = cogid;
            if(CogFunStat.EncounterType=="P")
            { CogFunStat.EncounterType = "I,A"; }
            _cognitiveFunc.Insert(CogFunStat);
            return CogFunStat;
        }


        public CognitiveAndFunctionalStatus GetById(int careplainid)
        {
            throw new NotImplementedException();
        }

        public void DeletePatient(string userid)
        {
            _cognitiveFunc.Remove(Query.EQ("UserId", userid));
        }

        public CognitiveAndFunctionalStatus UpdatePatient(CognitiveAndFunctionalStatus cognfstat)
        {
            var cogfexist = GetByUserId(cognfstat.UserId);

            if (cognfstat.EncounterType == "P")
            {
                if (cognfstat.UserId != "a8a56c1d-203c-4c53-9d95-972dec7a2ef5" || cognfstat.UserId == "b6c625f5-653a-429f-b134-5b4d128ce4e8" || cognfstat.UserId == "a24e6d62-414d-4434-bbe3-b3eec3a026e7")
                {
                    cognfstat.EncounterType = "I,A";
                }
                 
            }
            CognitiveAndFunctionalStatus cogfs = cogfexist.Where(p => p._id == cognfstat._id).First();
            cogfs.Description=cognfstat.Description;
            cogfs.SNOMEDCT=cognfstat.SNOMEDCT;
            cogfs.ReportedDate=cognfstat.ReportedDate;
            cogfs.Status=cognfstat.Status;
            
            _cognitiveFunc.Save(cogfs);

            return cogfs;
        }
    }
}