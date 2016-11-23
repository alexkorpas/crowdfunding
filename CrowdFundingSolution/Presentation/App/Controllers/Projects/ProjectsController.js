'use strict';
CrowdFundingApp.controller('ProjectsController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'baseService', '$mdToast',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, baseService, $mdToast) {
        baseService.httpGetAnonymous("api/Project/GetPageCount", {keyword: $stateParams.Search}).then(function (res) {
            var pages; 
            var pageindex=[];
            if (angular.isNumber(res)) {
                //console.log(res);
                //var rem = res % 3;
                //if (rem != 0) {
                    pages = (Math.ceil(res/3));
                //}
                //else {
                //    pages = res;
                //}                
                    $scope.tabs = [];
                //console.log(pages);
                    for (var i = 0; i < pages; i++) {
                        $scope.tabs.push({ title: i+1 });
                    //pageindex.push(i+1);
                    }
                    if (pages > 0)
                        $scope.currentNavItem = 1;
                //$scope.index=pageindex;
            }
            
            //var count = Object.keys(res).length;
            //console.log(res);
        });
        //$scope.page = 1;
        //$scope.Projects = {};        
        $scope.reload = function (n) {
            
            baseService.httpGetAnonymous("api/Project/GetProjects/", { Page: n - 1, Search: $stateParams.Search }).then(function (res) {
                $scope.Projects = res;
                for(let i=0; i<res.length;i++)
                    baseService.httpGetAnonymous("api/Photos/GetProjectMainImage/", { Id: res[i].Id }).then(function (res2) {
                        if ($scope.Projects[i].MainPhotoFK == res2.Id) {
                            $scope.Projects[i].MainPhoto = res2.Photo;
                            var circle = angular.element(document.querySelector("#progressCircle" + $scope.Projects[i].Id));
                            circle.remove();
                        }
                            //$scope.page++;
                            //var count = Object.keys(res).length;
                            //console.log(res);
                    });
                //$scope.page++;
                //var count = Object.keys(res).length;
                //console.log(res);
            });
        };
        baseService.httpGetAnonymous("api/Project/GetProjects/", { Page: 0, Search: $stateParams.Search }).then(function (res) {
             $scope.Projects = res;

            var count = Object.keys(res).length;
            console.log(res);
        });
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
        //var tabs = [],
        //selected = null,
        //previous = null;
        ////$scope.tabs = tabs;
        //$scope.selectedIndex = 2;
        //$scope.$watch('selectedIndex', function(current, old){
        //    previous = selected;
        //    selected = tabs[current];
        //    if ( old + 1 && (old != current)) $log.debug('Goodbye ' + previous.title + '!');
        //    if ( current + 1 )                $log.debug('Hello ' + selected.title + '!');
        //});
        //$scope.addTab = function (title, view) {
        //    view = view || title + " Content View";
        //    tabs.push({ title: title, content: view, disabled: false});
        //};
        //$scope.removeTab = function (tab) {
        //    var index = tabs.indexOf(tab);
        //    tabs.splice(index, 1);
        //};    
    }]);

//CrowdFundingApp.controller('ProjectPageController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'baseService',
//    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, baseService) {
        
//        baseService.httpGetAnonymous("api/Project/GetProjects/", { Page: $stateParams.ProjectPage }).then(function (res) {
//            $scope.Projects = res;

//            var count = Object.keys(res).length;
//            console.log(res);
//        });
//    }]);

//CrowdFundingApp.controller('TestController', function ($scope, $http) {
//    // create a message to display in our view
//    $scope.message = 'contacts';
//    $http.get("call_queue.json")
//    .success(function (response) {
//        var $obj = JSON.stringify(response);
//        $scope.rows = response
//        var count = Object.keys(response).length;


//        console.log('scope is ' + count);
//    });
//});