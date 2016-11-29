'use strict';
CrowdFundingApp.controller('ProjectController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'baseService', '$log', '$sce',
function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, baseService, $log, $sce) {
    $scope.activated = false;
    $scope.Updates = [];

    baseService.httpGetAnonymous("api/Project/GetProjects", { Id: $stateParams.Id }).then(function (res) {
        $scope.videoUrl = $sce.trustAsResourceUrl(res[0].Video);
        $scope.Project = res[0];
        $scope.Project.DueDate = new Date(res[0].DueDate);
        $scope.Project.CreatedDate = new Date(res[0].CreatedDate);
        $scope.Project.UpdatedDate = new Date(res[0].UpdatedDate);
    });

    baseService.httpGetAnonymous("api/ProjectDetails/GetProjectupdates", { Id: $stateParams.Id }).then(function (res) {
        $scope.Updates = res;
    });

    $scope.carouselOptions = {
        //sourceProp: 'Photo',
        visible: 3,
        //perspective: 35,
        startSlide: 0,
        border: 2,
        dir: 'ltr',
        width: 622,
        height: 350,
        //space: 220,
        loop: true
    };
        //$scope.removeSlide = removeSlide;
        //$scope.addSlide = addSlide;
        //$scope.selectedClick = selectedClick;
        //$scope.slideChanged = slideChanged;
        //$scope.beforeChange = beforeChange;
        //$scope.lastSlide = lastSlide;


        //function lastSlide(index) {
        //    $log.log('Last Slide Selected callback triggered. \n == Slide index is: ' + index + ' ==');
        //}

        //function beforeChange(index) {
        //    $log.log('Before Slide Change callback triggered. \n == Slide index is: ' + index + ' ==');
        //}

        //function selectedClick(index) {
        //    $log.log('Selected Slide Clicked callback triggered. \n == Slide index is: ' + index + ' ==');
        //}

        //function slideChanged(index) {
        //    $log.log('Slide Changed callback triggered. \n == Slide index is: ' + index + ' ==');
        //}


        //function addSlide(slide, array) {
        //    array.push(slide);
        //    vm.slide2 = {};
        //}

        //function removeSlide(index, array) {
        //    array.splice(array.indexOf(array[index]), 1);
        //}
        baseService.httpGetAnonymous("api/Photos/GetProjectImages", { Id: $stateParams.Id }).then(function (res) {
            //var image = new Image();
            //image.src = 'data:image/jpeg;base64,' + res[0].Photo;
            var test = angular.element(document.querySelector("#progressCircle"));
            test.remove();
            //res.push({Photo: $scope.videoUrl, Video: true});
            $scope.Photos = res;
            $scope.activated = true;;
        });
        //$scope.Photos = [
        //    { 'src': 'Content/Images/post01.jpg', caption: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Enim, maxime.' },
        //    { 'src': 'Content/Images/post02.jpg', caption: 'Lorem ipsum dolor sit amet ' },
        //    { 'src': 'Content/Images/post03.jpg', caption: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. ' },
        //    //{ 'src': 'Content/Images/photo5.jpg', caption: 'Lorem ipsum dolor sit amet,  Enim, maxime.' },
        //    //{ 'src': 'Content/Images/photo6.jpg', caption: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Enim, maxime.' }
        //];
        $scope.backIt = function () {
            var dialog = ngDialog.open({ // ngDialog
                template: 'App/Views/BackIt/BackItForm.html',
                className: 'ngdialog-theme-default',
                controller: 'BackItController',
                scope: $scope
            });
        };
        $scope.goToState = function (id) {
            $state.go('Home.Packages', { Id: id });
        };
    }]);