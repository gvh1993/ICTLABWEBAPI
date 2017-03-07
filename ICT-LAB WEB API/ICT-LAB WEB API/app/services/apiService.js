angular.module("app").service("apiService", ["$http", function ($http) {
    var httpConfig = {
    };
    return {
        addSensor: function(sensorName) {
            return $http.post("/api/Sensor/Add", '"' + sensorName + '"');
        },
        deleteSensor: function(sensorName) {
            return $http.post("/api/Sensor/Delete?sensorName="+sensorName);
        },
        getSensors: function() {
            return $http.get("/api/Sensor");
        }
    };
}])