'use strict';
CrowdFundingApp.controller('EmailController', ['$scope', '$state', '$filter', '$element', '$http', '$stateParams', 'authService', '$q', 'servers',
    function ($scope, $state, $filter, $element, $http, $stateParams, authService, $q, servers) {
        var _confirm = function () {
            //var urlData = "userId=" + $stateParams.userId + "&code=" + $stateParams.code;
            var deferred = $q.defer();
            $http.get(
                servers.AUTHENTICATION_SERVER_BASE + 'api/accounts/confirmemail?userId=' + $stateParams.userId + "&code=" + $stateParams.code, null, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' }, skipAuthorization: true }
                ).success(function (data) {
                    deferred.resolve(data)
                }).error(function (err, status) {
                    deferred.reject(err);
                });

            return deferred.promise;
        };
        _confirm();
    }]);