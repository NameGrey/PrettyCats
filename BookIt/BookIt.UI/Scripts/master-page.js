﻿var bookItApp = angular.module('bookItApp', ['ngRoute', 'bookItControllers']);

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

