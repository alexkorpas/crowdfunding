'use strict';
CrowdFundingApp.controller('HomePageController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'authService', 'baseService', 'CFHelpers', '$rootScope',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, authService, baseService, CFHelpers, $rootScope) {
        $scope.flag = 1;
        //$rootScope.currentNavItem = "HomePage";
        baseService.httpGetAnonymous("api/Project/GetProjectCategories", null).then(function (res) {
            res = $scope.shuffle(res);
            for (var i = 0; i < res.length; i++) {
                res[i].colspan = 2;
                res[i].rowspan = $scope.randomSpan();
            }
            $scope.Categories = $scope.shuffle(res);
        });
        //$scope.setSpan = function (n) {
        //    if
        //};
        $scope.getImage = function (title) {
            return 'url("../../../Content/Categories/image'+title+'.jpg")';
        };
        $scope.randomSpan = function () {
            var r = Math.random();
            if (r < 0.5) {
                return 2;
            } else if (r < 0.9) {
                return 3;
            } else {
                return 3;
            }
        };
        $scope.shuffle = function (array) {
            var currentIndex = array.length, temporaryValue, randomIndex;

            // While there remain elements to shuffle...
            while (0 !== currentIndex) {

                // Pick a remaining element...
                randomIndex = Math.floor(Math.random() * currentIndex);
                currentIndex -= 1;

                // And swap it with the current element.
                temporaryValue = array[currentIndex];
                array[currentIndex] = array[randomIndex];
                array[randomIndex] = temporaryValue;
            }

            return array;
        };
    }]);