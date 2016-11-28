'use strict';
CrowdFundingApp.controller('ProjectsController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'baseService', '$mdToast','$window',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, baseService, $mdToast, $window) {        
        $scope.page = 1;
        $scope.shown = false;
        $scope.pointer = 0;
        $scope.Projects = [];
        $scope.selectedCategory = $stateParams.CategoryId;
        $scope.keyword = $stateParams.Search;
        if ($scope.keyword != undefined && $scope.keyword != '' && $scope.keyword != null)
            $scope.shown = true;
        baseService.httpGetAnonymous("api/Project/GetProjectCategories", null).then(function (res) {
            $scope.BrowseCategories = res;
            res.splice(0, 0, {Id:'',Title:'All'});
        });
        if ($scope.keyword != null && $scope.keyword != undefined && $scope.keyword != "")
            $scope.browseTitle = $scope.keyword;
        $scope.reload = function () {            
            baseService.httpGetAnonymous("api/Project/GetProjects/", { Page: $scope.page - 1, Search: $scope.keyword, CategoryId: $scope.selectedCategory }).then(function (res) {
                if ($scope.selectedCategory != null && $scope.selectedCategory != undefined && $scope.selectedCategory != "") {
                    if(res.length > 0)
                        $scope.selectedCategory = res[0].CategoryFK;
                }
                $scope.page++;
                for (let i = 0; i < res.length; i++) {
                    res[i].progress = Math.round((res[i].Gathered / res[i].Goal) * 100);
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
                    .toastClass('success')
                    );
        };
        $scope.scroll = function () {
            angular.element($window).bind("scroll", function () {
                if ($state.current.name == 'Home.Projects')
                    if ($(window).scrollTop() + $(window).height() == $(document).height()) {
                        if ($scope.page>2)
                        $scope.reload();
                    }
            });
        };
        $scope.scroll();
        $scope.clearSearchTerm = function () {
            $scope.searchTerm = '';
        };
        $element.find('input').on('keydown', function (ev) {
            ev.stopPropagation();
        });
        $scope.categorySelected = function (id) {
            $state.go("Home.Projects", { CategoryId: id });
        };
        $scope.titleSearch = function () {
            $state.go("Home.Projects", { Search: $scope.keyword });
        };
        $scope.determinateProgress = function (gathered, goal) {
        };
    }]);