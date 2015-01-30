

$(function () {

//    var Myday = Backbone.Model.extend({
//        url: "http://localhost:56393/api/MyDays/",
//        IDAttribute: "MyDayId",
//        defaults: {
//            "MyDayId": '0',
//            "Name": "",
//            "Subject": "",
//            "Time": ""
//        }
//    });


//    var MydayCollection = Backbone.Collection.extend({
//        model: Myday,
//        url: "http://localhost:56393/api/MyDays"
//    });


//    var MydayView = Backbone.View.extend({
//        el: "#mydaylist",
//        template: _.template($('#mydaytemplate').html()),
//        render: function (eventName) {
//            _.each(this.model.models, function (myday) {
//                var mydaytemplate = this.template(myday.toJSON());
//                $(this.el).append(mydaytemplate);
//            }, this);
//            return this;
//        }
//    });
//    var mydays = new MydayCollection();
//    var mydaysview = new MydayView({ model: mydays });
//    mydays.fetch();
//    mydays.bind('reset', function () {
//        mydaysview.render();
//    });



//    var Watchlist = Backbone.Model.extend({
//        url: "http://localhost:56393/api/WatchLists/",
//        IDAttribute: "WatchListId",
//        defaults: {
//            "WatchListId": '0',
//            "Name": "",
//            "Subject": ""

//        }
//    });


//    var WatchlistCollection = Backbone.Collection.extend({
//        model: Watchlist,
//        url: "http://localhost:56393/api/WatchLists"
//    });


//    var WatchlistView = Backbone.View.extend({
//        el: "#watchlist",
//        template: _.template($('#watchlisttemplate').html()),
//        render: function (eventName) {
//            _.each(this.model.models, function (watchlist) {
//                var watchlisttemplate = this.template(watchlist.toJSON());
//                $(this.el).append(watchlisttemplate);
//            }, this);
//            return this;
//        }
//    });
//    var watchlists = new WatchlistCollection();
//    var watchlistview = new WatchlistView({ model: watchlists });
//    watchlists.fetch();
//    watchlists.bind('reset', function () {
//        watchlistview.render();
//    });




//    var Conversation = Backbone.Model.extend({
//        url: "http://localhost:56393/api/Conversation/",
//        IDAttribute: "ConversationId",
//        defaults: {
//            "ConversationId": '0',
//            "Name": "",
//            "Subject": "",
//            "MsgText": "",
//            "Date": "",
//            "Unread": ""
//        }
//    });


//    var ConversationCollection = Backbone.Collection.extend({
//        model: Conversation,
//        url: "http://localhost:56393/api/Conversation"
//    });


//    var ConversationView = Backbone.View.extend({
//        el: "#conversations .list-container ul",
//        template: _.template($('#conversationtemplate').html()),
//        render: function (eventName) {
//            _.each(this.model.models, function (conversation) {
//                var conversationtemplate = this.template(conversation.toJSON());
//                $(this.el).append(conversationtemplate);
//            }, this);
//            return this;
//        }
//    });
//    var conversations = new ConversationCollection();
//    var conversationview = new ConversationView({ model: conversations });
//    conversations.fetch();
//    conversations.bind('reset', function () {
//        conversationview.render();
//    });





   







//    var Diagnosi = Backbone.Model.extend({
//        url: "http://localhost:56393/api/Diagnosis/",
//        IDAttribute: "AdmissionId",
//        defaults: {
//            "AdmissionId": '0'
//        }
//    });


//    var DiagnosiCollection = Backbone.Collection.extend({
//        model: Diagnosi,
//        url: "http://localhost:56393/api/Diagnosis"
//    });


//    var DiagnosiView = Backbone.View.extend({
//        el: "#summdiagnosis tbody",
//        template: _.template($('#summarydiagnotemplate').html()),
//        render: function (eventName) {
//            _.each(this.model.models, function (Diagnosi) {
//                var summarydiagnotemplate = this.template(Diagnosi.toJSON());
//                $(this.el).append(summarydiagnotemplate);
//            }, this);
//            return this;
//        }
//    });
//    var diagnosis = new DiagnosiCollection();
//    var diagnosiview = new DiagnosiView({ model: diagnosis });
//    diagnosis.fetch();
//    diagnosis.bind('reset', function () {
//        diagnosiview.render();
//    });





//    var Encounters = Backbone.Model.extend({
//        url: "http://localhost:56393/api/Encounters/",
//        IDAttribute: "EncounterId",
//        defaults: {
//            "EncounterId": '0',
//            "Date": "",
//            "Encounter1": "",
//            "Facility": "",
//            "Provider": ""
//        }
//    });


//    var EncounterCollection = Backbone.Collection.extend({
//        model: Encounters,
//        url: "http://localhost:56393/api/Encounters"
//    });


//    var EncounterView = Backbone.View.extend({
//        el: "#patencounter tbody",
//        template: _.template($('#patencountertemplate').html()),
//        render: function (eventName) {
//            _.each(this.model.models, function (encounter) {
//                var patencountertemplate = this.template(encounter.toJSON());
//                $(this.el).append(patencountertemplate);
//            }, this);
//            return this;
//        }
//    });
//    var encounters = new EncounterCollection();
//    var encountersview = new EncounterView({ model: encounters });
//    encounters.fetch();
//    encounters.bind('reset', function () {
//        encountersview.render();
//    });



//    var Demographics = Backbone.Model.extend({
//        url: "http://localhost:56393/api/Demographics/",
//        IDAttribute: "DemoId",
//        defaults: {
//            "DemoId": '0'
//        }
//    });


//    var DemographicsCollection = Backbone.Collection.extend({
//        model: Demographics,
//        url: "http://localhost:56393/api/Demographics"
//    });


//    var DemographicsView = Backbone.View.extend({
//        el: "#patdemographics tbody",
//        template: _.template($('#patdemotemplate').html()),
//        render: function (eventName) {
//            _.each(this.model.models, function (demographic) {
//                var patdemotemplate = this.template(demographic.toJSON());
//                $(this.el).append(patdemotemplate);
//            }, this);
//            return this;
//        }
//    });
//    var demographics = new DemographicsCollection();
//    var demographicsview = new DemographicsView({ model: demographics });
//    demographics.fetch();
//    demographics.bind('reset', function () {
//        demographicsview.render();
//    });




//    var Procedures = Backbone.Model.extend({
//        url: "http://localhost:56393/api/PhyProcedures/",
//        IDAttribute: "ProcedureId",
//        defaults: {
//            "ProcedureId": '0'
//        }
//    });


//    var ProceduresCollection = Backbone.Collection.extend({
//        model: Procedures,
//        url: "http://localhost:56393/api/PhyProcedures"
//    });


//    var ProceduresView = Backbone.View.extend({
//        el: "#patprocedures tbody",
//        template: _.template($('#patprocedtemplate').html()),
//        render: function (eventName) {
//            _.each(this.model.models, function (procedure) {
//                var patprocedtemplate = this.template(procedure.toJSON());
//                $(this.el).append(patprocedtemplate);
//            }, this);
//            return this;
//        }
//    });
//    var procedures = new ProceduresCollection();
//    var proceduresview = new ProceduresView({ model: procedures });
//    procedures.fetch();
//    procedures.bind('reset', function () {
//        proceduresview.render();
//    });



//    var Allergies = Backbone.Model.extend({
//        url: "http://localhost:56393/api/PhyAllergies/",
//        IDAttribute: "AllergiesId",
//        defaults: {
//            "AllergiesId": '0'
//        }
//    });


//    var AllergiesCollection = Backbone.Collection.extend({
//        model: Allergies,
//        url: "http://localhost:56393/api/PhyAllergies"
//    });


//    var AllergiesView = Backbone.View.extend({
//        el: "#pataallergies tbody",
//        template: _.template($('#patallergiestemplate').html()),
//        render: function (eventName) {
//            _.each(this.model.models, function (allergy) {
//                var patallergiestemplate = this.template(allergy.toJSON());
//                $(this.el).append(patallergiestemplate);
//            }, this);
//            return this;
//        }
//    });
//    var allergies = new AllergiesCollection();
//    var allergiesview = new AllergiesView({ model: allergies });
//    allergies.fetch();
//    allergies.bind('reset', function () {
//        allergiesview.render();
//    });





//    var PatDiagnosis = Backbone.Model.extend({
//        url: "http://localhost:56393/api/Diagnosis/",
//        IDAttribute: "DiagnosisId",
//        defaults: {
//            "DiagnosisId": '0'
//        }
//    });


//    var PatDiagnosisCollection = Backbone.Collection.extend({
//        model: PatDiagnosis,
//        url: "http://localhost:56393/api/Diagnosis"
//    });


//    var PatDiagnosisView = Backbone.View.extend({
//        el: "#patdiagnosis tbody",
//        template: _.template($('#patdiagtemplate').html()),
//        render: function (eventName) {
//            _.each(this.model.models, function (patdiagnosis) {
//                var patdiagtemplate = this.template(patdiagnosis.toJSON());
//                $(this.el).append(patdiagtemplate);
//            }, this);
//            return this;
//        }
//    });
//    var patdiagnosis = new PatDiagnosisCollection();
//    var patdiagnosisview = new PatDiagnosisView({ model: patdiagnosis });
//    patdiagnosis.fetch();
//    patdiagnosis.bind('reset', function () {
//        patdiagnosisview.render();
//    });




//    var PhyMedications = Backbone.Model.extend({
//        url: "http://localhost:56393/api/PhyMedications/",
//        IDAttribute: "MedicationsId",
//        defaults: {
//            "MedicationsId": '0'
//        }
//    });


//    var PhyMedicationsCollection = Backbone.Collection.extend({
//        model: PhyMedications,
//        url: "http://localhost:56393/api/PhyMedications"
//    });


//    var PhyMedicationsView = Backbone.View.extend({
//        el: "#patmedications tbody",
//        template: _.template($('#patmedstemplate').html()),
//        render: function (eventName) {
//            _.each(this.model.models, function (phymedication) {
//                var patmedstemplate = this.template(phymedication.toJSON());
//                $(this.el).append(patmedstemplate);
//            }, this);
//            return this;
//        }
//    });
//    var phymedications = new PhyMedicationsCollection();
//    var phymedicationsview = new PhyMedicationsView({ model: phymedications });
//    phymedications.fetch();
//    phymedications.bind('reset', function () {
//        phymedicationsview.render();
//    });





//    var Labs = Backbone.Model.extend({
//        url: "http://localhost:56393/api/Labs/",
//        IDAttribute: "LabId",
//        defaults: {
//            "LabId": '0'
//        }
//    });


//    var LabsCollection = Backbone.Collection.extend({
//        model: Labs,
//        url: "http://localhost:56393/api/Labs"
//    });


//    var LabsView = Backbone.View.extend({
//        el: "#patlabs tbody",
//        template: _.template($('#patlabstemplate').html()),
//        render: function (eventName) {
//            _.each(this.model.models, function (lab) {
//                var patlabstemplate = this.template(lab.toJSON());
//                $(this.el).append(patlabstemplate);
//            }, this);
//            return this;
//        }
//    });
//    var labs = new LabsCollection();
//    var labsview = new LabsView({ model: labs });
//    labs.fetch();
//    labs.bind('reset', function () {
//        labsview.render();
//    });





//    var Immunizations = Backbone.Model.extend({
//        url: "http://localhost:56393/api/Immunizations/",
//        IDAttribute: "ImmunizationId",
//        defaults: {
//            "ImmunizationId": '0'
//        }
//    });


//    var ImmunizationsCollection = Backbone.Collection.extend({
//        model: Immunizations,
//        url: "http://localhost:56393/api/Immunizations"
//    });


//    var ImmunizationsView = Backbone.View.extend({
//        el: "#patimmunizations tbody",
//        template: _.template($('#patimmutemplate').html()),
//        render: function (eventName) {
//            _.each(this.model.models, function (immunization) {
//                var patimmutemplate = this.template(immunization.toJSON());
//                $(this.el).append(patimmutemplate);
//            }, this);
//            return this;
//        }
//    });
//    var immunizations = new ImmunizationsCollection();
//    var immunizationsview = new ImmunizationsView({ model: immunizations });
//    immunizations.fetch();
//    immunizations.bind('reset', function () {
//        immunizationsview.render();
//    });


//    var PatAdmission = Backbone.Model.extend({
//        url: "http://localhost:56393/api/Admissions/",
//        IDAttribute: "AdmissionsId",
//        defaults: {
//            "AdmissionsId": '0'
//        }
//    });


//    var PatAdmissionCollection = Backbone.Collection.extend({
//        model: PatAdmission,
//        url: "http://localhost:56393/api/Admissions"
//    });


//    var PatAdmissionView = Backbone.View.extend({
//        el: "#patadmission tbody",
//        template: _.template($('#patadmtemplate').html()),
//        render: function (eventName) {
//            _.each(this.model.models, function (patadmission) {
//                var patadmtemplate = this.template(patadmission.toJSON());
//                $(this.el).append(patadmtemplate);
//            }, this);
//            return this;
//        }
//    });
//    var patadmissions = new PatAdmissionCollection();
//    var patadmissionsview = new PatAdmissionView({ model: patadmissions });
//    patadmissions.fetch();
//    patadmissions.bind('reset', function () {
//        patadmissionsview.render();
//    });




});