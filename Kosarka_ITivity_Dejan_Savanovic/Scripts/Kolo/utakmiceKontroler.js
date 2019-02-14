$(document).ready(function () {

    var data = {
        id: $("#koloID").val()
    };

    $.get("/Kolo/GetUtakmice", data, function (result, status) {

        $("#utakmice").html(result);

    });

    $("#btnDodaj").click(function () {

        var podaci = {
            utakmicaID: "-1",
            koloID: $("#koloID").val()
        };


        $.get("/Kolo/FormaCreateEdit", podaci, function (result, status) {

            $("#btnDodaj").css("display", "none");
            $("#formaDodaj").html(result);

            $("#btnZatvori").click(function () {
                $("#formaDodaj").html("");
                $("#btnDodaj").css("display", "block");
            });

        })

    });

});