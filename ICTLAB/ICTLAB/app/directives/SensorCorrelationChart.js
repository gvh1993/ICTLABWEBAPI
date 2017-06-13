angular.module('app').directive('sensorCorrelationChart', ["apiService", "$filter", function (apiService, $filter) {
    return {
        restrict: 'E',
        scope: {
            sensorDetailModel: '='
        },
        link: function (scope, element, attrs) {
            scope.$watch(attrs.sensorDetailModel, function () { // to make sure the sensorDatailModel is filled by controller first!
                scope.home = window.home;
                var sensorCollection = {};
                var chartCount = 0;

                function plotData(sensor1, sensor2, filteredData) {
                    var target = 'element' + chartCount;
                    Highcharts.chart({
                        chart: {
                            renderTo: target,
                            type: 'scatter',
                            zoomType: 'xy',
                            width: '200',
                            height: '200'
                        },
                        title: {
                            text: '',
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
                            title: {
                                text: sensor1.Name + "(" + sensor1.Unit + ")"
                            },
                            minRange: -273
                        },
                        yAxis: {
                            title: {
                                text: sensor2.Name + "(" + sensor2.Unit + ")"
                            },
                            minRange: -273
                        },
                        plotOptions: {
                            scatter: {
                                marker: {
                                    radius: 5,
                                    states: {
                                        hover: {
                                            enabled: true,
                                            lineColor: 'rgb(100,100,100)'
                                        }
                                    }
                                },
                                states: {
                                    hover: {
                                        marker: {
                                            enabled: false
                                        }
                                    }
                                },
                                tooltip: {
                                    headerFormat: '',
                                    pointFormat: '{point.x}, {point.y}'
                                }
                            }
                        },
                        legend: {
                            enabled: false
                        },
                        series: [
                            {
                                name: 'correlation',
                                //type: 'area',
                                color: 'rgba(119, 152, 191, .5)',
                                data: filteredData
                            }
                        ]
                    });
                }

                function filterData(sensor1, sensor2) {
                    var data = [];

                    //loop over every sensorreading and check with other sensor reading if time is the same
                    angular.forEach(sensor1.Readings, function (sensor1Reading) {

                        angular.forEach(sensor2.Readings, function (sensor2Reading) {
                            //convert to string till minute accurate
                            var sensor1DateTime = $filter('date')(sensor1Reading.TimeStamp, "yyyy-MM-dd HH:mm");
                            var sensor2DateTime = $filter('date')(sensor2Reading.TimeStamp, "yyyy-MM-dd HH:mm");

                            if (sensor1DateTime == sensor2DateTime) {
                                //add data point to array because they have same datetime
                                //format: [sensor1Value, sensor2Value]
                                data.push([sensor1Reading.Value, sensor2Reading.Value]);
                            }
                        });
                    });

                    return data;
                }

                function getData() {
                    var sensor1 = scope.sensorDetailModel;
                        angular.forEach(sensorCollection, function (sensor2, key) {
                            var target = 'element' + chartCount;
                            element.append("<div id=" + target + " style='float: left'></div>");
                            //get data from sensor
                            var x = sensor1.Readings;
                            //get data from other sensor
                            var y = sensor2.Readings;
                            //plot both datasets in highcharts scatterplot
                            var filteredData = filterData(sensor1, sensor2);
                            plotData(sensor1, sensor2, filteredData);
                            chartCount++;
                        });
                }

                //get the data
                function getSensors() {
                    scope.sensorDetailModel.Home = scope.home;
                    apiService.getSensorsWithoutCurrent(scope.sensorDetailModel).then(
                    function successCallback(result) {
                        sensorCollection = result.data;
                        getData();
                    },
                    function errorCallback(result) {
                        var x = result;
                    });
                }

                getSensors();


                //double foreach and draw the correlation between i and j


                //var data = [];

            });
        }
    }
}]);