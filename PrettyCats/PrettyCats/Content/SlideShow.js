﻿$(document).ready(function() {
	var slidesCount = 10;
	var topSliderLocker = undefined;

	//-- Top slideshow characteristics
	var slideStep = 230;
	var slidesMovingDelay = 200;
	var durationMovingOfSlides = 1000;
	var autoSlidingTopSlideShow = 5000;
	// --------------------------------

	$(window).resize(function() {
		var width = $(window).width();

		if (width < 1462)
			setFourSlides();
		else if (width < 1682)
			setFiveSlides();
		else if (width < 1902)
			setSixSlides();
		else if (width < 2122)
			setSevenSlides();
		else if (width < 2342)
			setEightSlides();
		else if (width < 2400)
			setNineSlides();
	});

	// set timeout
	var tid;
	startTimer();

	$(".right-row").bind("click", function () {
		if (topSliderLocker != 'locked') {
			abortTimer();
			moveSlides('right');
			startTimer();
		}

	});

	$(".left-row").bind("click", function () {
		if (topSliderLocker != 'locked') {
			abortTimer();
			moveSlides('left');
			startTimer();
		}
	});
	
	function timerCode() {
		// do some stuff...
		moveSlides("right");
		tid = setTimeout(timerCode, autoSlidingTopSlideShow); // repeat myself
	}
	function abortTimer() { // to be called when you want to stop the timer
		clearTimeout(tid);
	}

	function startTimer() {
		tid = setTimeout(timerCode, autoSlidingTopSlideShow);
	}

	function moveSlides(state) {
		topSliderLocker = 'locked';
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
				$(slide).delay((slidesCount - i) * slidesMovingDelay).animate({ 'left': '+=' + (+slideStep) }, durationMovingOfSlides,
					function() {
						if (i == 0)
							topSliderLocker = undefined;
					});
		});

		var newNumber;
		
		if (+slideZeroNumber + 1 == +slidesCount) {

			if (slidesLength == slidesCount)
				newNumber = slideZeroNumber;
			else
				newNumber = 0;
		} else
			newNumber = +slideZeroNumber + 1;
		
		$(slides[0]).before('<div class="slide-box" id="slide' + newNumber + '" style=" left: 60px;"></div>');
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

		if (+slideZeroNumber + 1 == +slidesCount)
				newNumber = 0;
		else
			newNumber = +slideZeroNumber + 1;

		setTimeout(function() {
				$(slides[slidesLength])
					.after('<div class="slide-box" id="slide' + newNumber + '" style="opacity:0; left:' + prevLastSlidePosition + 'px"></div>');

				$('#slide' + newNumber).animate({ 'opacity': 1 }, 1000,
			function () {
				topSliderLocker = undefined;
			});
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
		var result = path.substring(path.length - 6, path.length - 5);
		return result;
	}

	function setNumberForSlide(slide, slideNumber, path) {
		$(slide).css({ 'background-image': replaceAt(path, path.length - 7, slideNumber) });
	}

	function replaceAt(sentence, index, newString) {
		return sentence.substring(0, index) + newString + sentence.substring(index + 1);
	}

	function setFourSlides() {
		//alert(4);
		// 1. Получаем количество слайдов.
		// 2. Если их кол-во меньше чем 4, то добавляем разницу слайдов (берем из констант) (какие номера слайдов брать нужно смотреть по правилу выше)
		// 3. Если больше, то удаляем разницу.
	}

	function setFiveSlides() {
		
	}

	function setSixSlides() {
		alert(6);
	}

	function setSevenSlides() {
		
	}

	function setEightSlides() {
		
	}

	function setNineSlides() {
		
	}
});

