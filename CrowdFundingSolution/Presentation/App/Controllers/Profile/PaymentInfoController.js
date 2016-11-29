'use strict';
CrowdFundingApp.controller('BackItController', ['$rootScope', '$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'authService', 'baseService', 'CFHelpers', '$q', 'servers', '$mdToast',
    function ($rootScope, $scope, $state, ngDialog, $filter, $element, $http, $stateParams, authService, baseService, CFHelpers, $q, servers, $mdToast) {
        baseService.httpGet("api/Payment/GetPaymentDetails", { transId: $scope.TransId }).then(function (res) {
            var test = res;
        });
    }]);