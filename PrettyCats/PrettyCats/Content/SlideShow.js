$(document).ready(function () {
	var slidesCount = 7;
	alert(window.width);

	$(".right-row").bind("click", function () {

		MoveAllSlidesToRight();
	});

	function MoveAllSlidesToRight() {
		var slidesCount = GetSlidesCount();
		var slds = $('.slides').find('.slide-box');
		alert(slds.length);

		for (var i = 1; i <= slidesCount; i++) {

			var slidePath = $("#slide" + i).css("background-image");
			var slideNumber = getSlideNumberFromPath(slidePath);

			if (1 == slideNumber) {
				setNumberForSlide("#slide" + i, slidesCount, slidePath);
			} else {
				setNumberForSlide("#slide" + i, slideNumber - 1, slidePath);
			}
		}
	}


	// This function defines how many slides are in the slideshow.
	function GetSlidesCount() {
		return slidesCount;
	}

	///Max number of slides should be less than 10.
	function getSlideNumberFromPath(path) {
		var result = path.substring(path.length - 7, path.length - 6);
		return result;
	}

	function setNumberForSlide(slide, slideNumber, path) {
		$(slide).css({ 'background-image': replaceAt(path, path.length - 7, slideNumber) });
	}

	function replaceAt(sentence, index, newString) {
		return sentence.substring(0, index) + newString + sentence.substring(index + 1);
	}
});

