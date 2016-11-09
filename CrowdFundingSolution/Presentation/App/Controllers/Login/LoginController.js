'use strict';
CrowdFundingApp.controller('LoginController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'authService',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, authService) {
        //$scope.login = function () {
        //    alert("asds");
        //}

        $scope.user = {};
        //$scope.invalid = false;
        //$scope.loginLoading = false;


        $scope.login = function () {
            //$scope.invalid = false;
            //$scope.loginLoading = true;
            var promise = authService.token($scope.user);
            promise.then(function (data) {
                iMaintHelpers.saveToken(data);
                //save was successfull and take loggedin use details//
                var logUserpromise = baseService.httpGet('api/User/GetLoggedInUser');
                logUserpromise.then(function (data) {
                    //$scope.loginLoading = false;
                    iMaintHelpers.setLogUser(data);
                    $state.go('Home.Projects');
                }, function (er) {
                    //$scope.loginLoading = false;
                });

            }, function (err) {
                //$scope.loginLoading = false;
                //$scope.invalid = true;
            });
        };
    }]);