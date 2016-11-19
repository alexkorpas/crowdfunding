'use strict';
CrowdFundingApp.controller('ProfileController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'authService', 'baseService', 'CFHelpers',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, authService, baseService, CFHelpers) {
        baseService.httpGet("api/Project/GetLoggedInUser".then(function (res) {
            $scope.user= res;
        }));
        //$scope.backIt = function () {
        //    var dialog = ngDialog.open({ // ngDialog
        //        template: 'App/Views/BackIt/BackItForm.html',
        //        className: 'ngdialog-theme-default',
        //        controller: 'BackItController',
        //        scope: $scope
        //    });
        //};
    }]);