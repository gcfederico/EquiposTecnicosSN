$(function () {
    $("#EquipoParado").bootstrapSwitch({
        onColor: "danger",
        onText: "Si",
        offText: "No"
    });

    $("#EquipoParado").on("switchChange.bootstrapSwitch", function (event, state) {
        if (state) {
            $("#Emergencia").click();
        }
    });
});