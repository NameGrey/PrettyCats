(function() {
    var app = angular.module('TestApp', ['datePickerApp']);
    app.controller('testController', function (datePickerUtils, $scope) {
        $scope.start = moment().format('YYYY-MM-DD');
        $scope.end = moment().add(14, 'days').format('YYYY-MM-DD');
        $scope.min = moment().format('YYYY-MM-DD');
        $scope.max = moment().add(14, 'days').format('YYYY-MM-DD');

        $scope.changeDate = function (modelName, newDate) {
            console.log(modelName + ' has had a date change. New value is ' + newDate.format());
            $scope.callbackState = 'Callback: Fired';
        }
    });
}());
