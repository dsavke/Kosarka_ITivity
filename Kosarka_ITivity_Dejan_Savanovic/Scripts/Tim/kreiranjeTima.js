$(document).ready(function () {

    $(".btnEdit").click(function () {
        console.log($(this).attr("data-timID"));

        var data = {
            id: $(this).attr("data-timID")
        };

        $.get("/Tim/Edit", data, function (result, status) {
            $("#modalTijelo").html(result);
            $("#modalEditTim").modal();
        });

    });

    $("#btnAzuriraj").click(function () {

        var data = {
            slikaID: $("#SlikaID").val(),
            timID: $("#TimID").val(),
            slika: $("#Slika").attr("src"),
            naziv: $("#Naziv").val(),
            trener: $("#Trener").val(),
            stadionNaziv: $("#NazivStadiona").val(),
            gradID: $("#GradID").val()
        };

        console.log(data);

        $.post("/Tim/Edit", data, function (result, status) {
            if (result.Success) {
                $("#modalEditTim").modal("hide");
            } else {
                console.log(result.Message);
            }
        });
    });

    $("#btnKreirajTim").click(function () {
        
        $.get("/Tim/GetGrads", function (result, status) {

            $("#txtGradID").empty();

            $.each(result, function (index, value) {
                $('#txtGradID').append($('<option/>', {
                    value: value.Value,
                    text: value.Text
                }));
            });

            $("#mdalKreirajTim").modal();
        });

    });

    $("#btnSacuvaj").click(function () {
   
        var data = {
            slika: $("#image").attr("src"),
            naziv: $("#txtNaziv").val(),
            trener: $("#txtTrener").val(),
            stadionNaziv: $("#txtNazivStadiona").val(),
            gradID: $("#txtGradID").val()
        };    

        $.post("/Tim/Create", data, function (result, status) {
            if (result.Success) {
                $("#mdalKreirajTim").modal("hide");
            } else {
                console.log(result.Message);
            }
        });

        console.log(data);

    });

});

function changeImage(input) {
    console.log(input);
    if (input.files && input.files[0]) {

        $("#SlikaID").val("-1");

        var reader = new FileReader();

        reader.onload = function (e) {
            $('#image')
                .attr('src', e.target.result);
            $('#Slika')
                .attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
    }
}