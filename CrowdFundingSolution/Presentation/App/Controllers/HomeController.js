'use strict';
CrowdFundingApp.controller('HomeController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'CFHelpers', 'CFConfig', 'baseService',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, CFHelpers, CFConfig, baseService) {
        $scope.logout = function () {
            CFHelpers.deleteToken();
            CFConfig.LOGUSER = "NULL"
            $state.go("Home.Login");
        }
        $scope.search = function (field) {
            console.log(field);
            $state.go("Home.Projects",{Search:field});
        }
        $scope.searchBtn = function () {
            baseService.httpGetAnonymous("api/Project/GetProjectCategories", null).then(function (res) {
                $scope.SelectCategories = res;
            });
            var dialog = ngDialog.open({ // ngDialog
                template: 'App/Views/SearchDialog.html',
                className: 'search ngdialog-theme-default',
                //controller: 'BackItController',
                scope: $scope
            });
            setTimeout(function () {
                var input = angular.element(document.querySelector("#searchInput"));
                input.focus();
            }, 200);
        };
    }
    
]);