$(document).ready(function () {

    rekurzivnaFunkcija();

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

    $("#btnObrisi").click(function () {

        $.post("/Kolo/Delete", { utakmicaID: $("#btnObrisi").data('utakmicaid') }, function (result, status) {

            $("#modalDelete").modal('hide');
            rekurzivnaFunkcija();

        });

    });

});

function rekurzivnaFunkcija() {

    var data = {
        id: $("#koloID").val()
    };

    $.get("/Kolo/GetUtakmice", data, function (result, status) {

        $("#utakmice").html(result);

        $(".utakmicaEdit").click(function () {

            var podaci = {
                utakmicaID: $(this).data('utakmicaid'),
                koloID: $(this).data('koloid')
            };

            $.get("/Kolo/UtakmicaEditForm", podaci, function (result, status) {

                $("#btnDodaj").css("display", "none");
                $("#formaDodaj").html("");
                $("#utakmice").html(result);

                console.log(result);

                $("#btnZatvori").click(function () {

                    rekurzivnaFunkcija();

                    $("#btnDodaj").css("display", "block");

                });

            });

        });

        $(".utakmicaDelete").click(function () {

            $("#btnObrisi").data("utakmicaid", $(this).data('utakmicaid'));
            $("#modalDelete").modal();

        });

    });

}