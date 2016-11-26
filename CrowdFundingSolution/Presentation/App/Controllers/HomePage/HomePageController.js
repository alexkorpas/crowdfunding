'use strict';
CrowdFundingApp.controller('HomePageController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'authService', 'baseService', 'CFHelpers',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, authService, baseService, CFHelpers) {
        $scope.flag = 1;
        baseService.httpGetAnonymous("api/Project/GetProjectCategories", null).then(function (res) {
            for (var i = 0; i < res.length; i++) {
                res[i].colspan = 2;
                res[i].rowspan = $scope.randomSpan();
            }
            $scope.Categories = res;
        });

        //$scope.Tiles = (function () {
        //    var tiles = [];
        //    for (var i = 0; i < 46; i++) {
        //        tiles.push({
        //            colspan: randomSpan(),
        //            rowspan: randomSpan()
        //        });
        //    }
        //    return tiles;
        //})();
        $scope.getImage = function (title) {
            return 'url("../../../Content/Categories/image'+title+'.jpg")';
        };

        $scope.randomSpan = function () {
            //if (n == 1)
            //    $scope.flag = 2;
            //else
            //    $scope.flag = 1;
            //return $scope.flag;
            var r = Math.random();
            if (r < 0.6) {
                return 2;
            } else if (r < 0.9) {
                return 1;
            } else {
                return 1;
            }
        }
    }]);