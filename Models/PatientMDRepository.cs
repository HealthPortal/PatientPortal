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
    public class PatientMDRepository:IPatientMDRepository
    {
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<PatientMD> _Patients;
       
        public PatientMDRepository()
            : this("")
        {

        }
        public PatientMDRepository(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _Patients = _database.GetCollection<PatientMD>("PatientDemoGraphic");

        }
        public IEnumerable<PatientMD> GetAll()
        {

            return _Patients.FindAll();
        }

        public IEnumerable<PatientMD> GetByUserId(string UserId)
        {
            IMongoQuery query = Query.EQ("UserId",UserId);
            return _Patients.Find(query).AsEnumerable();
        
        }
        public PatientMD GetById(int PatientID)
        {
            throw new NotImplementedException();
        }



        public PatientMD AddPatDemo(PatientMD patmd)
        {
            PatientMD demo = _Patients.FindAll().OrderBy(p => p.PatientID).Last();

            patmd._id = ObjectId.GenerateNewId().ToString();
            Int32 patid = patmd.PatientID + 1;
            patmd.PatientID = patid;
            if (patmd.EncounterType == "P")
            { patmd.EncounterType = "I,A"; }
            _Patients.Insert(patmd);
            return patmd;
        }

        public void DeletePatient(string userid)
        {
            _Patients.Remove(Query.EQ("UserId",userid));
        }

        public PatientMD UpdatePatient(PatientMD PatientMd)
        {
            var patexist = GetByUserId(PatientMd.UserId);

            if (PatientMd.EncounterType == "P")
            {
                if (PatientMd.UserId != "a8a56c1d-203c-4c53-9d95-972dec7a2ef5" || PatientMd.UserId == "b6c625f5-653a-429f-b134-5b4d128ce4e8" || PatientMd.UserId == "a24e6d62-414d-4434-bbe3-b3eec3a026e7")
                {
                    PatientMd.EncounterType = "I,A";
                }

            }
            PatientMD patient = patexist.Where(p => p._id == PatientMd._id).First();
            patient.PatientName=PatientMd.PatientName;
            patient.Sex=PatientMd.Sex;
            patient.DateOfBirth=PatientMd.DateOfBirth;
            patient.Race=PatientMd.Race;
            patient.Height=PatientMd.Height;
            patient.Weight=PatientMd.Weight;

            _Patients.Save(patient);

            return patient;
        }

        public PatientMD updatePersonalinfo(UpdateInfo upinfo,string userid)
        {
            PatientMD patexist = GetByUserId(userid).First();

            patexist.PatientName = upinfo.PatientName;
            patexist.DateOfBirth = upinfo.DOB;
            patexist.Sex = upinfo.Sex;

            _Patients.Save(patexist);

            return patexist;
        }
    }
}