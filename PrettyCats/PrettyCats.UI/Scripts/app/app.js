'use strict';

var artDuviksApp = angular.module("artDuviksApp", ["ngRoute", "SharedModule", "BreedsModule", "KittensModule"]);

artDuviksApp.factory("configuration", function() {
		return { ServerApi: "http://localhost:53820/api" };
});

artDuviksApp.controller("MainController", function () {

});

artDuviksApp.run([
	"$rootScope", function ($rootScope) {

		$rootScope.$on('$stateChangeError', function(event, toState, toParams, fromState, fromParams, error) {
			console.error("$stateChangeError: ", toState, error);
		});

		$rootScope.$on("$routeChangeSuccess", function(event, current, previous) {
			if (current && current.$$route) {
				if (current.$$route.title)
					$rootScope.title = current.$$route.title;

				if (current.$$route.keywords)
					$rootScope.keywords = current.$$route.keywords;

				if (current.$$route.description)
					$rootScope.description = current.$$route.description;
			}
		});
	}
]);
