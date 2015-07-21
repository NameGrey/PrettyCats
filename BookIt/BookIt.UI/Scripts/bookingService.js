(function () {
    'use strict';
    var serviceId = 'bookingService';
    angular.module('app').factory(serviceId, ['$http', '$q', bookingService]);
    function bookingService($http, $q) {
        // Define the functions and properties to reveal.
        var service = {
            getSubjects: getSubjects,
            getOffers: getOffers,
        };

        var serverBaseUrl = "http://localhost:55060/api/Booking";

        function getSubjects() {
            return $http.get(serverBaseUrl + '/subjects');
        }

        function getOffers() {
            return $http.get(serverBaseUrl + '/offers');
        }

        return service;
    }
})();