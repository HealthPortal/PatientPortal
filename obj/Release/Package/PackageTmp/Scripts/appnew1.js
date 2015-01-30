/// <reference path="backbone-min.js" />
/// <reference path="underscore-min.js" />
$(function () {

    var Patient = Backbone.Model.extend({
        url: "http://localhost:56393/api/Patients/",
        IDAttribute: "_id",
        defaults: {
            "PatientID": '0',
            "PatientName": "",
            "Sex": "",
            "DateOfBirth": "",
            "Race": "",
            "Ethnicity": "",
            "Preferred Language": "",
            "Image": "",
            "Height": "",
            "Weight": ""
        }
    });


    var PatientCollection = Backbone.Collection.extend({
        model: Patient,
        url: "http://localhost:56393/api/Patients"
    });


    var MobilePatientView = Backbone.View.extend({
        el: "#mobvcard",
        template: _.template($('#mobvcardtemplate').html()),
        render: function (eventName) {
            _.each(this.model.models, function (patient) {
                var mobvcardtemplate = this.template(patient.toJSON());
                $(this.el).append(mobvcardtemplate);
            }, this);
            return this;
        }
    });

    var PatientView = Backbone.View.extend({
        el: "#ptcontrol",
        template: _.template($('#vcardtemplate').html()),
        render: function (eventName) {
            _.each(this.model.models, function (patient) {
                var vcardtemplate = this.template(patient.toJSON());
                $(this.el).append(vcardtemplate);
            }, this);
            return this;
        }
    });
    var patients = new PatientCollection();
    var patientsview = new PatientView({ model: patients });
    var mobilepatientview = new MobilePatientView({ model: patients });
    patients.fetch();
    patients.bind('reset', function () {
        patientsview.render();
        mobilepatientview.render();
    });
//    var myPhyModel = Backbone.Model.extend({
//        //        url: function () {
//        //            return "http://localhost:56393/api/Patients";
//        //        },
//        url: "http://localhost:56393/api/Patients",
//        defaults: {
//            MyPhysician: []
//        }

//    });
//    var Phyview = Backbone.View.extend({
//        initialize: function (model) {
//            console.log(JSON.stringify(model));
//            console.log(model.get("MyPhysician"));
//            console.log(model.attributes);
//        }


//    });
//    var phy = new myPhyModel();
//    phy.fetch({ success: function () {
//        console.log("inside phy fetch..");
//        // console.log(model.get('MyPhysician'));
//        //MyPhysician = model.get('MyPhysician');
//        //console.log(MyPhysician);
//    }
//    });
//    var phyview = new Phyview({ model: phy });

//    var ProbsCollection = Backbone.Collection.extend({
//        url: "http://localhost:56393/api/Patients",
//        parse: function (response) {
//            console.log("inside parse");
//            var Problems = [];
//            Problems = response.valueOf(arguments.Problems);
//            console.log(Problems.get('Problems'));
//            console.log(this.model.get("Problems"));
//            return this.models;
//        },
//        initialize: function () {

//            this.bind("reset", function (model, options) {
//                console.log("Inside event");
//                console.log(model);

//            });

//        }

//    });


//    var problems = new ProbsCollection();
//    problems.fetch({
//        success: function (response, xhr) {
//            console.log("Inside success");
//            console.log(response);
//        },
//        error: function (errorResponse) {
//            console.log(errorResponse)
//        }
//    });

    var MedicationsModel = Backbone.Model.extend({

        //url: "http://localhost:56393/api/Patients",
        defaults: {
            //Medications: [{ 'headers': [], 'rows': [] }]
            Medications: []
        },

        initialize: function () {

            //            $ajax({
            //                type: "GET",
            //                url: this.url,
            //                contentType: "application/json; charset=utf-8",
            //                dataType: "json",
            //                success: function (response) { alert(response); },
            //                failure: function (response) { alert(response); }

            //            });
            var that = this;
            var jqxhr = $.ajax({
                // dataType: 'jsonp',
                url: 'http://localhost:56393/api/Patients'
                //url: '/services/Medications?userID=0'
                //url: UrlService.build(new ServiceMeta("Medications",new Array({"patientID":'0'})))
            })
                    .done(function (data, textStatus, xhr) {
                        alert(data);
                        var data = JSON.stringify(data);
                        alert(data.toString());
                        // data = data.toJSON();
                        var Medications = [];
                        //Medications = data.valueOf(Medications[0]);
                        Medications = data.valueOf("Medications");
                        alert(data.error);
                        console.log(Medications);

                        if (!data.error && data.Medications) {
                            Medications.rows = data.Medications;
                            Medications.headers = [];
                            for (var k in data.Medications[0]) {
                                Medications.headers.push(k);
                            }
                            that.set("Medications", Medications);
                            console.log(Medications);
                        }
                    })
                    .fail(function (xhr, ajaxOptions, thrownError) {
                        console.log("ERROR: " + xhr.status + ' ' + thrownError);
                    })
                    .always(function () {
                    });

        }
    });
    var medView = Backbone.View.extend({

        initialize: function (model) {
            console.log(JSON.stringify(model));
            var myphy = [];
            myphy = model.get('Medications');
            console.log(myphy);
        }

    });
    var medmodel = new MedicationsModel();
    var medview = new medView({model:medmodel});




    var MedsModel = Backbone.Model.extend({
        url: "http://localhost:56393/api/Patients/",
        //IDAttribute: "Medicationsid",
        defaults: {
            "Medications": [
                        {
                            "Medicationsid": '0',
                            "MedicationsName": "",
                            "DosageForm": "",
                            "SIG": "",
                            "Status": "",
                            "DateOfPrescription": "",
                            "StandardCode": ""
                        }
            ]

        },
        parse: function (response) {
            console.log("inside parsemeds");
            return response;
        }
    });


    var MedsCollection = Backbone.Collection.extend({
        model: MedsModel,
        url: "http://localhost:56393/api/Patients"
    });







    var MedsView = Backbone.View.extend({
        el: "#app-medications--medication-list",
        template: _.template($('#app-medstemplate').html()),
        initialize: function (model, options) {
            var thisview = this;
            console.log(model, options);
            //this.model.set({ 'Medications': this.model.get('Medications') });
            //console.log('Medications');
            this.model.bind('reset', function () {
                thisview.render();
            });

        },

        render: function (eventName) {
            _.each(this.model.models, function (medsmodel) {
                console.log("phyview");
                this.$el.html(this.template(medsmodel.toJSON()));
            }, this);
            return this;

        }
    });


    var model = new MedsModel();
    model.fetch({ success: function () {
        console.log("inside fetch..");
        //  this.model.fetch();
        console.log(model.get('Medications'));
    }
    });
    //    var meds = new MedsModel();
    //    meds.fetch();
    //    //   var medsmodel = new MedicationsModel();
    //    var medsview = new MedsView({ model: meds });
});