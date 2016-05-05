$(function () {
    $(".ddl-traslado-equipo").change(function () {
        
        $.ajax({
            type: 'POST',
            url: '/Traslados/GetUbicacionEquipo',
            dataType: 'json',
            data: { equipoId: $(".ddl-traslado-equipo").val() },
            success: function (ubicacionId) {
                $(".ddl-traslado-origen").val(ubicacionId);
            },
            error: function (ex) {
                alert('Failed to retrieve data.' + ex);
            }
        });
    
        return false;
    });
});