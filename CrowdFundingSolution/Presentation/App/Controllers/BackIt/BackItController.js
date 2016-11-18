'use strict';
CrowdFundingApp.controller('BackItController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'authService', 'baseService', 'CFHelpers', '$q', 'servers',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, authService, baseService, CFHelpers, $q, servers) {
        $scope.$on('$viewContentLoaded', function () {
            VivaPayments.cards.setup({
                publicKey: 'yZyg05StUwLG3QnfY2bY6fAfGsxPVawXI5etOxbNQ7E=',
                baseURL: 'http://demo.vivapayments.com',
                cardTokenHandler: function (response) {
                    if (!response.Error) {
                        $scope.hidToken = response.Token;
                        $scope.test();
                    }
                    else
                        alert(response.Error);
                },
                installmentsHandler: function (response) {
                    if (!response.Error) {
                        if (response.MaxInstallments == 0)
                            return;
                        $('#drpInstallments').show();
                        for (let i = 1; i <= response.MaxInstallments; i++) {
                            $('#drpInstallments').append($("<option>").val(i).text(i));
                        }
                    }
                    else
                        alert(response.Error);
                }
            });
            $scope.test = function () {
                var deferred = $q.defer();
                $http({
                    url: servers.CF_SERVER + 'api/Payment/Pay',
                    method: "POST",
                    params: { ourToken: $scope.hidToken },
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                    skipAuthorization: true
                }).success(function (data) {
                    deferred.resolve(data);
                }).error(function (err, status) {
                    deferred.reject(err);
                });
                return deferred.promise;
            };
        });
    }]);