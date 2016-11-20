﻿var CrowdFundingApp = angular.module('app', ['ngMaterial', 'ngDisqus', 'ui.router', 'ui.bootstrap', 'ngAnimate', 'ngDialog', 'angular-loading-bar', 'Services', 'LocalStorageModule']);
CrowdFundingApp.config(function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise("/Home"); // If no valid state is given, redirect to home
    const _requiresLogin = false;
    $stateProvider
        .state('Home', {
            url: "/Home",
            templateUrl: "/App/Views/HomeView.html",
            controller: "HomeController",
            data: {
                requireLogin: _requiresLogin,
                settings: {
                    displayName: 'Home'
                }
            }
        })
        .state('Home.Projects', {
            url: "/Projects/:Search",
            templateUrl: "/App/Views/Projects/ProjectsView.html",
            controller: "ProjectsController",
            data: {
                requireLogin: _requiresLogin,
                settings: {
                    displayName: 'Login'
                }
            }
        })
        .state('Home.EditProject', {
            url: "/EditProject/:id",
            templateUrl: "/App/Views/Projects/EditProject.html",
            controller: "EditProjectController",
            data: {
                requireLogin: _requiresLogin,
                settings: {
                    displayName: 'Edit Project'
                }
            }
        })
        .state('Home.Login', {
            url: "/Login",
            templateUrl: "/App/Views/Login/LoginView.html",
            controller: "LoginController",
            data: {
                requireLogin: _requiresLogin,
                settings: {
                    displayName: 'Login'
                }
            }
        })
        .state('Home.Profile', {
            url: "/Profile",
            templateUrl: "/App/Views/Profile/ProfileView.html",
            controller: "ProfileController",
            data: {
                requireLogin: _requiresLogin,
                settings: {
                    displayName: 'Profile'
                }
            }
        })
        .state('Home.Register', {
            url: "/Register",
            templateUrl: "/App/Views/Register/RegisterView.html",
            controller: "RegisterController",
            data: {
                requireLogin: _requiresLogin,
                settings: {
                    displayName: 'Register'
                }
            }
        })
        .state('Home.RegistrationSubmitted', {
            url: "/RegistrationSubmitted",
            templateUrl: "/App/Views/Register/RegistrationSubmitted.html",
            //controller: "EmailController",
            data: {
                requireLogin: _requiresLogin,
                settings: {
                    displayName: 'Email Confirmed'
                }
            }
        })
        .state('Home.EmailConfirmed', {
            url: "/EmailConfirmed/:userId/{code:.*}",//*code",
            templateUrl: "/App/Views/Email/EmailConfirmed.html",
            controller: "EmailController",
            data: {
                requireLogin: _requiresLogin,
                settings: {
                    displayName: 'Email Confirmed'
                }
            }
        })
        .state('Home.Project',{
            url: '/Project/:Id',
            templateUrl: "/App/Views/Projects/ProjectPage.html",
            controller:'ProjectController'
        }) 
    
});
CrowdFundingApp.run(function ($rootScope, $state, $location, CFHelpers) {
    $rootScope.$on('$stateChangeStart', function (e, to, params, from) { // On state change (page change)
        if (CFHelpers.getToken() == null)
            $rootScope.isLogged = false;
        else
            $rootScope.isLogged = true;
    });
});