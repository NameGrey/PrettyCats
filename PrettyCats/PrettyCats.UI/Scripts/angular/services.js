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
		getKittenMainPicture: function(kitten) {
			var baseServerApiUrl = configuration.ServerApi;

			return $http.get(baseServerApiUrl + "/pictures/main-picture/" + kitten.ID);
		},
		getKittenPictures: function (kittenId) {
			var baseServerApiUrl = configuration.ServerApi;

			return $http.get(baseServerApiUrl + "/pictures/" + kittenId);
		},
		setMainPhotoFor: function(file, kitten) {
			var baseServerApiUrl = configuration.ServerApi;
			var data = new FormData();

			data.append("image", file);
			data.append("kittenName", kitten.Name);
			data.append("kittenId", kitten.ID);

			return $http.post(baseServerApiUrl + '/pictures/main-picture/add', data, {
				headers: { "Content-Type": undefined }
			});
		},

		addThePhoto: function(files, kitten) {
			var baseServerApiUrl = configuration.ServerApi;
			var t = $.param({ f: value, kittenName: kitten.Name });

			angular.forEach(files, function(value, key) {
				$http({
					method: 'POST',
					url: baseServerApiUrl + "/pictures/add",
					data: 'df',
					headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
				});
			});
		},
		initializeMainPicture: function(kitten) {
			var baseServerApiUrl = configuration.ServerApi;

			$http.get(baseServerApiUrl + "/pictures/main-picture/" + kitten.ID)
				.success(function (data) {

					if (!data)
						kitten.MainPicture = { Image: "empty" };
					else {
						kitten.MainPicture = data;
						// We are adding milliseconds parameter to avoid caching of the picture.
						// It's not the best way. TODO:Change it later
						kitten.MainPicture.Image += "?" + new Date().getMilliseconds();
					}
				});
		}
	}
});
