'use strict';

artDuviksApp.directive("thumbnail", function() {
	return {
		restrict: "E",
		templateUrl: "pages/templates/thumbnail.html",
		replace: true,
		controller: function ($scope, kittensPathBuilder, kittensImageWorker) {

			$scope.kitten.Link = kittensPathBuilder.buildKittenLink($scope.kitten);
			kittensImageWorker.getKittenMainPicture($scope.kitten).success(function(data) {
				$scope.kitten.Image = data;
			});
		}
}
});