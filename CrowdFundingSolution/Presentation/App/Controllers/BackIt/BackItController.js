'use strict';
CrowdFundingApp.controller('BackItController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'authService', 'baseService', 'CFHelpers',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, authService, baseService, CFHelpers) {
        VivaPayments.cards.setup({
            publicKey: 'yZyg05StUwLG3QnfY2bY6fAfGsxPVawXI5etOxbNQ7E=',
            baseURL: 'http://demo.vivapayments.com',
            cardTokenHandler: function (response) {
                if (!response.Error) {
                    $('#hidToken').val(response.Token);
                    $('#payment-form').submit();
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
        $scope.test = function () { alert("hello"); };
    }]);