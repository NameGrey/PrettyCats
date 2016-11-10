'use strict';

artDuviksApp.directive("adminPageKitten", ['kittensImageWorker', function (kittensImageWorker) {
    return {
        restrict: "E",
        templateUrl: "pages/templates/admin/adminPageKitten.html",
        replace: true,
        link: function ($scope) {
            kittensImageWorker.initializeMainPicture($scope.kitten);
        }
    }
}]);