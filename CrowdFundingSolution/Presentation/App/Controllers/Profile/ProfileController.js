'use strict';
CrowdFundingApp.controller('ProfileController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'authService', 'baseService', 'CFHelpers', 'CFConfig',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, authService, baseService, CFHelpers, CFConfig) {
        //baseService.httpGet("api/User/GetLoggedInUser",null).then(function (res) {
        $scope.User = CFConfig.LOGUSER;
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
        //});
    }]);