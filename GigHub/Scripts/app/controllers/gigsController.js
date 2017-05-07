var GigsController = function (attendaceService) {
    var button;

    var toggleAttendance = function (e) {
        button = $(e.target);

        var gigId = button.attr("data-gig-id");

        if (button.hasClass("btn-default"))
            attendaceService.createAttendance(gigId, done, fail);

        else
            attendaceService.deleteAttendace();

    };





    var fail = function () {
        alert("Something failed!");
    }

    var done = function () {

        var text = (button.text === "Going") ? "Going?" : "Going";

        button
            .toggleClass("btn-info")
            .toggleClass("btn-default")
            .text(text);
    }


    var init = function (container) {
        $(container).on("click", ".js-toggle-attendance", toggleAttendance);

    };




    return {
        init: init
    }

}(AttendanceService);
