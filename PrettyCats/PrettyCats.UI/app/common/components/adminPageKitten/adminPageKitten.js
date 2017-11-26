define([
	'app/common/directives/mainPhotoSelector/mainPhotoSelector',
	'app/common/directives/progressBar/progressBar',
	'app/common/directives/multiplayPhotosSelector'
], function adminPagaKittenModule() {
	"use strict";

	angular.module("common.module")
		.component("adminPageKitten", {
			templateUrl: "app/common/components/adminPageKitten/adminPageKitten.html",
			bindings: {
				kitten: "<",
				removeKitten: "&",
				showKittenRemovedMessage: "=",
				hideKittenRemovedMessage: "="
			},
			controller: adminPageController,
			controllerAs: 'kittenCtrl'
		});

	adminPageController.$inject = ['kittensImageWorker', '$location', 'kittensPathBuilder'];

	function adminPageController(kittensImageWorker, $location, kittensPathBuilder) {
		var self = this;

		self.totalNumberOfFiles = 0;
		self.loadedFilesCount = 0;
		self.filesWithErrorsCount = 0;
		self.kittenRemoved = false;
		self.hideMessage = true;

		self.informMessage = null;
		self.errorMessage = null;

		self.loadingFinished = loadingFinished;
		self.editKitten = editKitten;
		self.changeKittenPictures = changeKittenPictures;

		self.closeInformMessage = closeInformMessage;
		self.showKittenRemovedMessage = showKittenRemovedMessage;
		self.closeErrorMessage = closeErrorMessage;
		self.hideKittenRemovedMessage = hideKittenRemovedMessage;

		self.$onInit = onInit;

		function onInit() {
			kittensImageWorker.initializeMainPicture(self.kitten);
		}

		function changeKittenPictures(kitten) {
			var url = kittensPathBuilder.kittenModifyPictures + kitten.ID;
			$location.url(url);
		}

		function editKitten(kitten) {
			var url = kittensPathBuilder.editKitten + kitten.ID;
			$location.url(url);
		}

		function loadingFinished() {
			if (self.filesWithErrorsCount > 0) {
				self.informMessage = null;
				self.errorMessage = "Не все фотографии успешно добавлены!";
			} else {
				self.errorMessage = null;
				self.informMessage = "Все фотографии успешно добавлены!";
			}
		}

		function closeInformMessage() {
			self.informMessage = null;
		}

		function closeErrorMessage() {
			self.informMessage = null;
		}

		function hideKittenRemovedMessage() {
			self.hideMessage = true;
		}

		function showKittenRemovedMessage() {
			self.kittenRemoved = true;
			self.hideMessage = false;
		}
	}
});