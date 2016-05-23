$(function () {
    $.ajax({
        type: 'POST',
        url: '/OrdenesDeTrabajo/OrdenesPorPrioridadCount',
        dataType: 'json',
        success: function (counts) {
            $("#OrdenesEmergenciaCount").html(counts.Emergencia);            
        },
        error: function (ex) {
            console.log(ex);
        }
    });

    $.ajax({
        type: 'POST',
        url: '/Home/EquiposDeUsuarioCount',
        dataType: 'json',
        success: function (count) {
            $("#EquiposUsuarioCount").html(count);
        },
        error: function (ex) {
            console.log(ex);
        }
    });


    
});