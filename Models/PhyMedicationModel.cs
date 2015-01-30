using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace HealthPortal.Models
{
    public class PhyMedicationModel:IPhyMedicationsRepository
    {
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<PhyMedication> _PhyMedication;

        public PhyMedicationModel()
            : this("")
        {

        }
        public PhyMedicationModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _PhyMedication = _database.GetCollection<PhyMedication>("PhyMedications");

        }

        public IEnumerable<PhyMedication> GebyUserId(string PatientId)
        {
            IMongoQuery query = Query.EQ("PatientId", PatientId);
            return _PhyMedication.Find(query).AsEnumerable();
        }

        public IEnumerable<PhyMedication> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}