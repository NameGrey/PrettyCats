'use strict';

angular.module('KittensModule').controller("kittensCtrl", function ($scope, $location, $http, $routeParams, $timeout, configuration, kittensImageWorker, kittenBackendCommunicator) {
    var baseServerApiUrl = configuration.ServerApi;
    $scope.isValidFields = false;

    var initController = function () {
        var page = $location.path().split("/")[2] ;

        if (page === "addParent") {
            $scope.kitten = { IsParent: true };
        } else if (page === "addKitten") {
            $scope.kitten = { IsParent: false };
            getParents();
        } else if (page === "addArchiveKitten") {
            $scope.kitten = { IsInArchive: true };
            getParents();
        } else if (page === "editKitten") {
            initKitten();
            getParents();
        } else if (page === "available-kittens") {
            getAvailableKittens();
            $scope.addKittenLink = "/admin/addKitten";
        } else if (page === "archive-kittens") {
            $scope.addKittenLink = "/admin/addArchiveKitten";
            $scope.kitten = { IsInArchive: true };
            getArchiveKittens();
        } else if (page === "parents") {
            $scope.addKittenLink = "/admin/addParent";
            getParents();
        }
        
        $scope.getOwners();
        $scope.getBreeds();
        $scope.getDisplayPlaces();
    }

    var initKitten = function() {
        var kittenId = $routeParams.id;
        $scope.kitten = {};

        if (kittenId) {
            kittenBackendCommunicator.getKittenById(kittenId).then(
                function(data) {
                    $scope.kitten = data;
                    // TODO: get only main Picture
                    getKittenPictures(data.ID).success(function(pics) {
                        $scope.kitten.pictures = pics;
                    });
                },
                function(e) {
                    console.log(e);
                });
        }
    }

    var getKittens = function () {
        var breedNameFromPath = "/" + $location.path().split(/[\s/]+/).pop();

        $http.get(baseServerApiUrl + "/kittens/kittensByPath" + breedNameFromPath)
			.success(function (data) {
			    $scope.kittens = data;
			})
			.error(function () {
			    $scope.kittens = null;
			});
    };

    var selectKitten = function() {
        var kittenId = $routeParams.id;

        if (kittenId) {
            kittenBackendCommunicator.getKittenById(kittenId).then(
                function(data) {
                    scope.selectedKitten = data;
                },
                function (e) {
                    scope.selectedKitten = null;
                    console.log(e);
                });
        }

    };

    var getAvailableKittens = function () {

        $http.get(baseServerApiUrl + "/kittens").success(function (data) {
            $scope.kittens = data;
        }).error(function () { $scope.kittens = null; });
    }

    var getArchiveKittens = function () {

        $http.get(baseServerApiUrl + "/kittens/archive").success(function (data) {
            $scope.kittens = data;
        }).error(function () { $scope.kittens = null; });
    }

    var removeKitten = function (kitten) {
        var index = kittens.indexOf(kitten);
        // TODO: fix this part - add html into template and create a message variable
        if (index > -1) {
            $http.get(baseServerApiUrl + "/kittens/remove/" + kitten.ID).success(function () {

                $("#" + kitten.Name + ".kitten-block-admin").replaceWith("<div class='alert alert-success'>Котенок был удален!</div>");

                $timeout(function () {
                    $("#" + kitten.Name + ".kitten-block-admin").remove();
                    kittens.splice(index, 1);
                }, 1000);
            }).error(function () {
                $("#" + kitten.Name + ".kitten-block-admin").find(".bottom-fotos-container").append("<div class='alert alert-danger'>Ошибка при удалении!</div>");

                $timeout(function () {
                    $("#" + kitten.Name + ".kitten-block-admin .bottom-fotos-container div").remove();
                }, 1000);

            });
        }
    }

    var getKittenPictures = function (id) {
        return kittensImageWorker.getKittenPictures(id);
    }

    $scope.initController = initController;
    $scope.theFile = null;
    $scope.getKittens = getKittens;
    $scope.selectKitten = selectKitten;
    $scope.setMainPhotoFor = kittensImageWorker.setMainPhotoFor;
    $scope.addThePhoto = kittensImageWorker.addThePhoto;
    $scope.removeKitten = removeKitten;
    $scope.getKittenPictures = getKittenPictures;
});