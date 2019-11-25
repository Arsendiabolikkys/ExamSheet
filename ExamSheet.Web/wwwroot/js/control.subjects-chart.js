$(function () {
    var $wrapper = $('.subject-chart-wrapper');
    if ($wrapper.length) {
        var lineChartCtx = document.getElementById('lineChart').getContext('2d');
        var frequencyChart = document.getElementById('frequencyChart').getContext('2d');
        var predictChart = document.getElementById('predictChart').getContext('2d');

        var createFrequencyChart = function (frequencies) {
            var labels = [];
            var data = [];
            for (var fr in frequencies) {
                labels.push(fr);
                data.push(frequencies[fr]);
            }
            new Chart(frequencyChart, {
                type: 'line',
                data: {
                    labels: labels,
                    datasets: [{
                        label: 'Частота оцінок по предмету',
                        data: data,
                        backgroundColor: [
                            'rgba(55, 202, 170)'
                        ]
                    }]
                }
            });
        };

        var createLineChart = function (avgRatings) {
            var labels = [];
            var data = [];
            for (var rating in avgRatings) {
                labels.push(rating);
                data.push(avgRatings[rating].toFixed(2));
            }
            new Chart(lineChartCtx, {
                type: 'line',
                data: {
                    labels: labels,
                    datasets: [{
                        label: 'Середній бал по предмету за роки навчання',
                        data: data,
                        backgroundColor: [
                            'rgba(153, 102, 255)'
                        ]
                    }]
                }
            });
        };

        //TODO: correct labels with percentage
        var createProbabilityChart = function (probabilities) {
            var labels = [];
            var data = [];
            for (var prob in probabilities) {
                labels.push(prob);
                data.push(probabilities[prob].toFixed(2));
            }
            new Chart(predictChart, {
                type: 'line',
                data: {
                    labels: labels,
                    datasets: [{
                        label: 'Нормальний розподіл по оцінкам за предмет',
                        data: data,
                        backgroundColor: [
                            'rgba(153, 102, 255)'
                        ]
                    }]
                }
            });
        };

        var getChartData = function () {
            var $form = $('.subject-filter-form').first();
            var url = $form.attr('action');
            $.ajax({
                url: url,
                type: 'POST',
                dataType: "json",
                data: $form.serialize(),
                success: function (data) {
                    if (!data || !data.averageRatings) {
                        $('.chart-not-available').show();
                        $('.chart-wrapper').hide();
                    }
                    else {
                        $('.chart-not-available').hide();
                        $('.chart-wrapper').show();
                        createLineChart(data.averageRatings);
                        if (data && data.ratingFrequency) {
                            createFrequencyChart(data.ratingFrequency);
                        }
                        if (data && data.normalDistribution) {
                            createProbabilityChart(data.normalDistribution);
                        }
                    }
                    //if (data && data.semesterMarks) {
                    //    createPieChart(data.semesterMarks);
                    //}
                    //if (data && data.rangeMarks) {
                    //    createRangeChart(data.rangeMarks);
                    //}
                    //if (data && data.studentsRating) {
                    //    generateStudentsTable(data.studentsRating);
                    //}
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
    }
});