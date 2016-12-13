"use strict";

angular.module("artDuviksApp").component("adminPageKitten", {
    templateUrl: "app/components/adminPageKitten/adminPageKitten.html",
    bindings: {
        kitten: "<",
        removeKitten: "&",
        showKittenRemovedMessage: "=",
        hideKittenRemovedMessage: "="
    },
    controller: function (kittensImageWorker, $location, kittensPathBuilder) {
        var ctrl = this;
        ctrl.totalNumberOfFiles = 0;
        ctrl.loadedFilesCount = 0;
        ctrl.filesWithErrorsCount = 0;
        ctrl.kittenRemoved = false;
        ctrl.hideMessage = true;

        ctrl.informMessage = null;
        ctrl.errorMessage = null;

        ctrl.loadingFinished = function () {
            if (ctrl.filesWithErrorsCount > 0) {
                ctrl.informMessage = null;
                ctrl.errorMessage = "Не все фотографии успешно добавлены!";
           } else {
                ctrl.errorMessage = null;
                ctrl.informMessage = "Все фотографии успешно добавлены!";
           }
        }

        ctrl.editKitten = function (kitten) {
            var url = kittensPathBuilder.editKitten + kitten.ID;
            $location.url(url);
        }

        ctrl.changeKittenPictures = function (kitten) {
            var url = kittensPathBuilder.kittenModifyPictures + kitten.ID;
            $location.url(url);
        }

        ctrl.closeInformMessage = function () {
            ctrl.informMessage = null;
        }

        ctrl.closeErrorMessage = function () {
            ctrl.informMessage = null;
        }

        ctrl.showKittenRemovedMessage = function () {
            ctrl.kittenRemoved = true;
            ctrl.hideMessage = false;
        }

        ctrl.hideKittenRemovedMessage = function () {
            ctrl.hideMessage = true;
        }

        kittensImageWorker.initializeMainPicture(ctrl.kitten);
    }
});