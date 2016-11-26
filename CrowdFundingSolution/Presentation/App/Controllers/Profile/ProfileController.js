'use strict';
CrowdFundingApp.controller('ProfileController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'authService', 'baseService', 'CFHelpers', 'CFConfig', '$rootScope',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, authService, baseService, CFHelpers, CFConfig, $rootScope) {
        //baseService.httpGet("api/User/GetLoggedInUser",null).then(function (res) {
        //$rootScope.LoggedUser <--- Aυτός Κόρπας
            $scope.save = function () {
                baseService.httpPost("api/User/UpdateUser", $rootScope.LoggedUser).then(function (res) {
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