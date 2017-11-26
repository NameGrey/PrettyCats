define([
	'angular',
	'app/pages/getArchiveKittensList/archiveKittens.module',
	'app/common/services/kittensPathBuilder',
	'app/common/services/kittensImageWorker',
	'app/common/services/kittenBackendCommunicator'
], function archiveKittensComponentModule(angular) {
	'use strict';

	archiveKittensController.$inject = ['kittenBackendCommunicator', 'kittensImageWorker', 'kittensPathBuilder'];

	angular.module('archiveKittens.module')
		.component('archiveKittensComponent', {
			templateUrl: '/app/pages/archiveKittens/partials/archiveKittens.html',
			controller: archiveKittensController,
			controllerAs: 'archiveKittensCtrl'
		}
	);	

	function archiveKittensController(kittenBackendCommunicator, kittensImageWorker, kittensPathBuilder, $stateParams) {
		var self = this;

		self.kittens = null;
		self.addKittenLink = null;

		self.$onInit = function () {
			self.addKittenLink = kittensPathBuilder.addArchiveKitten;
			kittenBackendCommunicator.getArchiveKittens()
				.then(successLoadedKittens, loadedWithErrorsKittens);
		}

		function successLoadedKittens(data) {
			self.kittens = data;
		}

		function loadedWithErrorsKittens(e) {
			self.kittens = null;
			console.log(e);
		}
	};
});
