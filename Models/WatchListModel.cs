using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace HealthPortal.Models
{
    public class WatchListModel:IWatchListRepository
    {
         MongoServer _server;
        MongoDatabase _database;
        MongoCollection<WatchList> _WatchList;

        public WatchListModel()
            : this("")
        {

        }
        public WatchListModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _WatchList = _database.GetCollection<WatchList>("WatchList");

        }

        public IEnumerable<WatchList> GetAll()
        {
            return _WatchList.FindAll().AsEnumerable();
        }



        public WatchList GetbyId(int id)
        {
            IMongoQuery query = Query.EQ("WatchListId", id);
            return _WatchList.Find(query).FirstOrDefault();
        }

        public WatchList AddToWatchlist(WatchList watchlist)
        {
             _WatchList.Insert(watchlist);
             return watchlist;
            
        }

        public void DeletePatient(string userid)
        {
            _WatchList.Remove(Query.EQ("PatientId", userid));
        }
    }
}