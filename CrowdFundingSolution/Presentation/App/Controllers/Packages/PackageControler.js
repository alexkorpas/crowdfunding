'use strict';
CrowdFundingApp.controller('PackageController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'baseService', '$mdToast', 'servers',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, baseService, $mdToast, servers) {
        baseService.httpGetAnonymous("api/ProjectDetails/GetProjectFundingLevels", { Id: $stateParams.Id }).then(function (res) {
            $scope.Packages = res;
            for (let i = 0; i < res.length; i++)
                $scope.Packages[i].AmountPledged = angular.copy($scope.Packages[i].Amount);
        });
        $scope.backIt = function (amountPledged,amount) {
            if (amount <= amountPledged) {
                console.log("lalaal");
                $scope.AmountPledged = amountPledged;
                var dialog = ngDialog.open({ // ngDialog
                    template: 'App/Views/BackIt/BackItForm.html',
                    className: 'ngdialog-theme-default',
                    controller: 'BackItController',
                    scope: $scope
                });
                $scope.backIt.closeDialog = function () {
                    dialog.close();
                };
            }
            else {
                return false;
            }
        };
        $scope.isOpen = false;

        $scope.demo = {
            isOpen: false,
            count: 0,
            selectedDirection: 'left'
        };
        $scope.showNumInput = function (id) {
            if ($scope.activePackageId != undefined) {
                var div = angular.element(document.querySelector("#div" + $scope.activePackageId));
                div.hide();
            }
            $scope.activePackageId = id;
            var div = angular.element(document.querySelector("#div" + id));            
            var input = angular.element(document.querySelector("#input" + id));
            div.show();
            input.focus();
        };
        $scope.check = function () {
            console.log("lalaal");
            if ($scope.package.amount < $scope.package.AmountPledged) {
                return true;
            }
            else {
                return false;
            }
        };
    }]);