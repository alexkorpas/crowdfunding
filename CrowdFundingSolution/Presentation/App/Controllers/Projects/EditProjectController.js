'use strict';
CrowdFundingApp.controller('EditProjectController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'baseService', '$mdToast', 'servers',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, baseService, $mdToast, servers) {
        $scope.project = {};
        if ($stateParams.id != "" && $stateParams.id != null && $stateParams.id != undefined)
            baseService.httpGetAnonymous("api/Project/GetProjects", { Id: $stateParams.id }).then(function (res) {                
                $scope.project = res[0];
                $scope.project.DueDate = new Date(res[0].DueDate);
                $scope.loadImages();
            });
        baseService.httpGetAnonymous("api/Project/GetProjectCategories", null).then(function (res) {
            $scope.Categories = res;
            $scope.selectedCat = res[0];
        });
        baseService.httpGetAnonymous("api/Project/GetProjectStates", null).then(function (res) {
            $scope.States = res;
            $scope.selectedState = res[0];
        });
        $scope.loadImages = function () {
            baseService.httpGetAnonymous("api/Photos/GetProjectImages", { Id: $scope.project.Id }).then(function (res) {
                $scope.Photos = res;
            });
        };
        $scope.saveProject = function () {
            baseService.httpPost("api/Project/SaveProject", $scope.project).then(function (res) {
                $mdToast.show(
                  $mdToast.simple()
                    .textContent("Project saved")
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
            baseService.httpPost("api/Photos/SaveImage", { Id: $scope.project.Id, PhotoString: $scope.ImageBinaryData }).then(function (res) {
                $mdToast.show(
                  $mdToast.simple()
                    .textContent("Image uploaded")
                    .position('top right')
                    .hideDelay(3000)
                    );
                $scope.loadImages();
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
        $scope.deletePhoto = function (id) {
            baseService.httpPost("api/Photos/DeleteImage?Id="+id, null).then(function (res) {
                $mdToast.show(
                  $mdToast.simple()
                    .textContent("Photo deleted successfully")
                    .position('top right')
                    .hideDelay(3000)
                    );
                $scope.loadImages();
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
    }]);