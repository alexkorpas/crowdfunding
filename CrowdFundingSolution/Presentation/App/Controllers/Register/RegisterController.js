'use strict';
CrowdFundingApp.controller('RegisterController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'authService', 'baseService', 'CFHelpers',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, authService, baseService, CFHelpers) {
        //$scope.login = function () {
        //    alert("asds");
        //}

        $scope.user = {};
        //$scope.invalid = false;
        //$scope.loginLoading = false;


        $scope.singUp = function () {
            //$scope.invalid = false;
            //$scope.loginLoading = true;
            //var promise = authService.token($scope.user);
            //promise.then(function (data) {
                //CFHelpers.saveToken(data);
                //save was successfull and take loggedin use details//
            authService.httpPost($scope.user).then(function (data) {
                    //$scope.loginLoading = false;
                    //CFHelpers.setLogUser(data);
                    $state.go('Home.Projects');
                }, function (er) {
                    //$scope.loginLoading = false;
                });

            //}, function (err) {
                //$scope.loginLoading = false;
                //$scope.invalid = true;
            //});
        };
    }]);