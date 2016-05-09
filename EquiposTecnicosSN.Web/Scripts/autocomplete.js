$(function () {

    var autocompleteNombreOptions = {
        source: $("input[data-umdns-autocomplete-nombre]").attr("data-umdns-autocomplete-nombre"),
        select: function (event, ui) {
            $("#NombreCompleto").val(ui.item.label);
            $("#UMDNS").val(ui.item.value);
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
            $("#NombreCompleto").val(ui.item.value);
            $("#UMDNS").val(ui.item.label);
            event.stopPropagation();
            return false;
        },
        focus: function (event, ui) {
            event.stopPropagation();
            event.preventDefault();
        }
    };

    $("input[data-umdns-autocomplete-nombre]").autocomplete(autocompleteNombreOptions);
    $("input[data-umdns-autocomplete-codigo]").autocomplete(autocompleteCodigoOptions);
});