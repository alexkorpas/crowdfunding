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

        _httpPost = function (user) {
            var urlData = null;
            if (user != null && user != undefined)
                if (user.username != null && user.password != null)
                    urlData = "username=alex&password=" + user.password + "&email=" + user.username + "&confirmpassword=" + user.passwordChk + "&firstname=fuck&lastname=fuck2";

            var deferred = $q.defer();

            $http.post(
                servers.AUTHENTICATION_SERVER_BASE + 'api/accounts/create', urlData, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' }, skipAuthorization: true }
                ).success(function (data) {
                    deferred.resolve(data)
                }).error(function (err, status) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        authServiceRes.token = _token;
        authServiceRes.httpPost = _httpPost;

        return authServiceRes;

    }]);