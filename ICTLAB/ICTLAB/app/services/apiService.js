angular.module("app").service("apiService", ["$http", function ($http) {
    var httpConfig = {
    };

    return {
        addSensor: function(sensor) {
            return $http.post("/api/Sensor/Add", sensor);
        },
        deleteSensor: function(sensorName) {
            return $http.post("/api/Sensor/Delete?sensorName="+sensorName);
        },
        getSensors: function () {
            var accesstoken = sessionStorage.getItem('accessToken');

            var authHeaders = {};
            if (accesstoken) {
                authHeaders.Authorization = 'Bearer ' + accesstoken;
            }

            var response = $http({
                url: "/api/Sensor",
                method: "GET",
                headers: authHeaders
            });
            return response;
        },
        register: function (userInfo) {
            //return $http.post("api/Account/Register", userInfo);

            var resp = $http({
                url: "/api/Account/Register",
                method: "POST",
                data: userInfo
            });
            return resp;
        },
        login: function (userlogin) {
            var resp = $http({
                url: "/TOKEN",
                method: "POST",
                data: $.param({ grant_type: 'password', username: userlogin.userName, password: userlogin.password }),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            });
            return resp;
        }
    };
}])