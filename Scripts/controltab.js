

$(function () {
    // Create the "tabs"
    $('.nav-tabs').each(
        function () {
            var currentTab, ul = $(this);
            $(this).find('a').each(
                function (i) {
                    var a = $(this).bind(
                        'click',
                        function () {
                            if (currentTab) {
                                ul.find('li.active').removeClass('active');
                                $(currentTab).hide();

                            }
                            $(this).parent().addClass('active');
                            currentTab = $(this).attr('href');
                            
                            $(currentTab).show();
                            setJscroll();
                           // $('.app-pane').jScrollPane();
                            return false;}

                    );
                    $(a.attr('href')).hide();
                    
                }
            );
           
        }
        
    );
    function setJscroll() {
        $('.app-pane').jScrollPane();
        var mediawidth=$(window).width();
        if (mediawidth <= 768)
        {
            var affixHeight = $('.control-panel').find('.controler').height();
            var affheight = affixHeight - 129;
            $('.jspContainer').css("height", affheight + "px");
        }
    }
    $('.app-pane').click(function () {
        setJscroll();
    });
    $('#profile a').click();
});



$(document).ready(function () {
    $('.authorization').click(function () {
        var dataurl = "http://localhost:56393/AuthenticateUser/GetAccessUser";
        $.ajax({
            url: dataurl,
            data:"",
            type: 'get',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            error: function (data) {
                if (data == undefined) {
                    showauthadd();
                }
             },
            success: function (data) {
                if (data !=null) {
                    showdatacurrent(data);
                   
                }
                else if (data == null) {
                    showauthadd();
                }
            }
        });

        return false;

        function showauthadd() {
            if (!$(".stream-authcurrent_new").hasClass("hide")) {
                $(".stream-authcurrent_new").addClass("hide");

            }
                    $(".stream-authorization_new").removeClass("hide");
                    $(".stream-authorization_new").find(".emailid").focus();
                    
                    $(".stream-authorization_new #btncurrent").unbind("click").on("click", function () {

                        $(this).parents(".stream-entry").addClass("hide");
                        $('.authorization').click();
                         return false;
                    });
                                   

                    $(".stream-authorization_new .btn-cancel").unbind("click").on("click", function () {
                        $(this).parents(".stream-entry").addClass("hide");
                        return false;
                    });
        }
        function showdatacurrent(data) {
                   $(".stream-authcurrent_new").removeClass("hide");
                    $(".stream-authcurrent_new .btn-cancel").unbind("click").on("click", function () {
                        $(this).parents(".stream-entry").addClass("hide");
                        return false;
                    });
                    $(".stream-authcurrent_new #btnAdd").unbind("click").on("click", function () {

                        $(this).parents(".stream-entry").addClass("hide");
                        showauthadd();
                       // $(".stream-authorization_new").removeClass("hide");
                        return false;
                    });
                    $(".stream-authcurrent_new #btnremove").unbind("click").on("click", function () {
                    var checklength = $('.currentuser:checked').length;
                    if (checklength != "0") {
                        removeaccessuser();
                        $(".stream-authcurrent .entry-wrapper .entry-body").empty();
                        $('.authorization').click();
                    }
                    else{
                        alert("please select user to remove access..")
                    }
                    //$(".stream-authcurrent .entry-wrapper .entry-body").empty();
                    //$(this).parents(".stream-entry").addClass("hide");
                    
                     return false;
                    });
                    function removeaccessuser() {

                        $('.currentuser:checked').each(function () {
                            var userid = $(this).attr('id');
                            var deleteurl = "http://localhost:56393/AuthenticateUser/RemoveAccessUser/?UserAccessId=" + userid;
                            $.ajax({
                                url: deleteurl,
                                type: 'delete',
                                dataType: 'json',
                                contentType: 'application/json; charset=utf-8',
                                complete: function (data) {
                                },
                                success: function (data) {
                                }
                            });
                        });
                        
                    }
                  
                    $(".stream-authcurrent .entry-wrapper .entry-body").empty();
                    var h3 = "<h3>Current access Users</h3>";
                    $(".stream-authcurrent .entry-wrapper .entry-body").append(h3);

                    $.each(data, function (key, value) {

                    var accessusers= "<input id=" + value.UserAccess + " type="+ "checkbox"+" name="+ value.UserAccess + " class=" + "currentuser"+" />" +"<span class=" + "curruser>" + value.EmailId + "</span><br/>";
                    $(".stream-authcurrent .entry-wrapper .entry-body").append(accessusers);

                     });

        }
       
    });

  

});




$(document).ready(function () {
    $('.sidebar-nav > ul > li').click(function () {
        var stream = $(this).find('a').attr('href');
        if (stream = "#stream-entry") {
            $('div.control-panel .controler').css('opacity', '1');
        }
        if (!$('.stream-authorization_new').hasClass('hide'))
        {
            $('.stream-authorization_new').addClass('hide');
        }
        var that = this;
        $('.sidebar-nav ul li').removeClass('active');
        $(that).addClass('active');
    });
});