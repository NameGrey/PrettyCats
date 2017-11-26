define([
	'app/common/common.module',
	'app/common/components/kittenFieldsComponent/kittenFieldsComponent'
], function addEditKittenModule() {
	"use strict";

	addEditKittenController.$inject = ['kittenBackendCommunicator', 'kittensPathBuilder', '$location', '$timeout'];

	// TODO: Refactor everything here! 
	// TODO: 1. This component should be dumm
	// TODO: 2. Create well orginized form with validated flags in use
	angular.module("common.module")
		.component("addEditKittenComponent",
		{
			bindings: {
				kitten: "<"
			},
			templateUrl: "app/common/components/addEditKittenComponent/addEditKittenComponent.html",
			controller: addEditKittenController,
			controllerAs: 'addEditKittenCtrl'
		});

	function addEditKittenController(kittenBackendCommunicator, kittensPathBuilder, $location, $timeout) {
		var errorMessageTimeout = 4000;
		var successMessageTimeout = 2000;
		var ctrl = this;
		ctrl.isValidFields = false;

		ctrl.saveEditedKitten = saveEditedKitten;
		ctrl.returnBack = returnBack;
		ctrl.addNewKitten = addNewKitten;
		ctrl.$onInit = onInit;

		ctrl.parents = [];
		ctrl.owners = [];
		ctrl.breeds = [];

		function onInit() {
			ctrl.successMessage = null;
			ctrl.errorMessage = null;
			selectKitten(ctrl.kitten);

			getOwners();
			getBreeds();
			getDisplayPlaces();
		}

		function getOwners() {
			kittenBackendCommunicator.getOwners().then(
				function success(data) {
					ctrl.owners = data;
				},
				function error(e) {
					ctrl.owners = null;
					console.log(e);
				});
		};

		function getBreeds() {
			kittenBackendCommunicator.getBreeds().then(
				function success(data) {
					ctrl.breeds = data;
				},
				function error(e) {
					ctrl.breeds = null;
					console.log(e);
				});
		};

		function getDisplayPlaces() {
			kittenBackendCommunicator.getDisplayPlaces().then(
				function success(data) {
					ctrl.displayPlaces = data;
				},
				function error(e) {
					ctrl.displayPlaces = null;
					console.log(e);
				});
		};

		function successSaveMessage(kitten) {
			if (kitten.IsParent) {
				ctrl.successMessage = "Родитель успешно сохранен!";
			} else {
				ctrl.successMessage = "Котенок успешно сохранен!";
			}
		}

		function returnBack(currKitten) {
			if (currKitten.IsParent) {
				$location.path(kittensPathBuilder.parents);
			} else if (currKitten.IsInArchive) {
				$location.path(kittensPathBuilder.archiveKittens);
			} else {
				$location.path(kittensPathBuilder.availableKittens);
			}
		}

		function returnAfterSomeTime(kitten, interval) {
			$timeout(function () {
				returnBack(kitten);
			}, interval);
		}

		function dropErrorMessageAfterSomeTime(kitten, interval) {
			$timeout(function () {
				ctrl.errorMessage = null;
			}, interval);
		}

		function dropSuccessMessageAndReturnBackAfterSomeTime(kitten, interval) {
			$timeout(function () {
				ctrl.successMessage = null;
				ctrl.returnBack(kitten);
			}, interval);
		}

		function notAllFieldsCorrect(kitten) {
			ctrl.errorMessage = "Не все поля правильно заполнены!";
			dropErrorMessageAfterSomeTime(kitten, errorMessageTimeout);
		}

		function saveEditedKitten(kitten) {
			if (kitten && ctrl.isValidFields) {
				kittenBackendCommunicator.saveEditedKitten(kitten).then(
					function () {
						successSaveMessage(kitten);
						returnAfterSomeTime(kitten, successMessageTimeout);
					},
					function (e) {
						ctrl.errorMessage = "Произошла ошибка на сервере!";
						console.log(e);
						dropErrorMessageAfterSomeTime(kitten, errorMessageTimeout);
					});
			} else {
				notAllFieldsCorrect(kitten);
			}
		}

		function selectKitten(kitten) {
			if (kitten && kitten.id > 0) {
				kittenBackendCommunicator.getKittenById(kitten.id).then(
					function success(data) {
						ctrl.kitten = data;
					},
					function error(e) {
						ctrl.errorMessage = "Произошла ошибка на сервере!";
						console.log(e);

						$timeout(function () {
							returnBack(kitten);
						}, errorMessageTimeout);
					});
			}
		}

		function addNewKitten(kitten) {
			if (kitten && ctrl.isValidFields) {
				kittenBackendCommunicator.addNewKitten(kitten)
					.then(function () {
						successSaveMessage(kitten);
						dropSuccessMessageAndReturnBackAfterSomeTime(kitten, successMessageTimeout);

					},
					function () {
						ctrl.errorMessage = "Произошла непредвиденная ошибка на сервере. Котенок не был добавлен.";

						dropErrorMessageAfterSomeTime(kitten, errorMessageTimeout);
					});
			} else {
				notAllFieldsCorrect(kitten);
			}
		}
	}
})

