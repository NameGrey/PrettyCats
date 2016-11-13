'use strict';

angular.module('AdminModule').directive("adminPageKitten", ['kittensImageWorker','$location', function (kittensImageWorker, $location) {
    return {
        restrict: "E",
        templateUrl: "Scripts/app/admin/partials/adminPageKitten.html",
        replace: false,
        scope: {
            kitten: "=",
            removeKitten: "&"
        },
        link: function ($scope) {
            $scope.editKitten = function (kitten) {
                var url = "/admin/editKitten/" + kitten.ID;
                $location.url(url);
            }

            kittensImageWorker.initializeMainPicture($scope.kitten);
        }
    }
}]);