(function () {
    'use strict';
    var serviceId = 'bookingService';
    angular.module('bookItApp').factory(serviceId, ['$http', '$q', bookingService]);

    function bookingService($http, $q) {
        // Define the functions and properties to reveal.
        var service = {
            getSubjects: getSubjects,
            getOffers: getOffers,
            getSubjectDetails: getSubjectDetails,
            getOfferDetails: getOfferDetails,
            getFilteredSubjects : getFilteredSubjects
        };

        var serverBaseUrl = "http://localhost:55060/api/Booking";

        function getSubjects() {
            return $http.get(serverBaseUrl + '/subjects');
        }

        function getFilteredSubjects(categoryId, subjectName) {
            return $http.get(serverBaseUrl + '/subjects/categoryId:' + categoryId + '/subjectName:' + subjectName );
        }

        function getOffers() {
            return $http.get(serverBaseUrl + '/offers');
        }

        function getSubjectDetails(subjectId) {
            return $http.get(serverBaseUrl + '/subjects/' + subjectId);
        }

        function getOfferDetails(offerId) {
            return $http.get(serverBaseUrl + '/offers/' + offerId);
        }

        return service;
    }
})();