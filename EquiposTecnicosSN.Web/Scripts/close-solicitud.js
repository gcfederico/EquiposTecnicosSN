$(function () {
    $('[data-action="close-solicitud"]').on("click", function (event) {

        var btn = $(this);
        $.ajax({
            type: 'POST',
            url: '/SolicitudesRepuestoServicio/Close',
            data: { solicitudId: btn.data("solicitud-id") },
            success: function (respuesta) {
                if (respuesta.result == "success") {
                    $('[data-toggle="tooltip"]').tooltip("destroy");
                    $('[data-solicitud-id=' + respuesta.solicitudId + '][data-action="close-solicitud"]').remove();
                    if (respuesta.updateEstado) {
                        $("#ordenEstado").html("Abierta")
                    }

                } else {
                    alert("Error. Reinténtelo en unos instantes.")
                }
            },
            error: function (ex) {
                console.log(ex);
            }
        });
    });
});