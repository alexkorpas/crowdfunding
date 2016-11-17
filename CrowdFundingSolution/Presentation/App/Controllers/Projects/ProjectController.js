'use strict';
CrowdFundingApp.controller('ProjectController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'baseService',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, baseService) {
        baseService.httpGetAnonymous("api/Project/GetProjects", { Id: $stateParams.Id }).then(function (res) {
            $scope.Project = res[0];
        });
    }]);