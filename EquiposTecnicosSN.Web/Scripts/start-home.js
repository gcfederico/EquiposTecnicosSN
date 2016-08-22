$(function () {
    $.ajax({
        type: 'POST',
        url: '/ODTMantenimientoCorrectivo/OrdenesPorPrioridadCount',
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

    var ajaxSubmit = function () {
        var $form = $(this);
        var $target = $($form.attr("data-nqn-target"));
        $target.html("<div class='loader'></div>");

        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        };

        $.ajax(options).done(function (data) {
            $target.html(data);
            $target.effect("highlight");
        });

        return false;
    }


    $("form[data-nqn-ajax='true']").submit(ajaxSubmit);

    


});

