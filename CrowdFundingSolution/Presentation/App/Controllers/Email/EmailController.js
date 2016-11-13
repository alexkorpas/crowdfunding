'use strict';
CrowdFundingApp.controller('EmailController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'authService', 'baseService', 'CFHelpers', '$q', 'servers',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, authService, baseService, CFHelpers, $q, servers) {
        var _confirm = function () {
            var urlData = $stateParams;
            var deferred = $q.defer();
            $http.get(
                servers.AUTHENTICATION_SERVER_BASE + 'api/accounts/ConfirmEmail', urlData, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' }, skipAuthorization: true }
                ).success(function (data) {
                    deferred.resolve(data)
                }).error(function (err, status) {
                    deferred.reject(err);
                });

            return deferred.promise;
        };
        _confirm();
    }]);