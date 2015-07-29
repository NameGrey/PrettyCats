
angular.module('bookItApp')

    .directive('filter', function () {
        return {
            restrict: 'E',
            templateUrl: 'templates/filter.html', // markup for filter
            scope: {
                search: '=' // allows data to be passed into directive from controller scope
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

