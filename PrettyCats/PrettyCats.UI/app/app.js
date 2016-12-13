'use strict';

var artDuviksApp = angular.module("artDuviksApp", ["ngRoute"]);

artDuviksApp.factory("configuration", function () {
    var serverHost = "http://localhost:53820";
    return {
        ServerHost: serverHost,
        ServerApi: serverHost + "/api"
    };
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
