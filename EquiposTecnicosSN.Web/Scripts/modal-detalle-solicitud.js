$(function () {
    // DETALLE DE SOLICITUD DE REPUESTO O SERVICIO
    $('#modalSolicitudRepuestoServicioEquipo').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var solicitudId = button.data('solicitud-id');


        $(".modal-body").html("<div class='loader'></div>");

        $.ajax({
            type: 'GET',
            url: '/SolicitudesRepuestoServicio/DetailsSolicitud',
            data: { id: solicitudId },
            success: function (viewDetalle) {
                $(".modal-body").html(viewDetalle);
            },
            error: function (ex) {
                $(".modal-body").html(ex.responseText);
            }
        });
    });

    // TRASLADAR EQUIPO
    $('#modalTrasladarEquipo').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var equipoId = button.data('equipo-id');


        $(".modal-body").html("<div class='loader'></div>");

        $.ajax({
            type: 'GET',
            url: '/Traslados/LoadTrasladarEquipo',
            data: { equipoId: equipoId },
            success: function (view) {
                $(".modal-body").html(view);
            },
            error: function (ex) {
                $(".modal-body").html(ex.responseText);
            }
        });
    });
});