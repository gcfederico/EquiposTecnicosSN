$(function () {
    var baseOpt = "<option>Seleccione uno</option>";

    $(".ddl-fabricante").change(function () {
        $(".ddl-marca").empty().append(baseOpt);
        $(".ddl-modelo").empty();

        if (this.selectedIndex != 0) {
            $.ajax({
                type: 'POST',
                url: '/InformacionHardware/GetMarcas',
                dataType: 'json',
                data: { fabricanteId: $(".ddl-fabricante").val() },
                success: function (marcas) {
                    $.each(marcas, function (i, marca) {
                        $(".ddl-marca").append('<option value="' + marca.MarcaId + '">' +
                             marca.Nombre + '</option>');                                                                                                
                    });
                    $(".ddl-marca").effect("highlight");
                },
                error: function (ex) {
                    console.log(ex);
                }
            });
        }
    
    return false;
    });

    $(".ddl-marca").change(function () {
        $(".ddl-modelo").empty().append(baseOpt);

        if (this.selectedIndex != 0) {
            $.ajax({
                type: 'POST',
                url: '/InformacionHardware/GetModelos',
                dataType: 'json',
                data: { marcaId: $(".ddl-marca").val() },
                success: function (modelos) {
                    $.each(modelos, function (i, modelo) {
                        $(".ddl-modelo").append('<option value="' + modelo.ModeloId + '">' +
                             modelo.Nombre + '</option>');
                    });
                    $(".ddl-modelo").effect("highlight");
                },
                error: function (ex) {
                    console.log(ex);
                }
            });
        }
        return false;
    });
});