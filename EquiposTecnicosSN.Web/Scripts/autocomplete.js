$(function () {

    var autocompleteNombreOptions = {
        source: $("input[data-umdns-autocomplete-nombre]").attr("data-umdns-autocomplete-nombre"),
        select: function (event, ui) {
            $("input[data-umdns-autocomplete-nombre]").val(ui.item.label);
            $("input[data-umdns-autocomplete-codigo]").val(ui.item.value);
            event.stopPropagation();
            return false;
        },
        focus: function (event, ui) {
            event.preventDefault();
        }
    };

    var autocompleteCodigoOptions = {
        source: $("input[data-umdns-autocomplete-codigo]").attr("data-umdns-autocomplete-codigo"),
        select: function (event, ui) {
            $("input[data-umdns-autocomplete-nombre]").val(ui.item.value);
            $("input[data-umdns-autocomplete-codigo]").val(ui.item.label);
            event.stopPropagation();
            return false;
        },
        focus: function (event, ui) {
            event.stopPropagation();
            event.preventDefault();
        }
    };

    var autocompleteCodigoRepuestoOptions = {
        source: $("input[data-autocomplete-codigo-repuesto]").attr("data-autocomplete-codigo-repuesto"),
        select: function (event, ui) {
            $("#Repuesto_Nombre").val(ui.item.value);
            $("#Repuesto_Codigo").val(ui.item.label);
            event.stopPropagation();
            $("#CantidadRepuesto").val(1);
            checkStock();

            return false;
        },
        focus: function (event, ui) {
            event.stopPropagation();
            event.preventDefault();
        }
    };

    function checkStock() {

        $.ajax({
            type: 'POST',
            url: '/Repuestos/CheckStockRepuesto',
            dataType: 'json',
            data: {
                codigo: $("#Repuesto_Codigo").val(),
                cantidad: $("#CantidadRepuesto").val()
            },
            success: function (response) {

                var alertHTML, color, mensaje;
                if (response.existeRepuesto) {

                    if (response.hayStock) {
                        color = "success";
                        mensaje = "Hay en stock";
                    } else {
                        color = "danger";
                        mensaje = "No hay stock";
                    }
                } else {
                    color = "warning";
                    mensaje = "El código de repuesto ingresado no se encuentra en el sistema. Es recomendado primero registrar el repuesto y luego ingresar la solicitud.";
                }

                alertHTML = '<div class="alert alert-' + color + ' alert-dismissable">' +
                                    '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>' +
                                    mensaje +
                                '</div>';

                $("#NotificacionStock").empty().append(alertHTML);

                $("#ProveedorId").val(response.proveedorId);

            },
            error: function (ex) {
                alert('Failed to retrieve data.' + ex);
            }
        });
    };
    
    ////////////////////////////////////////////////////////
    
    $("input[data-umdns-autocomplete-nombre]").autocomplete(autocompleteNombreOptions);
    $("input[data-umdns-autocomplete-codigo]").autocomplete(autocompleteCodigoOptions);
    $("input[data-autocomplete-codigo-repuesto]").autocomplete(autocompleteCodigoRepuestoOptions);

    $("#CantidadRepuesto").on("input", function (event, ui) {
        checkStock($("#CantidadRepuesto").val());
    });
});