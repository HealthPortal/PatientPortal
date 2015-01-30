using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;


namespace HealthPortal.Models
{
    public class VaccinationModel:IVaccinationRepository
    {
         MongoServer _server;
        MongoDatabase _database;
        MongoCollection<Vaccination> _vaccination;

        public VaccinationModel()
            : this("")
        {

        }
        public VaccinationModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _vaccination = _database.GetCollection<Vaccination>("Vaccination");

        }
        public IEnumerable<Vaccination> GetAll()
        {

            return _vaccination.FindAll();
        }

        public IEnumerable<Vaccination> GetByUserId(string UserId)
        {
            IMongoQuery query = Query.EQ("UserId", UserId);
            return _vaccination.Find(query).AsEnumerable();

        }

        public Vaccination AddVaccine(Vaccination vaccination)
        {
            Vaccination phy = _vaccination.FindAll().OrderBy(p => p.Vaccinationid).Last();

            vaccination._id = ObjectId.GenerateNewId().ToString();
            Int32 vaccid = phy.Vaccinationid + 1;
            vaccination.Vaccinationid = vaccid;
            if (vaccination.EncounterType == "P")
            { vaccination.EncounterType = "I,A"; }
            _vaccination.Insert(vaccination);
            return vaccination;
        }
        public Vaccination GetById(int vaccineid)
        {
            throw new NotImplementedException();
        }


        public void DeletePatient(string userid)
        {
            _vaccination.Remove(Query.EQ("UserId", userid));
        }

        public Vaccination UpdatePatient(Vaccination Vaccin)
        {
            var vaccexist = GetByUserId(Vaccin.UserId);

            if (Vaccin.EncounterType == "P")
            {
                if (Vaccin.UserId != "a8a56c1d-203c-4c53-9d95-972dec7a2ef5" || Vaccin.UserId == "b6c625f5-653a-429f-b134-5b4d128ce4e8" || Vaccin.UserId == "a24e6d62-414d-4434-bbe3-b3eec3a026e7")
                {
                    Vaccin.EncounterType = "I,A";
                }

            }
            Vaccination vaccint = vaccexist.Where(p => p._id == Vaccin._id).First();
            vaccint.VaccineName=Vaccin.VaccineName;
            vaccint.VaccineStatus=Vaccin.VaccineStatus;
            vaccint.DateAdministered=Vaccin.DateAdministered;
            vaccint.CVXCode=Vaccin.CVXCode;

            _vaccination.Save(vaccint);

            return vaccint;
        }
    }
}