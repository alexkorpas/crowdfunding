﻿'use strict';
CrowdFundingApp.controller('ProjectController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'baseService',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, baseService) {
        baseService.httpGetAnonymous("api/Project/GetProjects", { Id: $stateParams.Id }).then(function (res) {
            $scope.Project = res[0];
        });
        $scope.backIt = function () {
            var dialog = ngDialog.open({ // ngDialog
                template: 'App/Views/BackIt/BackItForm.html',
                className: 'ngdialog-theme-default',
                controller: 'BackItController',
                scope: $scope
            });
        };        
    }]);