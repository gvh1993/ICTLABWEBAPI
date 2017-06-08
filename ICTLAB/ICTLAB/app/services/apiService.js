angular.module("app").service("apiService", ["$http", function ($http) {
    var accesstoken = sessionStorage.getItem('accessToken');

    var authHeaders = {};
    if (accesstoken) {
        authHeaders.Authorization = 'Bearer ' + accesstoken;
    }

    return {
        addHome: function (home) {
            var response = $http({
                url: "/api/Home/Add",
                method: "PUT",
                data: home,
                headers: authHeaders
            });
            return response;
        },
        deleteHome: function (name) {
            var response = $http({
                url: "/api/Home/Delete?name=" + name,
                method: "DELETE",
                headers: authHeaders
            });
            return response;
        },
        getHomes: function () {
            var response = $http({
                url: "/api/Home",
                method: "GET",
                headers: authHeaders
            });
            return response;
        },
        addSensor: function (sensor) {
            var response = $http({
                url: "/api/Sensor/AddSensor",
                method: "Post",
                data: sensor,
                headers: authHeaders
            });
            return response;
        },
        updateSensor: function (sensor) {
            var response = $http({
                url: "/api/Sensor/UpdateSensor",
                method: "Post",
                data: sensor,
                headers: authHeaders
            });
            return response;
        },
        deleteSensor: function (sensor) {
            var response = $http({
                url: "/api/Sensor/DeleteSensor",
                method: "POST",
                data: sensor,
                headers: authHeaders
            });
            return response;
        },
        getSensorsWithoutCurrent: function (sensor) {
            var response = $http({
                url: "/api/Sensor/GetSensorsWithoutCurrent",
                method: "POST",
                headers: authHeaders,
                data: sensor
            });
            return response;
        },
        getSensorsWithoutReadings: function (home) {
            var response = $http({
                url: "/api/Sensor/GetSensorsWithoutReadings?home=" + home,
                method: "GET",
                headers: authHeaders
            });
            return response;
        },
        getSensorById: function (sensor) {
            var response = $http({
                url: "/api/Sensor/GetSensorBySensorId?id=" + sensor.id + "&home=" + sensor.home,
                method: "GET",
                headers: authHeaders
            });
            return response;
        },
        register: function (userInfo) {
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
        },
        logout: function () {
            var resp = $http({
                url: "/api/Account/Logout",
                method: "POST",
                headers: authHeaders
            });
            return resp;
        }
    };
}]);