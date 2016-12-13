'use strict';

angular.module('artDuviksApp').controller('adminKittensCtrl',
    function ($scope, kittenBackendCommunicator, kittensImageWorker, kittensPathBuilder, $routeParams) {
        $scope.kittens = null;
        $scope.addKittenLink = null;
        $scope.selectedKitten = null;
        $scope.returnBackUrl = null;

        var successLoadedKittens = function(data) {
            $scope.kittens = data;
        }

        var loadedWithErrorsKittens = function(e) {
            $scope.kittens = null;
            console.log(e);
        }

        var getKittenPictures = function (kittenId) {
            kittensImageWorker.getKittenPictures(kittenId)
                .then(
                    function success(result) {
                        $scope.selectedKitten = { pictures: result.data };
                    },
                    function error(e) {
                        console.log(e);
                    });
        }

        var initReturnBackUrl = function(kitten) {
            var result;

            if (kitten.IsParent) {
                result = kittensPathBuilder.parents;
            } else if (kitten.IsInArchive) {
                result = kittensPathBuilder.archiveKittens;
            } else {
                result = kittensPathBuilder.availableKittens;
            }
            return result;
        }

        $scope.initAvailableKittens = function () {
            $scope.addKittenLink = kittensPathBuilder.addKitten;
            kittenBackendCommunicator.getAvailableKittens()
                .then(successLoadedKittens, loadedWithErrorsKittens);
        }

        $scope.initArchiveKittens = function () {
            $scope.addKittenLink = kittensPathBuilder.addArchiveKitten;
            kittenBackendCommunicator.getArchiveKittens()
                .then(successLoadedKittens, loadedWithErrorsKittens);
        }

        $scope.initParents = function () {
            $scope.addKittenLink = kittensPathBuilder.addParent;
            kittenBackendCommunicator.getParents()
                .then(successLoadedKittens, loadedWithErrorsKittens);
        }

        $scope.initEditedKitten = function () {
            $scope.selectedKitten = { id: $routeParams.id };
        }

        $scope.initNewKitten = function() {
            $scope.selectedKitten = { isNew: true };
        }

        $scope.initKittenForPicturesModification = function() {
            var kittenId = $routeParams.id;

            if (kittenId) {
                kittenBackendCommunicator.getKittenById(kittenId).then(
                    function (data) {
                        $scope.selectedKitten = data;
                        $scope.returnBackUrl = initReturnBackUrl($scope.selectedKitten);

                        getKittenPictures($scope.selectedKitten.ID);
                    },
                    function (e) {
                        $scope.selectedKitten = null;
                        console.log(e);
                    });
            }
        }
    });