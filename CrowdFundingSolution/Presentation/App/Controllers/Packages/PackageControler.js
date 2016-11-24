'use strict';
CrowdFundingApp.controller('PackageController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'baseService', '$mdToast', 'servers',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, baseService, $mdToast, servers) {
        baseService.httpGetAnonymous("api/ProjectDetails/GetProjectFundingLevels", { Id: $stateParams.Id }).then(function (res) {
            $scope.Packages = res;
        });
        $scope.showDiv = function (id) {
            var div = angular.element(document.querySelector("#div" + id));
            div.show();
        };
        $scope.backIt = function () {
            var dialog = ngDialog.open({ // ngDialog
                template: 'App/Views/BackIt/BackItForm.html',
                className: 'ngdialog-theme-default',
                controller: 'BackItController',
                scope: $scope
            });
            $scope.backIt.closeDialog = function () {
                dialog.close();
            };
        };
        $scope.isOpen = false;

        $scope.demo = {
            isOpen: false,
            count: 0,
            selectedDirection: 'left'
        };
    }]);