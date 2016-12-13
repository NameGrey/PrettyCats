angular.module("artDuviksApp").directive("progressBar", function () {
    return {
        restrict: "E",        
        scope:{
            totalCount: "=",
            currentSuccess: "=",
            currentErrors: "=",
            displayFlag:"="
        },
        templateUrl:"app/directives/progressBar/progressBar.html"
    }
});