'use strict';

angular.module('AdminModule').component('messagePlaceComponent', {
    bindings: {
        errorMessage: "<",
        successMessage: "<",
        cleanInterval: "&"
    },
    templateUrl: "Scripts/app/admin/components/messagePlaceComponent/messagePlaceComponent.html",
    controller: function () {
        var ctrl = this;
        this.$onChanges = function (changes) {
            ctrl.successMessage = changes.successMessage.currentValue;
            ctrl.errorMessage = changes.errorMessage.currentValue;
        }
    }
})