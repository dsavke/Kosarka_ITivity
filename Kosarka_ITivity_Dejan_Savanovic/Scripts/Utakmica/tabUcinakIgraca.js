$(document).ready(function () {

    $("#btnDomaci").css('background-color', '#DF691A');

    $("#btnDomaci").click(function () {

        $("#btnDomaci").css('background-color', '#DF691A');
        $("#btnGosti").css('background-color', '#4E5D6C');

        var data = {
            utakmicaID: $("#btnDomaci").data('utakmicaid'),
            timID: $("#btnDomaci").data('timid')
        };

        console.log(data);

        $.get("/Utakmica/GetUcinakIgraca", data, function (result, status) {
            $("#prikazIgraca").html(result);
        });

    });

    $("#btnGosti").click(function () {

        $("#btnGosti").css('background-color', '#DF691A');
        $("#btnDomaci").css('background-color', '#4E5D6C');

        var data = {
            utakmicaID: $("#btnGosti").data('utakmicaid'),
            timID: $("#btnGosti").data('timid')
        };

        console.log(data);

        $.get("/Utakmica/GetUcinakIgraca", data, function (result, status) {
            $("#prikazIgraca").html(result);
        });

    });

});