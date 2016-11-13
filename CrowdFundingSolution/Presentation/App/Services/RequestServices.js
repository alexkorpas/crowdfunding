var services = angular.module('Services', ['ui.router', 'LocalStorageModule']);
services.factory('baseService', ['$http', '$q', '$state', 'localStorageService', 'servers', 'CFConfig',
    function ($http, $q, $state, localStorageService, servers, CFConfig) {
        var baseServiceResult = {};
        var _prepareRequestAnonymous = function () {
            try {
                var config = {
                    method: 'GET',
                    data: 'json',
                };
            } catch (ex) {
            }
            return config;
        }
        var _prepareRequestAnonymousWithParams = function (params) {
            var config = _prepareRequestAnonymous();
            config.params = params;
            return config;
        };
        var _httpGetAnonymous = function (URI, params) {
            var deferred = $q.defer();
            if (params != null && params != undefined)
                var config = _prepareRequestAnonymousWithParams(params);
            else
                var config = _prepareRequestAnonymous();
            $http.get(
                servers.CF_SERVER + URI, config
                ).success(function (data) {
                    deferred.resolve(data);
                }).error(function (err, status) {
                    deferred.reject(err);
                });
            return deferred.promise;
        };

        var _prepareRequest = function () {
            try {
                var authData = localStorageService.get(CFConfig.JWT);
                var authorizationValue = authData.token_type + ' ' + authData.access_token;

                var config = {
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json',
                        'Authorization': authorizationValue
                    }
                };
            }
            catch (ex) {
                $state.go("login");
            }
            return config;
        }


        var _prepareRequestWithParams = function (params) {
            var config = _prepareRequest();
            config.params = params;
            return config;
        };

        var _httpGet = function (URI, params) {
            var deferred = $q.defer();
            if (params != null && params != undefined) {
                var config = _prepareRequestWithParams(params);
            }
            else
                var config = _prepareRequest();
            $http.get(
                servers.CF_SERVER + URI, config
                ).success(function (data) {
                    deferred.resolve(data)
                }).error(function (err, status) {
                    deferred.reject(err);
                });

            return deferred.promise;
        };

        baseServiceResult.httpGet = _httpGet;
        baseServiceResult.httpGetAnonymous = _httpGetAnonymous;
        return baseServiceResult;
    }]);