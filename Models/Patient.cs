using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson.Serialization.Attributes;


namespace HealthPortal.Models
{
    public class Patient
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public Int32 PatientID { get; set; }
        public string PatientName { get; set; }
        public string Sex { get; set; }
        public string DateOfBirth { get; set; }
        public string Race { get; set; }
        public string Ethnicity { get; set; }
        public string PreferredLanguage { get; set; }
        public Int32 Height { get; set; }
        public Int32 Weight { get; set; }
        public string UserId { get; set; }
        public string Image { get; set; }
        public IEnumerable<MyPhysician> MyPhysician { get; set; }
        public IEnumerable<VitalSign> Vitals { get; set; }
        public IEnumerable<Allergy> Allergies { get; set; }
        public IEnumerable<Problem> Problems { get; set; }
        public IEnumerable<Medication> Medications { get; set; }
        public IEnumerable<Vaccination> Vaccinations { get; set; }
        public IEnumerable<ProcedureDetail> Procedures { get; set; }
        public IEnumerable<CarePlan> Careplans { get; set; }
        public IEnumerable<MessagesDetail> Messages { get; set; }
        
    }
}
