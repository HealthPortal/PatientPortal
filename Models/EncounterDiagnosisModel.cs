using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;


namespace HealthPortal.Models
{
    public class EncounterDiagnosisModel:IEncounterDiagnosis
    {
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<EncounterDiagnosis> _encounterdiagno;

        public EncounterDiagnosisModel()
            : this("")
        {

        }
        public EncounterDiagnosisModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _encounterdiagno = _database.GetCollection<EncounterDiagnosis>("EncounterDiagnosis");

        }
        public IEnumerable<EncounterDiagnosis> GetAll()
        {

            return _encounterdiagno.FindAll();
        }

        public IEnumerable<EncounterDiagnosis> GetByUserId(string UserId)
        {
            IMongoQuery query = Query.EQ("UserId", UserId);
            return _encounterdiagno.Find(query).AsEnumerable();

        }

        public EncounterDiagnosis AddEncDiag(EncounterDiagnosis EncDiag)
        {
            EncounterDiagnosis encdiag = _encounterdiagno.FindAll().OrderBy(p => p.DiagnosisId).Last();

            EncDiag._id = ObjectId.GenerateNewId().ToString();
            Int32 diagid = encdiag.DiagnosisId + 1;
            EncDiag.DiagnosisId = diagid;
            if (EncDiag.EncounterType == "P")
            { EncDiag.EncounterType = "I,A"; }

            _encounterdiagno.Insert(EncDiag);
            return EncDiag;
        }

        public EncounterDiagnosis GetById(int careplainid)
        {
            throw new NotImplementedException();
        }

        public void DeletePatient(string userid)
        {
            _encounterdiagno.Remove(Query.EQ("UserId", userid));
        }

        public EncounterDiagnosis UpdatePatient(EncounterDiagnosis EncDiag)
        {
            var encdexist = GetByUserId(EncDiag.UserId);

            if (EncDiag.EncounterType == "P")
            {
                if (EncDiag.UserId != "a8a56c1d-203c-4c53-9d95-972dec7a2ef5" || EncDiag.UserId == "b6c625f5-653a-429f-b134-5b4d128ce4e8" || EncDiag.UserId == "a24e6d62-414d-4434-bbe3-b3eec3a026e7")
                {
                    EncDiag.EncounterType = "I,A";
                }

            }
            EncounterDiagnosis encd = encdexist.Where(p => p._id == EncDiag._id).First();
            encd.Diagnosis=EncDiag.Diagnosis;
            encd.SNOMEDCT=EncDiag.SNOMEDCT;
            encd.Status=EncDiag.Status;
            encd.StartDate=EncDiag.StartDate;

            _encounterdiagno.Save(encd);

            return encd;
        }
    }
}