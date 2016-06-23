'use strict';

var artDuviksControllers = angular.module("artDuviksControllers", []);

artDuviksControllers.controller("KittensController", function($scope) {
	$scope.message = "Hello from kittens controller";
});

artDuviksControllers.controller("MainController", function ($scope) {
	/*TODO: set service vars and use it on the page. */
	/*$scope.Title = "Питомник Art Duviks Cat - купить котенка в Рязани пород Мейн Кун, Бенгальской, Британской и Шотландской пород";
	$scope.Description = "В питомнике Art Duviks Cat вы можете купить котят Мейн-кун, шотландцев и бенгалов.";
	$scope.Keywords = "Купить котенка в Рязани, купить котенка Мейн кун Рязань, купить шотландского котенка, купить бенгальского котенка, Арт дювикс кэт, Арт дювикс кет, Арт Дювекс";
	*/
});

artDuviksControllers.controller("BreedsCtrl", function ($scope, $http) {
	$scope.categories = $http({ method: 'GET', url: '/api/breeds' });
});