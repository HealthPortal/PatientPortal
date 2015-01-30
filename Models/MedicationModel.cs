using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;


namespace HealthPortal.Models
{
    public class MedicationModel:IMedicationRepository
    {
         MongoServer _server;
        MongoDatabase _database;
        MongoCollection<Medication> _Medications;

        public MedicationModel()
            : this("")
        {

        }
        public MedicationModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _Medications = _database.GetCollection<Medication>("Medication");

        }
        public IEnumerable<Medication> GetAll()
        {

            return _Medications.FindAll();
        }

        public IEnumerable<Medication> GetByUserId(string UserId)
        {
            IMongoQuery query = Query.EQ("UserId", UserId);
            return _Medications.Find(query).AsEnumerable();

        }



        public Medication AddMed(Medication medication)
        {
            Medication med = _Medications.FindAll().OrderBy(p => p.Medicationsid).Last();

            medication._id = ObjectId.GenerateNewId().ToString();
            Int32 newphyid = med.Medicationsid + 1;
            medication.Medicationsid = newphyid;
            if (medication.EncounterType == "P")
            { medication.EncounterType = "I,A"; }
            _Medications.Insert(medication);
            return medication;
        }

        public Medication GetById(string refid)
        {
            ObjectId newrefid = ObjectId.Parse(refid);
            IMongoQuery query = Query.EQ("_id", newrefid);
            return _Medications.Find(query).FirstOrDefault();
          
        }

        public void DeletePatient(string userid)
        {
            _Medications.Remove(Query.EQ("UserId", userid));
        }


        public Medication UpdatePatient(Medication Medcn)
        {
            var medexist = GetByUserId(Medcn.UserId);

            if (Medcn.EncounterType == "P")
            {
                if (Medcn.UserId != "a8a56c1d-203c-4c53-9d95-972dec7a2ef5" || Medcn.UserId == "b6c625f5-653a-429f-b134-5b4d128ce4e8" || Medcn.UserId == "a24e6d62-414d-4434-bbe3-b3eec3a026e7")
                {
                    Medcn.EncounterType = "I,A";
                }

            }
            Medication meds = medexist.Where(p => p._id == Medcn._id).First();
            meds.MedicationsName=Medcn.MedicationsName;
            meds.DosageForm=Medcn.DosageForm;
            meds.SIG=Medcn.SIG;
            meds.Status=Medcn.Status;
            meds.DateOfPrescription=Medcn.DateOfPrescription;
            meds.StandardCode=Medcn.StandardCode;

            _Medications.Save(meds);

            return meds;
        }
    }
}