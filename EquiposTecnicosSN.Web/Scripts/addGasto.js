$(function () {

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
});