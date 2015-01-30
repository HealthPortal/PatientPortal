using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace HealthPortal.Models
{
    public class AllergyModel:IAllergyRepository
    {
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<MedicationAllergie> _allergies;

        public AllergyModel()
            : this("")
        {

        }
        public AllergyModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _allergies = _database.GetCollection<MedicationAllergie>("MedicationAllergie");

        }
        public IEnumerable<MedicationAllergie> GetAll()
        {

            return _allergies.FindAll();
        }

        public IEnumerable<MedicationAllergie> GetByUserId(string UserId)
        {
            IMongoQuery query = Query.EQ("UserId", UserId);
            return _allergies.Find(query).AsEnumerable();

        }


        public MedicationAllergie AddAllergie(MedicationAllergie medallerg)
        {
            MedicationAllergie alg = _allergies.FindAll().OrderBy(p => p.MedicationAllergiesid).Last();

            medallerg._id = ObjectId.GenerateNewId().ToString();
            Int32 medallgid = alg.MedicationAllergiesid + 1;
            medallerg.MedicationAllergiesid = medallgid;
            if (medallerg.EncounterType == "P")
            { medallerg.EncounterType = "I,A"; }
            _allergies.Insert(medallerg);
            return medallerg;
        }

        public MedicationAllergie GetById(int allergyid)
        {
            throw new NotImplementedException();
        }

        public void DeletePatient(string userid)
        {
            _allergies.Remove(Query.EQ("UserId", userid));
        }

        public MedicationAllergie UpdatePatient(MedicationAllergie medalg)
        {
            var algexist = GetByUserId(medalg.UserId);

            if (medalg.EncounterType == "P")
            {
                if (medalg.UserId != "a8a56c1d-203c-4c53-9d95-972dec7a2ef5" || medalg.UserId == "b6c625f5-653a-429f-b134-5b4d128ce4e8" || medalg.UserId == "a24e6d62-414d-4434-bbe3-b3eec3a026e7")
                {
                    medalg.EncounterType = "I,A";
                }

            }
            MedicationAllergie algs = algexist.Where(p => p._id == medalg._id).First();
            algs.Allergen=medalg.Allergen;
            algs.Reaction=medalg.Reaction;
            algs.Status = medalg.Status;
            algs.RxNormCode=medalg.RxNormCode;

            _allergies.Save(algs);

            return algs;
        }
    }
}