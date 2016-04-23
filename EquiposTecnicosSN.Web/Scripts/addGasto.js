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
});