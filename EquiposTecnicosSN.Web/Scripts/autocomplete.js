$(function () {

/*    var createAutocomplete = function () {
        var $input = $(this);
        var options = {
            source: $input.attr("data-sn-autocomplete"),
            select: function (event, ui) {
                $("#NombreCompleto").val(ui.item.label);
                $("#UMDNS").val(ui.item.value);
                event.stopPropagation();
                return false;
            }
        };

        $input.autocomplete(options);
    };
    */
    var autocompleteNombreOptions = {
        source: $("input[data-umdns-autocomplete-nombre]").attr("data-umdns-autocomplete-nombre"),
        select: function (event, ui) {
            $("#NombreCompleto").val(ui.item.label);
            $("#UMDNS").val(ui.item.value);
            event.stopPropagation();
            return false;
        }
    };

    var autocompleteCodigoOptions = {
        source: $("input[data-umdns-autocomplete-codigo]").attr("data-umdns-autocomplete-codigo"),
        select: function (event, ui) {
            $("#NombreCompleto").val(ui.item.value);
            $("#UMDNS").val(ui.item.label);
            event.stopPropagation();
            return false;
        }
    };

    $("input[data-umdns-autocomplete-nombre]").autocomplete(autocompleteNombreOptions);
    $("input[data-umdns-autocomplete-codigo]").autocomplete(autocompleteCodigoOptions);
});