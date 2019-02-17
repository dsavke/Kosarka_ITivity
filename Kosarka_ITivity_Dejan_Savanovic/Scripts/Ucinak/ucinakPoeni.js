$(document).ready(function () {

    $(".poeni-igrac").change(function () {

        poeni();

    });

});

function poeni() {

    var igrac = {
        slobodna: Number($("#PogodjenihSlobodnihBacanja").val()) || 0,
        dvojke: Number($("#PogodjenihDvojki").val()) || 0,
        trojke: Number($("#PogodjenihTrojki").val()) || 0
    };

    $("#Poeni").val(igrac.slobodna + (igrac.dvojke * 2) + (igrac.trojke * 3));
};