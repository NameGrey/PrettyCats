(function () {
    'use strict';
    var controllerId = 'bookingCtrl';
    angular.module('app').controller(controllerId,
        ['bookingService', bookingCtrl]);
    function bookingCtrl(bookingService) {

        var bookingModel = this;

        getSubjects();
        getOffers();

        function getOffers() {
            bookingService.getOffers()
                
                .success(function (offers) {
                    bookingModel.offers = offers;

                })
                .error(function (error) {
                    bookingModel.status = 'Unable to load Offers: ' + error;

                });
        }

        function getSubjects() {
            bookingService.getSubjects()
                .success(function (subjects) {
                    bookingModel.subjects = subjects;

                })
                .error(function (error) {
                    bookingModel.status = 'Unable to load Subjects: ' + error;

                });
        }
    }
})();