'use strict';

angular.module('AdminModule').component('adminKittensPageComponent',
{
   bindings: {
       kittens: "<",
       addKittenLink: "<"
   },
   templateUrl: "Scripts/app/admin/components/adminKittensPageComponent/adminKittensPageComponent.html",
   controller: function (kittenBackendCommunicator) {
        this.removeKitten = function(kitten) {
            var index = kittens.indexOf(kitten);
            if (index > -1) {
                kittenBackendCommunicator.removeKitten(kitten.ID).then(
                    function success() {
                        //TODO: use angular approach for this part of code
                        $("#" + kitten.Name + ".kitten-block-admin").replaceWith("<div class='alert alert-success'>Котенок был удален!</div>");

                        $timeout(function() {
                            $("#" + kitten.Name + ".kitten-block-admin").remove();
                            kittens.splice(index, 1);
                        }, 1000);
                    },
                    function error() {
                        $("#" + kitten.Name + ".kitten-block-admin").find(".bottom-fotos-container").append("<div class='alert alert-danger'>Ошибка при удалении!</div>");

                        $timeout(function() {
                            $("#" + kitten.Name + ".kitten-block-admin .bottom-fotos-container div").remove();
                        }, 1000);

                    });
            }
        }
    }
});