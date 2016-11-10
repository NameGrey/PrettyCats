angular.module('BreedsModule').controller("breedsCtrl", function ($scope, $http, configuration) {
    var baseServerApiUrl = configuration.ServerApi;

    $http({ method: 'GET', url: baseServerApiUrl + '/breeds' }).success(function (data) {
        $scope.breeds = data;
    });
});