define([
	'app/common.module'
], function messagePlaceComponent() {
	'use strict';

	angular.module("common.module")
		.component('messagePlaceComponent', {
			bindings: {
				errorMessage: "<",
				successMessage: "<",
				cleanInterval: "&"
			},
			templateUrl: "app/common/components/messagePlaceComponent/messagePlaceComponent.html",
			controller: function () {
				var ctrl = this;
				this.$onChanges = function (changes) {

					if (changes.successMessage !== undefined) {
						ctrl.successMessage = changes.successMessage.currentValue;
					}

					if (changes.errorMessage !== undefined) {
						ctrl.errorMessage = changes.errorMessage.currentValue;
					}
				}
			}
		});
});
