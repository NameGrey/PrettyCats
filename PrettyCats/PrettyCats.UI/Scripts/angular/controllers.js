'use strict';

var artDuviksControllers = angular.module("artDuviksControllers", []);

artDuviksControllers.controller("KittensCtrl", function ($scope, $location, $http, configuration, kittensPathBuilder, kittensImageWorker) {
	var baseServerApiUrl = configuration.ServerApi;

	var getKittens = function() {
		var breedNameFromPath = "/" + $location.path().split(/[\s/]+/).pop();

		$scope.buildKittenLink = kittensPathBuilder.buildKittenLink;
		$scope.getKittenMainPageUrl = kittensImageWorker.getKittenMainPageUrl;

		$http.get(baseServerApiUrl + "/kittens/kittensByPath" + breedNameFromPath)
			.success(function(data) {
				$scope.kittens = data;
			})
			.error(function() {
				$scope.kittens = null;
			});
	}

	var getParents = function() {
		var baseServerApiUrl = configuration.ServerApi;

		$http.get(baseServerApiUrl + "/kittens/parents")
			.success(function(data) {
				$scope.parents = data;
			})
			.error(function() {
				$scope.parents = null;
			});
	}

	$scope.getKittens = getKittens;
	$scope.getParents = getParents;
});

artDuviksControllers.controller("MainController", function ($scope) {

});

artDuviksControllers.controller("BreedsCtrl", function ($scope, $http, configuration) {
	var baseServerApiUrl = configuration.ServerApi;

	$http({ method: 'GET', url: baseServerApiUrl + '/breeds' }).success(function(data) {
		$scope.breeds = data;
	});
});