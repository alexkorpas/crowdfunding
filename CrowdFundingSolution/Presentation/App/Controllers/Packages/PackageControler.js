'use strict';
CrowdFundingApp.controller('PackageController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'baseService', '$mdToast', 'servers',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, baseService, $mdToast, servers) {
        baseService.httpGetAnonymous("api/ProjectDetails/GetProjectFundingLevels", { Id: $stateParams.Id }).then(function (res) {
            $scope.Packages = res;
        });
    }]);