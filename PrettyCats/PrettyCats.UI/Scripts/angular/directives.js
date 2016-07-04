'use strict';

artDuviksApp.directive("thumbnail", function() {
	return {
		restrict: "E",
		templateUrl: "pages/templates/thumbnail.html",
		replace: true,
		controller: function ($scope, kittensPathBuilder, kittensImageWorker) {

			$scope.kitten.Link = kittensPathBuilder.buildKittenLink($scope.kitten);
			kittensImageWorker.getKittenMainPicture($scope.kitten).success(function(data) {
				$scope.kitten.MainPicture = data;
			});
		}
}
});

artDuviksApp.directive("adminPageKitten", ['kittensImageWorker', function (kittensImageWorker) {
	return {
		restrict: "E",
		templateUrl: "pages/templates/admin/adminPageKitten.html",
		replace: true,
		link: function ($scope) {
			kittensImageWorker.initializeMainPicture($scope.kitten);
		}
	}
}]);

artDuviksApp.directive("kittenPicture", function() {
	return {
		restrict: "E",
		replace: true
	}
});


artDuviksApp.directive('mainPhotoSelector',
	function (kittensImageWorker, $compile) {
		return {
			require: "ngModel",
			restrict: 'A',
			link: function ($scope, el, attrs, ngModel) {
				el.bind('change', function(event) {
					kittensImageWorker.setMainPhotoFor(event.target.files[0], $scope.kitten)
						.success(function (data) {
							var random = (new Date()).toString();

							data.Image += "?t=" + random;
							$scope.kitten.MainPicture = data;
							$scope.theFile = null;
						})
						.error(function() {
							$scope.theFile = null;
						});
				});
			}
		}
	}
);

artDuviksApp.directive('multiplayPhotosSelector', ['kittensImageWorker',
	function (kittensImageWorker) {
		return {
			require: "ngModel",
			restrict: 'A',
			link: function ($scope, el, attrs, ngModel) {
				el.bind('change', function (event) {
					angular.forEach(event.target.files, function(value, key) {
						kittensImageWorker.addThePhoto(key, $scope.kitten);
					});
				});
			}
		}
	}
]);