angular.module("app").controller("sensorController", ["$scope", "$window", "apiService", "$location",
    function ($scope, $window, apiService, $location) {

        $scope.sensorTypes = ["Temperature", "Moist", "Light", "Humidity"];

        $scope.sensors = $window.mySensors;
        $scope.selectedSensor = "Temperature";
        $scope.addSensor = function () {

            apiService.addSensor($scope.selectedSensor.toString()).then(
                function successCallback(result) {
                    alert("Added new sensor: " + result.data);
                },
                function errorCallback(result) {
                    alert("failed to add new sensor");
                });

        }

        $scope.removeSensor = function(sensor) {
            apiService.deleteSensor(sensor.Name).then(
                function successCallback(result) {
                    var x = result;
                },
                function errorCallback(result) {
                    var x = result;
                }
            );

            //TODO reload the table

        }

        $scope.test = "test";
    }]);