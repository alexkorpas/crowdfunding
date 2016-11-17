'use strict';
CrowdFundingApp.controller('ProjectsController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'baseService',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, baseService) {
        baseService.httpGetAnonymous("api/Project/GetPageCount", null).then(function (res) {
            var pages; 
            var pageindex=[];
            if (angular.isNumber(res)) {
                console.log(res);
                var rem = res % 3;
                if (rem != 0) {
                    pages = (Math.floor(res/3)) + 1;
                }
                else {
                    pages = res;
                }                
                
                console.log(pages);
                for (var i=0; i<pages; i++) {
                    pageindex.push(i+1);
                }
                $scope.index=pageindex;
            }
            
            var count = Object.keys(res).length;
            //console.log(res);
        });
        $scope.reload = function (n) {
            baseService.httpGetAnonymous("api/Project/GetProjects/", { Page: n-1 }).then(function (res) {
                $scope.Projects = res;

                var count = Object.keys(res).length;
                console.log(res);
            });
        };
        baseService.httpGetAnonymous("api/Project/GetProjects/", { Page: 0 }).then(function (res) {
             $scope.Projects = res;

            var count = Object.keys(res).length;
            console.log(res);
        });
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