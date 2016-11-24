'use strict';

angular.module('AdminModule').controller('adminKittensCtrl',
    function ($scope, kittenBackendCommunicator) {
        $scope.kittens = null;
        $scope.addKittenLink = null;

        var successLoadedKittens = function(data) {
            $scope.kittens = data;
        }

        var loadedWithErrorsKittens = function(e) {
            $scope.kittens = null;
            console.log(e);
        }

        $scope.initAvailableKittens = function () {
            $scope.addKittenLink = "/admin/addKitten";
            kittenBackendCommunicator.getAvailableKittens()
                .then(successLoadedKittens, loadedWithErrorsKittens);
        }

        $scope.initArchiveKittens = function () {
            $scope.addKittenLink = "/admin/addArchiveKitten";
            kittenBackendCommunicator.getArchiveKittens()
                .then(successLoadedKittens, loadedWithErrorsKittens);
        }

        $scope.initParents = function () {
            $scope.addKittenLink = "/admin/addParent";
            kittenBackendCommunicator.getParents()
                .then(successLoadedKittens, loadedWithErrorsKittens);
        }
    });