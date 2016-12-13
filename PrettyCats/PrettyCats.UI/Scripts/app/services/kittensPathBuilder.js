﻿'use strict';

angular.module('artDuviksApp').factory("kittensPathBuilder", function (configuration) {
    return {
        buildKittenLink: function (kitten) {
            var path = "";

            if (kitten.IsParent) {
                path = "/parent-kitten-page/" + kitten.ID;
            } else {
                path = "/kitten-page/" + kitten.ID;
            }

            return path;
        },
        editKitten: "/editKitten/",
        kittenModifyPictures: "/kitten/modify-pictures/",
        parents: "/parents",
        archiveKittens: "/archive-kittens",
        availableKittens: "/available-kittens",
        addKitten: "/addKitten",
        addArchiveKitten: "/addArchiveKitten",
        addParent: "/addParent",
        siteIndex: configuration.ServerHost
    }
});