using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using System.Web.Security;

namespace HealthPortal.Models
{
    public class PhysicianModel:IPhysicianRepository
    {
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<MyPhysician> _Physicians;

        public PhysicianModel()
            : this("")
        {

        }
        public PhysicianModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _Physicians = _database.GetCollection<MyPhysician>("Myphysicians");

        }
        public IEnumerable<MyPhysician> GetAll()
        {

            return _Physicians.FindAll();
        }

        public IEnumerable<MyPhysician> GetByUserId(string UserId)
        {
            IMongoQuery query = Query.EQ("UserId", UserId);
            return _Physicians.Find(query).AsEnumerable();

        }
        
        public MyPhysician GetById(int Physicianid)
        {
            throw new NotImplementedException();
        }


        public MyPhysician AddPhy(MyPhysician physician)
        {
            MyPhysician phy = _Physicians.FindAll().OrderBy(p=>p.Physicianid).Last();
            
            physician._id = ObjectId.GenerateNewId().ToString();
            Int32 newphyid = phy.Physicianid + 1;
            physician.Physicianid = newphyid;
            MembershipUser usert = Membership.GetUser(physician.Name);
            physician.PhyUserId = usert.ProviderUserKey.ToString();
            if (physician.EncounterType == "P")
            { physician.EncounterType = "I,A"; }
            _Physicians.Insert(physician);
            return physician;
        }

        public void DeletePatient(string userid)
        {
            _Physicians.Remove(Query.EQ("UserId", userid));
        }

        public MyPhysician UpdatePatient(MyPhysician physician)
        {
            var physexist = GetByUserId(physician.UserId);
            MembershipUser usert = Membership.GetUser(physician.Name);
            if (physician.EncounterType == "P")
            {
                if (physician.UserId != "a8a56c1d-203c-4c53-9d95-972dec7a2ef5" || physician.UserId == "b6c625f5-653a-429f-b134-5b4d128ce4e8" || physician.UserId == "a24e6d62-414d-4434-bbe3-b3eec3a026e7")
                {
                    physician.EncounterType = "I,A";
                }

            }
            MyPhysician myphy = physexist.Where(p => p._id == physician._id).First();
            myphy.Name=physician.Name;
            myphy.StreetAddress=physician.StreetAddress;
            myphy.Locality=physician.Locality;
            myphy.Region=physician.Region;
            myphy.PostalCode=physician.PostalCode;
            myphy.PrimaryPhone=physician.PrimaryPhone;
            myphy.PhyUserId = usert.ProviderUserKey.ToString();
            myphy.EncounterType=physician.EncounterType;

            _Physicians.Save(myphy);

            return myphy;
        }
    }
}