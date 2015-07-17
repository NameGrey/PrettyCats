(function () {
    'use strict';
    var controllerId = 'mainCtrl';
    angular.module('app').controller(controllerId,
        ['bookingService', mainCtrl]);
    function mainCtrl(bookingService) {

    }
})();