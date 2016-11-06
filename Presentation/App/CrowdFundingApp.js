var CrowdFundingApp = angular.module('app', ['ui.router', 'ui.bootstrap', 'ngAnimate', 'ngDialog', 'angular-loading-bar', 'Services']);
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
            controller: "ProjectsController",
            data: {
                requireLogin: _requiresLogin,
                settings: {
                    displayName: 'Projects'
                }
            }
        });
});
CrowdFundingApp.run(function ($rootScope, $state, $location) {
    $rootScope.$on('$stateChangeStart', function (e, to, params, from) { // On state change (page change)

    });
});