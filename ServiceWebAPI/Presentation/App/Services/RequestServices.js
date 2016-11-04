var services = angular.module('Services', ['ui.router']);
services.factory('baseService', ['$http', '$q', '$state',
    function ($http, $q, $state) {
        var baseServiceResult = {};
        var _prepareRequestWinAuth = function () {
            try {
                var config = {
                    method: 'GET',
                    withCredentials: true,
                    data: 'json',
                };
            } catch (ex) {
            }
            return config;
        }
        var _prepareRequestWinAuthWithParams = function (params) {
            var config = _prepareRequestWinAuth();
            config.params = params;
            return config;
        };
        var _httpGet = function (URI, params) {
            var deferred = $q.defer();
            if (params != null && params != undefined)
                var config = _prepareRequestWinAuthWithParams(params);
            else
                var config = _prepareRequestWinAuth();
            $http.get(
                "http://localhost:52188/" + URI, config
                ).success(function (data) {
                    deferred.resolve(data);
                }).error(function (err, status) {
                    deferred.reject(err);
                });
            return deferred.promise;
        };
        baseServiceResult.httpGet = _httpGet;
        return baseServiceResult;
    }]);