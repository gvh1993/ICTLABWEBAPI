angular.module("app").service("apiService", ["$http", function ($http) {
    var httpConfig = {
    };

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
            //return $http.post("/api/Sensor/Delete?sensorName=" + sensorName);
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
        updateSensor: function(sensor) {
            var response = $http({
                url: "/api/Sensor/UpdateSensor",
                method: "Post",
                data: sensor,
                headers: authHeaders
            });
            return response;
        },
        deleteSensor: function (sensor) {
            //return $http.post("/api/Sensor/Delete?sensorName=" + sensorName);
            var response = $http({
                url: "/api/Sensor/DeleteSensor",
                method: "POST",
                data: sensor,
                //headers: { authHeaders, 'Content-Type': 'application/json' }
                headers: authHeaders
            });
            return response;
        },
        getSensors: function (home) {
            sessionStorage.setItem('currentHome', home); // set it for unity visualisation

            var response = $http({
                url: "/api/Sensor/Get?home=" + home,
                method: "GET",
                headers: authHeaders
            });
            return response;
        },
        getSensorById: function(id) {
            var response = $http({
                url: "/api/Sensor/GetSensorBySensorId?id=" + id,
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