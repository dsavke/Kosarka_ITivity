$(document).ready(function () {

    $("#IgracID").change(function () {

        $("#Slika").attr("src", $("#IgracID :selected").data('url'));

    });

});