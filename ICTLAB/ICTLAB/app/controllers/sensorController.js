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
        $scope.sensor = {
            name: "",
            type: "",
            targetApiLink: "",
            unit: "",

            home: $scope.home
        }

        $scope.addSensor = function () {
            apiService.addSensor($scope.sensor).then(
                function successCallback(result) {
                    alert("Added new sensor: " + result.data);
                    $scope.sensor.name = "";
                    $scope.sensor.type = "";
                    $scope.sensor.targetApiLink = "";
                    $scope.sensor.unit = "";

                    var url = "http://" + $window.location.host + "/sensor?id=" + $scope.home;
                    $window.location.href = url;
                },
                function errorCallback(result) {
                    alert("failed to add new sensor");
                });
        }

        // MANAGE SENSORS
        $scope.toggleSensor = function (sensor) {
            if (sensor.IsActive) {
                sensor.IsActive = false;

            } else {
                sensor.IsActive = true;
            }
            //call api service to update sensor
            apiService.updateSensor(sensor).then(
                function successCallback(result) {
                    $scope.refreshSensors();
                },
                function errorCallback(result) {
                    alert("Something went wrong with the updating of the sensor");
                });
        }

        //SENSOR DETAILS
        $scope.sensorDetailModel = {};
        $scope.sensorId = $window.sensorId;

        $scope.getSensorById = function () {
            apiService.getSensorById($scope.sensorId).then(
            function successcallback(result) {
                $scope.sensorDetailModel = result.data;
            },
            function errorCallback(result) {
                var x = result;
            });
        }
    }]);