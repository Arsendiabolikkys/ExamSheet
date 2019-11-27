$(function () {
    var $wrapper = $('.subject-chart-wrapper');
    if ($wrapper.length) {
        var lineChartCtx = document.getElementById('lineChart').getContext('2d');
        var frequencyChartCtx = document.getElementById('frequencyChart').getContext('2d');
        var predictChartCtx = document.getElementById('predictChart').getContext('2d');

        var lineChart = null;
        var frequencyChart = null;
        var predictChart = null;

        var createFrequencyChart = function (frequencies) {
            var labels = [];
            var data = [];
            for (var fr in frequencies) {
                labels.push(fr);
                data.push(frequencies[fr]);
            }
            if (frequencyChart) {
                frequencyChart.destroy();
            }
            frequencyChart = new Chart(frequencyChartCtx, {
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
            if (lineChart) {
                lineChart.destroy();
            }
            lineChart = new Chart(lineChartCtx, {
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
            if (predictChart) {
                predictChart.destroy();
            }
            predictChart = new Chart(predictChartCtx, {
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
            $('.spinner').show();
            $('.chart-wrapper').hide();
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
                    $('.spinner').hide();
                },
                error: function (err) {
                    console.log(err);
                    $('.spinner').hide();
                }
            });
        };

        $('.filter').on('change', function () {
            getChartData();
        });
        getChartData();
    }
});