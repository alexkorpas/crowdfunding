'use strict';
CrowdFundingApp.controller('EditProjectController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'baseService', '$mdToast', '$rootScope',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, baseService, $mdToast, $rootScope) {
        $scope.project = {};
        baseService.httpGetAnonymous("api/Project/GetProjectCategories", null).then(function (res) {
            $scope.Categories = res;
            $scope.selectedCat = res[0];
        });
        $scope.test = function () {
            //baseService.httpPost("api/Project/SaveProject", $scope.project).then(function (res) {
            $scope.showSimpleToast()
            //});           
        };
        //$rootScope.showSimpleToast = function () {
        //    //var pinTo = $scope.getToastPosition();
        //    $mdToast.show($mdToast.simple().textContent('Hello!').position('top right').toastClass('toast'));
        //    //$mdToast.show(
        //    //  $mdToast.simple()
        //    //    .textContent('Simple Toast!')
        //    //    .position("top")
        //    //    .hideDelay(3000)
        //    //);
        //};

        var last = {
            bottom: false,
            top: true,
            left: false,
            right: true
        };

        $scope.toastPosition = angular.extend({}, last);

        $scope.getToastPosition = function () {
            sanitizePosition();

            return Object.keys($scope.toastPosition)
              .filter(function (pos) { return $scope.toastPosition[pos]; })
              .join(' ');
        };

        function sanitizePosition() {
            var current = $scope.toastPosition;

            if (current.bottom && last.top) current.top = false;
            if (current.top && last.bottom) current.bottom = false;
            if (current.right && last.left) current.left = false;
            if (current.left && last.right) current.right = false;

            last = angular.extend({}, current);
        }

        $scope.showSimpleToast = function () {
            var pinTo = $scope.getToastPosition();

            $mdToast.show(
              $mdToast.simple()
                .textContent('Simple Toast!')
                .position(pinTo)
                .hideDelay(300000)
                //.toastClass('toast')
            );
        };
    }]);