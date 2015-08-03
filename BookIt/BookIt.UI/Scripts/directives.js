
angular.module('bookItApp')

    .directive('customfilter', function () {
        return {
            restrict: 'E',
            templateUrl: 'templates/filter.html', // markup for filter
            controller: function ($scope, bookingService) {
            	bookingService.getCategories()
				.success(function (data, status, headers, config) {
					$scope.categories = data;
				})
				.error(catchServiceError);
            }

        };
    })

.directive('timeslot', function () {
    return {
        restrict: 'E',
        templateUrl: 'templates/timeslot.html', // markup
        scope: {
            slot: '=' // allows data to be passed into directive from controller scope
        }
    };
});

