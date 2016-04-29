$(function () {
    $(".ddl-fabricante").change(function () {
        $(".ddl-marca").empty().append('<option>Seleccione</option>');
        $(".ddl-modelo").empty().append('<option>Seleccione</option>');

        $.ajax({
            type: 'POST',
            url: '/InformacionHardware/GetMarcas',
            dataType: 'json',
            data: { fabricanteId: $(".ddl-fabricante").val() },
            success: function (marcas) {
                $.each(marcas, function (i, marca) {
                    $(".ddl-marca").append('<option value="' + marca.MarcaId + '">' +
                         marca.Nombre + '</option>');                                                                                                
                    // here we are adding option for States
 
                });
            },
            error: function (ex) {
                alert('Failed to retrieve states.' + ex);
            }
        });
    
    return false;
    });

    $(".ddl-marca").change(function () {
        $(".ddl-modelo").empty().append('<option>Seleccione</option>');

        $.ajax({
            type: 'POST',
            url: '/InformacionHardware/GetModelos',
            dataType: 'json',
            data: { marcaId: $(".ddl-marca").val() },
            success: function (modelos) {
                $.each(modelos, function (i, modelo) {
                    $(".ddl-modelo").append('<option value="' + modelo.ModeloId + '">' +
                         modelo.Nombre + '</option>');
                    // here we are adding option for States

                });
            },
            error: function (ex) {
                alert('Failed to retrieve states.' + ex);
            }
        });

        return false;
    });
});