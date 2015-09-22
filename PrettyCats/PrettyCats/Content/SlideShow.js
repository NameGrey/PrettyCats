$(document).ready(function () {
	var slidesCount = 10;

	//-- Top slideshow characteristics
	var slideStep = 230;
	var slidesMovingDelay = 200;
	var durationMovingOfSlides = 1000;
	var autoSlidingTopSlideShow = 5000;
	// --------------------------------

	// set timeout
	var tid = setTimeout(timerCode, autoSlidingTopSlideShow);

	$(".right-row").bind("click", function () {
		moveSlides('right');
	});

	$(".left-row").bind("click", function () {
		
		moveSlides('left');
	});
	
	function timerCode() {
		// do some stuff...
		moveSlides("right");
		tid = setTimeout(timerCode, autoSlidingTopSlideShow); // repeat myself
	}
	function abortTimer() { // to be called when you want to stop the timer
		clearTimeout(tid);
	}

	function moveSlides(state) {
		var slides = $('.slides').find('.slide-box');

		//-- Remove old hidden slide from previous iteration (if it is exist)
		removeOldHiddenSlides(slides, 0); // remove from the beginning
		removeOldHiddenSlides(slides, slides.length - 1); // remove from the end

		if (slides.length > 0) {

			if (state == 'right') {
				moveAllSlidesToRight(slides);
			} else {
				moveAllSlidesToLeft(slides);
			}
		}
	}

	function moveAllSlidesToRight(slides) {
		var slidesLength = slides.length - 1;
		var slidePath = $(slides[slidesLength]).css("background-image");
		var slideZeroNumber = getSlideNumberFromPath(slidePath);

		$(slides[slidesLength]).animate({ 'opacity': 0, queue: false }, durationMovingOfSlides);

		$.each(slides, function (i, slide) {
			if (i != slidesLength)
				$(slide).delay((slidesCount - i)*slidesMovingDelay).animate({ 'left': '+=' + (+slideStep) }, durationMovingOfSlides);
		});

		var newNumber;
		if (slideZeroNumber == slidesCount) {

			if (slidesLength == slidesCount)
				newNumber = slideZeroNumber;
			else
				newNumber = 0;
		} else
			newNumber = +slideZeroNumber + 1;

		$(slides[0]).before('<div class="slide-box" id="slide' + newNumber + '" style="left:60px"></div>');
	}

	function moveAllSlidesToLeft(slides) {
		var slidesLength = slides.length - 1 ;
		var slidePath = $(slides[slidesLength]).css("background-image");
		var slideZeroNumber = getSlideNumberFromPath(slidePath);
		var prevLastSlidePosition = parseInt($(slides[slidesLength]).css('left'),10);
		
		$.each(slides, function (i, slide) {
			if (i != 0)
				$(slide).delay( i * slidesMovingDelay).animate({ 'left': '-=' + (+slideStep) }, durationMovingOfSlides);
		});

		$(slides[0]).animate({ 'opacity': 0 }, durationMovingOfSlides);
		
		var newNumber;
		if (slideZeroNumber == slidesCount)
				newNumber = 0;
		else
			newNumber = +slideZeroNumber + 1;

		setTimeout(function() {
			$(slides[slidesLength])
				.after('<div class="slide-box" id="slide' + newNumber + '" style="opacity:0; left:' + prevLastSlidePosition + 'px"></div>');
			
			$('#slide' + newNumber).animate({ 'opacity': 1 }, 1000);
		}, durationMovingOfSlides + 1300);
	}

	// -- Remove hidden slides from previous iteration
	function removeOldHiddenSlides(slides, index) {
		if ($(slides[index]).css('opacity') == 0) {
			$(slides[index]).remove();
			slides.splice(index, 1);
		}
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

