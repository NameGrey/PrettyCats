﻿'use strict';

    var serviceId = 'bookingService';
    angular.module('bookItApp').factory(serviceId, ['$http', '$q', 'Configuration', bookingService]);

    function bookingService($http, $q, Configuration) {
        // Define the functions and properties to reveal.
        var service = {
            getSubjects: getSubjects,
            getOffers: getOffers,
            getSubjectDetails: getSubjectDetails,
            getOfferDetails: getOfferDetails,
            getFilteredSubjects: getFilteredSubjects,
			getCategories:getCategories,
            createOffer: createOffer,
            bookOffer: bookOffer,
            unBookOffer: unBookOffer
        };

        var serverBaseUrl = Configuration.API;

        function getSubjects() {
            return $http.get(serverBaseUrl + '/subjects');
        }

        function getCategories() {
        	return $http.get(serverBaseUrl + '/categories');
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

        function unBookOffer(slot) {
            var serviceUrl = serverBaseUrl + '/offers';
            return $http.delete(serviceUrl, { params: { slotId: slot.Id, offerId: slot.BookingOfferId } });

        }

        function bookSlot(slotId) {
            var serviceUrl = serverBaseUrl + '/offers/' + offerId;
            return $http.post(serviceUrl);

        }

        return service;
    }
