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

	var getAllKittens = function () {

		$http.get(baseServerApiUrl + "/kittens").success(function(data) {
			$scope.kittens = data;
		}).error(function() { $scope.kittens = null; });
	}

	var removeKitten = function (kitten) {
		var index = $scope.kittens.indexOf(kitten);

		if (index > 0) {
			$http.get(baseServerApiUrl + "/kittens/remove").success(function() {
				$("#" + kitten.Name + " .kitten-block-admin").remove();
				$scope.kittens.splice(index, 1);
			});
		}
	}

	var getKittenPictures = function() {
		return kittensImageWorker.getKittenPictures($routeParams.id);
	}

	$scope.theFile = null;
	$scope.getKittens = getKittens;
	$scope.getParents = getParents;
	$scope.selectKitten = selectKitten;
	$scope.getAllKittens = getAllKittens;
	$scope.setMainPhotoFor = kittensImageWorker.setMainPhotoFor;
	$scope.addThePhoto = kittensImageWorker.addThePhoto;
	$scope.removeKitten = removeKitten;
	$scope.getKittenPictures = getKittenPictures;
});

artDuviksControllers.controller("MainController", function ($scope) {

});

artDuviksControllers.controller("BreedsCtrl", function ($scope, $http, configuration) {
	var baseServerApiUrl = configuration.ServerApi;

	$http({ method: 'GET', url: baseServerApiUrl + '/breeds' }).success(function(data) {
		$scope.breeds = data;
	});
});
