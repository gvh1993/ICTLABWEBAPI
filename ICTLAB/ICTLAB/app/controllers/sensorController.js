angular.module("app").controller("sensorController", ["$scope", "$window", "apiService", "$location",
    function ($scope, $window, apiService, $location) {
        $scope.home = $window.home;
        //GET SENSOR LIST
        $scope.sensors = {};

        $scope.removeSensor = function (sensor) {
            if ($window.confirm("Are you sure you want to delete: " + sensor.Name)) {
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
        }

        $scope.refreshSensors = function () {
            apiService.getSensorsWithoutReadings($scope.home)
                .then(
                    function successCallback(result) {
                        $scope.sensors = result.data;
                    },
                    function errorCallback(result) {
                        var x = result;
                        alert("Unable to retrieve the sensors");
                    });
        };


        //ADD SENSOR
        $scope.floors = [0, 1, 2];
        $scope.floor0 = ["Hall", "Veranda", "room right"];
        $scope.floor1 = ["left room", "right room"];
        $scope.floor2 = ["roof terrace"];

        $scope.sensor = {
            name: "",
            type: "",
            targetApiLink: "",
            unit: "",
            floor: 0,
            room: "Hall",
            home: $scope.home
        }

        $scope.changedFloor = function () {
            switch ($scope.sensor.floor) {
                case 0:
                    $scope.sensor.room = $scope.floor0[0];
                    break;
                case 1:
                    $scope.sensor.room = $scope.floor1[0];
                    break;
                case 2:
                    $scope.sensor.room = $scope.floor2[0];
                    break;
                default:
            }
        }
        $scope.addSensor = function () {
            if ($scope.sensor.name !== "" && $scope.sensor.type !== "" && $scope.sensor.targetApiLink !== "" && $scope.sensor.unit !== "" && $scope.sensor.floor !== "" && $scope.sensor.room !== "") {
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
            } else {
                alert("please fill in all fields");
            }

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
        $scope.detailSensor = {
            id: $window.sensorId,
            home: $scope.home
        };

        $scope.getSensorById = function () {
            apiService.getSensorById($scope.detailSensor).then(
                function successcallback(result) {
                    $scope.sensorDetailModel = result.data;
                },
                function errorCallback(result) {
                    var x = result;
                    alert(result.statusText);
                });
        }

        // UPDATE SENSOR
        $scope.updateSensor = function () {
            apiService.updateSensor($scope.sensorDetailModel).then(
                function successCallback(result) {
                    alert("Successfully updatet " + $scope.sensorDetailModel.Name);
                    var url = "http://" + $window.location.host + "/sensor/Details/" + $scope.home + "?sensorId=" + $scope.sensorDetailModel._id;
                    $window.location.href = url;
                },
            function errorCallback(result) {
                alert("There was an error while updating " + $scope.sensorDetailModel.Name);
            });
        }
    }]);