define([
	'app/common/common.module'
], function adminKittensPageModule() {
	"use strict";

	adminKittensPageController.$inject = ['kittenBackendCommunicator', '$timeout'];

	angular.module("common.module")
		.component("adminKittensPageComponent",
		{
			bindings: {
				kittens: "<",
				addKittenLink: "<"
			},
			templateUrl: "app/common/components/adminKittensPageComponent/adminKittensPageComponent.html",
			controller: adminKittensPageController
		});

	function adminKittensPageController(kittenBackendCommunicator, $timeout) {
		var ctrl = this;
		var messageInterval = 1000;

		this.kittenHasBeenRemoved = false;
		this.hideKittenRemovedMessage = true;

		this.removeKitten = removeKitten;

		function onShowKittenRemovedMessage(kitten) {
			if (kitten.showKittenRemovedMessage != null) {
				kitten.showKittenRemovedMessage();
			};
		}

		function onHideKittenRemovedMessage(kitten) {
			if (kitten.hideKittenRemovedMessage != null) {
				kitten.hideKittenRemovedMessage();
			}
		}

		function removeKitten(kitten) {
			kittenBackendCommunicator.removeKitten(kitten.ID).then(
				function success() {
					onShowKittenRemovedMessage(kitten);

					$timeout(function () {
						onHideKittenRemovedMessage(kitten);
					}, messageInterval);
				},
				function error() {
					onShowKittenRemovedMessage(kitten);

					$timeout(function () {
						onHideKittenRemovedMessage(kitten);
					}, messageInterval);

				});
		}
	}
});

