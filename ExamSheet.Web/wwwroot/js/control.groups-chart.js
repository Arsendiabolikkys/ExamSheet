$(function(){
    var $wrapper = $('.group-chart-wrapper');
    if ($wrapper.length) {
        var pieChartCtx = document.getElementById('pieChart').getContext('2d');
        var barChartCtx = document.getElementById('barChart').getContext('2d');

        //TODO: check for multiple filters 
        //var chart = null;
        //if (chart) {
        //    chart.destroy();
        //}
        //chart = createChart

        var createPieChart = function (marks) {
            var labels = [];
            var data = [];
            var sum = 0;
            for (var i in marks) {
                sum += marks[i];
            }
            for (var mark in marks)
            {
                var percentage = (marks[mark] * 100 / sum).toFixed(1);
                labels.push(mark + ' (' + percentage + '%)');
                data.push(marks[mark]);
            }
            new Chart(pieChartCtx, {
                type: 'pie',
                data: {
                    labels: labels,
                    datasets: [{
                        label: 'Статистика оцінок',
                        data: data,
                        backgroundColor: [
                            'rgba(153, 102, 255)',
                            'rgba(54, 162, 235)',
                            'rgba(255, 206, 86)',
                            'rgba(75, 192, 192)',
                            'rgba(255, 159, 64)',
                            'rgba(255, 99, 132)'
                        ]
                    }]
                }
            });
        };

        var createRangeChart = function (marks) {
            var labels = [];
            var data = [];
            for (var mark in marks) {
                labels.push(mark);
                data.push(marks[mark]);
            }
            new Chart(barChartCtx, {
                type: 'bar',
                data: {
                    labels: labels,
                    datasets: [{
                        label: 'Діапазон оцінок',
                        data: data,
                        backgroundColor: [
                            'rgba(153, 102, 255)',
                            'rgba(54, 162, 235)',
                            'rgba(255, 206, 86)',
                            'rgba(75, 192, 192)',
                            'rgba(255, 159, 64)',
                            'rgba(255, 99, 132)',
                            'rgba(255, 99, 132, 1)',
                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 206, 86, 1)',
                            'rgba(75, 192, 192, 1)'
                        ]
                    }]
                },
                options: {
                    scales: {
                        yAxes: [{
                            ticks: {
                                beginAtZero: true
                            }
                        }]
                    }
                }
            });
        };

        var generateStudentsTable = function (ratings) {
            var $tbody = $('.students-table').find('tbody');
            $tbody.html('');
            var rows = '';
            for (var i = 0; i < ratings.length; i++) {
                var nameTd = '<td>' + ratings[i].surname + ' ' + ratings[i].name + '</td>';
                var ratingTd = '<td>' + ratings[i].rating + '</td>';
                var charTd = '<td>' + ratings[i].stringRepresentation + '</td>';
                var css = 'student-rating-' + ratings[i].stringRepresentation;
                rows += '<tr class="' + css + '">' + nameTd + ratingTd + charTd + '</tr>';
            }
            $tbody.append(rows);
        };

        var initChart = function () {
            var $group = $("select.group-filter");
            var $subject = $("select.subject-filter");
            var $year = $("select.year-filter");
            var $semester = $("select.semester-filter");

            var showAppropriateGroup = function ($select, group) {
                $select.find("optgroup:not([label='" + group + "'])").hide();
                $select.find("optgroup[label='" + group + "']").show();
                if ($select.find("option:selected").not(":visible")) {
                    $select.val("");
                }
                var value = $select.find("optgroup[label='" + group + "'] option").val();
                $select.val(value);
            };
            if ($group.length && $subject.length) {
                $(function () {
                    function updateFilter() {
                        var selected = $group.find("option:selected").text();
                        if (selected) {
                            showAppropriateGroup($subject, selected);
                            showAppropriateGroup($year, selected);
                            showAppropriateGroup($semester, selected);
                        }
                        else {
                            $subject.find("optgroup").show();
                            $year.find("optgroup").show();
                            $semester.find("optgroup").show();
                        }
                    }

                    $("select.group-filter").on("change", function (e) {
                        updateFilter();
                    });
                    updateFilter();
                });
            }
            //TODO: add loading indicator
            //TODO: test with many groups, teachers, subjects, years
            var getChartData = function () {
                var $form = $('.group-filter-form').first();
                var url = $form.attr('action');
                $.ajax({
                    url: url,
                    type: 'POST',
                    dataType: "json",
                    data: $form.serialize(),
                    success: function (data) {
                        if (!data || !data.semesterMarks) {
                            $('.chart-not-available').show();
                            $('.chart-wrapper').hide();
                        }
                        else {
                            $('.chart-not-available').hide();
                            $('.chart-wrapper').show();
                            createPieChart(data.semesterMarks);
                            if (data && data.rangeMarks) {
                                createRangeChart(data.rangeMarks);
                            }
                            if (data && data.studentsRating) {
                                generateStudentsTable(data.studentsRating);
                            }
                        }
                    },
                    error: function (err) {
                        console.log(err);
                    }
                });
            };

            $('.filter').on('change', function () {
                getChartData();
            });
            getChartData();
        };

        initChart();
    }
});