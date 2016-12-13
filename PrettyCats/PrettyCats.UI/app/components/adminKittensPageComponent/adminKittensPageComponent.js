"use strict";

angular.module("artDuviksApp").component("adminKittensPageComponent",
{
   bindings: {
       kittens: "<",
       addKittenLink: "<"
   },
   templateUrl: "app/components/adminKittensPageComponent/adminKittensPageComponent.html",
   controller: function (kittenBackendCommunicator, $timeout) {
       var ctrl = this;
       var messageInterval = 1000;

       function onShowKittenRemovedMessage(kitten) {
           if (kitten.showKittenRemovedMessage != null) {
               kitten.showKittenRemovedMessage();
           };
       }

       function onHideKittenRemovedMessage(kitten) {
           if (kitten.hideKittenRemovedMessage != null) {
               kitten.hideKittenRemovedMessage();
           }
       }

       this.kittenHasBeenRemoved = false;
       this.hideKittenRemovedMessage = true;

        this.removeKitten = function(kitten) {
           kittenBackendCommunicator.removeKitten(kitten.ID).then(
               function success() {
                   onShowKittenRemovedMessage(kitten);

                   $timeout(function() {
                       onHideKittenRemovedMessage(kitten);
                   }, messageInterval);
               },
               function error() {
                   onShowKittenRemovedMessage(kitten);

                   $timeout(function() {
                       onHideKittenRemovedMessage(kitten);
                   }, messageInterval);

               });
       }
   }
});