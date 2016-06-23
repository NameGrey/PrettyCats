function ShowAllImages() {
	$('.btn').fadeOut();

	$('.row').fadeIn("slow");
}

function PostToController(cName, cAction, obj) {
	$.post("/" + cName + "/" + cAction + "/" + obj);
}