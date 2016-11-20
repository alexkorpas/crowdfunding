//angular.module('app').directive('apsUploadFile', apsUploadFile);
CrowdFundingApp.directive('apsUploadFile', function () {
    return {
            restrict: 'E',
            template: '<input id="fileInput" type="file" class="ng-hide"> <md-button id="uploadButton" class="md-raised" style="width:210px;" aria-label="attach_file">    Choose file </md-button><md-input-container  md-no-float>    <input id="textInput" ng-model="fileName" type="text" placeholder="No file chosen" ng-readonly="true" style="width:210px"></md-input-container>',
            link: apsUploadFileLink
        };

    function apsUploadFileLink(scope, element, attrs) {
        var input = $(element[0].querySelector('#fileInput'));
        var button = $(element[0].querySelector('#uploadButton'));
        var textInput = $(element[0].querySelector('#textInput'));

        if (input.length && button.length && textInput.length) {
            button.click(function (e) {
                input.click();
            });
            textInput.click(function (e) {
                input.click();
            });
        }

        input.on('change', function (e) {

            var files = e.target.files;
            if (files[0]) {
                scope.fileName = files[0].name;
                scope.file = files[0];
                fr = new FileReader();
                fr.readAsDataURL(files[0]);
                fr.onload = (function () {
                    scope.ImageBinaryData = fr.result.split(",")[1];
                });
            } else {
                scope.fileName = null;
            }
            scope.$apply();
        });
    }
});