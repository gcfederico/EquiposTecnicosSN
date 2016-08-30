$(function () {

    var ajaxSubmit = function () {
        var $form = $(this);
        var targetRow = Boolean($form.attr("data-nqn-target-row"));
        var $target = $($form.attr("data-nqn-target"));
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

            $target.effect("highlight");
        });

        return false;
    }

    $("form[data-nqn-ajax='true']").submit(ajaxSubmit);
});

