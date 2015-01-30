using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace HealthPortal.Models
{
    public class MyDayModel:IMyDayRepository
    {
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<MyDay> _MyDay;

        public MyDayModel()
            : this("")
        {

        }
        public MyDayModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _MyDay = _database.GetCollection<MyDay>("MyDay");

        }
        
        public IEnumerable<MyDay> GetAll()
        {
            return _MyDay.FindAll().AsEnumerable();
        }



        public MyDay GetbyId(int id)
        {
            IMongoQuery query = Query.EQ("MyDayId", id);
            return _MyDay.Find(query).FirstOrDefault();
        }

        public MyDay GetRemovePatient(string id)
        {
            MyDay myday = GetAll().Where(m => m.PatientId == id).First();
            IMongoQuery query = Query.EQ("PatientId", id);
            _MyDay.Remove(query);
           return myday;
        }



    }
}