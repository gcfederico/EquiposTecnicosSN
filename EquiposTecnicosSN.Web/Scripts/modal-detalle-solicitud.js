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

    // INDICADORES DE EQUIPO

    $('#modalTrasladarEquipo').on('show.bs.modal', function (event) {
        $("#indicadoresEquipoResult").html("");
    });

    $("#btnCalcularIndicadores").on("click", function (event) {
        var button = $("#indicadoresBtn");
        var equipoId = button.data('equipo-id');
        var fechaInicioPeriodo = $("#fechaInicioPeriodo").val();
        var fechaFinPeriodo =  $("#fechaFinPeriodo").val();
    
        if (fechaInicioPeriodo == "" || fechaFinPeriodo == "") {
            var errorContent = "<div class='alert alert-danger'>Debe seleccionar las fechas de inicio y fin.</div>";
            $("#indicadoresEquipoResult").html(errorContent);
            return false;
        }

        $("#indicadoresEquipoResult").html("<div class='loader'></div>");

        $.ajax({
            type: 'GET',
            url: '/Indicadores/IndicadoresEquipo',
            data: {
                equipoId: equipoId,
                fechaInicio: fechaInicioPeriodo,
                fechaFin: fechaFinPeriodo
                },
            success: function (view) {
                $("#indicadoresEquipoResult").html(view);
            },
            error: function (ex) {
                $("#indicadoresEquipoResult").html(ex.responseText);
            }
        });

    });
});