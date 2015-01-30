using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;


namespace HealthPortal.Models
{
    public class MessagesModel:IMessagesRepository
    {
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<MessagesDetail> _messages;

        public MessagesModel()
            : this("")
        {

        }
        public MessagesModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _messages = _database.GetCollection<MessagesDetail>("MessagesDetails");

        }
        public IEnumerable<MessagesDetail> GetAll()
        {

            return _messages.FindAll();
        }

        public IEnumerable<MessagesDetail> GetByUserId(string UserId)
        {
            IMongoQuery query = Query.EQ("UserId", UserId);
            return _messages.Find(query).AsEnumerable();

        }



        public MessagesDetail GetById(int MessageID)
        {
            throw new NotImplementedException();
        }
    }
}