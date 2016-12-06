angular.module("artDuviksApp").component("changePicturesComponent", {
    bindings: {
        pictures: "=",
        returnBackUrl: "<"
    },
    templateUrl: "Scripts/app/components/changePicturesComponent/changePicturesComponent.html",
    controller: function (kittensImageWorker, $scope, $location, $timeout) {
        // TODO: extract logic for messages into separate service
        // because the same functionality is used in other parts of app
        this.successMessage = null;
        this.errorMessage = null;
        this.messageTimeout = 2000;
        var ctrl = this;

        var dropMessagesAfterSomeTime = function () {
            $timeout(function() {
                ctrl.successMessage = null;
                ctrl.errorMessage = null;
            }, ctrl.messageTimeout);
        }

        this.removePicture = function(picture) {
            kittensImageWorker.removePhoto(picture.ID).then(
                function success() {
                    var pics = $scope.$ctrl.pictures;
                    var index = pics.indexOf(picture);
                    pics.splice(index, 1);
                },
                function error(e) {
                    console.log(e);
                });
        }

        this.returnBack = function(backUrl) {
            $location.path(backUrl);
        }

        this.savePicturesOrder = function (pictures) {
            var picturesList = [];

            // TODO: it's not good to interact with DOM here, change it in future
            $('change-pictures-component').children('.image-container').each(function (el) {
                picturesList.push({ id: this.id, order: el + 1 });
            });
            
            kittensImageWorker.changePicturesOrder(picturesList).then(
                function success(result) {
                    ctrl.successMessage = "Сохранено!";
                    dropMessagesAfterSomeTime();
                },
                function error(e) {
                    ctrl.errorMessage = "Не удалось сохранить!";
                    dropMessagesAfterSomeTime();
                    console.log(e);
                });
        }
    }
});