services.factory('CFHelpers', ['localStorageService', '$state', 'CFConfig',
    function (localStorageService, $state, CFConfig) {

    var CFHelper = {};

    var _getToken = function () {
        var token = null;
        try {
            token = localStorageService.get(CFConfig.JWT).access_token;
        }
        catch (ex) { }
        return token;
    };

    var _saveToken = function (data) {
        localStorageService.set(CFConfig.JWT, data);
    };

    var _deleteToken = function () {
        localStorageService.remove(CFConfig.JWT);
    };

    var _getHeaders = function () {
        try {
            var authData = localStorageService.get(CFConfig.JWT);
            var authorizationValue = authData.token_type + ' ' + authData.access_token;

            var config = {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': authorizationValue
            };
        }
        catch (ex) {
            console.log(ex.message);
            $state.go("login");
        }

        return config;
    };

    var _setLogUser = function (data) {
        localStorageService.set(CFConfig.LOGUSER, data);
    };

    var _getLogUser = function () {
        var _log = null;
        try {
            _log = localStorageService.get(CFConfig.LOGUSER);
        } catch (e) {
            _log = null;
        }

        return _log;

    };
    CFHelper.getHeaders = _getHeaders;
    CFHelper.getToken = _getToken;
    CFHelper.saveToken = _saveToken;
    CFHelper.deleteToken = _deleteToken;
    CFHelper.setLogUser = _setLogUser;
    CFHelper.getLogUser = _getLogUser;
    return CFHelper;
}]);