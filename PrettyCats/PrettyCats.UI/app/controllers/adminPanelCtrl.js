'use strict';

angular.module('artDuviksApp').controller('adminPanelCtrl',
    function($scope, kittensPathBuilder) {
        $scope.availableKittensLink = kittensPathBuilder.availableKittens;
        $scope.archiveKittensLink = kittensPathBuilder.archiveKittens;
        $scope.parentsLink = kittensPathBuilder.parents;
        $scope.siteIndex = kittensPathBuilder.siteIndex;
    }
);