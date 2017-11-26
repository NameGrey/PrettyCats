define([
	'angular',
	'app/pages/editKitten/editKitten.module'
], function editKittenComponentModule(angular) {
	'use strict';

	editKittenController.$inject = ['$stateParams'];

	angular.module('editKitten.module')
		.component('editKittenComponent', {
			templateUrl: '/app/pages/editKitten/partials/editKitten.html',
			controller: editKittenController,
			controllerAs: 'editKittenCtrl'
		});

	function editKittenController($stateParams) {
		var self = this;

		self.$onInit = onInit;

		function onInit() {
			self.selectedKitten = { id: $stateParams.id };
		}
	}
});