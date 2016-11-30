﻿'use strict';

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

        //if you change these conditions, don't forget to change the same in template
        this.changeValidationFlag = function () {
            this.onlyEnglish = !this.kitten || !this.kitten.Name || this.kitten.Name.length > 0 && !regexp.test(this.kitten.Name);
            this.isCorrectOwner = this.kitten.OwnerID > 0;
            this.isCorrectBreed = this.kitten.BreedID > 0;
            this.isCorrectWhereDisplay = this.kitten.WhereDisplay > 0;

            this.isValid =
                this.kitten.Name && this.kitten.Name.length > 0 &&
                this.isCorrectOwner &&
                this.isCorrectBreed &&
                this.isCorrectWhereDisplay &&
                this.onlyEnglish;
        }

        this.$onChanges = function ()
        {
            this.changeValidationFlag();
        }
    }
});