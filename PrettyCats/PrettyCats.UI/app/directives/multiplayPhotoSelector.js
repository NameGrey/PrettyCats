'use strict';

angular.module('artDuviksApp').directive('multiplayPhotosSelector',
	function (kittensImageWorker, $q) {        
	    return {
	        restrict: 'A',
            scope: {
                kitten: "=",
                numberOfFiles: "=",
                sendFilesCount: "=",
                filesWithErrors: "=",
                onLoadingFinished: "&"
            },
            link: function (scope, el, attrs) {
                var loadingFinishedEvent = function () {
                    if (scope.numberOfFiles > 0 && scope.numberOfFiles === scope.filesWithErrors + scope.sendFilesCount) {
                       scope.onLoadingFinished();
                    }
                }

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
                                    loadingFinishedEvent();
                                },
                                function error(e) {
                                    scope.filesWithErrors = scope.filesWithErrors + 1;
                                    loadingFinishedEvent();
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