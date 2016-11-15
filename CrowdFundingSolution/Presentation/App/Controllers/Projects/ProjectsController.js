﻿'use strict';
CrowdFundingApp.controller('ProjectsController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'baseService',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, baseService) {
        baseService.httpGetAnonymous("api/Project/GetProjects", null).then(function (res) {
            $scope.Projects = res;
            
            var count = Object.keys(res).length;
            console.log(res.Remaining_Days);
        });
    }]);

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