$(document).ready(function () {

    $("#btnClose1").click(function () {
        console.log("create");
        $("#modalIgracCreate").modal('hide');
    });

    $("#btnClose2").click(function () {
        console.log("edit");
        $("#modalIgracEdit").modal('hide');
    });

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
            gradID: $("#GradID").val(),
            brojDresa: $("#BrojDresa").val()
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
            ime: $("#txtIme").val(),
            prezime: $("#txtPrezime").val(),
            pozicijaID: $("#txtPozicijaID").val(),
            drzava: $("#txtDrzava").val(),
            slikaID: $("#txtSlikaID").val(),
            slika: $("#txtSlika").attr("src"),
            datumRodjenja: $("#txtDatumRodjenja").val(),
            timID: $("#txtTimID").val(),
            gradID: $("#txtGradID").val(),
            igracID: $("#txtIgracID").val(),
            brojDresa: $("#txtBrojDresa").val()
        };

        console.log(data);

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

        if (idSlike.startsWith("#txt")) $("#txtSlikaID").val("-1");
        else $("#SlikaID").val("-1");

        var reader = new FileReader();

        reader.onload = function (e) {

            $(idSlike).attr('src', e.target.result);
 
        };

        reader.readAsDataURL(input.files[0]);
    }
}