angular.module("app").controller("sensorController", ["$scope", "$window", "apiService", "$location",
    function ($scope, $window, apiService, $location) {
        $scope.home = $window.home;
        //GET SENSOR LIST
        $scope.sensors = {};

        $scope.removeSensor = function (sensor) {
            apiService.deleteSensor(sensor).then(
                function successCallback(result) {
                    $scope.refreshSensors();
                    //alert("deleted " + sensor.Name);
                },
                function errorCallback(result) {
                    var x = result;
                    alert("unable to delete: " + sensor.Name);
                }
            );
        }

        $scope.refreshSensors = function () {
            apiService.getSensors($scope.home)
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
            name: "",
            type: "Temperature",
            targetApiLink: "",
            home: $scope.home
        }

        $scope.addSensor = function () {
            apiService.addSensor($scope.sensor).then(
                function successCallback(result) {
                    alert("Added new sensor: " + result.data);
                    $scope.sensor.name = "";
                    $scope.sensor.type = "";
                    $scope.sensor.targetApiLink = "";
                    var url = "http://" + $window.location.host + "/sensor?id=" + $scope.home;
                    $window.location.href = url;
                },
                function errorCallback(result) {
                    alert("failed to add new sensor");
                });
        }
    }]);