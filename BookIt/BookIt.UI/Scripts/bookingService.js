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
            createOffer: createOffer
        };

        var serverBaseUrl = "http://localhost:55060/api/Booking";

        function getSubjects() {
            return $http.get(serverBaseUrl + '/subjects');
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

        function createOffer(subjectsId) {
            var dataObj = {
                SubjectName : "djkfhsdkjf",
                StartDate : '07/07/2015',
                EndDate : '08/08/2015',
                IsInfinite : false
            };
            var serviceUrl = serverBaseUrl + '/subjects/' + subjectsId + "/offers";
            //return $http.post(serviceUrl, dataObj);
            return $http({
                method: 'POST',
                url: serviceUrl,
                data: dataObj,
                headers: {
                    'Content-Type': 'application/json'
                }
            });
        }

        return service;
    }
})();