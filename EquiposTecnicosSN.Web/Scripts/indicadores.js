$(function () {

    $("#seleccionarToggle").on("click", function () {
        var check = $("[name='sectores']").is(":checked");
        
        $("[name='sectores']").each(function (i, element) {
            element.checked = !check;            
        });
    });
    
    var getChartData = function () {

        var sectoresIds = [];

        $("[name='sectores']").each(function (i, e) {
            if (e.checked) {
                sectoresIds.push(parseInt(e.value));
            }
        });

        $("#ParetoChart").empty();

        if (sectoresIds.length < 2) {
            var errorContent = "<div class='alert alert-danger'>Debe seleccionar al menos dos sectores.</div>";
            $("#ParetoChart").html(errorContent);
            return false;
        }

        $("#ParetoChart").html("<div class='loader'></div>");

        $.ajax({
            type: 'GET',
            url: '/Indicadores/ParetoPorSectoresJSON',
            dataType: 'json',
            data: {
                sectoresIds: JSON.stringify(sectoresIds)
            },
            success: function (response) {
                //var keys = Object.keys(response);
                //var values = new Array;
                var keys = [];
                var values = [];

                for (key in response) {
                    keys.push(key);
                    values.push(response[key]);
                }

                //for (i in keys) {
                //    values.push(response[keys[i]]);
                //}

                var paretoChartConfig = {
                    "type": "pareto",
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
                        "line-width": 1,
                        "line-color": "#3285A6",
                        "items-overlap": true,
                        "guide": {
                            "visible": true
                        },
                        "item": {
                            "font-color": "#444",
                            "font-size": 9,
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

                zingchart.exec('ParetoChart', 'destroy');
                $("#ParetoChart").empty();

                zingchart.render({
                    id: 'ParetoChart',
                    data: paretoChartConfig,
                    height: "100%",
                    width: "100%"
                });

            },
            error: function (ex) {
                alert('Failed to retrieve data.' + ex);
            }
        });
    };


    $("#GenerateParetoChart").on("click", getChartData);

    
});

