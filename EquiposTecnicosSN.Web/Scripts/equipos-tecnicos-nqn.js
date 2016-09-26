$(function () {

    //Forms
    var ajaxSubmit = function () {
        var $form = $(this);
        if (!$form.valid()) {
            return false;
        }

        var targetRow = Boolean($form.attr("data-nqn-target-row"));
        var $target = $($form.attr("data-nqn-target"));
        var modalId = $form.attr("data-nqn-modal");
        $target.html("<div class='loader'></div>");

        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        };

        $.ajax(options).done(function (data) {

            if (targetRow) {
                $target = $($target.selector + "-" + data.RowId);
                $target.html(data.Value);
            } else {
                $target.html(data);
            }

            if (modalId) {
                $(modalId).modal("hide");
            }

            $target.effect("highlight");
        });

        return false;
    }

    $("form[data-nqn-ajax='true']").submit(ajaxSubmit);

    //Pagination
    var getPage = function () {
        var $a = $(this);

        if ($a.parent().hasClass("disabled")) {
            return false;
        }

        var $form = $($a.parents("div.pagedList").attr("data-nqn-form"));

        var options = {
            url : $a.attr("href"),
            type : "get",
            data : $form.serialize() 
        };
        
        $.ajax(options).done(function (data) {
            var target = $a.parents("div.pagedList").attr("data-nqn-target");
            $(target).html(data);
        });

        return false;
    };

    $(".body-content").on("click", ".pagedList a", getPage)

    //Autocompletes
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
            checkStock($("input[data-autocomplete-codigo-repuesto]").attr("data-nqn-check-stock"));

            return false;
        },
        focus: function (event, ui) {
            event.stopPropagation();
            event.preventDefault();
        }
    };

    function checkStock(url) {

        $.ajax({
            type: 'POST',
            url:url,
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

    $("input[data-umdns-autocomplete-nombre]").autocomplete(autocompleteNombreOptions);
    $("input[data-umdns-autocomplete-codigo]").autocomplete(autocompleteCodigoOptions);
    $("input[data-autocomplete-codigo-repuesto]").autocomplete(autocompleteCodigoRepuestoOptions);

    $("#CantidadRepuesto").on("input", function (event, ui) {
        checkStock($("#CantidadRepuesto").val());
    });

    //Dropdowns Info-Hardware
    var baseOpt = "<option>Seleccione uno</option>";

    $(".ddl-fabricante").change(function () {
        $(".ddl-marca").empty().append(baseOpt);
        $(".ddl-modelo").empty();

        var url = $(".ddl-fabricante").attr("data-nqn-action");

        if (this.selectedIndex != 0) {
            $.ajax({
                type: 'POST',
                url: url,
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

        var url = $(".ddl-marca").attr("data-nqn-action");

        if (this.selectedIndex != 0) {
            $.ajax({
                type: 'POST',
                url: url,
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

    //Dropdown Traslados
    $(".ddl-traslado-equipo").change(function () {

        var url = $(".ddl-traslado-equipo").attr("data-nqn-action");
        $.ajax({
            type: 'POST',
            url: url,
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


    //Modales
    //MODAL DETALLE DE SOLICITUD DE REPUESTO O SERVICIO
    $('#modalSolicitudRepuestoServicioEquipo').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var solicitudId = button.data('solicitud-id');
        var url = button.data('nqn-action')


        $(".modal-body").html("<div class='loader'></div>");

        $.ajax({
            type: 'GET',
            url: url,
            data: { id: solicitudId },
            success: function (viewDetalle) {
                $(".modal-body").html(viewDetalle);
            },
            error: function (ex) {
                $(".modal-body").html(ex.responseText);
            }
        });
    });

    //MODAL TRASLADAR EQUIPO
    $('#modalTrasladarEquipo').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var equipoId = button.data('equipo-id');
        var url = button.data("nqn-action");

        $(".modal-body").html("<div class='loader'></div>");

        $.ajax({
            type: 'GET',
            url: url,
            data: { equipoId: equipoId },
            success: function (view) {
                $(".modal-body").html(view);
            },
            error: function (ex) {
                $(".modal-body").html(ex.responseText);
            }
        });
    });

    //MODAL INDICADORES DE EQUIPO
    $('#modalTrasladarEquipo').on('show.bs.modal', function (event) {
        $("#indicadoresEquipoResult").html("");
    });

    $("#btnCalcularIndicadores").on("click", function (event) {
        var button = $("#indicadoresBtn");
        var equipoId = button.data('equipo-id');
        var fechaInicioPeriodo = $("#fechaInicioPeriodo").val();
        var fechaFinPeriodo = $("#fechaFinPeriodo").val();
        var url = $("#btnCalcularIndicadores").data("nqn-action");

        if (fechaInicioPeriodo == "" || fechaFinPeriodo == "") {
            var errorContent = "<div class='alert alert-danger'>Debe seleccionar las fechas de inicio y fin.</div>";
            $("#indicadoresEquipoResult").html(errorContent);
            return false;
        }

        $("#indicadoresEquipoResult").html("<div class='loader'></div>");

        $.ajax({
            type: 'GET',
            url: url,
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

    //Gastos
    $("#addGasto").click(function () {
        $.ajax({
            url: this.href,
            cache: false,
            success: function (html) {
                $("#tablaGastos").append(html);
            }
        });
        return false;
    });

    $(document).on("click", "a.deleteGasto", function () {
        $(this).parents("tr.gastoRow:first").remove();
        return false;
    });

    //Solicitudes de Repuestos o Servicios
    $('[data-action="close-solicitud"]').on("click", function (event) {

        var btn = $(this);
        var url = btn.data("nqn-action");
        $.ajax({
            type: 'POST',
            url: url,
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
    
    //Switches
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

    //Tooltips
    $('[data-toggle="tooltip"]').tooltip();
});

