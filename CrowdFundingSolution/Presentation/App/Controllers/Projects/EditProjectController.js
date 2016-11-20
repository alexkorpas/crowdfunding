'use strict';
CrowdFundingApp.controller('EditProjectController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'baseService', '$mdToast', 'servers',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, baseService, $mdToast, servers) {
        $scope.project = {};
        if ($stateParams.id != "" && $stateParams.id != null && $stateParams.id != undefined)
            baseService.httpGetAnonymous("api/Project/GetProjects", { Id: $stateParams.id }).then(function (res) {                
                $scope.project = res[0];
                $scope.project.DueDate = new Date(res[0].DueDate);
            });
        baseService.httpGetAnonymous("api/Project/GetProjectCategories", null).then(function (res) {
            $scope.Categories = res;
            $scope.selectedCat = res[0];
        });
        baseService.httpGetAnonymous("api/Project/GetProjectStates", null).then(function (res) {
            $scope.States = res;
            $scope.selectedState = res[0];
        });
        $scope.saveProject = function () {
            baseService.httpPost("api/Project/SaveProject", $scope.project).then(function (res) {
                $mdToast.show(
                  $mdToast.simple()
                    .textContent("Success")
                    .position('top right')
                    .hideDelay(3000)
                    );
                if ($scope.project.Id == undefined)
                    $scope.project.Id = res;
            }, function (error) {
                $mdToast.show(
                  $mdToast.simple()
                    .textContent(error.Message)
                    .position('top right')
                    .hideDelay(3000)
                    .toastClass('failure')
                    );
            });           
        };
        $scope.uploadPhoto = function () {
            var oReq = new XMLHttpRequest();
            var url = servers.CF_SERVER + "api/project/SaveProjectImage";
            oReq.open("POST", url, true);
            oReq.withCredentials = true;
            oReq.onload = function (oEvent) {
                //if (toSend.IsDefault == true) {
                //    for (let i = 0; i < $scope.subCategoriesPhotosDataSource.data().length; i++) {
                //        $scope.subCategoriesPhotosDataSource.data()[i].IsDefault = false;
                //    }
                //}
                //if (toSend.FotoID == null) {
                //    var response = JSON.parse(oReq.responseText);

                //    toSend.FotoID = response.Id;
                //    baseService.showNotification({ message: response.Message, type: "success" });
                //    $scope.subCategoriesPhotosDataSource.add(toSend);
                //} else {
                //    dataItem.Designation = toSend.Designation;
                //    dataItem.Caption = toSend.Caption;
                //    dataItem.IsDefault = toSend.IsDefault;
                //    dataItem.ImageBinaryData = toSend.ImageBinaryData;
                //}

                //angular.element(document.querySelector('#subCategoryPhotosGrid')).data("kendoGrid").refresh();
            };

            var bytesArray = [toSend.FotoID + ';', toSend.ImageBinaryData];
            var blob = new Blob(bytesArray, { type: 'text/plain' }, 'native');

            oReq.send(blob);
        };
    }]);