'use strict';

angular.module('AdminModule').component("kittenFieldsComponent",
{
    templateUrl: "Scripts/app/admin/components/kittenFieldsComponent/kittenFieldsComponent.html",
    bindings: {
        kitten: "=",
        breeds: "<",
        owners: "<",
        parents: "<",
        displayPlaces: "<"
    }
});