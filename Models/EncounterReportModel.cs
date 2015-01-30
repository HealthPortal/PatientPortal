using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthPortal.Models
{
    public class EncounterReportModel:IEncounterReport
    {
        
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<EncounterReport> _encounterreport;

        public EncounterReportModel()
            : this("")
        {

        }
        public EncounterReportModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _encounterreport = _database.GetCollection<EncounterReport>("EncounterReport");

        }
        public IEnumerable<EncounterReport> GetAll()
        {

            return _encounterreport.FindAll();
        }

        public IEnumerable<EncounterReport> GetByUserId(string patientid)
        {
            IMongoQuery query = Query.EQ("PatientId", patientid);
            return _encounterreport.Find(query).AsEnumerable();

        }


        public EncounterReport AddEncReport(EncounterReport EncReport)
        {
            var reports = _encounterreport.FindAll().AsEnumerable();
            EncounterReport encrp = (from rep in reports
                                   orderby rep.EncounterId descending
                                   select rep).First();

            EncReport._id = ObjectId.GenerateNewId().ToString();
            Int32 encid = encrp.EncounterId + 1;
            EncReport.EncounterId = encid;
            DateTime ddt = DateTime.Now;
            EncReport.Encounterdate = ddt;
            
            _encounterreport.Insert(EncReport);
            return EncReport;
        }

      

        public void DeletePatient(string patientid)
        {
            _encounterreport.Remove(Query.EQ("PatientId", patientid));
        }

        //public MedicationAllergie UpdatePatient(MedicationAllergie medalg)
        //{
        //    var algexist = GetByUserId(medalg.UserId);

        //    if (medalg.EncounterType == "P")
        //    {
        //        if (medalg.UserId != "a8a56c1d-203c-4c53-9d95-972dec7a2ef5" || medalg.UserId == "b6c625f5-653a-429f-b134-5b4d128ce4e8" || medalg.UserId == "a24e6d62-414d-4434-bbe3-b3eec3a026e7")
        //        {
        //            medalg.EncounterType = "I,A";
        //        }

        //    }
        //    MedicationAllergie algs = algexist.Where(p => p._id == medalg._id).First();
        //    algs.Allergen=medalg.Allergen;
        //    algs.Reaction=medalg.Reaction;
        //    algs.Status = medalg.Status;
        //    algs.RxNormCode=medalg.RxNormCode;

        //    _allergies.Save(algs);

        //    return algs;
        //}
    }
}