﻿define([
	'angular',
	'app/common/common.module',
	'app/common/services/configurationService'
], function kittensPathBuilderModule() {
	'use strict';

	kittensPathBuilder.$inject = ['configurationService'];

	angular.module('common.module')
		.service("kittensPathBuilder", kittensPathBuilder);

	function kittensPathBuilder(configurationService) {
		return {
			buildKittenLink: function (kitten) {
				var path = "";

				if (kitten.IsParent) {
					path = "/parent-kitten-page/" + kitten.ID;
				} else {
					path = "/kitten-page/" + kitten.ID;
				}

				return path;
			},
			editKitten: "/editKitten/",
			kittenModifyPictures: "/kitten/modify-pictures/",
			parents: "/parents",
			archiveKittens: "/archive-kittens",
			availableKittens: "/available-kittens",
			addKitten: "/addKitten",
			addArchiveKitten: "/addArchiveKitten",
			addParent: "/addParent",
			siteIndex: configurationService.ServerHost
		}
	}
});


