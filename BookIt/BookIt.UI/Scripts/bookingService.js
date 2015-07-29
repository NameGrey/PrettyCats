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
            getFilteredSubjects: getFilteredSubjects,
            createOffer: createOffer,
            bookOffer: bookOffer
        };

        var serverBaseUrl = "http://localhost:55060/api/Booking";

        function getSubjects() {
            return $http.get(serverBaseUrl + '/subjects');
        }

        function getFilteredSubjects(categoryId, subjectName) {
            return $http.get(serverBaseUrl + '/subjects/' + categoryId + '/' + subjectName);
        }

        function getOffers(subjectId) {
            if (subjectId)
                return $http.get(serverBaseUrl + '/subjects/' + subjectId + '/offers');
            else
                return $http.get(serverBaseUrl + '/offers');
        }

        function getSubjectDetails(subjectId) {
            return $http.get(serverBaseUrl + '/subjects/' + subjectId);
        }

        function getOfferDetails(offerId) {
            return $http.get(serverBaseUrl + '/offers/' + offerId);
        }

        function createOffer(subjectId, startDate, endDate) {
            var dataObj = {
                StartDate: startDate,
                EndDate: endDate,
                IsInfinite: false
            };
            var serviceUrl = serverBaseUrl + '/subjects/' + subjectId + "/offers";
            return $http.post(serviceUrl, dataObj);

        }

        function bookOffer(offerId, startDate, endDate) {
            var slotData = {
                StartDate: startDate,
                EndDate: endDate
            };
            var serviceUrl = serverBaseUrl + '/offers/' + offerId;
            return $http.post(serviceUrl, slotData);

        }

        function bookSlot(slotId) {
            var serviceUrl = serverBaseUrl + '/offers/' + offerId;
            return $http.post(serviceUrl);

        }

        return service;
    }
