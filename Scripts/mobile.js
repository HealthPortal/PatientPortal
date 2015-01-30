$(document).ready(function () {

    //    $(".container .navbar-inner .btn-navbar").on("click", function (e) {
    //        e.preventDefault();

    //        if ($(".navbar").hasClass("shown") && $(".sidebar").hasClass("shown")) {
    //            $(".navbar").removeClass("shown") && $(".sidebar").removeClass("shown");
    //        }
    //        else
    //        $(".navbar").addClass("shown") && $(".sidebar").addClass("shown");
    //    });

    //$(".btn-navbar").click(function(){

    //    if (!$find($(".navbar")).hasClass("shown")) {
    //        $find($(".navbar")).addClass("shown");
    //    } else {
    //        $find($(".navbar")).removeClass("shown");
    //       }

    //    if (!$find($(".sidebar")).hasClass("shown")) {
    //        $find($(".sidebar")).addClass("shown");
    //    } else {
    //        $find($(".sidebar")).removeClass("shown");
    //    }
    //    
    //});

    $("#btn-nav").on("click",function () {
       
        if ($(".navbar").hasClass('shown')) {
            $(".navbar").addClass('shown');
        }
        else {
            $(".navbar").removeClass('shown');
        }

        if ($(".sidebar").hasClass('shown')) {
            $(".sidebar").addClass('shown');
        }
        else {
            $(".sidebar").removeClass('shown');
        }
    });

});