$(function () {

    var PhysicianDetail = Backbone.Model.extend({

        url: function () {
            return "http://localhost:56393/api/PhysicianDetails/" + this.get("PhyscianId");
        },

        idAttribute: "PhyscianId",

        defaults: {

            "PhyscianId": '0',
            "UserId": "",
            "Image": "",
            "PhysicianName": ""
        }

    });

    var PhysicianDetailCollection = Backbone.Collection.extend({

        url: "http://localhost:56393/api/PhysicianDetails",

        model: PhysicianDetail,

        load: function (callback) {
            this.fetch({
                success: callback

            });
        }

    });
    var PhysicianDetailsView = Backbone.View.extend({
        el: ".Phy-Control",

        template: _.template($('#PhyImagetemplate').html()),

        initialize: function () {
            var thisview = this;
            this.options.model.bind('reset', function () {
                thisview.render();
            });

        },

        render: function (eventName) {
            _.each(this.model.models, function (Physiciandetail) {
                this.$el.html(this.template(Physiciandetail.toJSON()));
            }, this);
            return this;

        }

    });


    var MyDay = Backbone.Model.extend({

        url: function () {
            return "http://localhost:56393/api/MyDays/" + this.get("MyDayId");
        },

        idAttribute: "MyDayId",

        defaults: {

            "MyDayId": '0',
            "Name": "",
            "Subject": "",
            "Time": ""
        }

    });

    var MyDayList = Backbone.Collection.extend({

        url: "http://localhost:56393/api/MyDays",

        model: MyDay,

        load: function (callback) {
            this.fetch({
                success: callback

            });
        }

    });
    var MyDayListView = Backbone.View.extend({
        el: "#mydaylist",

        template: _.template($('#mydaytemplate').html()),

        initialize: function () {
            var thisview = this;
            this.options.model.bind('reset', function () {
                thisview.render();
            });

        },

        render: function (eventName) {
            _.each(this.model.models, function (myday) {
                var mydaytemplate = this.template(myday.toJSON());
                $(this.el).append(mydaytemplate);
            }, this);
            return this;

        }

    });


    var Watchlist = Backbone.Model.extend({
        url: function () {
            return "http://localhost:56393/api/WatchLists/" + this.get("WatchListId");
        },

        IDAttribute: "WatchListId",
        defaults: {
            "WatchListId": '0',
            "Name": "",
            "Subject": ""

        }
    });


    var WatchlistCollection = Backbone.Collection.extend({
        model: Watchlist,
        url: "http://localhost:56393/api/WatchLists",

        load: function (callback) {
            this.fetch({
                success: callback

            });
        }

    });


    var WatchlistView = Backbone.View.extend({
        el: "#watchlist",
        template: _.template($('#watchlisttemplate').html()),
        initialize: function () {
            var thisview = this;
            this.options.model.bind('reset', function () {
                thisview.render();
            });

        },

        render: function (eventName) {
            _.each(this.model.models, function (watchlist) {
                var watchlisttemplate = this.template(watchlist.toJSON());
                $(this.el).append(watchlisttemplate);
            }, this);
            return this;
        }
    });


    var Conversation = Backbone.Model.extend({
        url: function () {
            return "http://localhost:56393/api/Conversation/" + this.get("ConversationId");
        },

        IDAttribute: "ConversationId",

        defaults: {
            "ConversationId": '0',
            "Name": "",
            "Subject": "",
            "MsgText": "",
            "Date": "",
            "Unread": ""
        }
    });


    var ConversationCollection = Backbone.Collection.extend({
        model: Conversation,
        url: "http://localhost:56393/api/Conversation",
        load: function (callback) {
            this.fetch({
                success: callback

            });
        }

    });


    var ConversationView = Backbone.View.extend({
        el: "#conversations .list-container ul",
        template: _.template($('#conversationtemplate').html()),
        initialize: function () {
            var thisview = this;
            this.options.model.bind('reset', function () {
                thisview.render();
            });

        },

        render: function (eventName) {
            _.each(this.model.models, function (conversation) {
                var conversationtemplate = this.template(conversation.toJSON());
                $(this.el).append(conversationtemplate);
            }, this);
            return this;
        }
    });


    var PatientHeader = Backbone.Model.extend({

        url: function () {
            return "http://localhost:56393/api/PatientDetails/" + this.get("PhyId");
        },

        IDAttribute: "PhyId",

        defaults: {
            "PhyId": '0',
            "PatientId": '0'

        }
    });


    var PatientHeaderCollection = Backbone.Collection.extend({

        model: PatientHeader,

        url: "http://localhost:56393/api/PatientDetails",

        loadByPatient: function (userid, callback) {
            this.fetch({

                data: { "PatientId": userid },
                success: callback
            });
        }

    });

    var PatientHeaderView = Backbone.View.extend({
        el: ".patient-record-header",
        template: _.template($('#PatientHeadertemplate').html()),
        initialize: function () {
            var thisview = this;
            this.options.model.bind('reset', function () {
                thisview.render();
            });

        },
        render: function (eventName) {
            _.each(this.model.models, function (Patientdetail) {
                this.$el.html(this.template(Patientdetail.toJSON()));
            }, this);
            return this;
        }
    });




    var Admission = Backbone.Model.extend({

        url: function () {
            return "http://localhost:56393/api/Admissions/" + this.get("AdmissionId");
        },

        IDAttribute: "AdmissionId",

        defaults: {
            "AdmissionId": '0',
            "Cause": "",
            "Date": "",
            "AdmittingDept": "",
            "Doctor": "",
            "DischargeReport": "",
            "UserId": '0'
        }
    });


    var AdmissionCollection = Backbone.Collection.extend({

        model: Admission,

        url: "http://localhost:56393/api/Admissions",

        loadByMyday: function (userid, callback) {
            this.fetch({

                data: { "PatientId": userid },
                success: callback
            });
        }

    });

    var AdmissionView = Backbone.View.extend({
        el: "#summadmission tbody",
        template: _.template($('#summaryadmtemplate').html()),
        initialize: function () {
            var thisview = this;
            this.options.model.bind('reset', function () {
                thisview.render();
            });

        },
        render: function (eventName) {
            _.each(this.model.models, function (admission) {
                this.$el.html(this.template(admission.toJSON()));
            }, this);
            return this;
        }
    });


    var Diagnosi = Backbone.Model.extend({
        url: function () {
            return "http://localhost:56393/api/Diagnosis/" + this.get("DiagnosisId");
        },

        IDAttribute: "DiagnosisId",
        defaults: {
            "DiagnosisId": '0'
        }
    });


    var DiagnosiCollection = Backbone.Collection.extend({
        model: Diagnosi,
        url: "http://localhost:56393/api/Diagnosis",
        loadByDiagnosi: function (userid, callback) {
            this.fetch({

                data: { "PatientId": userid },
                success: callback
            });
        }
    });


    var DiagnosiView = Backbone.View.extend({
        el: "#summdiagnosis tbody",
        template: _.template($('#summarydiagnotemplate').html()),
        initialize: function () {
            var thisview = this;
            this.options.model.bind('reset', function () {
                thisview.render();
            });

        },

        render: function (eventName) {
            _.each(this.model.models, function (Diagnosi) {
                this.$el.html(this.template(Diagnosi.toJSON()));
            }, this);
            return this;
        }
    });


    var Encounters = Backbone.Model.extend({
        url: function () {
            return "http://localhost:56393/api/Encounters/" + this.get("EncounterId");
        },
        IDAttribute: "EncounterId",
        defaults: {
            "EncounterId": '0',
            "Date": "",
            "Encounter1": "",
            "Facility": "",
            "Provider": ""
        }
    });


    var EncounterCollection = Backbone.Collection.extend({
        model: Encounters,
        url: "http://localhost:56393/api/Encounters",
        loadByEncounter: function (userid, callback) {
            this.fetch({

                data: { "PatientId": userid },
                success: callback
            });
        }
    });


    var EncounterView = Backbone.View.extend({
        el: "#patencounter tbody",
        template: _.template($('#patencountertemplate').html()),
        initialize: function () {
            var thisview = this;
            this.options.model.bind('reset', function () {
                thisview.render();
            });
        },


        render: function (eventName) {
            _.each(this.model.models, function (encounter) {
                this.$el.html(this.template(encounter.toJSON()));
            }, this);
            return this;
        }
    });


    var Demographics = Backbone.Model.extend({
        url: function () {
            return "http://localhost:56393/api/Demographics/" + this.get("DemoId");
        },
        IDAttribute: "DemoId",
        defaults: {
            "DemoId": '0'
        }
    });


    var DemographicsCollection = Backbone.Collection.extend({
        model: Demographics,
        url: "http://localhost:56393/api/Demographics",
        loadByDemographics: function (userid, callback) {
            this.fetch({

                data: { "PatientId": userid },
                success: callback
            });
        }
    });


    var DemographicsView = Backbone.View.extend({
        el: "#patdemographics tbody",
        template: _.template($('#patdemotemplate').html()),
        initialize: function () {
            var thisview = this;
            this.options.model.bind('reset', function () {
                thisview.render();
            });

        },


        render: function (eventName) {
            _.each(this.model.models, function (demographic) {
                this.$el.html(this.template(demographic.toJSON()));
            }, this);
            return this;
        }

    });


    var Procedures = Backbone.Model.extend({
        url: function () {
            return "http://localhost:56393/api/PhyProcedures/" + this.get("ProcedureId");
        },
        IDAttribute: "ProcedureId",
        defaults: {
            "ProcedureId": '0'
        }
    });


    var ProceduresCollection = Backbone.Collection.extend({
        model: Procedures,
        url: "http://localhost:56393/api/PhyProcedures",
        loadByProcedure: function (userid, callback) {
            this.fetch({

                data: { "PatientId": userid },
                success: callback
            });
        }
    });


    var ProceduresView = Backbone.View.extend({
        el: "#patprocedures tbody",
        template: _.template($('#patprocedtemplate').html()),
        initialize: function () {
            var thisview = this;
            this.options.model.bind('reset', function () {
                thisview.render();
            });

        },


        render: function (eventName) {
            _.each(this.model.models, function (procedure) {
                this.$el.html(this.template(procedure.toJSON()));
            }, this);
            return this;
        }

    });


    var Allergies = Backbone.Model.extend({
        url: function () {
            return "http://localhost:56393/api/PhyAllergies/" + this.get("AllergiesId");
        },
        IDAttribute: "AllergiesId",
        defaults: {
            "AllergiesId": '0'
        }
    });


    var AllergiesCollection = Backbone.Collection.extend({
        model: Allergies,
        url: "http://localhost:56393/api/PhyAllergies",
        loadByAllergy: function (userid, callback) {
            this.fetch({

                data: { "PatientId": userid },
                success: callback
            });
        }
    });


    var AllergiesView = Backbone.View.extend({
        el: "#pataallergies tbody",
        template: _.template($('#patallergiestemplate').html()),
        initialize: function () {
            var thisview = this;
            this.options.model.bind('reset', function () {
                thisview.render();
            });

        },


        render: function (eventName) {
            _.each(this.model.models, function (allergy) {
                this.$el.html(this.template(allergy.toJSON()));
            }, this);
            return this;
        }


    });



    var PatDiagnosis = Backbone.Model.extend({
        url: function () {
            return "http://localhost:56393/api/Diagnosis/" + this.get("DiagnosisId");
        },
        IDAttribute: "DiagnosisId",
        defaults: {
            "DiagnosisId": '0'
        }
    });


    var PatDiagnosisCollection = Backbone.Collection.extend({
        model: PatDiagnosis,
        url: "http://localhost:56393/api/Diagnosis",
        loadByPatDiagno: function (userid, callback) {
            this.fetch({

                data: { "PatientId": userid },
                success: callback
            });
        }
    });


    var PatDiagnosisView = Backbone.View.extend({
        el: "#patdiagnosis tbody",
        template: _.template($('#patdiagtemplate').html()),
        initialize: function () {
            var thisview = this;
            this.options.model.bind('reset', function () {
                thisview.render();
            });

        },


        render: function (eventName) {
            _.each(this.model.models, function (patdiagnosis) {
                this.$el.html(this.template(patdiagnosis.toJSON()));
            }, this);
            return this;
        }

    });



    var PhyMedications = Backbone.Model.extend({
        url: function () {
            return "http://localhost:56393/api/PhyMedications/" + this.get("MedicationsId");
        },
        IDAttribute: "MedicationsId",
        defaults: {
            "MedicationsId": '0'
        }
    });


    var PhyMedicationsCollection = Backbone.Collection.extend({
        model: PhyMedications,
        url: "http://localhost:56393/api/PhyMedications",

        loadByPhyMed: function (userid, callback) {
            this.fetch({

                data: { "PatientId": userid },
                success: callback
            });
        }
    });


    var PhyMedicationsView = Backbone.View.extend({
        el: "#patmedications tbody",
        template: _.template($('#patmedstemplate').html()),
        initialize: function () {
            var thisview = this;
            this.options.model.bind('reset', function () {
                thisview.render();
            });

        },

        render: function (eventName) {
            _.each(this.model.models, function (phymedication) {
                this.$el.html(this.template(phymedication.toJSON()));
            }, this);
            return this;
        }

    });


    var Labs = Backbone.Model.extend({
        url: function () {
            return "http://localhost:56393/api/Labs/" + this.get("LabId");
        },
        IDAttribute: "LabId",
        defaults: {
            "LabId": '0'
        }
    });


    var LabsCollection = Backbone.Collection.extend({
        model: Labs,
        url: "http://localhost:56393/api/Labs",

        loadByLab: function (userid, callback) {
            this.fetch({

                data: { "PatientId": userid },
                success: callback
            });
        }
    });


    var LabsView = Backbone.View.extend({
        el: "#patlabs tbody",
        template: _.template($('#patlabstemplate').html()),
        initialize: function () {
            var thisview = this;
            this.options.model.bind('reset', function () {
                thisview.render();
            });

        },

        render: function (eventName) {
            _.each(this.model.models, function (lab) {
                this.$el.html(this.template(lab.toJSON()));
            }, this);
            return this;
        }

    });



    var Immunizations = Backbone.Model.extend({
        url: function () {
            return "http://localhost:56393/api/Immunizations/" + this.get("ImmunizationId");
        },

        IDAttribute: "ImmunizationId",
        defaults: {
            "ImmunizationId": '0'
        }
    });


    var ImmunizationsCollection = Backbone.Collection.extend({
        model: Immunizations,
        url: "http://localhost:56393/api/Immunizations",

        loadByImmun: function (userid, callback) {
            this.fetch({

                data: { "PatientId": userid },
                success: callback
            });
        }
    });


    var ImmunizationsView = Backbone.View.extend({
        el: "#patimmunizations tbody",
        template: _.template($('#patimmutemplate').html()),
        initialize: function () {
            var thisview = this;
            this.options.model.bind('reset', function () {
                thisview.render();
            });

        },
        render: function (eventName) {
            _.each(this.model.models, function (immunization) {
                this.$el.html(this.template(immunization.toJSON()));
            }, this);
            return this;
        }

    });


    var PatAdmission = Backbone.Model.extend({
        url: function () {
            return "http://localhost:56393/api/Admissions/" + this.get("AdmissionsId");
        },
        IDAttribute: "AdmissionsId",
        defaults: {
            "AdmissionsId": '0'
        }
    });


    var PatAdmissionCollection = Backbone.Collection.extend({
        model: PatAdmission,
        url: "http://localhost:56393/api/Admissions",

        loadByPatAdm: function (userid, callback) {
            this.fetch({

                data: { "PatientId": userid },
                success: callback
            });
        }
    });


    var PatAdmissionView = Backbone.View.extend({
        el: "#patadmission tbody",
        template: _.template($('#patadmtemplate').html()),
        initialize: function () {
            var thisview = this;
            this.options.model.bind('reset', function () {
                thisview.render();
            });

        },
        render: function (eventName) {
            _.each(this.model.models, function (patadmission) {
                this.$el.html(this.template(patadmission.toJSON()));
            }, this);
            return this;
        }

    });






    var RouterMain = Backbone.Router.extend({
        routes: {
            "": "MydayList",
            "MyDay/:userid": "AdmissionList",
            "WatchList/:userid": "WatchList"
        },

        MydayList: function () {
            var physiciandetails = new PhysicianDetailCollection();
            physiciandetails.load();
            var physiciandetailsview = new PhysicianDetailsView({model:physiciandetails});

            var mydaylist = new MyDayList();
            mydaylist.load();
            var mydayview = new MyDayListView({ model: mydaylist });
            var watchlist = new WatchlistCollection();
            watchlist.load();
            var watchlistview = new WatchlistView({ model: watchlist });
            var conversation = new ConversationCollection();
            conversation.load();
            var conversationview = new ConversationView({ model: conversation });

        },

        AdmissionList: function (userid) {
            var patientdetails = new PatientHeaderCollection();
            patientdetails.loadByPatient(userid, function () { });
            var patientheaderview = new PatientHeaderView({ model: patientdetails });

            var admissions = new AdmissionCollection();
            admissions.loadByMyday(userid, function () {
            });
            var admissionview = new AdmissionView({ model: admissions });

            var diagnosis = new DiagnosiCollection();
            diagnosis.loadByDiagnosi(userid, function () { });
            var diagnosisview = new DiagnosiView({ model: diagnosis });


            var encounters = new EncounterCollection();
            encounters.loadByEncounter(userid, function () { });
            var encountersview = new EncounterView({ model: encounters });

            var demographics = new DemographicsCollection();
            demographics.loadByDemographics(userid, function () { });
            var demographicsview = new DemographicsView({ model: demographics });

            var procedures = new ProceduresCollection();
            procedures.loadByProcedure(userid, function () { });
            var proceduresview = new ProceduresView({ model: procedures });

            var allergies = new AllergiesCollection();
            allergies.loadByAllergy(userid, function () { });
            var allergiesview = new AllergiesView({ model: allergies });

            var Patdiagnosis = new PatDiagnosisCollection();
            Patdiagnosis.loadByPatDiagno(userid, function () { });
            var patdiagnosisview = new PatDiagnosisView({ model: Patdiagnosis });

            var phymedication = new PhyMedicationsCollection();
            phymedication.loadByPhyMed(userid, function () { });
            var phymedicationview = new PhyMedicationsView({ model: phymedication });

            var labs = new LabsCollection();
            labs.loadByLab(userid, function () { });
            var labsview = new LabsView({ model: labs });

            var immunizations = new ImmunizationsCollection();
            immunizations.loadByImmun(userid, function () { });
            var immunizationsview = new ImmunizationsView({ model: immunizations });

            var patadmissions = new PatAdmissionCollection();
            patadmissions.loadByPatAdm(userid, function () { });
            var patadmissionview = new PatAdmissionView({ model: patadmissions });
        },



        WatchList: function (userid) {
            var patientdetails = new PatientHeaderCollection();
            patientdetails.loadByPatient(userid, function () { });
            var patientheaderview = new PatientHeaderView({ model: patientdetails });

            var admissions = new AdmissionCollection();
            admissions.loadByMyday(userid, function () {
            });
            var admissionview = new AdmissionView({ model: admissions });

            var diagnosis = new DiagnosiCollection();
            diagnosis.loadByDiagnosi(userid, function () { });
            var diagnosisview = new DiagnosiView({ model: diagnosis });


            var encounters = new EncounterCollection();
            encounters.loadByEncounter(userid, function () { });
            var encountersview = new EncounterView({ model: encounters });

            var demographics = new DemographicsCollection();
            demographics.loadByDemographics(userid, function () { });
            var demographicsview = new DemographicsView({ model: demographics });

            var procedures = new ProceduresCollection();
            procedures.loadByProcedure(userid, function () { });
            var proceduresview = new ProceduresView({ model: procedures });

            var allergies = new AllergiesCollection();
            allergies.loadByAllergy(userid, function () { });
            var allergiesview = new AllergiesView({ model: allergies });

            var Patdiagnosis = new PatDiagnosisCollection();
            Patdiagnosis.loadByPatDiagno(userid, function () { });
            var patdiagnosisview = new PatDiagnosisView({ model: Patdiagnosis });

            var phymedication = new PhyMedicationsCollection();
            phymedication.loadByPhyMed(userid, function () { });
            var phymedicationview = new PhyMedicationsView({ model: phymedication });

            var labs = new LabsCollection();
            labs.loadByLab(userid, function () { });
            var labsview = new LabsView({ model: labs });

            var immunizations = new ImmunizationsCollection();
            immunizations.loadByImmun(userid, function () { });
            var immunizationsview = new ImmunizationsView({ model: immunizations });

            var patadmissions = new PatAdmissionCollection();
            patadmissions.loadByPatAdm(userid, function () { });
            var patadmissionview = new PatAdmissionView({ model: patadmissions });
        }



    });

    var router = new RouterMain();

    Backbone.history.start();

});
