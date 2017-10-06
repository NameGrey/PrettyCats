define([
	'angular',
	'app/config',
	'angular-ui-router'
], function(angular, configuration) {

	'use strict';

	var artDuviksApp = angular.module("artDuviksApp", ["ui.router"]);

	configuration.configureRoutes();
	configuration.configureHttpHeaders();

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

