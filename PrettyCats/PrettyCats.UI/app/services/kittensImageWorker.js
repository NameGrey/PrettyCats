angular.module('artDuviksApp').factory("kittensImageWorker", function ($http, configuration) {
    var baseServerApiUrl = configuration.ServerApi;
    return {
        getKittenMainPicture: function (kitten) {
            return $http.get(baseServerApiUrl + "/pictures/main-picture/" + kitten.ID);
        },
        getKittenPictures: function (kittenId) {
            var baseServerApiUrl = configuration.ServerApi;

            return $http.get(baseServerApiUrl + "/pictures/" + kittenId);
        },
        changePicturesOrder: function (pictures) {
            return $http.post(baseServerApiUrl + '/pictures/changeOrder', JSON.stringify(pictures), {
                headers: { "Content-Type": 'application/json' }
            });
        },
        setMainPhotoFor: function (file, kitten) {
            var baseServerApiUrl = configuration.ServerApi;
            var data = new FormData();

            data.append("image", file);
            data.append("kittenName", kitten.Name);
            data.append("kittenId", kitten.ID);

            return $http.post(baseServerApiUrl + '/pictures/main-picture/add', data, {
                headers: { "Content-Type": undefined }
            });
        },
        addThePhoto: function (file, kitten) {
            var data = new FormData();
            data.append("image", file);
            data.append("kittenName", kitten.Name);
            data.append("kittenId", kitten.ID);

            return $http.post(baseServerApiUrl + '/pictures/add', data, {
                headers: { "Content-Type": undefined }
            });
        },
        removePhoto: function (photoId) {
            return $http.delete(baseServerApiUrl + '/pictures/' + photoId);
        },
        initializeMainPicture: function (kitten) {
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