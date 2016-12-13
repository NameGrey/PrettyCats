function successRequest(itemId) {
	$('#' + itemId).hide();
}

function successChangeOrderRequest(itemId, newOrder) {
	$('input').find('#' + itemId).val(newOrder);
}

function ChangeOrder() {

	var mas = [];
	$('.kitten-block-admin').each(
		function (i, entry) {
			var id = $(entry).attr('id');
			var order = $(entry).find('#' + id).val();

			mas.push({ ID: id, Order: order });
		});

	//json += '}';
	json = JSON.stringify(mas, null, 2);
	//var json = $.toJSON(mas);

	$.ajax({
		url: '/Admin/ChangePicturesOrder',
		type: 'POST',
		datetype: 'json',
		data: json,
		contentType: 'application/json;',
		success: function () {
			$(':input').val('');
			location.reload();
		}
	});
}