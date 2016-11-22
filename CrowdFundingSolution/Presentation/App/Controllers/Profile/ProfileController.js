'use strict';
CrowdFundingApp.controller('ProfileController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'authService', 'baseService', 'CFHelpers',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, authService, baseService, CFHelpers) {
        baseService.httpGet("api/User/GetLoggedInUser",null).then(function (res) {
            $scope.User = res;
            $scope.save = function () {
                baseService.httpPost("api/User/UpdateUser", $scope.User).then(function (res) {
                    $mdToast.show(
                      $mdToast.simple()
                        .textContent("Profile saved")
                        .position('top right')
                        .hideDelay(3000)
                        );
                }, function (error) {
                    $mdToast.show(
                      $mdToast.simple()
                        .textContent(error.Message)
                        .position('top right')
                        .hideDelay(3000)
                        .toastClass('failure')
                        );
                });
            };
        });
    }]);