var CrowdFundingApp = angular.module('app', ['ui.router', 'ui.bootstrap', 'ngAnimate', 'ngDialog', 'angular-loading-bar', 'Services', 'LocalStorageModule']);
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
            url: "/Projects",
            templateUrl: "/App/Views/Projects/ProjectsView.html",
            controller: "TestController",
            data: {
                requireLogin: _requiresLogin,
                settings: {
                    displayName: 'Projects'
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
        });
    
});
CrowdFundingApp.run(function ($rootScope, $state, $location) {
    $rootScope.$on('$stateChangeStart', function (e, to, params, from) { // On state change (page change)

    });
});