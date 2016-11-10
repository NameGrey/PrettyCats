'use strict';

angular.module('KittensModule').factory("kittensPathBuilder", function () {
    return {
        buildKittenLink: function (kitten) {
            var path = "";

            if (kitten.IsParent) {
                path = "/parent-kitten-page/" + kitten.ID;
            } else {
                path = "/kitten-page/" + kitten.ID;
            }

            return path;
        }
    }
});