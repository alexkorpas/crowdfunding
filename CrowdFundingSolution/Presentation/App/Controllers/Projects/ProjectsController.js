'use strict';
CrowdFundingApp.controller('ProjectsController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'baseService', '$mdToast','$window',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, baseService, $mdToast, $window) {        
        $scope.page = 1;
        $scope.pointer = 0;
        $scope.Projects = [];        
        $scope.reload = function () {            
            baseService.httpGetAnonymous("api/Project/GetProjects/", { Page: $scope.page - 1, Search: $stateParams.Search }).then(function (res) {
                $scope.page++;
                for (let i = 0; i < res.length; i++) {
                    $scope.Projects.push(res[i]);
                    $scope.pointer++;
                    baseService.httpGetAnonymous("api/Photos/GetProjectMainImage/", { id: res[i].Id, pointer: $scope.pointer-1 }).then(function (res2) {
                            $scope.Projects[res2.Pointer].MainPhoto = res2.Photo;
                            var circle = angular.element(document.querySelector("#progressCircle" + res2.ProjectFK));
                            circle.remove();
                    });
                }
            });
        };
        $scope.reload();
        $scope.goToState = function (id) {
            $state.go('Home.Project', { Id: id });
        };
        $scope.addToFavorites = function () {
            $mdToast.show(
                  $mdToast.simple()
                    .textContent("Added to favorites")
                    .position('top right')
                    .hideDelay(3000)
                    );
        };
        $scope.scroll = function () {
            angular.element($window).bind("scroll", function () {
                if ($(window).scrollTop() + $(window).height() == $(document).height()) {
                    if ($scope.page>2)
                    $scope.reload();
                }
            });
        };
        $scope.scroll();
    }]);