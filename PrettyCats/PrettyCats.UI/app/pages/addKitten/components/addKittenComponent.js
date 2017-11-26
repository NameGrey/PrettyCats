define([
	'angular',
	'app/pages/addKitten/addKitten.module',
	'app/common/components/addEditKittenComponent/addEditKittenComponent'
], function addKittenComponentModule(angular) {
	'use strict';

	addKittenController.$inject = [];

	angular.module('addKitten.module')
		.component('addKittenComponent', {
			templateUrl: '/app/pages/addKitten/partials/addKitten.html',
			controller: addKittenController,
			controllerAs: 'addKittenCtrl'
		});

	function addKittenController() {
		var self = this;

		self.$onInit = onInit;

		function onInit() {
			self.selectedKitten = { isNew: true };
		}
	}
});