'use strict';
CrowdFundingApp.controller('HomeController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'CFHelpers', 'CFConfig',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, CFHelpers, CFConfig) {
        $scope.logout = function () {
            CFHelpers.deleteToken();
            CFConfig.LOGUSER = "NULL"
            $state.go("Home.Login");
        }
        $scope.search = function (field) {
            console.log(field);
            $state.go("Home.Projects",{Search:field});
        }        
    }

]);