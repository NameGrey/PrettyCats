'use strict';

//Create application module with services
var artDuviksApp = angular.module("artDuviksApp", ["ngRoute", "artDuviksControllers"]);
var baseUrl = "/pages/html-partials/";

//Configure $routeProvider service
artDuviksApp.config(function($routeProvider, $locationProvider) {
	$routeProvider.
		when("/", {
			title: "Питомник Art Duviks Cat - купить котенка в Рязани пород Мейн Кун, Бенгальской, Британской и Шотландской пород",
			description: "В питомнике Art Duviks Cat вы можете купить котят Мейн-кун, шотландцев и бенгалов.",
			keywords: "Купить котенка в Рязани, купить котенка Мейн кун Рязань, купить шотландского котенка, купить бенгальского котенка, Арт дювикс кэт, Арт дювикс кет, Арт Дювекс",
			templateUrl: baseUrl + "main.html",
			controller: "MainController"
		})
		.when("/kittens", {
			title: "Наши котята - выбрать котенка",
			templateUrl: baseUrl + "chooseBreed.html",
			controller: "KittensCtrl"
		})
		.when("/parent-kittens", {
			title: "Наши кошки",
			templateUrl: baseUrl + "parents.html",
			controller: "KittensCtrl"
		})
		.when("/scotland-kittens", {
			title: "Шотландские котята - выбрать котенка шотландской породы",
			templateUrl: baseUrl + "kittens.html",
			controller: "KittensCtrl"
		})
		.when("/mainkun-kittens", {
			title: "Мейн-куны - выбрать котенка Мейн-кун",
			templateUrl: baseUrl + "kittens.html",
			controller: "KittensCtrl"
		})
		.when("/bengal-kittens", {
			title: "Бенгальские котята - выбрать котенка",
			templateUrl: baseUrl + "kittens.html",
			controller: "KittensCtrl"
		})
		.when("/kitten-page/:id", {
			title: "Информация о котенке",
			templateUrl: baseUrl + "kitten.html",
			controller: "KittensCtrl"
		})
		.when("/parent-kitten-page/:id", {
			title: "Информация о кошке",
			templateUrl: baseUrl + "kitten.html",
			controller: "KittensCtrl"
		})
		.when("/admin-panel", {
			title: "Панель администратора",
			templateUrl: baseUrl + "admin/admin.html"
		})
		.when("/admin/available-kittens", {
			title: "Панель доступных котят",
			templateUrl: baseUrl + "admin/available-kittens.html",
			controller: "KittensCtrl"
		})
		.when("/admin/archive-kittens", {
			title: "Панель архивных котят",
			templateUrl: baseUrl + "admin/available-kittens.html",
			controller: "KittensCtrl"
		})
		.when("/admin/parents", {
			title: "Панель родителей",
			templateUrl: baseUrl + "admin/available-kittens.html",
			controller: "KittensCtrl"
		})
		.when("/admin/addKitten", {
			title: "Добавление нового котенка",
			templateUrl: baseUrl + "admin/addKitten.html",
			controller: "KittensCtrl"
		})
		.when("/admin/editKitten", {
			title: "Редактирование нового котенка",
			templateUrl: baseUrl + "admin/editKitten.html",
			controller: "KittensCtrl"
		})
		.otherwise("/");

	$locationProvider.html5Mode({ enabled: true, requireBase: false });
});

artDuviksApp.config([
	'$httpProvider',
	function ($httpProvider) {
		$httpProvider.defaults.useXDomain = true;
		delete $httpProvider.defaults.headers.common['X-Requested-With'];
	}
]);

artDuviksApp.factory("configuration", function() {
		return { ServerApi: "http://localhost:53820/api" };
	});

artDuviksApp.run([
	"$rootScope", function ($rootScope) {

		$rootScope.$on('$stateChangeError', function(event, toState, toParams, fromState, fromParams, error) {
			console.error("$stateChangeError: ", toState, error);
		});

		$rootScope.$on("$routeChangeSuccess", function(event, current, previous) {
			if (current && current.$$route) {
				if (current.$$route.title)
					$rootScope.title = current.$$route.title;

				if (current.$$route.keywords)
					$rootScope.keywords = current.$$route.keywords;

				if (current.$$route.description)
					$rootScope.description = current.$$route.description;
			}
		});
	}
]);
