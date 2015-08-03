angular.module('bookItApp')

.filter('categoryFilter', function () {
	return function (items, subject) {
		var filtered = [];
		var undefined;
		if (items != undefined)
			for (var i = 0; i < items.length; i++) {
				var item = items[i];
				if ((subject == undefined) || (subject.CategoryId == undefined && (subject.NameFilter == undefined || subject.NameFilter.length == 0)) ||//all objects
					(subject.CategoryId == undefined && item.Name.toLowerCase().indexOf(subject.NameFilter.toLowerCase()) == 0) ||//filter by name only
					(item.Category == subject.CategoryId && (subject.NameFilter == undefined || subject.NameFilter.length == 0 || item.Name.toLowerCase().indexOf(subject.NameFilter.toLowerCase()) == 0))) {
					filtered.push(item);
				}
			}
		return filtered;
	};
});
