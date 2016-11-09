services.factory('authService', ['$http', '$q', 'servers', 'CFAuthUris', 'CFConfig',
    function ($http, $q, servers, CFAuthUris, CFConfig) {
        var authServiceRes = {};
        function createUrlparameters(user) {
            var params = null;
            if (user != null && user != undefined)
                if (user.username != null && user.password != null)
                    params = "grant_type=password&username=" + user.username + "&password=" + user.password + "&client_id=" + CFConfig.CLIENT_ID;
            return params;
        };


        var _token = function (userData) {
            var urlData = createUrlparameters(userData);
            var deferred = $q.defer();

            $http.post(
                servers.AUTHENTICATION_SERVER + CFAuthUris.GET_TOKEN, urlData, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' }, skipAuthorization: true }
                ).success(function (data) {
                    deferred.resolve(data)
                }).error(function (err, status) {
                    deferred.reject(err);
                });

            return deferred.promise;
        };

        authServiceRes.token = _token;

        return authServiceRes;

    }]);