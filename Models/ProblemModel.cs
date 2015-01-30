using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;


namespace HealthPortal.Models
{
    public class ProblemModel:IProblemRepository
    {
         MongoServer _server;
        MongoDatabase _database;
        MongoCollection<Problem> _Problems;

        public ProblemModel()
            : this("")
        {

        }
        public ProblemModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _Problems = _database.GetCollection<Problem>("Problem");

        }
        public IEnumerable<Problem> GetAll()
        {

            return _Problems.FindAll();
        }

        public IEnumerable<Problem> GetByUserId(string UserId)
        {
            IMongoQuery query = Query.EQ("UserId", UserId);
            return _Problems.Find(query).AsEnumerable();

        }

        public Problem AddProb(Problem prob)
        {
            Problem prbl = _Problems.FindAll().OrderBy(p => p.Problemsid).Last();

            prob._id = ObjectId.GenerateNewId().ToString();
            Int32 problemid = prbl.Problemsid + 1;
            prob.Problemsid = problemid;
            if (prob.EncounterType == "P")
            { prob.EncounterType = "I,A"; }
            _Problems.Insert(prob);
            return prob;
        }


        public Problem GetById(int Problemid)
        {
            throw new NotImplementedException();
        }

        public void DeletePatient(string userid)
        {
            _Problems.Remove(Query.EQ("UserId", userid));
        }

        public Problem UpdatePatient(Problem Prob)
        {
            var probexist = GetByUserId(Prob.UserId);

            if (Prob.EncounterType == "P")
            {
                if (Prob.UserId != "a8a56c1d-203c-4c53-9d95-972dec7a2ef5" || Prob.UserId == "b6c625f5-653a-429f-b134-5b4d128ce4e8" || Prob.UserId == "a24e6d62-414d-4434-bbe3-b3eec3a026e7")
                {
                    Prob.EncounterType = "I,A";
                }

            }
            Problem probs = probexist.Where(p => p._id == Prob._id).First();
            probs.ProblemCause=Prob.ProblemCause;
            probs.Status = Prob.Status;
            probs.ProblemReportedDate=Prob.ProblemReportedDate;
            probs.StandardCode=Prob.StandardCode;

            _Problems.Save(probs);

            return probs;
        }
    }
}