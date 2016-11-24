﻿'use strict';

angular.module('KittensModule').service("kittenBackendCommunicator", function ($http, $q, configuration, kittensImageWorker) {
    var baseServerApiUrl = configuration.ServerApi;
    var getKittenById = function(kittenId) {
        return $q(function(resolve, reject) {
            $http.get(baseServerApiUrl + "/kittens/" + kittenId)
                .success(function(data) {
                    var kitten = data;

                    kittensImageWorker.getKittenMainPicture(kitten).success(function(data) {
                        kitten.MainPicture = data;
                    });

                    kittensImageWorker.getKittenPictures(kitten.ID).success(function(data) {
                        kitten.Pictures = data;
                    });
                    resolve(kitten);
                })
                .error(function(e) {
                    reject(e);
                });
        });
    }

    var commonGetKittensFunc = function (queryUrl) {
        return $q(function (resolve, reject) {
            $http.get(baseServerApiUrl + queryUrl)
                .success(function (data) {
                    resolve(data);
                })
                .error(function (e) {
                    reject(e);
                });
        });
    }

    var getParents = function () {
        return commonGetKittensFunc("/kittens/parents");
    }

    var getAvailableKittens = function () {
        return commonGetKittensFunc("/kittens");
    }

    var getArchiveKittens = function () {
        return commonGetKittensFunc("/kittens/archive");
    }

    var removeKitten = function(id) {
        return $q(function(resolve, reject) {
            $http.get(baseServerApiUrl + "/kittens/remove/" + id).success(function () {
                resolve();
            }).error(function (e) {
                reject(e);
            });
        });
    }

    var addNewKitten = function(kitten) {
        return $q(function(resolve, reject) {
                var data = new FormData();
                var json = JSON.stringify(kitten, null, 2);
                data.append("newKitten", json);

                $http.post(baseServerApiUrl + "/kittens/add", data, { headers: { "Content-Type": undefined } })
                    .success(function() { resolve() })
                    .error(function(e) { reject(e) });
            }
        )
    }

    var saveEditedKitten = function(kitten) {
        return $q(function(resolve, reject) {
            var data = new FormData();
            var json = JSON.stringify(kitten, null, 2);
            data.append("editedKitten", json);

            $http.post(baseServerApiUrl + "/kittens/edit", data, { headers: { "Content-Type": undefined } })
                .success(function() { resolve(); })
                .error(function(e) {
                    reject(e);
                });
        });
    }

    return {
        getKittenById: getKittenById,
        getParents: getParents,
        addNewKitten: addNewKitten,
        saveEditedKitten: saveEditedKitten,
        getAvailableKittens: getAvailableKittens,
        getArchiveKittens: getArchiveKittens,
        removeKitten: removeKitten
}
});