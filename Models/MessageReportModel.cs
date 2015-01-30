using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthPortal.Models
{
    public class MessageReportModel:IMessageReport
    {
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<MessageReport> _msgreport;

        public MessageReportModel()
            : this("")
        {

        }
        public MessageReportModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _msgreport = _database.GetCollection<MessageReport>("MessageReport");

        }
        public IEnumerable<MessageReport> GetAll()
        {

            return _msgreport.FindAll();
        }

        public IEnumerable<MessageReport> GetByUserId(string patientid)
        {
            IMongoQuery query = Query.EQ("SentTo", patientid);
            return _msgreport.Find(query).AsEnumerable();

        }


        public MessageReport AddMessageReport(MessageReport MsgReport)
        {
            var reports = _msgreport.FindAll().AsEnumerable();
            MessageReport msgrp = (from msg in reports
                                     orderby msg.MessageReportId descending
                                     select msg).First();

            MsgReport._id = ObjectId.GenerateNewId().ToString();
            Int32 msgid = msgrp.MessageReportId + 1;
            MsgReport.MessageReportId = msgid;
            MsgReport.Messagedate = DateTime.Now;
           
            _msgreport.Insert(MsgReport);
            return MsgReport;
        }

      

        public void DeletePatient(string patientid)
        {
            _msgreport.Remove(Query.EQ("PatientId", patientid));
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