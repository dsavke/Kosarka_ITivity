$(document).ready(function () {

    $(".dtCetvrtine").change(function () {

        poeni();

    });

    $(".gtCetvrtine").change(function () {

        poeni();

    });

    $("#btnDodajUtakmicu").submit(function () {
        poeni();
        console.log("izvrsilo");
    });

});

function poeni() {

    var domaci = {
        cetvrtina1: Number($("#PoeniPrvaCetvrtina").val()) || 0,
        cetvrtina2: Number($("#PoeniDrugaCetvrtina").val()) || 0,
        cetvrtina3: Number($("#PoeniTrecaCetvrtina").val()) || 0,
        cetvrtina4: Number($("#PoeniCetvrtaCetvrtina").val()) || 0,
        produzetak: Number($("#PoeniProduzetak").val()) || 0
    };

    $("#PoeniDomaciTim").val(domaci.cetvrtina1 + domaci.cetvrtina2 + domaci.cetvrtina3 + domaci.cetvrtina4 + domaci.produzetak);

    var gostujuci = {
        cetvrtina1: Number($("#GostiPrvaCetvrtina").val()) || 0,
        cetvrtina2: Number($("#GostiDrugaCetvrtina").val()) || 0,
        cetvrtina3: Number($("#GostiTrecaCetvrtina").val()) || 0,
        cetvrtina4: Number($("#GostiCetvrtaCetvrtina").val()) || 0,
        produzetak: Number($("#GostiProduzetak").val()) || 0
    };

    $("#PoeniGostujuciTim").val(gostujuci.cetvrtina1 + gostujuci.cetvrtina2 + gostujuci.cetvrtina3 + gostujuci.cetvrtina4 + gostujuci.produzetak);

};