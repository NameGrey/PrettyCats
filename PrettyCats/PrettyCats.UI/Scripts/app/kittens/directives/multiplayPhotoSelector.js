'use strict';

angular.module('KittensModule').directive('multiplayPhotosSelector',
	function (kittensImageWorker) {
	    return {
	        restrict: 'A',
            scope: {
                kitten:"="
            },
	        link: function (scope, el, attrs) {
	            el.append("<input type='file' multiple class='hidden' accept='image/jpeg'>")
					.bind("change", function (evt) {
					    angular.forEach(evt.target.files, function (value, key) {
					        kittensImageWorker.addThePhoto(value, scope.kitten);
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