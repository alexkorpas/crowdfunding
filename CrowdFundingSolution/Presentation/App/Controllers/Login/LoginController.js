'use strict';
CrowdFundingApp.controller('LoginController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'authService', 'baseService', 'CFHelpers',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, authService, baseService, CFHelpers) {
        $scope.user = {};
        $scope.loading = false;
        $scope.login = function () {
            $('#resultTxt').text(" ");
            $scope.loading = true;
            var promise = authService.token($scope.user);
            promise.then(function (data) {
                CFHelpers.saveToken(data);
                $scope.loading = false;
                $state.go('Home.Projects');
                //var logUserpromise = baseService.httpGet('api/User/GetLoggedInUser');
                //logUserpromise.then(function (data) {
                //    CFHelpers.setLogUser(data);
                //    $scope.loading = false;
                //    $state.go('Home.Projects');
                //}, function (er) {
                //    $scope.loading = false;
                //});

            }, function (err) {
                $('#resultTxt').text(err.error_description);
                $scope.loading = false;
            });
        };
    }]);