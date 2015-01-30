using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;


namespace HealthPortal.Models
{
    public class EncounterInfoModel:IEncounterInformation
    {
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<EncounterInformation> _encounterinfo;

        public EncounterInfoModel()
            : this("")
        {

        }
        public EncounterInfoModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _encounterinfo = _database.GetCollection<EncounterInformation>("EncounterInformation");

        }
        public IEnumerable<EncounterInformation> GetAll()
        {

            return _encounterinfo.FindAll();
        }

        public IEnumerable<EncounterInformation> GetByUserId(string UserId)
        {
            IMongoQuery query = Query.EQ("UserId", UserId);
            return _encounterinfo.Find(query).AsEnumerable();

        }

        public EncounterInformation AddEncInfo(EncounterInformation EncInfo)
        {
            EncounterInformation encinfo = _encounterinfo.FindAll().OrderBy(p => p.EncounterID).Last();

            EncInfo._id = ObjectId.GenerateNewId().ToString();
            Int32 encinfoid = encinfo.EncounterID + 1;
            EncInfo.EncounterID = encinfoid;
            if (EncInfo.EncounterType == "P")
            { EncInfo.EncounterType = "I,A"; }
            _encounterinfo.Insert(EncInfo);
            return EncInfo;
        }

        public EncounterInformation GetById(int careplainid)
        {
            throw new NotImplementedException();
        }

        public void DeletePatient(string userid)
        {
            _encounterinfo.Remove(Query.EQ("UserId", userid));
        }

        public EncounterInformation UpdatePatient(EncounterInformation EncInfo)
        {
            var enciexist = GetByUserId(EncInfo.UserId);

            if (EncInfo.EncounterType == "P")
            {
                if (EncInfo.UserId != "a8a56c1d-203c-4c53-9d95-972dec7a2ef5" || EncInfo.UserId == "b6c625f5-653a-429f-b134-5b4d128ce4e8" || EncInfo.UserId == "a24e6d62-414d-4434-bbe3-b3eec3a026e7")
                {
                    EncInfo.EncounterType = "I,A";
                }

            }
            EncounterInformation enci = enciexist.Where(p => p._id == EncInfo._id).First();
            enci.AdmissionDate=EncInfo.AdmissionDate;
            enci.DischargeDate=EncInfo.DischargeDate;
            enci.Admissiondischargelocation=EncInfo.Admissiondischargelocation;

            _encounterinfo.Save(enci);

            return enci;
        }
    }
}