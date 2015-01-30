using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace HealthPortal.Models
{
    public class ConversationModel:IConversationRepository
    {
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<ConversationsDetail> _ConversationsDetail;

        public ConversationModel()
            : this("")
        {

        }
        public ConversationModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _ConversationsDetail = _database.GetCollection<ConversationsDetail>("ConversationsDetails");

        }

        public IEnumerable<ConversationsDetail> GetAll()
        {
            return _ConversationsDetail.FindAll().AsEnumerable();
        }



        public ConversationsDetail GetbyId(int id)
        {
            IMongoQuery query = Query.EQ("ConversationId", id);
            return _ConversationsDetail.Find(query).FirstOrDefault();
        }

    }
}