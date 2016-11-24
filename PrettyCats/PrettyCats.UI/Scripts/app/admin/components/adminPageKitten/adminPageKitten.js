'use strict';

angular.module('AdminModule').component("adminPageKitten", {
    templateUrl: "Scripts/app/admin/components/adminPageKitten/adminPageKitten.html",
    bindings: {
        kitten: "<",
        removeKitten: "&"
    },
    controller: function (kittensImageWorker, $location) {
        this.totalNumberOfFiles = 0;
        this.loadedFilesCount = 0;
        this.filesWithErrorsCount = 0;

        this.informMessage = null;
        this.errorMessage = null;

        this.loadingFinished = function() {
           if (this.filesWithErrorsCount > 0) {
               this.informMessage = null;
               this.errorMessage = "Не все фотографии успешно добавлены!";
           } else {
               this.errorMessage = null;
               this.informMessage = "Все фотографии успешно добавлены!";
           }
        }

        this.editKitten = function(kitten) {
            var url = "/admin/editKitten/" + kitten.ID;
            $location.url(url);
        }

        this.changeKittenPictures = function (kitten) {            
            var url = "/admin/kitten/modify-pictures/" + kitten.ID;
            $location.url(url);
        }

        this.closeInformMessage = function() {
            this.informMessage = null;
        }

        this.closeErrorMessage = function () {
            this.informMessage = null;
        }

        kittensImageWorker.initializeMainPicture(this.kitten);
    }
});