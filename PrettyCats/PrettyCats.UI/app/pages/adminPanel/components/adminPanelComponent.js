define([
	'angular',
	'app/common/services/kittensPathBuilder',
	'app/pages/adminPanel/adminPanel.module'
], function adminPanelComponentModule(angular) {
	'use strict';

	adminPanelController.$inject = ['kittensPathBuilder'];

	angular.module('adminPanel.module')
		.component('adminPanelComponent', {
			templateUrl: '/app/pages/adminPanel/partials/adminPanel.html',
			controller: adminPanelController,
			controllerAs: 'adminPanelCtrl'
		}
	);

	function adminPanelController(kittensPathBuilder) {
		var self = this;

		self.availableKittensLink = kittensPathBuilder.availableKittens;
		self.archiveKittensLink = kittensPathBuilder.archiveKittens;
		self.parentsLink = kittensPathBuilder.parents;
		self.siteIndex = kittensPathBuilder.siteIndex;
	}
});
