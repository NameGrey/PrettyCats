(function () {
    'use strict';
    // Module name is handy for logging
    var id = 'app';
    // Create the module and define its dependencies.
    var app = angular.module('app', []);

    app.config(['$httpProvider', function ($httpProvider) {
        $httpProvider.defaults.useXDomain = true;
        delete $httpProvider.defaults.headers.common['X-Requested-With'];
    }
    ]);

    app.run(['$q', '$rootScope',
        function ($q, $rootScope) {
        }]);
})();