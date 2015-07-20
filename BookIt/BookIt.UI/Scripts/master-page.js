var bookItApp = angular.module('bookItApp', ['ngRoute', 'bookItControllers']);

bookItApp.config(['$routeProvider',
    function ($routeProvider) {
        $routeProvider.
            when('/reservations', {
                templateUrl: 'html-partials/reservations.html',
                controller: 'ReservationListCtrl'
            }).
            when('/reservations/:reservationId', {
                templateUrl: 'html-partials/reservation-detail.html',
                controller: 'ReservationDetailCtrl'
            }).
             when('/reservations-search', {
                 templateUrl: 'html-partials/reservations-search.html',
                 controller: 'ReservationSearchCtrl'
             }).
            otherwise({
                redirectTo: '/reservations'
            });
    }]);

