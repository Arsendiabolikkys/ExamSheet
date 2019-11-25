// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//$('document').on('click', '.card-new', function () {

//});

$(function () {
    $('.selectpicker').selectpicker();

    $('.datetime-picker').datetimepicker({
        //locale: 'ua'
    });

    $('.year-picker').datepicker({
        format: " yyyy",
        viewMode: "years",
        minViewMode: "years"
    });

    $('.btn-delete').on('click', function (e) {
        e.preventDefault();
        if (confirm("Підтвердіть видалення?")) {
            var $btn = $(e.target);
            $.post($btn.attr('href'), function (data) {
                window.location.reload();
            });
        }
    });

    $('select.faculty-selector').on('change', function (e) {
        e.preventDefault();
        var redirectTo = $(this).attr('data-url') + '?facultyId=' + $(e.target).val();
        window.location = redirectTo;
    });

    $('select.group-selector').on('change', function (e) {
        e.preventDefault();
        var facultyId = $('select.faculty-selector option:selected').val();
        var redirectTo = $(this).attr('data-url') + '?facultyId=' + facultyId + '&groupId=' + $(e.target).val();
        window.location = redirectTo;
    });
});