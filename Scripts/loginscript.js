
   $(document).ready(function(){

       $('.popup .btnsubmit').click(function () {
           e.preventDefault();
                var login = $('#login').val();
                var password = $('#password').val();
                var model = {
                    UserName: login,
                    Password: password
                }
                if (login == '' || password == '') {
                    var err = "<label>Username or Password Field should not be left blank</label>";
                    $('.popup p').next().append(err);
                }
                else {
                    var dataurl = "http://localhost:56393/Account/LogIn";
                    $.ajax({
                        url: dataurl,
                        data: $('#signin-form').serialize(),
                        type: 'post',
                        dataType: 'json',
                        contentType: 'application/json; charset=utf-8',
                        error: function (data) {
                            if (data == undefined) {

                            }
                        },
                        success: function (data) {
                            if (data.roles != null) {
                                $.each(data.roles, function (key, value) {
                                    if (value == "Admin") {
                                        window.location.href = "http://localhost:56393/Home/Admin";
                                    }
                                    if (value == "Patient") {
                                        window.location.href = "http://localhost:56393/Home/AccessModels";
                                    }
                                    if (value == "Physician") {
                                        window.location.href = "http://localhost:56393/Home/Physician";
                                    }
                                });
                            }
                            else if (data.err != null) {
                                var thiserr = "<label>" + data.err + "</label>";
                                $('.popup p').next().append(thiserr);
                            }
                        }
                    });

                }
            });

        });
