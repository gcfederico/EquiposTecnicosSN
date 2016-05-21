$(function () {
    $('#modalSolicitudRepuestoServicioEquipo').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var solicitudId = button.data('solicitud-id'); // Extract info from data-* attributes
        // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
        // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.

        $(".modal-body").html("Cargando...");

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
    })
});