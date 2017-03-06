angular.module("app").controller("sensorController", ["$scope", "$window",
    function ($scope, $window) {


    $scope.sensors = $window.mySensors;

    $scope.newSensorName = "";

    $scope.addSensor = function() {
        //TODO check if exists
    }
}])