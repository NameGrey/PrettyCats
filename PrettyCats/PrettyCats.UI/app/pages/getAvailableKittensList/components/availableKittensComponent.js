define([
	'angular',
	'app/pages/getAvailableKittensList/availableKittens.module',
	'app/common/services/kittensPathBuilder',
	'app/common/services/kittensImageWorker',
	'app/common/services/kittenBackendCommunicator',
	'app/common/components/adminKittensPageComponent/adminKittensPageComponent',
	'app/common/components/adminPageKitten/adminPageKitten'
], function availableKittensComponentModule(angular) {
	'use strict';

	availableKittensController.$inject = ['kittenBackendCommunicator', 'kittensImageWorker', 'kittensPathBuilder'];

	angular.module('availableKittens.module')
		.component('availableKittensComponent', {
			templateUrl: '/app/pages/getAvailableKittensList/partials/availableKittens.html',
			controller: availableKittensController,
			controllerAs: 'availableKittensCtrl'
		}
	);	

	function availableKittensController(kittenBackendCommunicator, kittensImageWorker, kittensPathBuilder) {
		var self = this;

		self.kittens = null;
		self.addKittenLink = null;

		self.$onInit = function () {
			self.addKittenLink = kittensPathBuilder.addKitten;
			kittenBackendCommunicator.getAvailableKittens()
				.then(successLoadedKittens, loadedWithErrorsKittens);
		};

		function successLoadedKittens(data) {
			self.kittens = data;
		}

		function loadedWithErrorsKittens(e) {
			self.kittens = null;
			console.log(e);
		}
	}
});
