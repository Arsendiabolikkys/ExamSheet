$(function () {
    var $wrapper = $('.subject-chart-wrapper');
    if ($wrapper.length) {
        var lineChartCtx = document.getElementById('lineChart').getContext('2d');
        var frequencyChartCtx = document.getElementById('frequencyChart').getContext('2d');
        var predictChartCtx = document.getElementById('predictChart').getContext('2d');

        var lineChart = null;
        var frequencyChart = null;
        var predictChart = null;

        var getRGBA = function () {
            var max = 250;
            var min = 100;
            var r = Math.floor(Math.random() * (max - min + 1)) + min;
            return r;
        };

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
                            'rgba(' + getRGBA() + ', ' + getRGBA() + ', ' + getRGBA() + ')'
                        ]
                    }]
                }
            });
        };

        //TODO: correct labels with percentage

        var getColor = function (x) {
            if (x == 0)
                return 'rgba(231, 76, 60, 1)';
            if (x == 1)
                return 'rgba(255, 255, 126, 1)';
            if (x == 2)
                return 'rgba(252, 214, 112, 1)';

            return 'rgba(123, 239, 178, 1)';
        }
        var createProbabilityChart = function (probabilities) {
            var labels = [];
            var lines = [];
            var count = 0;
            var data = [];
            for (var i = 0; i < probabilities.length; i++) {
                for (var ind in probabilities[i]) {
                    labels.push(count);
                    data[count] = probabilities[i][ind].toFixed(2);
                    count++;
                }
                var lastAdded = data[count - 1];
                var color = getColor(i);
                lines.push({
                    label: "test",
                    fill: true,
                    data: data,
                    backgroundColor: [
                        color
                    ]
                });
                var data = [];
                data[count - 1] = lastAdded;
            }

            //for (var prob in probabilities) {
            //    var data = [];
            //    for (var p in probabilities[prob]) {
            //        labels.push(p);
            //        data.push(probabilities[prob][p].toFixed(2));
            //    }
            //    lines.push({
            //        label: "test",
            //        fill: true,
            //        data: data
            //    });
            //}
            if (predictChart) {
                predictChart.destroy();
            }
            predictChart = new Chart(predictChartCtx, {
                type: 'line',
                data: {
                    labels: labels,
                    datasets: lines
                    //datasets: [{
                    //    label: 'Нормальний розподіл по оцінкам за предмет',
                    //    data: lines,
                    //    backgroundColor: [
                    //        'rgba(153, 102, 255)'
                    //    ]
                    //}]
                },
                options: {
                    scales: {
                        yAxes: [{
                            ticks: {
                                //display: false
                            }
                        }]
                    }
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
                        if (data && data.normalDistributions) {
                            createProbabilityChart(data.normalDistributions);
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