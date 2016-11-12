﻿'use strict';

angular.module('AdminModule').directive("adminPageKitten", ['kittensImageWorker', function (kittensImageWorker) {
    return {
        restrict: "E",
        templateUrl: "Scripts/app/admin/partials/adminPageKitten.html",
        replace: false,
        scope: {
            kitten: "=",
            removeKitten: "&"
        },
        link: function ($scope) {
            kittensImageWorker.initializeMainPicture($scope.kitten);
        }
    }
}]);