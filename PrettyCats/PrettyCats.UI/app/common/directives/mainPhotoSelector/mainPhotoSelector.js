﻿define([
	'angular',
	'app/common/common.module'
], function mainPhotoSelectorModule(angular) {
	'use strict';

	angular.module('common.module')
		.directive('mainPhotoSelector', mainPhotoSelectorDirective)

	function mainPhotoSelectorDirective(kittensImageWorker, $compile) {
		return {
			restrict: 'E',
			templateUrl: "app/common/directives/mainPhotoSelector/mainPhotoSelector.html",
			replace: true,
			scope: {
				kitten: "="
			},
			link: function ($scope, el, attrs, ngModel) {

				el.bind("click", function (event) {
					var i = $(event.target).find("input[type='file']");

					if (i) {
						i.trigger("click");
					}
				});

				el.find("input[type='file']").bind('change', function (event) {
					kittensImageWorker.setMainPhotoFor(event.target.files[0], $scope.kitten)
						.success(function (data) {
							var random = (new Date()).toString();

							data.Image += "?t=" + random;
							$scope.kitten.MainPicture = data;
							$scope.theFile = null;
						})
						.error(function () {
							$scope.theFile = null;
						});
				});
			}
		}
	}
});
