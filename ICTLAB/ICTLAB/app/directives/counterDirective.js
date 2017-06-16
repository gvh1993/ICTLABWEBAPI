angular.module('app').directive('counterDirective', ["$timeout", function ($timeout) {
    return {
        restrict: 'E',
        scope: {
            sensorDetailModel: '='
        },
        link: function (scope, element) {
            scope.$watch('sensorDetailModel', function () {

                scope.counter = 0;
                var countTo = scope.sensorDetailModel.Readings.length;
                scope.onTimeout = function () {
                    if (scope.counter <= countTo) {
                        //element.html(scope.counter);
                        element.html("<center><h2>Total number of measurements</h2> <br/> <h3>" + scope.counter + "</h3> </center>");
                        scope.counter++;
                        mytimeout = $timeout(scope.onTimeout, 5);
                    } else {
                        scope.stop();
                    }
                }
                var mytimeout = $timeout(scope.onTimeout, 5);


                scope.stop = function () {
                    $timeout.cancel(mytimeout);
                }
            });
        }
    }
}]);