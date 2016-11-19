'use strict';

angular.module('AdminModule').component("kittenFieldsComponent",
{
    templateUrl: "Scripts/app/admin/components/kittenFieldsComponent/kittenFieldsComponent.html",
    bindings: {
        kitten: "=",
        breeds: "<",
        owners: "<",
        parents: "<",
        displayPlaces: "<",
        isValid: "="
    },
    controller: function () {
        var regexp = new RegExp("[0-9а-яА-Я]");
        this.onlyEnglish = !regexp.test(this.kitten.Name);

        //if you change these conditions, don't forget to change the same in template
        this.changeValidationFlag = function () {
            this.onlyEnglish = !regexp.test(this.kitten.Name);

            this.isValid =
            this.kitten.Name && this.kitten.Name.length > 0 &&
            this.kitten.OwnerID > 0 &&
            this.kitten.BreedID > 0 &&
            this.kitten.WhereDisplay > 0 &&
            this.onlyEnglish;
        }
    }
});