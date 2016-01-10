var portfolioApp = angular.module('portfolioApp', [/*'ngRoute', */'ui.bootstrap']);

// route config
//var configFunction = ['$routeProvider', function ($routeProvider) {
//    $routeProvider
//        .when('Portfolio/Index', {
//            templateUrl: 'Portfolio/Equity'
//        });
//}
//];

//portfolioApp.config(configFunction);

// service config
portfolioApp.factory('portfolioService', ['$http', function ($http) {

    var equityService = {};

    equityService.getEquities = function () {
        return $http.get('/Portfolio/GetEquities');
    };

    return equityService;
}]);

// controller config
portfolioApp.controller('portfolioController', ['$scope', 'portfolioService', function ($scope, equityService) {
    $scope.isCollapsed = true;

    $scope.getEquities = function () {
        equityService.getEquities()
            .success(function (eq) {
                $scope.equities = eq;
            })
            .error(function (error) {
                $scope.status = 'Unable to load equities data: ' + error.message;
                console.log($scope.status);
            });
    };

    $scope.getEquities();
}]);
