var myApp = angular.module('myApp', ['ngRoute', 'ngAnimate']);

myApp.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.defaults.headers.common["X-Requested-With"] = 'XMLHttpRequest';
}]);

myApp.config(function ($routeProvider, $locationProvider) {
    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
    
    $routeProvider.when('/',
         {
             templateUrl: '/Home/Index',
             controller: 'ContentCtrl'
         });
    $routeProvider.when('/home/index',
         {
             templateUrl: '/Home/Index',
             controller: 'ContentCtrl'
         });
    $routeProvider.when('/section/discount',
          {
              templateUrl: '/Section/Discount',
              controller: 'ContentCtrl'
          });
    $routeProvider.when('/section/services',
          {
              templateUrl: '/Section/Services',
              controller: 'ContentCtrl'
          });
    $routeProvider.when('/section/history',
          {
              templateUrl: '/Section/History',
              controller: 'ContentCtrl'
          });
    $routeProvider.when('/section/partners',
          {
              templateUrl: '/Section/Partners',
              controller: 'ContentCtrl'
          });
    $routeProvider.when('/section/contacts',
         {
             templateUrl: '/Section/Contacts',
             controller: 'ContentCtrl'
         });

    $routeProvider.when('/home/catalog/:sectionId',
         {
             templateUrl: '/Home/Catalog',
             controller: 'CatalogCtrl'
         });
    $routeProvider.when('/home/catalog/:sectionId/:categoryId',
         {
             templateUrl: '/Home/Catalog',
             controller: 'CatalogCtrl'
         });
    $routeProvider.when('/home/declaration/:declarationId',
         {
             templateUrl: '/Home/Declaration',
             controller: 'DeclarationCtrl'
         });
    
    $routeProvider.when('/manage',
         {
             templateUrl: '/Manage/Index',
             controller: 'ManageCtrl'
         });
    $routeProvider.when('/manage/:manageSection',
         {
             templateUrl: '/Manage/Index',
             controller: 'ManageCtrl'
         });

    //$routeProvider.otherwise({ redirectTo: '/Home/NotAvailable' });
});
