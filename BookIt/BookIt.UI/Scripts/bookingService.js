(function () {
    'use strict';
    var serviceId = 'bookingService';
    angular.module('app').factory(serviceId, ['$http', '$q', bookingService]);
    function bookingService($http, $q) {
        // Define the functions and properties to reveal.
        var service = {
            registerUser: registerUser,    
        };
        var serverBaseUrl = "http://localhost:60737";

        return service;
    }
})();