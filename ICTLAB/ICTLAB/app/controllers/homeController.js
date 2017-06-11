angular.module("app").controller("homeController", ["$scope", "apiService", "$window", "$location",
    function ($scope, apiService, $window, $location) {
        //GET HOME LIST
        $scope.homes = {};

        $scope.removeHome = function (home) {
            if ($window.confirm("Are you sure you want to delete: " + home.Name)) {
                apiService.deleteHome(home.Name).then(
                function successCallback(result) {
                    $scope.refreshHomes();
                },
                function errorCallback(result) {
                    alert("unable to delete: " + home.Name);
                }
            );
            }
        };

        $scope.refreshHomes = function () {
            apiService.getHomes()
                .then(
                function successCallback(result) {
                    $scope.homes = result.data;
                },
                function errorCallback(result) {
                    var x = result;
                    alert("Unable to retrieve the homes");
                });
        };



        //ADD HOME
        $scope.home = {
            name: ""
        };

        $scope.addHome = function () {
            apiService.addHome($scope.home).then(
                function successCallback(result) {
                    alert("Added new home: " + result.data);

                    var url = "http://" + $window.location.host + "/Dashboard";
                    $window.location.href = url;
                },
                function errorCallback(result) {
                    alert("failed to add new home");
                });
        };

        $scope.refreshHomes();
    }]);