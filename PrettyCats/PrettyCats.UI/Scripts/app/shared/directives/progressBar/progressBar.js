angular.module("SharedModule").directive("progressBar", function () {
    return {
        restrict: "E",        
        scope:{
            totalCount: "=",
            currentSuccess: "=",
            currentErrors: "=",
            displayFlag:"="
        },
        templateUrl:"Scripts/app/shared/directives/progressBar/progressBar.html",
        link: function (scope, el, attrs) {
            scope.width = function () {
                return currentStateNumber / currentStateNumber;
            }
        }
    }
});