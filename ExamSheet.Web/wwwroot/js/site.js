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

    var filterSheets = function (pageParam) {
        var url = $('.form-exam-sheets-filter').attr('action');
        url = url + pageParam;
        $.each($('.form-exam-sheets-filter select'), function (index, value) {
            var val = $(value).find('option:selected').val();
            var name = $(value).attr('data-name');
            url = url + '&' + name + '=' + val;
        });
        window.location = url;
    };

    $('.form-exam-sheets-filter select').on('change', function (e) {
        e.preventDefault();
        filterSheets('?page=1');
    });

    $('.exam-sheets-prev, .exam-sheets-next').on('click', function (e) {
        e.preventDefault();
        filterSheets(this.search);
    });
});