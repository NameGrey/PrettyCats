'use strict';

artDuviksApp.factory("kittensPathBuilder", function () {
	return {
		buildKittenLink: function (kitten) {
			var path = "";

			if (kitten.IsParent) {
				path = "/parent-kitten-page/" + kitten.ID;
			} else {
				path = "/kitten-page/" + kitten.ID;
			}

			return path;
		}
	}
});

artDuviksApp.factory("kittensImageWorker", function($http, configuration) {
	return {
		getKittenMainPageUrl: function (kitten) {
			var baseServerApiUrl = configuration.ServerApi;

			return $http.get(baseServerApiUrl + "kittens/kitten-main-image/" + kitten.ID).success(function (data) { return data; });
		}
	}
});