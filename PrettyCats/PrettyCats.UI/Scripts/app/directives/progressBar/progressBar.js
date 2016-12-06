angular.module("artDuviksApp").directive("progressBar", function () {
    return {
        restrict: "E",        
        scope:{
            totalCount: "=",
            currentSuccess: "=",
            currentErrors: "=",
            displayFlag:"="
        },
        templateUrl:"Scripts/app/directives/progressBar/progressBar.html",
        link: function (scope, el, attrs) {
            scope.width = function () {
                return currentStateNumber / currentStateNumber;
            }
        }
    }
});