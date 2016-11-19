'use strict';
CrowdFundingApp.controller('EditProjectController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'baseService',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, baseService) {
        $scope.project = {};
        baseService.httpGetAnonymous("api/Project/GetProjectCategories", null).then(function (res) {
            $scope.Categories = res;
            $scope.selectedCat = res[0];
        });
        $scope.test = function () {
            baseService.httpPost("api/Project/SaveProject", $scope.project).then(function (res) {
            });
        };
    }]);