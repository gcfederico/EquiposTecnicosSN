$(function () {
    // This will make every element with the class "date-picker" into a DatePicker element
    $('.date-picker').datepicker({
        format: "dd/mm/yyyy",
        language: "es",
        autoclose: true,
        todayHighlight: true
    });

})