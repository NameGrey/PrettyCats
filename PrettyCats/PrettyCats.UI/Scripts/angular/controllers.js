'use strict';

var artDuviksControllers = angular.module("artDuviksControllers", []);

artDuviksControllers.controller("KittensCtrl", function ($scope, $location, $http, $routeParams, configuration, kittensImageWorker) {
	var baseServerApiUrl = configuration.ServerApi;

	var getKittens = function() {
		var breedNameFromPath = "/" + $location.path().split(/[\s/]+/).pop();

		$http.get(baseServerApiUrl + "/kittens/kittensByPath" + breedNameFromPath)
			.success(function(data) {
				$scope.kittens = data;
			})
			.error(function() {
				$scope.kittens = null;
			});
	};

	var getParents = function() {
		var baseServerApiUrl = configuration.ServerApi;

		$http.get(baseServerApiUrl + "/kittens/parents")
			.success(function(data) {
				$scope.parents = data;
			})
			.error(function() {
				$scope.parents = null;
			});
	};

	var selectKitten = function () {
		var kittenId = $routeParams.id;

		$http.get(baseServerApiUrl + "/kittens/" + kittenId)
			.success(function(data) {
				$scope.selectedKitten = data;

				kittensImageWorker.getKittenMainPicture($scope.selectedKitten).success(function (data) {
					$scope.selectedKitten.MainPicture = data;
				});

				kittensImageWorker.getKittenPictures($scope.selectedKitten).success(function (data) {
					$scope.selectedKitten.Pictures = data;
				});

			})
			.error(function() {
				$scope.selectedKitten = null;
			});
	};

	$scope.getKittens = getKittens;
	$scope.getParents = getParents;
	$scope.selectKitten = selectKitten;
});

artDuviksControllers.controller("MainController", function ($scope) {

});

artDuviksControllers.controller("BreedsCtrl", function ($scope, $http, configuration) {
	var baseServerApiUrl = configuration.ServerApi;

	$http({ method: 'GET', url: baseServerApiUrl + '/breeds' }).success(function(data) {
		$scope.breeds = data;
	});
});