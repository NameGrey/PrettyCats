define([
	'angular',
	'app/config',
	'angular-ui-router',
	'app/pages/adminPanel/components/adminPanelComponent',
	'app/pages/getAvailableKittensList/components/availableKittensComponent',
	'app/pages/getArchiveKittensList/components/archiveKittensComponent',
	'app/pages/getParentsList/components/parentsComponent',
	'app/pages/addKitten/components/addKittenComponent',
	'app/pages/editKitten/components/editKittenComponent',
	'app/pages/modifyPicturesOrder/components/modifyPicturesOrderComponent',
], function(angular, configurationService) {

	'use strict';

	var artDuviksApp = angular.module("artDuviksApp", [
		'ui.router',
		'adminPanel.module',
		'availableKittens.module',
		'archiveKittens.module',
		'parents.module',
		'addKitten.module',
		'editKitten.module',
		'modifyPicturesOrder.module'
	]);

	configurationService.configureRoutes();
	configurationService.configureHttpHeaders();

	artDuviksApp.init = function() {
		angular.bootstrap(angular.element(document), ['artDuviksApp']);
	};

	artDuviksApp.controller("MainController", function () {

	});

	return artDuviksApp;
});

