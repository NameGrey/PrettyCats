'use strict';

angular.module('AdminModule').component("adminPageKitten", {
    templateUrl: "Scripts/app/admin/components/adminPageKitten/adminPageKitten.html",
    bindings: {
        kitten: "=",
        removeKitten: "&"
    },
    controller: function(kittensImageWorker, $location) {
        this.editKitten = function(kitten) {
            var url = "/admin/editKitten/" + kitten.ID;
            $location.url(url);
        }

        kittensImageWorker.initializeMainPicture(this.kitten);
    }
});