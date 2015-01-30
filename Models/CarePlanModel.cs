using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;


namespace HealthPortal.Models
{
    public class CarePlanModel:ICarePlanRepository
    {
     MongoServer _server;
        MongoDatabase _database;
        MongoCollection<CarePlan> _Careplans;

        public CarePlanModel()
            : this("")
        {

        }
        public CarePlanModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _Careplans = _database.GetCollection<CarePlan>("CarePlan");

        }
        public IEnumerable<CarePlan> GetAll()
        {

            return _Careplans.FindAll();
        }

        public IEnumerable<CarePlan> GetByUserId(string UserId)
        {
            IMongoQuery query = Query.EQ("UserId", UserId);
            return _Careplans.Find(query).AsEnumerable();

        }
        public CarePlan AddCareplan(CarePlan cplan)
        {
            CarePlan phy = _Careplans.FindAll().OrderBy(p => p.CarePlanid).Last();

            cplan._id = ObjectId.GenerateNewId().ToString();
            Int32 carepl = phy.CarePlanid + 1;
            cplan.CarePlanid = carepl;
            if (cplan.EncounterType == "P")
            { cplan.EncounterType = "I,A"; }
            _Careplans.Insert(cplan);
            return cplan;
        }

        public CarePlan GetById(int careplainid)
        {
            throw new NotImplementedException();
        }

        public void DeletePatient(string userid)
        {
            _Careplans.Remove(Query.EQ("UserId", userid));
        }

        public CarePlan UpdatePatient(CarePlan CarePlan)
        {
            var carepexist = GetByUserId(CarePlan.UserId);

            if (CarePlan.EncounterType == "P")
            {
                if (CarePlan.UserId != "a8a56c1d-203c-4c53-9d95-972dec7a2ef5" || CarePlan.UserId == "b6c625f5-653a-429f-b134-5b4d128ce4e8" || CarePlan.UserId == "a24e6d62-414d-4434-bbe3-b3eec3a026e7")
                {
                    CarePlan.EncounterType = "I,A";
                }

            }
            CarePlan cares = carepexist.Where(p => p._id == CarePlan._id).First();
            cares.Goal = CarePlan.Goal;
            cares.Instructions = CarePlan.Instructions;
            cares.SNOMEDCT = CarePlan.SNOMEDCT;
            _Careplans.Save(cares);

            return cares;
        }
    }
}