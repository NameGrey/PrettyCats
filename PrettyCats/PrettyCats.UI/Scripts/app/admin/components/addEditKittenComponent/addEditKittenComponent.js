'use strict';

angular.module('AdminModule').component('addEditKittenComponent',
{
    bindings: {
        kitten:'<'
    },
    templateUrl: 'Scripts/app/admin/components/addEditKittenComponent/addEditKittenComponent.html',
    controller: function (kittenBackendCommunicator, $location, $timeout) {
        var errorMessageTimeout = 4000;
        var successMessageTimeout = 2000;
        var ctrl = this;
        ctrl.isValidFields = false;

        var getOwners = function () {
            kittenBackendCommunicator.getOwners().then(
                function success(data) {
                    ctrl.owners = data;
                },
                function error(e) {
                    ctrl.owners = null;
                    console.log(e);
                });
        };

        var getBreeds = function () {
            kittenBackendCommunicator.getBreeds().then(
                function success(data) {
                    ctrl.breeds = data;
                },
                function error(e) {
                    ctrl.breeds = null;
                    console.log(e);
                });
        };

        var getDisplayPlaces = function () {
            kittenBackendCommunicator.getDisplayPlaces().then(
                function success(data) {
                    ctrl.displayPlaces = data;
                },
                function error(e) {
                    ctrl.displayPlaces = null;
                    console.log(e);
                });
        };

        var successSaveMessage = function(kitten) {
            if (kitten.IsParent) {
                ctrl.successMessage = "Родитель успешно сохранен!";
            } else {
                ctrl.successMessage = "Котенок успешно сохранен!";
            }
        }

        var returnBack = function (currKitten) {
            if (currKitten.IsParent) {
                $location.path("/admin/parents");
            } else if (currKitten.IsInArchive) {
                $location.path("/admin/archive-kittens");
            } else {
                $location.path("/admin/available-kittens");
            }
        }

        var returnAfterSomeTime = function (kitten, interval) {
            $timeout(function () {
                returnBack(kitten);
            }, interval);
        }

        var dropErrorMessageAfterSomeTime = function (kitten, interval) {
            $timeout(function () {
                ctrl.errorMessage = null;
            }, interval);
        }

        var dropSuccessMessageAndReturnBackAfterSomeTime = function (kitten, interval) {
            $timeout(function () {
                ctrl.successMessage = null;
                ctrl.returnBack(kitten);
            }, interval);
        }

        var saveEditedKitten = function (kitten) {
            if (kitten) {
                kittenBackendCommunicator.saveEditedKitten(kitten).then(
                    function () {
                        successSaveMessage(kitten);
                        returnAfterSomeTime(kitten, successMessageTimeout);
                    },
                    function (e) {
                        ctrl.errorMessage = "Произошла ошибка на сервере!";
                        console.log(e);
                        dropErrorMessageAfterSomeTime(kitten, errorMessageTimeout);
                    });
            }
        }

        var selectKitten = function (kitten) {
            if (kitten && kitten.id > 0) {
                kittenBackendCommunicator.getKittenById(kitten.id).then(
                    function success(data) {
                        ctrl.kitten = data;
                    },
                function error(e) {
                    ctrl.errorMessage = "Произошла ошибка на сервере!";
                    console.log(e);

                    $timeout(function () {
                        returnBack(kitten);
                    }, errorMessageTimeout);
                });
            }
        }

        var addNewKitten = function (kitten) {
            if (kitten && ctrl.isValidFields) {
                kittenBackendCommunicator.addNewKitten(kitten)
                    .then(function () {
                            successSaveMessage(kitten);
                            dropSuccessMessageAndReturnBackAfterSomeTime(kitten, successMessageTimeout);
                        
                    },
                    function () {
                        ctrl.errorMessage = "Произошла непредвиденная ошибка на сервере. Котенок не был добавлен.";

                        dropErrorMessageAfterSomeTime(kitten, errorMessageTimeout);
                    });
            } else {
                ctrl.errorMessage = "Не все поля правильно заполнены!"

                dropErrorMessageAfterSomeTime(kitten, errorMessageTimeout);
            }
        }

        this.$onInit = function () {
            ctrl.successMessage = null;
            ctrl.errorMessage = null;
            selectKitten(ctrl.kitten);

            getOwners();
            getBreeds();
            getDisplayPlaces();
        }

        ctrl.saveEditedKitten = saveEditedKitten;
        ctrl.returnBack = returnBack;
        ctrl.addNewKitten = addNewKitten;
    }
});