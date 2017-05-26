angular.module('app').directive('sensorCorrelationChart', ["apiService", function (apiService) {
    return {
        restrict: 'E',
        scope: {
            home : '='
        },
        link: function (scope, element) {


            var data = [];

            Highcharts.chart(element[0], {
                chart: {
                    zoomType: 'x'
                },
                title: {
                    text: 'Measurements last 3 months',
                    dateTimeLabelFormats: {
// don't display the dummy year
                        month: '%e. %b',
                        year: '%b'
                    },
                    title: {
                        text: 'Datum / tijd'
                    }
                },
                xAxis: {
                    type: 'datetime',
                    title: {
                        text: 'Datum / tijd'
                    }

                },
                yAxis: {
                    title: {
                        text: scope.sensorDetailModel.Type
                    }
                },
                //plotOptions: {
                //    spline: {
                //        marker: {
                //            enabled: true
                //        }
                //    }
                //},
                plotOptions: {
                    area: {
                        fillColor: {
                            linearGradient: {
                                x1: 0,
                                y1: 0,
                                x2: 0,
                                y2: 1
                            },
                            stops: [
                                [0, Highcharts.getOptions().colors[0]],
                                [1, Highcharts.Color(Highcharts.getOptions().colors[0]).setOpacity(0).get('rgba')]
                            ]
                        },
                        marker: {
                            radius: 2
                        },
                        lineWidth: 1,
                        states: {
                            hover: {
                                lineWidth: 1
                            }
                        },
                        threshold: null
                    }
                },
                legend: {
                    enabled: false
                },
                series: [
                    {
                        //name: 'sensordata',
                        type: 'area',
                        data: data
                    }
                ]
            });
        }
    }
}]);