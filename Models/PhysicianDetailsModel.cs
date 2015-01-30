using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace HealthPortal.Models
{
    public class PhysicianDetailsModel:IPhysicianDetailsRepository
    {
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<PhysicianImage> _phyimages;

        public PhysicianDetailsModel()
            : this("")
        {

        }
        public PhysicianDetailsModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _phyimages = _database.GetCollection<PhysicianImage>("PhysicianImages");

        }
        
        public IEnumerable<PhysicianImage> GetAll()
        {
            return _phyimages.FindAll();
        }


        public IEnumerable<PhysicianImage> GetbyId(string userid)
        {
            IMongoQuery query = Query.EQ("UserId", userid);
            return _phyimages.Find(query).AsEnumerable();

        }

    }
}