$(function () {

    $("#seleccionarSectoresToggle").on("click", function () {
        var check = $("[name='sectores']").is(":checked");
        
        $("[name='sectores']").each(function (i, element) {
            element.checked = !check;            
        });
    });

    $("#seleccionarUbicacionesToggle").on("click", function () {
        var check = $("[name='ubicaciones']").is(":checked");

        $("[name='ubicaciones']").each(function (i, element) {
            element.checked = !check;
        });
    });
    
    var getChartData = function () {

        var indicador = $(this).data("nqn-indicador");

        $("#ParetoChartContainer").empty();

        var fechaInicio = $("#fechaInicio").val();
        var fechaFin = $("#fechaFin").val();

        if (fechaInicio == "" || fechaFin == "") {
            var errorContent = "<div class='alert alert-danger'>Debe seleccionar las fechas de inicio y fin.</div>";
            $("#ParetoChartContainer").html(errorContent);
            return false;
        }

        $("#ParetoChart").html("<div class='loader'></div>");

        $.ajax({
            type: 'GET',
            url: '/Indicadores/Pareto' + indicador + 'Data',
            dataType: 'json',
            data: {
                ubicacionId: $("#UbicacionId").val(),
                sectorId: $("#SectorId").val(),
                fechaInicio: fechaInicio,
                fechaFin: fechaFin
            },
            success: function (response) {
                var keys = [];
                var values = [];

                for (key in response) {
                    keys.push(key);
                    values.push(response[key]);
                }

                var paretoChartConfig = {
                    "type": "pareto",
                    "plotarea": {
                        "margin-bottom": "150px"
                    },
                    "options": {
                        "line-plot": {
                            "line-color": "#FF7F45",
                            "value-box": {
                                "background-color": "#FF7F45",
                                "shadow": false
                            },
                            "marker": {
                                "background-color": "#FF7F45",
                            }
                        }
                    },
                    "series": [],
                    "scale-x": {
                        "values": [],
                        "tooltip": {
                            "text": "%v"
                        },
                        "margin-bottom": "200px",
                        "line-width": 1,
                        "line-color": "#3285A6",
                        "items-overlap": true,
                        "guide": {
                            "visible": true
                        },
                        "item": {
                            "font-color": "#444",
                            "font-size": 10,
                            "angle": -30
                        },
                        "tick": {
                            "line-width": 1,
                            "line-color": "#3285A6"
                        }
                    }
                };

                paretoChartConfig["scale-x"]["values"] = keys;
                paretoChartConfig.series.push({ "values": values });

                zingchart.exec('ParetoChartContainer', 'destroy');
                $("#ParetoChartContainer").empty();

                zingchart.render({
                    id: 'ParetoChartContainer',
                    data: paretoChartConfig,
                    height: "700px",
                    width: "100%"
                });

            },
            error: function (ex) {
                alert('Failed to retrieve data.' + ex);
            }
        });

        return false;
    };


    $("[id$=Chart]").on("click", getChartData);

    
});

