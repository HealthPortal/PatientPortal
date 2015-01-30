using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace HealthPortal.Models
{
    public class SocialHistoryModel:ISocialHistory
    {
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<SocialHistory> _socialhistory;

        public SocialHistoryModel()
            : this("")
        {

        }
        public SocialHistoryModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _socialhistory = _database.GetCollection<SocialHistory>("SocialHistory");

        }
        public IEnumerable<SocialHistory> GetAll()
        {

            return _socialhistory.FindAll();
        }

        public IEnumerable<SocialHistory> GetByUserId(string UserId)
        {
            IMongoQuery query = Query.EQ("UserId", UserId);
            return _socialhistory.Find(query).AsEnumerable();

        }

        public SocialHistory AddHistory(SocialHistory Sochistry)
        {
            SocialHistory soc = _socialhistory.FindAll().OrderBy(p => p.SocialHistId).Last();

            Sochistry._id = ObjectId.GenerateNewId().ToString();
            Int32 sochistid = soc.SocialHistId + 1;
            Sochistry.SocialHistId = sochistid;
            if (Sochistry.EncounterType == "P")
            { Sochistry.EncounterType = "I,A"; }
            _socialhistory.Insert(Sochistry);
            return Sochistry;
        }

        public SocialHistory GetById(int careplainid)
        {
            throw new NotImplementedException();
        }

        public void DeletePatient(string userid)
        {
            _socialhistory.Remove(Query.EQ("UserId", userid));
        }


        public SocialHistory UpdatePatient(SocialHistory SocialHistory)
        {
            var sochistryexist = GetByUserId(SocialHistory.UserId);

            if (SocialHistory.EncounterType == "P")
            {
                if (SocialHistory.UserId != "a8a56c1d-203c-4c53-9d95-972dec7a2ef5" || SocialHistory.UserId == "b6c625f5-653a-429f-b134-5b4d128ce4e8" || SocialHistory.UserId == "a24e6d62-414d-4434-bbe3-b3eec3a026e7")
                {
                    SocialHistory.EncounterType = "I,A";
                }

            }
            SocialHistory socht = sochistryexist.Where(p => p._id == SocialHistory._id).First();
            socht.SocialHistoryItem = SocialHistory.SocialHistoryItem;
            socht.Description = SocialHistory.Description;
            socht.SNOMEDCT = SocialHistory.SNOMEDCT;

            _socialhistory.Save(socht);

            return socht;
        }
    }
}