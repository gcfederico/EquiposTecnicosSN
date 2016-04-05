$(function () {

    var createAutocomplete = function () {
        var $input = $(this);
        var options = {
            source: $input.attr('data-sn-autocomplete')
        };

        $input.autocomplete(options);
    };

    $('input[data-sn-autocomplete]').each(createAutocomplete);
});