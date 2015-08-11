var bookItApp = angular.module('bookItApp', ['ngRoute', 'bookItControllers']);

bookItApp.config(['$routeProvider',
    function ($routeProvider) {
        $routeProvider.
            when('/subjects', {
                templateUrl: 'html-partials/subjects.html',
                controller: 'SubjectsCtrl'
            }).
            when('/subjects/:id', {
                templateUrl: 'html-partials/subject-details.html',
                controller: 'SubjectDetailsCtrl'
            }).
             when('/offers', {
                 templateUrl: 'html-partials/offers.html',
                 controller: 'OffersCtrl'
             }).
             when('/offers/:id', {
                 templateUrl: 'html-partials/offer-details.html',
                 controller: 'OfferDetailsCtrl'
             }).
            otherwise({
                redirectTo: '/subjects'
            });
    }]);

bookItApp.service("Configuration", function () {
    if (window.location.host.match('localhost:65265')) {
        return this.API = 'http://localhost:55060/api/Booking';
    } else {
        return this.API = 'http://10.6.196.27/BookITAPI/api/Booking';
    }
});

bookItApp.config(['$httpProvider',
    function ($httpProvider) {
        $httpProvider.defaults.useXDomain = true;
        delete $httpProvider.defaults.headers.common['X-Requested-With'];
    }]);


