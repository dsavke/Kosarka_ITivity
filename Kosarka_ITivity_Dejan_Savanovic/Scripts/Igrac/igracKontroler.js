$(document).ready(function () {

    $("#btnKreirajIgraca").click(function () {

        $.get("/Igrac/Create", function (result, status) {

            $("#modalTijeloCreate").html(result);
            $("#modalIgracCreate").modal();

        });

    });

    $("#btnSacuvaj").click(function () {

        var data = {
            ime: $("#Ime").val(),
            prezime: $("#Prezime").val(),
            pozicijaID: $("#PozicijaID").val(),
            drzava: $("#Drzava").val(),
            slikaID: $("#SlikaID").val(),
            slika: $("#Slika").attr("src"),
            datumRodjenja: $("#DatumRodjenja").val(),
            timID: $("#TimID").val(),
            gradID: $("#GradID").val()
        };

        console.log(data);

        $.post("/Igrac/Create", data, function (result, status) {

            if (result.Success) {
                $("#modalIgracCreate").modal("hide");
                console.log(result.Success);
            } else {
                console.log(result.Message);
            }

        });

    });

    $(".btnEdit").click(function () {

        var data = {
            id: $(this).attr("data-igracID")
        };

        $.get("/Igrac/Edit", data, function (result, status) {

            $("#modalTijeloEdit").html(result);
            $("#modalIgracEdit").modal();

        });

    });

    $("#btnAzuriraj").click(function () {

        var data = {
            ime: $("#Ime").val(),
            prezime: $("#Prezime").val(),
            pozicijaID: $("#PozicijaID").val(),
            drzava: $("#Drzava").val(),
            slikaID: $("#SlikaID").val(),
            slika: $("#Slika").attr("src"),
            datumRodjenja: $("#DatumRodjenja").val(),
            timID: $("#TimID").val(),
            gradID: $("#GradID").val(),
            igracID: $("#IgracID").val()
        };

        $.post("/Igrac/Edit", data, function (result, status) {

            if (result.Success) {
                $("#modalIgracEdit").modal("hide");
                console.log(result.Success);
            } else {
                console.log(result.Message);
            }

        });

    });

});

function changeImage(input, slika) {
    var idSlike = '#' + slika.id;
    if (input.files && input.files[0]) {

        $("#SlikaID").val("-1");
        var reader = new FileReader();

        reader.onload = function (e) {
            $(idSlike)
                .attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
    }
}