$(function () {

    

    var sess_pollInterval = 60000;
    var sess_expirationMinutes = 2;
    var sess_warningMinutes = 0.50;
    var sess_intervalID;
    var sess_lastActivity;

    function initSession() {
        sess_lastActivity = new Date();
        sessSetInterval();
        $(document).bind('keypress.session', function (ed, e) {
            sessKeyPressed(ed, e);
        });
    }
    function sessSetInterval() {
        sess_intervalID = setInterval('sessInterval()', sess_pollInterval);
    }
    function sessClearInterval() {
        clearInterval(sess_intervalID);
    }
    function sessKeyPressed(ed, e) {
        sess_lastActivity = new Date();
    }
    function sessLogOut() {
        //window.location.replace = "/";
        window.location.href = '@Url.Action("LogOff", "Account")';

    }
    function sessInterval() {
        var now = new Date();
        //get milliseconds of differneces
        var diff = now - sess_lastActivity;
        //get minutes between differences
        var diffMins = (diff / 1000 / 60);
        if (diffMins >= sess_warningMinutes) {
            //wran before expiring
            //stop the timer
            sessClearInterval();
            //promt for attention
            var active = confirm('Your session will expire in ' + (sess_expirationMinutes - sess_warningMinutes) +
                ' minutes (as of ' + now.toTimeString() + '), press OK to remain logged in ' +
                'or press Cancel to log off. \nIf you are logged off any changes will be lost.');
            if (active == true) {
                now = new Date();
                diff = now - sess_lastActivity;
                diffMins = (diff / 1000 / 60);
                if (diffMins > sess_expirationMinutes) {
                    sessLogOut();
                }
                else {
                    initSession();
                    sessSetInterval();
                    sess_lastActivity = new Date();
                }
            }
            else {
                sessLogOut();
            }
        }
    }


});



$(function () { });