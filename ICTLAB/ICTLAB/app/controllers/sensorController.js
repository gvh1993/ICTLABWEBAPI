angular.module("app").controller("sensorController", ["$scope", "$window", "apiService", "$location",
    function ($scope, $window, apiService, $location) {
        //GET SENSOR LIST
        $scope.sensors = {};

        $scope.removeSensor = function (sensor) {
            apiService.deleteSensor(sensor.Name).then(
                function successCallback(result) {
                    $scope.refreshSensors();
                    //alert("deleted " + sensor.Name);
                },
                function errorCallback(result) {
                    var x = result;
                    alert("unable to delete: " + sensor.Name);
                }
            );

            $scope.$apply($scope.refreshSensors());
        }

        $scope.refreshSensors = function () {
            apiService.getSensors()
                .then(
                function successCallback(result) {
                    $scope.sensors = result.data;
                },
                function errorCallback(result) {
                    var x = result;
                });
        };



        //ADD SENSOR
        $scope.sensorTypes = ["Temperature", "Moist", "Light", "Humidity"];
        $scope.sensor = {
            type: "Temperature",
            targetApiLink: ""
        }

        $scope.addSensor = function () {
            apiService.addSensor($scope.sensor).then(
                function successCallback(result) {
                    alert("Added new sensor: " + result.data);
                },
                function errorCallback(result) {
                    alert("failed to add new sensor");
                });
        }

        $scope.refreshSensors();
    }]);