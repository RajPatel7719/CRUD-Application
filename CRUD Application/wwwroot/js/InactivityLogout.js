
//// Set timeout variables.
//var timoutWarning = 840000; // Display warning in 14 Mins.
//var timoutNow = 60000; // Warning has been shown, give the user 1 minute to interact
//var logoutUrl = 'logout.php'; // URL to logout page.

//var warningTimer;
//var timeoutTimer;

//// Start warning timer.
//function StartWarningTimer() {
//    warningTimer = setTimeout("IdleWarning()", timoutWarning);
//    window.onload = ResetTimeOutTimer;
//    window.onmousemove = ResetTimeOutTimer;
//    window.onmousedown = ResetTimeOutTimer;  // catches touchscreen presses as well
//    window.ontouchstart = ResetTimeOutTimer; // catches touchscreen swipes as well
//    window.ontouchmove = ResetTimeOutTimer;  // required by some devices
//    window.onclick = ResetTimeOutTimer;      // catches touchpad clicks as well
//    window.onkeydown = ResetTimeOutTimer;
//    window.addEventListener('scroll', ResetTimeOutTimer, true); // improved; see comments

//}

//// Reset timers.
//function ResetTimeOutTimer() {
//    clearTimeout(timeoutTimer);
//    StartWarningTimer();
//    $("#timeout").dialog('close');
//}

//// Show idle timeout warning dialog.
//function IdleWarning() {
//    clearTimeout(warningTimer);
//    timeoutTimer = setTimeout("IdleTimeout()", timoutNow);
//    $("#timeout").dialog({
//        modal: true
//    });
//    // Add code in the #timeout element to call ResetTimeOutTimer() if
//    // the "Stay Logged In" button is clicked
//}

//// Logout the user.
//function IdleTimeout() {
//    window.location = logoutUrl;
//}

$(document).ready(function () {
    setupTimers();
});

var warningTimeout = 840000; // Display warning in 14 Mins.
var timeoutTimerID;

function startTimer() {
    // window.setTimeout returns an Id that can be used to start and stop a timer
    warningTimerID = window.setTimeout(IdleTimeout, warningTimeout);
}

function resetTimer() {
    window.clearTimeout(timeoutTimerID);
    startTimer();
}

// Logout the user.
function IdleTimeout() {
    window.location = "https://localhost:7114";
}

function setupTimers() {
    document.addEventListener("mousemove", resetTimer, false);
    document.addEventListener("mousedown", resetTimer, false);
    document.addEventListener("keypress", resetTimer, false);
    document.addEventListener("touchmove", resetTimer, false);
    startTimer();
}

//$(document).ready(function () {
//    ResetThisSession();
//});

//var timeInSecondsAfterSessionOut = 10;
//var secondTick = 0;

//function ResetThisSession() {
//    secondTick = 0;
//}

//function StartThisSessionTimer() {
//    secondTick++;
//    var timeLeft = ((timeInSecondsAfterSessionOut - secondTick) / 60).toFixed(0);
//    timeLeft = timeInSecondsAfterSessionOut - secondTick;

//    $("#spanTimeLeft").html(timeLeft);

//    if (secondTick >= timeInSecondsAfterSessionOut) {
//        clearTimeout(tick);
//        window.location = "https://localhost:7114";
//        return;
//    }
//    tick = setTimeout("StartThisSessionTimer()", 1000);
//}

//StartThisSessionTimer();