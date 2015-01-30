
//$(document).ready(function () {
//    $appPane = $(".app-pane");
//    $jspPane = $appPane.find(".jspPane");
//    $vcard = $(".js-vcard");


//    $appPane.jScrollPane({
//        verticalGutter:0,
//        'showArrows': false
//    });
 
//    $appPane.data("jsp");

//    //bind window resize
//    $(window).resize(function(){
//        if ($(window).width() > 768) {
//            $jspPane.css({ width: '+=' + $('.jspTrack').width() });
//        }
//        });
  
//        $appPane.bind("jsp-scroll-y",function(event,scrollPositionY,isAtTop,isAtBottom){
//            if (isAtTop){
//                $(this).addClass("atTop");
//            } else {
//                $(this).removeClass("atTop");
//            }
//            if (isAtBottom || $(".jspContainer").height() > $(".jspPane").height()){
//                $(this).addClass("atBottom");
//            } else {
//                $(this).removeClass("atBottom");
//            }
//        });


//        function getPercentScrolledY(){
//            $('.jspPane').css({width:'+=' + $('.jspTrack').width()});
//            $appPane.prepend("<b class='shadTop'></b><b class='shadBottom'></b>");
//        }
//});
//            //function refreshRemoteScroll() {
            //    if ($(this).width() > 728){
            //        self.api.reinitialise();
            //        self.api.scrollToY(0,false);
            //        $jspPane.css({width:'+=' + $('.jspTrack').width()});
            //    }
            //}

    //function navcollapse() {

    //    var navcoll= $(".container").find(".navbar .navbar-fixed-top");
    //    navcoll.toggleClass("shown");
    //    $(".row").find(".sidebar").toggleClass("shown");
    //    var rowstream= $(".row").find(".stream");
    //    rowstream.toggleClass("shown");
    //}

