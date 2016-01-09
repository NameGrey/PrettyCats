
var files = [];

function iterateFiles(filesArray, kittenName) {
	var count = 0;

	for (var i = 0; i < filesArray.length; i++) {
		for (var j = 0; j < filesArray[i].length; j++) {
			var request = new XMLHttpRequest();
			var formData = new FormData();

			formData.append('file', filesArray[i][j]);
			formData.append('kittenName', kittenName);

			request.open('post', '/Admin/AddImage');
			request.upload.onprogress = function (evt) {
				if (evt.lengthComputable) {
					var percentComplete = Math.ceil((evt.loaded / evt.total) * 100);

					if (percentComplete == 100) {
						++count;
					}
					$('#left-files-' + kittenName).text(percentComplete + "% завершено");
				}
			};

			request.onreadystatechange = function () {
				if (request.readyState == 4) {
					if (request.status == 200) {
						$('#left-files-' + kittenName).text(filesArray[0].length + " файлов загрузилось!!!");
					}
				}
			}
			request.send(formData);
		}
	}
}