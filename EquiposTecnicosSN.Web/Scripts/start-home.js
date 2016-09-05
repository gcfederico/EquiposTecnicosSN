$(function () {

    var ajaxSubmit = function () {
        var $form = $(this);
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
});

