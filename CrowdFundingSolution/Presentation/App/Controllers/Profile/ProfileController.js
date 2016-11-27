'use strict';
CrowdFundingApp.controller('ProfileController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'authService', 'baseService', 'CFHelpers', 'CFConfig', '$rootScope',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, authService, baseService, CFHelpers, CFConfig, $rootScope) {
        $scope.Projects = [];
        $scope.Payments = [];
        $scope.pointer = 0

        baseService.httpGet("api/User/GetLoggedInUser", null).then(function (res) {
            //$rootScope.LoggedUser; //<--- Aυτός Κόρπας
            $scope.User = res;

            baseService.httpGetAnonymous("api/Project/GetProjects", { UserId: $scope.User.Id }).then(function (res) {
                for (let i = 0; i < res.length; i++) {
                    res[i].progress = Math.round((res[i].Gathered / res[i].Goal) * 100);
                    $scope.Projects.push(res[i]);
                    $scope.pointer++;
                    baseService.httpGetAnonymous("api/Photos/GetProjectMainImage/", { id: res[i].Id, pointer: $scope.pointer - 1 }).then(function (res2) {
                        $scope.Projects[res2.Pointer].MainPhoto = res2.Photo;
                        var circle = angular.element(document.querySelector("#progressCircle" + res2.ProjectFK));
                        circle.remove();
                    });
                }
            });

            baseService.httpGet("api/Payment/GetPayments", { UserId: $scope.User.Id }).then(function (res) {
                $scope.Payments = res;
            });

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
        });
    }]);