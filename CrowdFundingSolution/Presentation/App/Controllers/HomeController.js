'use strict';
CrowdFundingApp.controller('HomeController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'CFHelpers',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, CFHelpers) {
        $scope.logout = function () {
            CFHelpers.deleteToken();
            $state.go("Home.Login");
        }
        $scope.test = "///////This is a controller parameter////////";
    }]);