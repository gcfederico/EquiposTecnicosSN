$(function () {
    // This will make every element with the class "date-picker" into a DatePicker element
    $('.date-picker').datetimepicker({
        locale: 'es',
        format: 'D/M/YYYY hh:mm a',
        keepOpen: false,
        useCurrent: false
    });
})