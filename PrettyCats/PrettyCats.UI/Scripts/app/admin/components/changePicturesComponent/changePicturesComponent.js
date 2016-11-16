angular.module("AdminModule").component("changePicturesComponent", {
    bindings: {
        pictures:"="
    },
    templateUrl: "Scripts/app/admin/components/changePicturesComponent/changePicturesComponent.html",
    controller: function () {
        var p = this.pictures;
    }
});