define([
	'app/common/common.module'
], function kittenFieldsModule() {
	"use strict";

	angular.module("common.module")
		.component("kittenFieldsComponent",
		{
			templateUrl: "app/common/components/kittenFieldsComponent/kittenFieldsComponent.html",
			bindings: {
				kitten: "=",
				breeds: "<",
				owners: "<",
				parents: "<",
				displayPlaces: "<",
				isValid: "="
			},
			controller: kittenFieldsController
		});

	function kittenFieldsController() {
		var
			self = this,
			regexp = new RegExp("[0-9а-яА-Я]");

		self.$onInit = onInit;
		self.$onChanges = onChanges;
		self.changeValidationFlag = changeValidationFlag;

		function onChanges() {
			self.changeValidationFlag();
		}

		function onInit() {
			self.kitten.isParent = false;
			self.changeValidationFlag();
		}

		function changeValidationFlag() {
			self.onlyEnglish = !self.kitten || !self.kitten.Name || self.kitten.Name.length > 0 && !regexp.test(self.kitten.Name);
			self.isCorrectOwner = self.kitten.OwnerID > 0;
			self.isCorrectBreed = self.kitten.BreedID > 0;
			self.isCorrectWhereDisplay = self.kitten.WhereDisplay > 0;

			self.isValid =
				self.kitten.Name && self.kitten.Name.length > 0 &&
				self.isCorrectOwner &&
				self.isCorrectBreed &&
				self.isCorrectWhereDisplay &&
				self.onlyEnglish;
		}
	}
});