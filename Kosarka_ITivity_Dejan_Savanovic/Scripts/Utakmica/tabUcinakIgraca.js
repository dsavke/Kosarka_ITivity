$(document).ready(function () {

    $("#btnDomaci").on('click', function () {

        $("#btnDomaci").css('background-color', '#DF691A');
        $("#btnGosti").css('background-color', '#4E5D6C');

        var data = {
            utakmicaID: $("#btnDomaci").data('utakmicaid'),
            timID: $("#btnDomaci").data('timid')
        };


        $.get("/Utakmica/GetUcinakIgraca", data, function (result, status) {
            $("#prikazIgraca").html(result);

            $("#btnDodajUcinakIgraca").data('utakmicaid', $("#btnDomaci").data('utakmicaid'));
            $("#btnDodajUcinakIgraca").data('timid', $("#btnDomaci").data('timid'));

            $("#btnDodajUcinakIgraca").click(function () {

                var data = {
                    utakmicaID: $("#btnDodajUcinakIgraca").data('utakmicaid'),
                    timID: $("#btnDodajUcinakIgraca").data('timid')
                };

                $.get('/Utakmica/GetPartialCreateUcinak', data, function (result, status) {

                    $("#tijeloDodajIgraca").html(result);
                    $("#modalDodajIgraca").modal();

                });
                
            });

            $(".ucinakIgracaEdit").on('click', function () {

                var data = {
                    ucinakIgracaID: $(this).data('ucinakigraca')
                };

                $.get('/Utakmica/GetPartialEditUcinak', data, function (result, status) {

                    $("#tijeloEditIgraca").html(result);
                    $("#modalEditIgraca").modal();

                });

            });

            $(".ucinakIgracaDelete").on('click', function () {

                $("#btnObrisi").data('ucinakid', $(this).data('ucinakigraca'));
                $("#btnObrisi").data('timid', $(this).data('timid'));

                $("#modalDeleteIgraca").modal();

            })

        });

    });

    $("#btnGosti").click(function () {

        $("#btnGosti").css('background-color', '#DF691A');
        $("#btnDomaci").css('background-color', '#4E5D6C');

        var data = {
            utakmicaID: $("#btnGosti").data('utakmicaid'),
            timID: $("#btnGosti").data('timid')
        };

        $.get("/Utakmica/GetUcinakIgraca", data, function (result, status) {
            $("#prikazIgraca").html(result);

            $("#btnDodajUcinakIgraca").data('utakmicaid', $("#btnGosti").data('utakmicaid'));
            $("#btnDodajUcinakIgraca").data('timid', $("#btnGosti").data('timid'));

            $("#btnDodajUcinakIgraca").click(function () {

                var data = {
                    utakmicaID: $("#btnDodajUcinakIgraca").data('utakmicaid'),
                    timID: $("#btnDodajUcinakIgraca").data('timid')
                };

                $.get('/Utakmica/GetPartialCreateUcinak', data, function (result, status) {

                    $("#tijeloDodajIgraca").html(result);
                    $("#modalDodajIgraca").modal();

                });

            });

            $(".ucinakIgracaEdit").on('click', function () {

                var data = {
                    ucinakIgracaID: $(this).data('ucinakigraca')
                };

                $.get('/Utakmica/GetPartialEditUcinak', data, function (result, status) {

                    $("#tijeloEditIgraca").html(result);
                    $("#modalEditIgraca").modal();

                });

            });

            $(".ucinakIgracaDelete").on('click', function () {

                $("#btnObrisi").data('ucinakid', $(this).data('ucinakigraca'));
                $("#btnObrisi").data('timid', $(this).data('timid'));

                $("#modalDeleteIgraca").modal();

            })

        });

    });

    $("#btnZatvori").on('click', function () {
        $("#modalDodajIgraca").modal('hide');
    });

    $("#btnX").on('click', function () {
        $("#modalDodajIgraca").modal('hide');
    });

    $("#btnZatvoriE").on('click', function () {
        $("#modalEditIgraca").modal('hide');
    });

    $("#btnXE").on('click', function () {
        $("#modalEditIgraca").modal('hide');
    });

    $("#btnZatvoriD").on('click', function () {
        $("#modalDeleteIgraca").modal('hide');
    });

    $("#btnXD").on('click', function () {
        $("#modalDeleteIgraca").modal('hide');
    });


    $("#btnDodaj").click(function () {
        poeni();

        var data = {
            igracID: $("#IgracID").val(),
            utakmicaID: $("#UtakmicaID").val(),
            timID: $("#TimID").val(),
            brojMinuta: $("#Minute").val(),
            pSB: $("#PogodjenihSlobodnihBacanja").val(),
            uSB: $("#UkupnoSlobodnihBacanja").val(),
            pTrojki: $("#PogodjenihTrojki").val(),
            uTrojki: $("#UkupnoTrojki").val(),
            pDvojki: $("#PogodjenihDvojki").val(),
            uDvojki: $("#UkupnoDvojki").val(),
            skokovoi: $("#Skokovi").val(),
            asistencije: $("#Asistencije").val(),
            blokade: $("#Blokade").val(),
            poeni: $("#Poeni").val(),
            faulovi: $("#Faulovi").val(),
            izgubljene: $("#Izgubljene").val(),
            ukradene: $("#Ukradene").val(),
        };

        console.log(data);

        $.post("/Utakmica/CreateUcinak", data, function (result, status) {

            if (result.Success) {

                $("#modalDodajIgraca").modal('hide');
                $("#Greska").text("");

                if (Number($("#btnDomaci").data('timid')) === Number(data.timID)) {
                    $("#btnDomaci").trigger('click');
                } else {
                    $("#btnGosti").trigger('click');
                }

            } else {
                $("#Greska").text(result.Message);
            }

        });

    });

    $("#btnAzuriraj").click(function () {

        var data = {
            igracID: $("#txtIgracID").val(),
            utakmicaID: $("#txtUtakmicaID").val(),
            timID: $("#txtTimID").val(),
            brojMinuta: $("#txtMinute").val(),
            pSB: $("#txtPogodjenihSlobodnihBacanja").val(),
            uSB: $("#txtUkupnoSlobodnihBacanja").val(),
            pTrojki: $("#txtPogodjenihTrojki").val(),
            uTrojki: $("#txtUkupnoTrojki").val(),
            pDvojki: $("#txtPogodjenihDvojki").val(),
            uDvojki: $("#txtUkupnoDvojki").val(),
            skokovoi: $("#txtSkokovi").val(),
            asistencije: $("#txtAsistencije").val(),
            blokade: $("#txtBlokade").val(),
            poeni: $("#txtPoeni").val(),
            faulovi: $("#txtFaulovi").val(),
            izgubljene: $("#txtIzgubljene").val(),
            ukradene: $("#txtUkradene").val(),
            ucinakIgracaID: $("#txtUcinakIgracaID").val()
        };

        console.log(data);

        $.post("/Utakmica/EditUcinak", data, function (result, status) {

            if (result.Success) {

                $("#modalEditIgraca").modal('hide');
                $("#txtGreska").text("");

                if (Number($("#btnDomaci").data('timid')) === Number(data.timID)) {
                    $("#btnDomaci").trigger('click');
                } else {
                    $("#btnGosti").trigger('click');
                }

            } else {
                $("#txtGreska").text(result.Message);
            }

        });

    });

    $("#btnObrisi").on('click', function () {

        var data = {
            ucinakIgracaID: $("#btnObrisi").data('ucinakid')
        };

        $.post('/Utakmica/UcinakDelete', data, function (result, status) {

            if (result.Success) {

                $("#modalDeleteIgraca").modal('hide');

                if (Number($("#btnDomaci").data('timid')) === Number($("#btnObrisi").data('timid'))) {
                    $("#btnDomaci").trigger('click');
                } else {
                    $("#btnGosti").trigger('click');
                }

            } else {
                console.log(result.Message);
            }

        });

    });


    $("#btnDomaci").trigger('click');

});