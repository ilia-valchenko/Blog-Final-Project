$(document).ready(function () {
    $("#open").click(function (e) {
        e.preventDefault();
        $("#popup").show();
    });

    $("#close_button").click(function () {
        $("#popup").hide();
    });
});