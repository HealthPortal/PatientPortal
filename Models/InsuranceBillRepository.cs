using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using HealthPortal.Controllers;


namespace HealthPortal.Models
{
    public class InsuranceBillRepository : IInsuranceBillRepository
    {
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<InsuranceBilling> _inbilling;

        public InsuranceBillRepository()
            : this("")
        {

        }
        public InsuranceBillRepository(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _inbilling = _database.GetCollection<InsuranceBilling>("InsuranceBilling");

        }
        public IEnumerable<InsuranceBilling> GetAll()
        {

            return _inbilling.FindAll();
        }

        public IEnumerable<InsuranceBilling> GetByUserId(string UserId)
        {
            IMongoQuery query = Query.EQ("UserId", UserId);
            return _inbilling.Find(query).AsEnumerable();

        }
        public InsuranceBilling GetById(int PatientID)
        {
            throw new NotImplementedException();
        }



        public InsuranceBilling AddInsBill(InsuranceBilling insbl)
        {
            InsuranceBilling demo = _inbilling.FindAll().OrderByDescending(p => p.InsuranceBillingid).First();

            insbl._id = ObjectId.GenerateNewId().ToString();
            Int32 insid = insbl.InsuranceBillingid + 1;
            insbl.InsuranceBillingid = insid;
            if (insbl.EncounterType == "P")
            { insbl.EncounterType = "I,A"; }
            _inbilling.Insert(insbl);
            return insbl;
        }

        public void DeletePatient(string userid)
        {
            _inbilling.Remove(Query.EQ("UserId", userid));
        }

        public InsuranceBilling UpdateInsBill(InsuranceBilling insbill)
        {
            var inbillex = GetByUserId(insbill.UserId);

            if (insbill.EncounterType == "P")
            {
                if (insbill.UserId != "a8a56c1d-203c-4c53-9d95-972dec7a2ef5" || insbill.UserId == "b6c625f5-653a-429f-b134-5b4d128ce4e8" || insbill.UserId == "a24e6d62-414d-4434-bbe3-b3eec3a026e7")
                {
                    insbill.EncounterType = "I,A";
                }

            }
            InsuranceBilling ins = inbillex.Where(p => p._id == insbill._id).First();
            ins.Balance = insbill.Balance;
            ins.Charges = insbill.Charges;
            ins.Date = insbill.Date;
            ins.Diagnosis = insbill.Diagnosis;
            ins.ICDCode = insbill.ICDCode;
            ins.Payments = insbill.Payments;
            ins.Image1 = insbill.Image1;
            ins.Image2 = insbill.Image2;
            _inbilling.Save(ins);

            return ins;

        }


    }
}