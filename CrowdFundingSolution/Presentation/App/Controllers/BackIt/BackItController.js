'use strict';
CrowdFundingApp.controller('BackItController', ['$rootScope','$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'authService', 'baseService', 'CFHelpers', '$q', 'servers', '$mdToast',
    function ($rootScope,$scope, $state, ngDialog, $filter, $element, $http, $stateParams, authService, baseService, CFHelpers, $q, servers, $mdToast) {
        //$scope.$on('$viewContentLoaded', function () {
        setTimeout(function(){ 
            VivaPayments.cards.setup({
                publicKey: 'yZyg05StUwLG3QnfY2bY6fAfGsxPVawXI5etOxbNQ7E=',
                baseURL: 'http://demo.vivapayments.com',
                cardTokenHandler: function (response) {
                    if (!response.Error) {
                        $scope.hidToken = response.Token;
                        $scope.pay();
                    }
                    else
                        alert(response.Error);
                },
                //installmentsHandler: function (response) {
                //    if (!response.Error) {
                //        if (response.MaxInstallments == 0)
                //            return;
                //        $('#drpInstallments').show();
                //        for (let i = 1; i <= response.MaxInstallments; i++) {
                //            $('#drpInstallments').append($("<option>").val(i).text(i));
                //        }
                //    }
                //    else
                //        alert(response.Error);
                //}
            });
            $scope.submitPayment = function () {
                VivaPayments.cards.requestToken();
            };
            $scope.pay = function () {
                //var deferred = $q.defer();
                $scope.backIt.closeDialog();
                baseService.httpPostPayment("api/Payment/Pay", { ourToken: $scope.hidToken, amountPledged: $scope.AmountPledged, projectId: $stateParams.Id, userId: $rootScope.LoggedUser.Id }).then(function (res) {
                    $mdToast.show(
                      $mdToast.simple()
                        .textContent("Transasction Successful")
                        .position('top right')
                        .hideDelay(3000)
                        );
                }, function (error) {
                    $mdToast.show(
                      $mdToast.simple()
                        .textContent(error.Message)
                        .position('top right')
                        .hideDelay(3000)
                        .toastClass('failure')
                        );
                });
                //$http({
                //    url: servers.CF_SERVER + 'api/Payment/Pay',
                //    method: "POST",
                //    params: { ourToken: $scope.hidToken, amountPledged: $scope.AmountPledged },
                //    headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                //    skipAuthorization: true
                //}).success(function (data) {
                //    deferred.resolve(data);                    
                //    $mdToast.show(
                //                  $mdToast.simple()
                //                    .textContent("Transasction Successful")
                //                    .position('top right')
                //                    .hideDelay(3000)
                //                    );
                //}).error(function (err, status) {
                //    deferred.reject(err);
                //    $mdToast.show(
                //                  $mdToast.simple()
                //                    .textContent(err.Message)
                //                    .position('top right')
                //                    .hideDelay(3000)
                //                    .toastClass('failure')
                //                    );
                //});
                //return deferred.promise;
            };
        }, 500);
        //});
    }]);