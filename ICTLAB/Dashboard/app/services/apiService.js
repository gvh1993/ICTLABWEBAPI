angular.module("app").service("apiService", ["$http", function ($http) {
    var url = "http://localhost:52369/";

    var httpConfig = {
    };

    return {
        addSensor: function(sensor) {
            return $http.post(url + "/api/Sensor/Add", sensor);
        },
        deleteSensor: function(sensorName) {
            return $http.post(url + "/api/Sensor/Delete?sensorName="+sensorName);
        },
        getSensors: function() {
            return $http.get(url + "/api/Sensor");
        }
    };
}])