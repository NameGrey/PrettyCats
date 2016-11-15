'use strict';

angular.module('AdminModule').component("adminKittensList", {
    templateUrl: "Scripts/app/admin/components/adminKittensList/adminKittensList.html",
    bindings: {
        kittens: "=",
        removeKitten: "&"
    }
});