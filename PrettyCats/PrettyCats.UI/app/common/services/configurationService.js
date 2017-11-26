define([
	'angular',
	'app/common/common.module'
], function configurationServiceModule() {
	'use strict';

	angular.module('common.module')
		.service("configurationService", function () {
			var serverHost = "http://localhost:53820";
			return {
				ServerHost: serverHost,
				ServerApi: serverHost + "/api"
			};
		});
});



