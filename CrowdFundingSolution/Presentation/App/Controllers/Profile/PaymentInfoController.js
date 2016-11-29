'use strict';
CrowdFundingApp.controller('PaymentInfoController', ['$rootScope', '$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'authService', 'baseService', 'CFHelpers', '$q', 'servers',
    function ($rootScope, $scope, $state, ngDialog, $filter, $element, $http, $stateParams, authService, baseService, CFHelpers, $q, servers, $mdToast) {
        baseService.httpGet("api/Payment/GetPaymentDetails", { transId: $scope.TransId }).then(function (res) {
            $scope.info = res.Data.Transactions[0];
            $scope.info.ClearanceDate = new Date(res.Data.Transactions[0].ClearanceDate);
        });
    }]);