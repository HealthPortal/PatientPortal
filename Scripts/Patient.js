

$(function () {

    var Patient = Backbone.Model.extend({
        url: "http://localhost:56393/api/PatientDemographics/",
        IDAttribute: "PatientID",
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
        url: "http://localhost:56393/api/PatientDemographics"
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




    var Physician = Backbone.Model.extend({
        url: "http://localhost:56393/api/Physicians/",
        IDAttribute: "Physicianid",
        defaults: {
            "Physicianid": '0',
            "Name": "",
            "StreetAddress": "",
            "StreetAddress2": "",
            "Locality": "",
            "Region": "",
            "PostalCode": "",
            "Country": "",
            "PrimaryPhone": "",
            "Department": "",
            "HospitalName": ""
        }
    });


    var PhysicianCollection = Backbone.Collection.extend({
        model: Physician,
        url: "http://localhost:56393/api/Physicians"
    });


    var PhysicianView = Backbone.View.extend({
        el: "#app-profile .phr-data--body",
        template: _.template($('#profiletemplate').html()),
        events: {
            'click .new-message': 'sendmessage'
        },
        sendmessage: function (e) {
            var clickme = $(e.currentTarget);
            var sendTo = clickme.attr("id");

            $(".stream-entry--message_new").find("a.sendTo").text(sendTo);
            $(".stream-entry--message_new").find("input.sendTo").text(sendTo);
            $(".stream-entry--message_new").removeClass("hide");
            $(".stream-entry--message_new").find("textarea").focus();


            $(".stream-entry--message_new .btn-cancel").unbind("click").on("click", function () {
                $(this).parents(".stream-entry").addClass("hide");
                return false;
            });

            //open new stream item
            //$(".btn-post-message").unbind("click").on("click", function () {
            // $(".stream-entry--message_new").addClass("hide");
            // $(".new-message-textarea").val("");

            // //    //submitNewMessage();
            // //    return false;
            // //    e.preventDefault();
            // });

            function submitNewMessage() {
                var msgText = $(".new-message-textarea").val().replace(/\r\n\r\n/g, "</p><p>").replace(/\n\n/g, "</p><p>");
                $(".stream-entry--message_new").addClass("hide");
                $(".new-message-textarea").val("");
                var newMessage = $("#entry-new").last().clone().removeAttr("id");
                var date = new Date();
                newMessage.find(".entry-body").first().html(msgText);
                newMessage.find("a.sendTo").text(sendTo);
                newMessage.find(".meta-ago").attr("title", date.toISOString()).timeago();
                newMessage.removeClass("hide");
                $("#entry-new").after(newMessage);
                setTimeout(function () {
                    newMessage.find("#reply-template").removeClass("hide");
                    newMessage.find(".new-reply").removeClass("new-reply");
                    newMessage.find(".stream-entry_reply .entry-from-user").text(sendTo);
                }, 4000);
                return false;
            }

            $(".entry_reply-input textarea").unbind("keydown").live("keydown", function (e) {
                if (e.which == 13) {
                    var newReply = $("#reply-template").clone().removeAttr("id");
                    var msgText = "<a class='entry-from-user' href=''>Me</a> " + $(this).val().replace(/\r\n\r\n/g, "</p><p>").replace(/\n\n/g, "</p><p>");
                    var date = new Date();
                    newReply.removeClass("hide");
                    newReply.find(".entry-body").html(msgText);
                    newReply.find(".meta-ago").attr("title", date.toISOString()).timeago();
                    newReply.find(".user-avatar img").attr("src", "../../img/me.jpg");
                    $(this).parent().prev().append(newReply);
                    $(this).val("");
                    newReply.removeClass("new-reply");
                    return false;
                }
            });

            e.preventDefault();
        },
        render: function (eventName) {
            _.each(this.model.models, function (physician) {
                var appprofiletemplate = this.template(physician.toJSON());
                $(this.el).append(appprofiletemplate);
            }, this);
            return this;
        }

    });
    var physicians = new PhysicianCollection();
    var physiciansview = new PhysicianView({ model: physicians });
    physicians.fetch();
    physicians.bind('reset', function () {
        physiciansview.render();
    });







    var VitalsModel = Backbone.Model.extend({
        url: "http://localhost:56393/api/Vitals/",
        IDAttribute: "Vitalsid",
        defaults: {
            "Vitalsid": '0',
            "Height": "",
            "Weight": "",
            "BloodPressure": "",
            "BMI": "",
            "RecordedDate": ""
        }
    });


    var VitalsCollection = Backbone.Collection.extend({
        model: VitalsModel,
        url: "http://localhost:56393/api/Vitals"
    });


    var VitalsView = Backbone.View.extend({
        el: "#app-vitals",
        template: _.template($('#vitalstemplate').html()),
        render: function (eventName) {
            _.each(this.model.models, function (vital) {
                var vitalstemplate = this.template(vital.toJSON());
                $(this.el).append(vitalstemplate);
            }, this);
            return this;
        }
    });
    var vitals = new VitalsCollection();
    var vitalsview = new VitalsView({ model: vitals });
    vitals.fetch();
    vitals.bind('reset', function () {
        vitalsview.render();
    });





    var AllergiesModel = Backbone.Model.extend({
        url: "http://localhost:56393/api/Allergies",
        defaults: {
            "MedicationAllergiesid": "",
            "Allergen": "",
            "Reaction": "",
            "Status": "",
            "RxNormCode": ""
        }
    });


    var AllergiesCollection = Backbone.Collection.extend({
        model: AllergiesModel,
        url: "http://localhost:56393/api/Allergies"
    });


    var AllergiesView = Backbone.View.extend({
        el: "#app-issues #app-issues--allergies",
        template: _.template($('#appissuesallergytemplate').html()),
        render: function (eventName) {
            _.each(this.model.models, function (allergy) {
                var appissuesallergytemplate = this.template(allergy.toJSON());
                $(this.el).append(appissuesallergytemplate);
            }, this);

            return this;
        }
    });
    var allergies = new AllergiesCollection();
    var allergiesview = new AllergiesView({ model: allergies });
    allergies.fetch();
    allergies.bind('reset', function () {
        allergiesview.render();
        var $table_collapses = $('#app-issues--allergies .issueallerg'),
            $collapsebodies = $('.collapse-body');

        $collapsebodies.collapse();

        $table_collapses.on('click', function () {
            $(this).find('.collapse-header').toggleClass('active');
            var $target = $(this).find('.collapse-body');
            $target.collapse('toggle');
        });
    });




    var ProblemsModel = Backbone.Model.extend({
        url: "http://localhost:56393/api/Problems",
        defaults: {
            "Problemsid": "",
            "ProblemCause": "",
            "Status": "",
            "ProblemReportedDate": "",
            "StandardCode": ""
        }
    });


    var ProblemsCollection = Backbone.Collection.extend({
        model: ProblemsModel,
        url: "http://localhost:56393/api/Problems"
    });


    var ProblemsView = Backbone.View.extend({
        el: "#app-issues #app-issues--problems",
        template: _.template($('#appissuesproblemtemplate').html()),
        render: function (eventName) {
            _.each(this.model.models, function (problem) {
                var appissuesproblemtemplate = this.template(problem.toJSON());
                $(this.el).append(appissuesproblemtemplate);
            }, this);
            return this;
        }
    });
    var problems = new ProblemsCollection();
    var problemsview = new ProblemsView({ model: problems });
    problems.fetch();
    problems.bind('reset', function () {
        problemsview.render();

        var $table_collapses = $('#app-issues--problems .issueprob'),
            $collapsebodies = $('.collapse-body');

        $collapsebodies.collapse();

        $table_collapses.on('click', function () {
            $(this).find('.collapse-header').toggleClass('active');
            var $target = $(this).find('.collapse-body');
            $target.collapse('toggle');
        });
    });




    var MedsModel = Backbone.Model.extend({
        url: "http://localhost:56393/api/Medications/",
        IDAttribute: "Medicationsid",
        defaults: {
            "Medicationsid": '0',
            "MedicationsName": "",
            "DosageForm": "",
            "SIG": "",
            "Status": "",
            "DateOfPrescription": "",
            "StandardCode": ""
        }
    });


    var MedsCollection = Backbone.Collection.extend({
        model: MedsModel,
        url: "http://localhost:56393/api/Medications"
    });


    var MedsView = Backbone.View.extend({
        el: "#app-medications--medication-list",
        template: _.template($('#app-medstemplate').html()),
        render: function (eventName) {
            _.each(this.model.models, function (med) {
                var appmedstemplate = this.template(med.toJSON());
                $(this.el).append(appmedstemplate);
            }, this);
            return this;
        }
    });
    var meds = new MedsCollection();
    var medsview = new MedsView({ model: meds });
    meds.fetch();
    meds.bind('reset', function () {
        medsview.render();
        var $table_collapses = $('#app-medications--medication-list .medscollap'),
            $collapsebodies = $('.collapse-body');

        $collapsebodies.collapse();

        $table_collapses.on('click', function () {
            $(this).find('.collapse-header').toggleClass('active');
            var $target = $(this).find('.collapse-body');
            $target.collapse('toggle');
        });
    });





    var VaccinationsModel = Backbone.Model.extend({
        url: "http://localhost:56393/api/Vaccinations",
        defaults: {
            "Vaccinationid": "",
            "VaccinationsName": "",
            "VaccineStatus": "",
            "DateAdministered": "",
            "CVXCode": ""
        }
    });


    var VaccinationsCollection = Backbone.Collection.extend({
        model: VaccinationsModel,
        url: "http://localhost:56393/api/Vaccinations"
    });


    var VaccinationsView = Backbone.View.extend({
        el: "#app-history #app-history--vaccinations",
        template: _.template($('#apphistoryvaccinetemplate').html()),
        render: function (eventName) {
            _.each(this.model.models, function (vaccination) {
                var apphistoryvaccinetemplate = this.template(vaccination.toJSON());
                $(this.el).append(apphistoryvaccinetemplate);
            }, this);
            return this;
        }
    });
    var vaccinations = new VaccinationsCollection();
    var vaccinationsview = new VaccinationsView({ model: vaccinations });
    vaccinations.fetch();
    vaccinations.bind('reset', function () {
        vaccinationsview.render();

        var $table_collapses = $('#app-history--vaccinations .vactbcollapse'),
            $collapsebodies = $('.collapse-body');

        $collapsebodies.collapse();

        $table_collapses.on('click', function () {
            $(this).find('.collapse-header').toggleClass('active');
            var $target = $(this).find('.collapse-body');
            $target.collapse('toggle');
        });
    });





    var ProceduresModel = Backbone.Model.extend({
        url: "http://localhost:56393/api/Procedures",
        defaults: {
            "Proceduresdetailsid": "",
            "ProcedureDetails": "",
            "ProcedureDate": "",
            "SNONEDCT": ""
        }
    });


    var ProceduresCollection = Backbone.Collection.extend({
        model: ProceduresModel,
        url: "http://localhost:56393/api/Procedures"
    });


    var ProceduresView = Backbone.View.extend({
        el: "#app-history #app-history--procedures",
        template: _.template($('#apphistoryproceduretemplate').html()),
        render: function (eventName) {
            _.each(this.model.models, function (procedure) {
                var apphistoryproceduretemplate = this.template(procedure.toJSON());
                $(this.el).append(apphistoryproceduretemplate);
            }, this);
            return this;
        }
    });
    var procedures = new ProceduresCollection();
    var proceduresview = new ProceduresView({ model: procedures });
    procedures.fetch();
    procedures.bind('reset', function () {
        proceduresview.render();

        var $table_collapses = $('#app-history--procedures .procdtbcollapse'),
            $collapsebodies = $('.collapse-body');

        $collapsebodies.collapse();

        $table_collapses.on('click', function () {
            $(this).find('.collapse-header').toggleClass('active');
            var $target = $(this).find('.collapse-body');
            $target.collapse('toggle');
        });
    });





    var CarePlansModel = Backbone.Model.extend({
        url: "http://localhost:56393/api/CarePlans",
        defaults: {
            "CarePlanid": "",
            "Goal": "",
            "Instructions": "",
            "SNOMEDCT": ""
        }
    });


    var CarePlansCollection = Backbone.Collection.extend({
        model: CarePlansModel,
        url: "http://localhost:56393/api/CarePlans"
    });


    var CarePlansView = Backbone.View.extend({
        el: "#app-history #app-history--carePlans",
        template: _.template($('#apphistorycareplanstemplate').html()),
        render: function (eventName) {
            _.each(this.model.models, function (careplan) {
                var apphistorycareplanstemplate = this.template(careplan.toJSON());
                $(this.el).append(apphistorycareplanstemplate);
            }, this);
            return this;
        }
    });
    var careplans = new CarePlansCollection();
    var careplansview = new CarePlansView({ model: careplans });
    careplans.fetch();
    careplans.bind('reset', function () {
        careplansview.render();

        var $table_collapses = $('#app-history--carePlans .careptbcollapse'),
            $collapsebodies = $('.collapse-body');

        $collapsebodies.collapse();

        $table_collapses.on('click', function () {
            $(this).find('.collapse-header').toggleClass('active');
            var $target = $(this).find('.collapse-body');
            $target.collapse('toggle');
        });
    });



    var SmokingStatusModel = Backbone.Model.extend({
        url: "http://localhost:56393/api/SocialHistories",
        defaults: {
            "SocialHistId": "",
            "SocialHistoryItem": "",
            "Description": "",
            "SNOMEDCT": ""
        }
    });


    var SmokingStatusCollection = Backbone.Collection.extend({
        model: SmokingStatusModel,
        url: "http://localhost:56393/api/SocialHistories"
    });


    var SmokingStatusView = Backbone.View.extend({
        el: "#app-history #app-history--smokestatus",
        template: _.template($('#apphistorysmokestatustemplate').html()),
        render: function (eventName) {
            _.each(this.model.models, function (smokestatus) {
                var apphistorysmokestatustemplate = this.template(smokestatus.toJSON());
                $(this.el).append(apphistorysmokestatustemplate);
            }, this);
            return this;
        }
    });
    var smokestatuss = new SmokingStatusCollection();
    var smokestatusview = new SmokingStatusView({ model: smokestatuss });
    smokestatuss.fetch();
    smokestatuss.bind('reset', function () {
        smokestatusview.render();

        var $table_collapses = $('#app-history--smokestatus .smoketbcollapse'),
            $collapsebodies = $('.collapse-body');

        $collapsebodies.collapse();

        $table_collapses.on('click', function () {
            $(this).find('.collapse-header').toggleClass('active');
            var $target = $(this).find('.collapse-body');
            $target.collapse('toggle');
        });
    });



    //var MessageModel = Backbone.Model.extend({
    //    url: "http://localhost:56393/api/Messages/",
    //    IDAttribute: "MessageID",
    //    defaults: {
    //        "MessageID": '0',
    //        "MessageDateTime": "",
    //        "MessageSequenceNumber": "",
    //        "MessageText": "",
    //        "SenderName": "",
    //        "ReceiverName": "",
    //        "Unread": ""
    //    }
    //});


    //var MessagesCollection = Backbone.Collection.extend({
    //    model: MessageModel,
    //    url: "http://localhost:56393/api/Messages"
    //});



    //var MessagesView = Backbone.View.extend({
    //    el: ".stream-entries",
    //    template: _.template($('#streammessagetemplate').html()),
    //    render: function () {
    //        _.each(this.model.models, function (message) {
    //            var msgnewview = new MSGChildView({ model: message });
    //        }, this);http://localhost:56393/Controllers/AuthenticateUserController.cs
    //        return this;
    //    }
    //});
    //var messages = new MessagesCollection();
    //var messagesview = new MessagesView({ model: messages });
    //messages.fetch();
    //messages.bind('reset', function () {
    //    messagesview.render();
    //});

    //var MSGChildView = Backbone.View.extend({
    //    el: ".stream-entries",
    //    template: _.template($('#streammessagetemplate').html()),
    //    initialize: function () {
    //        this.render();

    //    },
    //    render: function () {
    //        var streammessagetemplate = this.template(this.model.toJSON());
    //        $(this.el).append(streammessagetemplate);
    //    }

    //});





    var StreamMedicationView = Backbone.View.extend({
        el: ".stream-entries",
        template: _.template($('#streammedicationstemplate').html()),
        render: function (eventName) {
            _.each(this.model.models, function (med) {
                var streammedicationstemplate = this.template(med.toJSON());
                $(this.el).append(streammedicationstemplate);
            }, this);
            return this;
        }
    });
    var meds = new MedsCollection();
    var streammedicationview = new StreamMedicationView({ model: meds });
    meds.fetch();
    meds.bind('reset', function () {
        streammedicationview.render();

    });



    var LabResultsModel = Backbone.Model.extend({
        url: "http://localhost:56393/api/LabResult/",
        IDAttribute: "LabId",
        defaults: {
            "LabId": '0',
            "OrderableTestName": "",
            "TestResultValue": "",
            "DatePerformed": "",
            "LONICCode": ""
        }
    });


    var LabResultsCollection = Backbone.Collection.extend({
        model: LabResultsModel,
        url: "http://localhost:56393/api/LabResult"
    });

    var StreamLabResultsView = Backbone.View.extend({
        el: ".stream-entries",
        template: _.template($('#streamlabresultstemplate').html()),
        render: function (eventName) {
            _.each(this.model.models, function (labrest) {
                var streamlabresultstemplate = this.template(labrest.toJSON());
                $(this.el).append(streamlabresultstemplate);
            }, this);
            return this;
        }
    });
    var labrests = new LabResultsCollection();
    var streamlabresultsview = new StreamLabResultsView({ model: labrests });
    labrests.fetch();
    labrests.bind('reset', function () {
        streamlabresultsview.render();

    });

    var InsuBillModel = Backbone.Model.extend({
        url: "http://localhost:56393/api/InsuranceBilling/",
        IDAttribute: "InsuranceBillingid",
        defaults: {
            "InsuranceBillingid": '0',
            "Date": "",
            "Diagnosis": "",
            "ICDCode": "",
            "Charges": "",
            "Payments": "",
            "Image1": "",
            "Image2": "",
            "Balance": ""
        }
    });


    var InsuBillCollection = Backbone.Collection.extend({
        model: InsuBillModel,
        url: "http://localhost:56393/api/InsuranceBilling"
    });
    var StreamInsuBillView = Backbone.View.extend({
        el: ".stream-entries",
        template: _.template($('#streaminsbilltemplate').html()),
        events: {
            'click .insurance_pdf': 'viewpdf'
        },
        viewpdf: function (e) {
            var clickme = $(e.currentTarget);
            $.ajax({
                url: '/ViewInsBillPdf/InsBillPdf',
                type: 'get',
                contentType: 'application/json;charset=utf-8',
                error: function (data) {
                },
                success: function (result) {
                    $('#InsBillingView').find('.modal-body').html(result);
                    var insmodal = "<a data-toggle=" + "modal" + " class=" + "showInsBillingView" + " href=" + "#InsBillingView" + "></a>";
                    $('#app-insurance').append(insmodal);
                    $('.showInsBillingView').click();
                }
            });

            e.preventDefault();
        },
        render: function (eventName) {
            _.each(this.model.models, function (insbill) {
                var streaminsbilltemplate = this.template(insbill.toJSON());
                $(this.el).append(streaminsbilltemplate);
            }, this);
            return this;
        }
    });
    var insbills = new InsuBillCollection();
    var streaminsbillsview = new StreamInsuBillView({ model: insbills });
    insbills.fetch();
    insbills.bind('reset', function () {
        streaminsbillsview.render();

    });

    var AppointmentModel = Backbone.Model.extend({
        url: "http://localhost:56393/api/Appointment/",
        IDAttribute: "AppointmentsID",
        defaults: {
            "AppointmentsID": '0',
            "Date": "",
            "Reason": "",
            "Time": "",
            "Physician": "",
            "CenterName": "",
            "Address": ""
        }
    });


    var AppointmentCollection = Backbone.Collection.extend({
        model: AppointmentModel,
        url: "http://localhost:56393/api/Appointment"
    });

    var StreamAppointmentsView = Backbone.View.extend({
        el: ".stream-entries",
        template: _.template($('#streamappointmentsstemplate').html()),
        events: {
            'click .btn-cancel': 'cancelappointment',
            'click .btn-danger': 'nocancelappointment',
            'click .btn-success': 'yescancelappointment'
        },
        cancelappointment: function (e) {
            var clickme = $(e.currentTarget);
            $(clickme).parent().parent().find('.entry_cancel-screen').removeClass('hide');
            e.preventDefault();
        },
        nocancelappointment: function (e) {
            var clickme = $(e.currentTarget);
            $(clickme).parent().parent().addClass('hide');
            e.preventDefault();
        },
        yescancelappointment: function (e) {
            var clickme = $(e.currentTarget);
            var id = $(clickme).attr('data-id');
          
            $.ajax({
                url: '/api/Appointment?id=' + id,
                type: 'delete',
                contentType: 'application/json;charset=utf-8',
                error: function () {
                },
                success: function () {
                    $(clickme).parents('.stream-entry--appointment').remove().animate('fadeOut', 'slow');
                }
            });

            e.preventDefault();

        },
        render: function (eventName) {
            _.each(this.model.models, function (appointment) {
                var streamappointmentsstemplate = this.template(appointment.toJSON());
                $(this.el).append(streamappointmentsstemplate);
            }, this);
            return this;
        }
    });
    var appointments = new AppointmentCollection();
    var streamappointmentsview = new StreamAppointmentsView({ model: appointments });
    appointments.fetch();
    appointments.bind('reset', function () {
        streamappointmentsview.render();

    });



    var Approuter = Backbone.Router.extend({
        initialize: function () {



        }


    });


    var Appview = Backbone.View.extend({


    });

});