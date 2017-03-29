angular.module("app").controller("homeController", ["$scope", "apiService", "$window", "$location",
    function ($scope, apiService, $window, $location) {
        //GET HOME LIST
        $scope.homes = {};

        $scope.removeHome = function (home) {
            apiService.deleteHome(home.Name).then(
                function successCallback(result) {
                    $scope.refreshHomes();
                    //alert("deleted " + home.Name);
                },
                function errorCallback(result) {
                    alert("unable to delete: " + home.Name);
                }
            );

            //$scope.$apply($scope.refreshHomes());
        };

        $scope.refreshHomes = function () {
            apiService.getHomes()
                .then(
                function successCallback(result) {
                    $scope.homes = result.data;
                },
                function errorCallback(result) {
                    var x = result;
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
                },
                function errorCallback(result) {
                    alert("failed to add new home");
                });
        };

        $scope.refreshHomes();
    }]);