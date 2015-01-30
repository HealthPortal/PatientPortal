using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;


namespace HealthPortal.Models
{
    public class ProcedureModel:IProcedureRepository
    {
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<ProcedureDetail> _Procedures;

        public ProcedureModel()
            : this("")
        {

        }
        public ProcedureModel(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://Amit:pooja123@ds027829.mongolab.com:27829/healthcare";
            }
            _server = MongoServer.Create(connection);
            _database = _server.GetDatabase("healthcare", SafeMode.True);
            _Procedures = _database.GetCollection<ProcedureDetail>("ProcedureDetail");

        }
        public IEnumerable<ProcedureDetail> GetAll()
        {

            return _Procedures.FindAll();
        }

        public IEnumerable<ProcedureDetail> GetByUserId(string UserId)
        {
            IMongoQuery query = Query.EQ("UserId", UserId);
            return _Procedures.Find(query).AsEnumerable();

        }
        public ProcedureDetail AddProcDetail(ProcedureDetail procdetails)
        {
            ProcedureDetail phy = _Procedures.FindAll().OrderBy(p => p.Proceduredetailsid).Last();

            procdetails._id = ObjectId.GenerateNewId().ToString();
            Int32 procdetid = phy.Proceduredetailsid + 1;
            procdetails.Proceduredetailsid = procdetid;
            if (procdetails.EncounterType == "P")
            { procdetails.EncounterType = "I,A"; }
            _Procedures.Insert(procdetails);
            return procdetails;
        }


        public ProcedureDetail GetById(int procedureid)
        {
            throw new NotImplementedException();
        }

        public void DeletePatient(string userid)
        {
            _Procedures.Remove(Query.EQ("UserId", userid));
        }


        public ProcedureDetail UpdatePatient(ProcedureDetail ProcDetails)
        {
            var procexist = GetByUserId(ProcDetails.UserId);

            if (ProcDetails.EncounterType == "P")
            {
                if (ProcDetails.UserId != "a8a56c1d-203c-4c53-9d95-972dec7a2ef5" || ProcDetails.UserId == "b6c625f5-653a-429f-b134-5b4d128ce4e8" || ProcDetails.UserId == "a24e6d62-414d-4434-bbe3-b3eec3a026e7")
                {
                    ProcDetails.EncounterType = "I,A";
                }

            }
            ProcedureDetail procde = procexist.Where(p => p._id == ProcDetails._id).First();
            procde.ProcedureName=ProcDetails.ProcedureName;
            procde.ProcedureDate=ProcDetails.ProcedureDate;
            procde.SNOMEDCT = ProcDetails.SNOMEDCT;

            _Procedures.Save(procde);

            return procde;
        }
    }
}