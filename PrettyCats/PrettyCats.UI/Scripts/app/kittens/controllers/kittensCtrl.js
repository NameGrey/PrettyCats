'use strict';

angular.module('KittensModule').controller("kittensCtrl", function ($scope, $location, $http, $routeParams, $timeout, configuration, kittensImageWorker, kittenBackendCommunicator) {
    var baseServerApiUrl = configuration.ServerApi;

    var initKitten = function() {
        var kittenId = $routeParams.id;
        $scope.kitten = {};

        if (kittenId) {
            kittenBackendCommunicator.getKittenById(kittenId).then(
                function(data) {
                    $scope.kitten = data;
                },
                function(e) {
                    console.log(e);
                });
        }
    }
    initKitten();

    var showSuccessMessage = function(message)
    {
        
    }

    var showErrorMessage = function(message)
    {
        
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

    var getParents = function () {
        var baseServerApiUrl = configuration.ServerApi;

        $http.get(baseServerApiUrl + "/kittens/parents")
			.success(function (data) {
			    $scope.parents = data;
                console.log("parents:", data);
            })
			.error(function (e) {
			    $scope.parents = null;
			    console.log("error:", e);
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

    var getAllKittens = function () {

        $http.get(baseServerApiUrl + "/kittens").success(function (data) {
            $scope.kittens = data;
        }).error(function () { $scope.kittens = null; });
    }

    var saveEditedKitten = function(kitten) {
        if (kitten) {
            kittenBackendCommunicator.saveEditedKitten(kitten).then(
                function () {
                    if (kitten.IsParent) {
                        $scope.successMessage = "Родитель успешно сохранен!";
                    } else {
                        $scope.successMessage = "Котенок успешно сохранен!";
                    }

                    $timeout(function () {
                        $scope.successMessage = null;
                        if (kitten.IsParent) {
                            $location.path("/admin/parents");
                        } else {
                            $location.path("/admin/available-kittens");
                        }
                    }, 2000);
                },
                function (e) {
                    $scope.errorMessage = "Произошла ошибка на сервере!";
                    console.log(e);

                    $timeout(function () {
                        $scope.errorMessage = null;
                    }, 4000);
                });
        }
    }

    var removeKitten = function (kitten) {
        var collection;

        if (kitten.IsParent) {
            collection = $scope.parents;
        } else {
            collection = $scope.kittens;
        }
        var index = collection.indexOf(kitten);
        // TODO: fix this part - add html into template and create a message variable
        if (index > -1) {
            $http.get(baseServerApiUrl + "/kittens/remove/" + kitten.ID).success(function () {

                $("#" + kitten.Name + ".kitten-block-admin").replaceWith("<div class='alert alert-success'>Котенок был удален!</div>");

                $timeout(function () {
                    $("#" + kitten.Name + ".kitten-block-admin").remove();
                    collection.splice(index, 1);
                }, 1000);
            }).error(function () {
                $("#" + kitten.Name + ".kitten-block-admin").find(".bottom-fotos-container").append("<div class='alert alert-danger'>Ошибка при удалении!</div>");

                $timeout(function () {
                    $("#" + kitten.Name + ".kitten-block-admin .bottom-fotos-container div").remove();
                }, 1000);

            });
        }
    }

    var addNewKitten = function(kitten) {
        if (kitten) {
            kittenBackendCommunicator.addNewKitten(kitten)
                .then(function() {
                    if (kitten.IsParent) {
                        $scope.successMessage = "Родитель успешно сохранен!";
                    } else {
                        $scope.successMessage = "Котенок успешно сохранен!";
                    }

                    $timeout(function() {
                        $scope.successMessage = null;
                        if (kitten.IsParent) {
                            $location.path("/admin/parents");
                        } else {
                            $location.path("/admin/available-kittens");
                        }
                    }, 2000);
                },
                function () {
                    $scope.errorMessage = "Произошла непредвиденная ошибка на сервере. Котенок не был добавлен.";

                    $timeout(function() {
                        $scope.errorMessage = null;
                    }, 4000);
                });
        }
    }

    var getKittenPictures = function () {
        return kittensImageWorker.getKittenPictures($routeParams.id);
    }

    var getOwners = function () {
        var baseServerApiUrl = configuration.ServerApi;

        $http.get(baseServerApiUrl + "/owners")
			.success(function (data) {
			    $scope.owners = data;
			})
			.error(function () {
			    $scope.owners = null;
			});
    };

    var getBreeds = function () {
        var baseServerApiUrl = configuration.ServerApi;

        $http.get(baseServerApiUrl + "/breeds")
			.success(function (data) {
			    $scope.breeds = data;
			})
			.error(function () {
			    $scope.breeds = null;
			});
    };

    var getDisplayPlaces = function () {
        var baseServerApiUrl = configuration.ServerApi;

        $http.get(baseServerApiUrl + "/display-places")
			.success(function (data) {
			    $scope.displayPlaces = data;
			})
			.error(function () {
			    $scope.displayPlaces = null;
			});
    };

    $scope.theFile = null;
    $scope.getKittens = getKittens;
    $scope.getParents = getParents;
    $scope.selectKitten = selectKitten;
    $scope.getAllKittens = getAllKittens;
    $scope.setMainPhotoFor = kittensImageWorker.setMainPhotoFor;
    $scope.addThePhoto = kittensImageWorker.addThePhoto;
    $scope.removeKitten = removeKitten;
    $scope.addNewKitten = addNewKitten;
    $scope.saveEditedKitten = saveEditedKitten;
    $scope.getKittenPictures = getKittenPictures;

    $scope.getOwners = getOwners;
    $scope.getBreeds = getBreeds;
    $scope.getDisplayPlaces = getDisplayPlaces;
});