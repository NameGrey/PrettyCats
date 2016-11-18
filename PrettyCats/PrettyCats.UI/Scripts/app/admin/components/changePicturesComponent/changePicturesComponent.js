angular.module("AdminModule").component("changePicturesComponent", {
    bindings: {
        pictures:"="
    },
    templateUrl: "Scripts/app/admin/components/changePicturesComponent/changePicturesComponent.html",
    controller: function (kittensImageWorker, $scope) {
        this.removePicture = function(picture) {
            kittensImageWorker.removePhoto(picture.ID).then(
                function success() {
                    var pics = $scope.$ctrl.pictures;
                    var index = pics.indexOf(picture);
                    pics.splice(index, 1);
                },
                function error(e) {
                    console.log(e);
                });
        }
    }
});