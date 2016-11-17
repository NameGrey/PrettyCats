'use strict';

angular.module('KittensModule').directive('multiplayPhotosSelector',
	function (kittensImageWorker, $q) {        
	    return {
	        restrict: 'A',
            scope: {
                kitten: "=",
                numberOfFiles: "=",
                sendFilesCount: "=",
                filesWithErrors: "="
            },
	        link: function (scope, el, attrs) {
	            el.append("<input type='file' multiple class='hidden' accept='image/jpeg'>")
					.bind("change", function (evt) {
					    scope.sendFilesCount = 0;
					    if (evt.target.files != null) {
					        scope.numberOfFiles = evt.target.files.length;
					    } else {
					        scope.numberOfFiles = null;
					    }

					    angular.forEach(evt.target.files, function (value, key) {
					        kittensImageWorker.addThePhoto(value, scope.kitten).then(
                                function success() {
                                    scope.sendFilesCount = scope.sendFilesCount + 1;
                                },
                                function error(e) {
                                    scope.filesWithErrors = scope.filesWithErrors + 1;
                                });
					    });
					    $(evt.target).trigger("reset");
					});

	            el.bind("click", function (event) {
	                $(event.target).next().trigger("click"); // the next should be input because we have added it before in this function
	            });
	        }
	    }
	}
);