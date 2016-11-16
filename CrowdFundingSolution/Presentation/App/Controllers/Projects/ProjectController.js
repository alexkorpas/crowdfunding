'use strict';
CrowdFundingApp.controller('ProjectController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'baseService',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, baseService) {
        baseService.httpGetAnonymous("api/Project/GetProjects?id=" + $stateParams.ProjectId, null).then(function (res) {
            $scope.ProjectId = $stateParams.ProjectId;
            $scope.Project = res;
            var obj = JSON.stringify(res);
            console.log('Mpikame sto project me id ' + $scope.ProjectId);
        });

    }]);