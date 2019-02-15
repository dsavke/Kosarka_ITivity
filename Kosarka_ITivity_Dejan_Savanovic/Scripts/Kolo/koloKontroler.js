$(document).ready(function () {

    ograniciDatum(true);

    $(window).resize(function () {

        if ($(document).width() < 994) ograniciDatum(false);
        else ograniciDatum(true);

    });

    $("#btnKreirajKolo").click(function () {

        $.get("/Kolo/GetNextBrojKola", function (result, status) {

            const fp1 = flatpickr("#pocetakKola", {
                defaultDate: new Date()
            });

            const fp2 = flatpickr("#krajKola", {
                defaultDate: new Date()
            });

            $("#BrojKola").val("" + result.Brojkola);

            $("#modalKoloCreate").modal();

        })

    });

    $("#btnSacuvaj").click(function () {

        var data = {
            brojKola: $("#BrojKola").val(),
            pocetakKola: $("#pocetakKola").val(),
            krajKola: $("#krajKola").val()
        };

        console.log(data);

        $.post("/Kolo/Create", data, function (result, status) {

            if (result.Success) {
                $("#modalKoloCreate").modal("hide");
            } else {
                console.log(result.Message);
            }

        });

    });

});

function ograniciDatum(ispod) {

    var data = {
        koloID: $("#KoloID").val()
    };

    $.get("/Kolo/GetDate", data, function (result, status) {

        const fp = flatpickr("#datum", {
            inline: ispod,
            minDate: result.PocetniDatum,
            maxDate: result.KrajniDatum,
            defaultDate: result.PocetniDatum + " to " + result.KrajniDatum,
            mode: "range",
            onChange: function (selectedDates, dateStr, instance) {

                var podaci = {

                    pocetniDatum: dateStr.substr(0, 10),
                    krajDatum: dateStr.substr(14, 10),
                    id: $("#KoloID").val()

                };

                //console.log(podaci);

                $.get("/Kolo/GetRaspored", podaci, function (result, status) {

                    $("#utakmice").html(result);

                    $(".detaljiUtakmice").click(function () {
                        console.log("kliknuta utakmica");
                    });

                });

            }
        });


        var podaci = {

            pocetniDatum: $("#datum").val().substr(0, 10),
            krajDatum: $("#datum").val().substr(14, 10),
            id: $("#KoloID").val()

        };

        $.get("/Kolo/GetRaspored", podaci, function (result, status) {

            $("#utakmice").html(result);

            $(".detaljiUtakmice").click(function () {

                window.location.href = '/Utakmica/Index?utakmicaID=' + $(this).data('utakmicaid');

            });

        });

    });

};


$("#KoloID").change(function () {

    ograniciDatum(true);

})