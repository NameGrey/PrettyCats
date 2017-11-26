define([
	'angular',
	'app/pages/getParentsList/parents.module',
	'app/common/services/kittensPathBuilder',
	'app/common/services/kittensImageWorker',
	'app/common/services/kittenBackendCommunicator'
], function parentsComponentModule(angular) {
	'use strict';

	parentsController.$inject = ['kittenBackendCommunicator', 'kittensImageWorker', 'kittensPathBuilder'];

	angular.module('parents.module')
		.component('parentsComponent', {
			templateUrl: '/app/pages/getParentsList/partials/parents.html',
			controller: parentsController,
			controllerAs: 'parentsCtrl'
		}
	);	

	function parentsController(kittenBackendCommunicator, kittensImageWorker, kittensPathBuilder, $stateParams) {
		var self = this;

		self.kittens = null;
		self.addKittenLink = null;

		self.$onInit = function () {
			self.addKittenLink = kittensPathBuilder.addParent;
			kittenBackendCommunicator.getParents()
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
