$(document).ready(function () {

    $("#DomaciTim").change(function () {

        $("#SlikaDomaciTim").attr("src", $("#DomaciTim :selected").data('url'));
        $("#txtStadion").val($("#DomaciTim :selected").data('stadion'));
        $("#BrojMogucihGledalaca").val($("#DomaciTim :selected").data('brojgledalaca'));
        $("#lblNaziv").html($("#DomaciTim :selected").text() + " vs " + $("#GostujuciTim :selected").text());

    });

    $("#GostujuciTim").change(function () {

        $("#SlikaGostujuciTim").attr("src", $("#GostujuciTim :selected").data('url'));
        $("#lblNaziv").html($("#DomaciTim :selected").text() + " vs " + $("#GostujuciTim :selected").text());

    });

    console.log($("#pocetniDatum").val());

    const fpDatum = flatpickr("#DatumOdigravanja", {
        //defaultDate: $("#pocetniDatum").val(),
        minDate: $("#pocetniDatum").val(),
        maxDate: $("#krajniDatum").val()
    });
    
});