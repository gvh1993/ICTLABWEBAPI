angular.module('app').directive('sensorHistoryChart', function() {
    return {
        restrict: 'E',
        scope: {
            sensorDetailModel: '='
        },
        link: function (scope, element) {
            scope.$watch('sensorDetailModel', function () {// to make sure the sensorDatailModel is filled by controller first!
                var data = [];
                angular.forEach(scope.sensorDetailModel.Readings, function(value, key) {
                    var dataPoint = [new Date(value.TimeStamp).getTime(), value.Value];
                    data.push(dataPoint);
                });

                Highcharts.chart(element[0], {
                    chart: {
                        zoomType: 'x'
                    },
                    title: {
                        text: scope.sensorDetailModel.Name,
                        dateTimeLabelFormats: { // don't display the dummy year
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
                        },
                        minRange: 1
                    },
                    yAxis: {
                        title: {
                            text: scope.sensorDetailModel.Type + "(" + scope.sensorDetailModel.Unit + ")"
                        }
                    },
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
                    series: [{
                        //name: 'sensordata',
                        type: 'area',
                        data: data
                    }]
                });
            }); 


            
        }
    }


});