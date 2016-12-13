'use strict';

angular.module("artDuviksApp").component('messagePlaceComponent', {
    bindings: {
        errorMessage: "<",
        successMessage: "<",
        cleanInterval: "&"
    },
    templateUrl: "app/components/messagePlaceComponent/messagePlaceComponent.html",
    controller: function () {
        var ctrl = this;
        this.$onChanges = function (changes) {

            if (changes.successMessage !== undefined) {
                ctrl.successMessage = changes.successMessage.currentValue;
            }

            if (changes.errorMessage !== undefined) {
                ctrl.errorMessage = changes.errorMessage.currentValue;
            }
        }
    }
})