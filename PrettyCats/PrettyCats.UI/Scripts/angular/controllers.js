'use strict';

var artDuviksControllers = angular.module("artDuviksControllers", []);

artDuviksControllers.controller("KittensCtrl", function ($scope, $location, $http, $routeParams, $timeout, configuration, kittensImageWorker) {
	var baseServerApiUrl = configuration.ServerApi;
	$scope.kitten = {};

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

		if (index > -1) {
			$http.get(baseServerApiUrl + "/kittens/remove/" + kitten.ID).success(function() {

				$("#" + kitten.Name + ".kitten-block-admin").replaceWith("<div class='alert alert-success'>Котенок был удален!</div>");

				$timeout(function() {
					$("#" + kitten.Name + ".kitten-block-admin").remove();
					$scope.kittens.splice(index, 1);
				}, 1000);
			}).error(function() {
				$("#" + kitten.Name + ".kitten-block-admin").find(".bottom-fotos-container").append("<div class='alert alert-danger'>Ошибка при удалении!</div>");

				$timeout(function() {
					$("#" + kitten.Name + ".kitten-block-admin .bottom-fotos-container div").remove();
				}, 1000);

			});
		}
	}

	var addNewKitten = function(kitten) {
		var data = new FormData();
		var json = JSON.stringify(kitten, null, 2);
		data.append("newKitten", json);

		$http.post(baseServerApiUrl + "/kittens/add", data, {headers:{"Content-Type": undefined}})
			.success(function() {
				$scope.successMessage = "Котенок успешно сохранен!";

				$timeout(function() {
					$scope.successMessage = null;
					$location.path("/admin/available-kittens");
				}, 2000);
			})
			.error(function() {
				$scope.errorMessage = "Произошла непредвиденная ошибка на сервере. Котенок не был добавлен.";

				$timeout(function() {
					$scope.errorMessage = null;
				}, 5000);
			});
	}

	var getKittenPictures = function() {
		return kittensImageWorker.getKittenPictures($routeParams.id);
	}

	var getOwners = function() {
		var baseServerApiUrl = configuration.ServerApi;

		$http.get(baseServerApiUrl + "/owners")
			.success(function (data) {
				$scope.owners = data;
			})
			.error(function () {
				$scope.owners = null;
			});
	};

	var getBreeds = function() { 
		var baseServerApiUrl = configuration.ServerApi;

		$http.get(baseServerApiUrl + "/breeds")
			.success(function (data) {
				$scope.breeds = data;
			})
			.error(function () {
				$scope.breeds = null;
			});
	};

	var getDisplayPlaces = function () {
		var baseServerApiUrl = configuration.ServerApi;

		$http.get(baseServerApiUrl + "/display-places")
			.success(function (data) {
				$scope.displayPlaces = data;
			})
			.error(function () {
				$scope.displayPlaces = null;
			});
	};

	$scope.theFile = null;
	$scope.getKittens = getKittens;
	$scope.getParents = getParents;
	$scope.selectKitten = selectKitten;
	$scope.getAllKittens = getAllKittens;
	$scope.setMainPhotoFor = kittensImageWorker.setMainPhotoFor;
	$scope.addThePhoto = kittensImageWorker.addThePhoto;
	$scope.removeKitten = removeKitten;
	$scope.addNewKitten = addNewKitten;
	$scope.getKittenPictures = getKittenPictures;

	$scope.getOwners = getOwners;
	$scope.getBreeds = getBreeds;
	$scope.getDisplayPlaces = getDisplayPlaces;
});

artDuviksControllers.controller("MainController", function ($scope) {

});

artDuviksControllers.controller("BreedsCtrl", function ($scope, $http, configuration) {
	var baseServerApiUrl = configuration.ServerApi;

	$http({ method: 'GET', url: baseServerApiUrl + '/breeds' }).success(function(data) {
		$scope.breeds = data;
	});
});
