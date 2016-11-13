'use strict';
CrowdFundingApp.controller('RegisterController', ['$scope', '$state', 'ngDialog', '$filter', '$element', '$http', '$stateParams', 'authService', 'baseService', 'CFHelpers',
    function ($scope, $state, ngDialog, $filter, $element, $http, $stateParams, authService, baseService, CFHelpers) {
        $scope.user = {};
        $scope.loading = false;
        $scope.singUp = function (send) {
            $scope.loading = true;
            $('#resultTxt').text(" ");
            if (!$scope.check()) {
                $scope.loading = false;
                return;
            }
            if (!$scope.checkMatching()) {
                $scope.loading = false;
                return;
            }
            if (!$scope.validateEmail($scope.user.username)) {
                $('#resultTxt').text("This is not a valid e-mail address.");
                $('#userNameTxt').addClass('validator');
                $('#userNameChkTxt').addClass('validator');
                $scope.loading = false;
                return;
            } else {
                $('#userNameTxt').removeClass('validator');
                $('#userNameChkTxt').removeClass('validator');
            }
            if (!$scope.validatePassword($scope.user.password)) {
                $('#resultTxt').text("Password must be minimun 6 characters long and contain at least one digit, one lowercase character and one uppercase character.");
                $('#passwordTxt').addClass('validator');
                $('#passwordChkTxt').addClass('validator');
                $scope.loading = false;
                return;
            } else {
                $('#passwordTxt').removeClass('validator');
                $('#passwordChkTxt').removeClass('validator');
            }
            if (send) {
                authService.httpPost($scope.user).then(function (data) {
                    $state.go('Home.RegistrationSubmitted');
                    $scope.loading = false;
                }, function (er) {
                    var test = er.modelState[""][1];
                    $('#resultTxt').text(er.modelState[""][1]);
                    $scope.loading = false;
                });
            }
        };
        $scope.check = function () {
            var valid = true;
            if ($scope.isNullOrWhitespace($scope.user.firstname)) {
                valid = false;
                $('#firstNameTxt').addClass('validator');
                $('#resultTxt').text("Missing required fields.");
            } else {
                $('#firstNameTxt').removeClass('validator');
            }
            if ($scope.isNullOrWhitespace($scope.user.lastname)) {
                valid = false;
                $('#lastNameTxt').addClass('validator');
                $('#resultTxt').text("Missing required fields.");
            } else {
                $('#lastNameTxt').removeClass('validator');
            }
            if ($scope.isNullOrWhitespace($scope.user.username)) {
                valid = false;
                $('#userNameTxt').addClass('validator');
                $('#resultTxt').text("Missing required fields.");
            } else {
                $('#userNameTxt').removeClass('validator');
            }
            if ($scope.isNullOrWhitespace($scope.user.usernameChk)) {
                valid = false;
                $('#userNameChkTxt').addClass('validator');
                $('#resultTxt').text("Missing required fields.");
            } else {
                $('#userNameChkTxt').removeClass('validator');
            }
            if ($scope.isNullOrWhitespace($scope.user.password)) {
                valid = false;
                $('#passwordTxt').addClass('validator');
                $('#resultTxt').text("Missing required fields.");
            } else {
                $('#passwordTxt').removeClass('validator');
            }
            if ($scope.isNullOrWhitespace($scope.user.passwordChk)) {
                valid = false;
                $('#passwordChkTxt').addClass('validator');
                $('#resultTxt').text("Missing required fields.");
            } else {
                $('#passwordChkTxt').removeClass('validator');
            }
            return valid;
        };
        $scope.checkMatching = function () {
            var valid = true;
            if ($scope.user.username != $scope.user.usernameChk) {
                valid = false;
                $('#resultTxt').text("Emails do not match.");
                $('#userNameTxt').addClass('validator');
                $('#userNameChkTxt').addClass('validator');
                return valid;
            } else {
                $('#userNameTxt').removeClass('validator');
                $('#userNameChkTxt').removeClass('validator');
            }
            if ($scope.user.password != $scope.user.passwordChk) {
                valid = false;
                $('#resultTxt').text("Passwords do not match.");
                $('#passwordTxt').addClass('validator');
                $('#passwordChkTxt').addClass('validator');
                return valid;
            } else {
                $('#passwordTxt').removeClass('validator');
                $('#passwordChkTxt').removeClass('validator');
            }
            return valid;
        };
        $scope.isNullOrWhitespace = function (input) {

            if (typeof input === 'undefined' || input == null) return true;

            return input.replace(/\s/g, '').length < 1;
        };
        $scope.validateEmail = function (email) {
            var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            return re.test(email);
        }
        $scope.validatePassword = function (password) {
            var pass = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$/;
            return pass.test(password);
        }
    }]);