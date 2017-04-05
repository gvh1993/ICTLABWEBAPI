angular.module('app').controller('accountController', ["apiService", "$scope",
    function (apiService, $scope) {
    //Scope Declaration
    $scope.responseData = "";

    $scope.userName = "";

    $scope.userRegistrationEmail = "";
    $scope.userRegistrationPassword = "";
    $scope.userRegistrationConfirmPassword = "";

    $scope.userLoginEmail = "";
    $scope.userLoginPassword = "";

    $scope.accessToken = "";
    $scope.refreshToken = "";

    $scope.loading = false;
    //Ends Here

    //Function to register user
    $scope.registerUser = function () {
        $scope.loading = true;
        $scope.responseData = "";

        //The User Registration Information
        var userRegistrationInfo = {
            Email: $scope.userRegistrationEmail,
            Password: $scope.userRegistrationPassword,
            ConfirmPassword: $scope.userRegistrationConfirmPassword
        };

        var promiseregister = apiService.register(userRegistrationInfo);

        promiseregister.then(function (resp) {
            $scope.responseData = "User is Successfully";
            $scope.userRegistrationEmail = "";
            $scope.userRegistrationPassword = "";
            $scope.userRegistrationConfirmPassword = "";
            $scope.loading = false;
        }, function (err) {
            $scope.responseData = err.data.ModelState;
            $scope.loading = false;
        });
    };


    $scope.redirect = function () {
        window.location.href = '/Dashboard/Index';
    };

    //Function to Login. This will generate Token 
    $scope.login = function () {
        $scope.loading = true;
        //This is the information to pass for token based authentication
        var userLogin = {
            grant_type: 'password',
            userName: $scope.userLoginEmail,
            password: $scope.userLoginPassword
        };

        var promiselogin = apiService.login(userLogin);
        
        promiselogin.then(function (resp) {
            $scope.userName = resp.data.userName;
            //Store the token information in the SessionStorage
            //So that it can be accessed for other views
            $scope.loading = false;
            sessionStorage.setItem('userName', resp.data.userName);
            sessionStorage.setItem('accessToken', resp.data.access_token);
            sessionStorage.setItem('refreshToken', resp.data.refresh_token);
            window.location.href = '/Dashboard/Index';
        }, function (err) {
            $scope.responseData = err.data.error_description;
            $scope.loading = false;
        });
    }

    $scope.logout = function () {
        apiService.logout().then(
            function successCallback(result) {
                var x = result;
                sessionStorage.removeItem('accessToken');
                window.location.href = '/Login/Login';
            },
            function errorCallback(result) {
                var x = result
            });
    };

    loading = function () {
        if ($scope.loading) {
            //rotate
        }
    }


}]);