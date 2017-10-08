define([
	'angular',
	'app/config',
	'angular-ui-router',
	'app/pages/adminPanel/components/adminPanelComponent'
], function(angular, configurationService) {

	'use strict';

	var artDuviksApp = angular.module("artDuviksApp", [
		'ui.router',
		'adminPanel.module'
	]);

	configurationService.configureRoutes();
	configurationService.configureHttpHeaders();

	artDuviksApp.init = function() {
		angular.bootstrap(angular.element(document), ['artDuviksApp']);
	};

	artDuviksApp.factory("configuration", function () {
		var serverHost = "http://localhost:53820";
		return {
			ServerHost: serverHost,
			ServerApi: serverHost + "/api"
		};
	});

	artDuviksApp.controller("MainController", function () {

	});

	return artDuviksApp;
});

