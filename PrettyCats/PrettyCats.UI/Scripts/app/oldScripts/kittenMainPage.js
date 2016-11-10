function ChooseKitten(ownerPhone, ownerName, kittenRussianName) {
	var elements = $("div[data-u='slides']")[1].children;

	for (var i = 1; i < elements.length - 1; i++) {

		if ($(elements[i]).css('left') == '0px') {
			var imgSrc = $(elements[i]).find("img").prop('src');
			var text = "<br/><br/><span class='big-number'> <b>Вы выбрали котенка '" + kittenRussianName + "'</b></span><br/> &nbsp;&nbsp;Для покупки необходимо <b>позвонить по номеру</b> ниже и сообщить имя котенка. &nbsp;&nbsp;Если у Вас остались какие-то <b>вопросы</b>, мы всегда готовы ответить на них!";
			var $message = "<div class='dialog-message'><img class='main-slide-image' src='" + imgSrc + "'/><p class='message-text'>" + text + "</p> <span class='big-number'><b>" + ownerPhone + " " + ownerName + " </b><br /> Будем рады вашему звонку!</span></div>";

			BootstrapDialog.show({
				title: "Котенок " + kittenRussianName,
				message: $message,
				buttons: [
					{
						label: 'Назад',
						cssClass: 'btn-primary',
						action: function (dialogItself) {
							dialogItself.close();
						}
					}
				]
			});
		}
	}
}

function DisplayVideo(kittenRussianName, videoUrl) {
	var text = '<iframe width="100%" height="500px" src="' + videoUrl + '" frameborder="0" allowfullscreen></iframe>';

	var $message = "<div class='dialog-message><div class='video-container'>" + text + "</div></div>";

	BootstrapDialog.show({
		size: BootstrapDialog.SIZE_WIDE,
		title: "Котенок " + kittenRussianName,
		message: $message,
		buttons: [
			{
				label: 'Назад',
				cssClass: 'btn-primary',
				action: function (dialogItself) {
					dialogItself.close();
				}
			}
		]
	});
}
