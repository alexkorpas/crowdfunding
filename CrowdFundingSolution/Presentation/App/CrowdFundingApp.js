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
            
                    url: "/Projects/1",
                    views: {
                        '': {
                            templateUrl: 'App/Views/Projects/ProjectsView.html',
                            controller: 'ProjectsController'
                        },
                        'Pagination@Home.Projects': { templateUrl: 'App/Views/Pagination/PaginationIndexView.html', },
                        
                    },
                    data: {
                        requireLogin: _requiresLogin,
                        settings: {
                            displayName: 'Home'
                        }
                    }
              
            
        })

         .state('Home.ProjectsByPage', {

             url: "/Projects/:ProjectPage",
             views: {
                 '': {
                     templateUrl: 'App/Views/Projects/ProjectsView.html',
                     controller: 'ProjectPageController'
                 },
                 'Pagination@Home.Projects': { templateUrl: 'App/Views/Pagination/PaginationIndexView.html', },
                 
             },
             data: {
                 requireLogin: _requiresLogin,
                 settings: {
                     displayName: 'ProjectsByPage'
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
    //.state('new-qs', {
    .state('Home.Project',{
       // url: '/Project?portfolioId',
        url: '/Project/:ProjectId',
        templateUrl: "/App/Views/Projects/ProjectPage.html",
        controller:'ProjectController'
            //function ($scope, $stateParams) {
            //$scope.portfolioId = $stateParams.portfolioId;
      //  }
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