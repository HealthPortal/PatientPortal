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
    public class AppointmentRepository : IAppointment
    {
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<Appointment> _Appointments;

        public AppointmentRepository()
            : this("")
        {

        }
        public AppointmentRepository(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _Appointments = _database.GetCollection<Appointment>("Appointments");

        }
        public IEnumerable<Appointment> GetAll()
        {

            return _Appointments.FindAll();
        }

        public IEnumerable<Appointment> GetByUserId(string UserId)
        {
            IMongoQuery query = Query.EQ("UserId", UserId);
            return _Appointments.Find(query).AsEnumerable();

        }

        public Appointment GetById(string refid)
        {
            ObjectId newrefid = ObjectId.Parse(refid);
            IMongoQuery query = Query.EQ("_id", newrefid);
            return _Appointments.Find(query).FirstOrDefault();
        }

        public void DeleteAppointmentById(string ID)
        {
            ObjectId newid = ObjectId.Parse(ID);
            IMongoQuery query = Query.EQ("_id", newid);
            _Appointments.Remove(query);
        }



        public Appointment AddAppointment(Appointment appoints)
        {
            Appointment demo = _Appointments.FindAll().OrderBy(p => p.AppointmentsID).Last();

            appoints._id = ObjectId.GenerateNewId().ToString();
            Int32 patid = appoints.AppointmentsID + 1;
            appoints.AppointmentsID = patid;
            if (appoints.EncounterType == "P")
            { appoints.EncounterType = "I,A"; }
            _Appointments.Insert(appoints);
            return appoints;
        }

        public void DeleteAppointment(string userid)
        {
            _Appointments.Remove(Query.EQ("UserId", userid));
        }

        public Appointment UpdateAppointment(Appointment appoints)
        {
            var patexist = GetByUserId(appoints.UserId);

            if (appoints.EncounterType == "P")
            {
                if (appoints.UserId != "a8a56c1d-203c-4c53-9d95-972dec7a2ef5" || appoints.UserId == "b6c625f5-653a-429f-b134-5b4d128ce4e8" || appoints.UserId == "a24e6d62-414d-4434-bbe3-b3eec3a026e7")
                {
                    appoints.EncounterType = "I,A";
                }

            }
            Appointment appoint = patexist.Where(p => p._id == appoints._id).First();
            appoint.Physician = appoints.Physician;
            appoint.CenterName = appoints.CenterName;
            appoint.Date = appoints.Date;
            appoint.Address = appoints.Address;
            appoint.Reason = appoints.Reason;
            appoint.Time = appoints.Time;

            _Appointments.Save(appoint);

            return appoint;
        }
    }
}