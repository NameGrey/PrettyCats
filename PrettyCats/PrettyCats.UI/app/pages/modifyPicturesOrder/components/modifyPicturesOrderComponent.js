define([
	'angular',
	'app/pages/modifyPicturesOrder/modifyPicturesOrder.module'
], function modifyPicturesOrderComponentModule(angular) {
	'use strict';

	angular.module('modifyPicturesOrder.module')
		.component('modifyPicturesOrderComponent', {
			templateUrl: '/app/pages/modifyPicturesOrder/partials/modifyPicturesOrder.html',
			controller: modifyPicturesOrderController,
			controllerAs: 'modifyPicturesOrderCtrl'
		}
	);

	function modifyPicturesOrderController() {
		var self = this;

		self.$postLink = function () {
			$("change-pictures-component").sortable({
				stop: function (event, ui) {
					var picturesList = [];

					//  $(event.target).children('.image-container').each(function (el) {
					//    picturesList.push({ id: this.id, order: el + 1 });
					// });

					// angular.element($(event.target)).scope().
				}
			});
			$("change-pictures-component").disableSelection();
		}
	}
});
