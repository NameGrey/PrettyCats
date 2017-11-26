define([
	'app/common/common.module'
], function progressBarModule() {
	'use strict';

	// TODO: create a new progress bar directive and fix its usage on the page
	angular.module("common.module")
		.directive("progressBar", function () {
			return {
				restrict: "E",
				scope: {
					totalCount: "=",
					currentSuccess: "=",
					currentErrors: "=",
					displayFlag: "="
				},
				templateUrl: "app/common/directives/progressBar/progressBar.html"
			}
	});
});
