'use strict';

var artDuviksApp = angular.module("artDuviksApp");
var adminPartialsUrl = ""; //TODO: add admin partials
var kittensPartialUrl = "kittens/partials/";
var breedsPartialurl = "breeds/partials/";
var mainPagePartialUrl = "Scripts/app/";

//Configure $routeProvider service
artDuviksApp.config(function ($routeProvider, $locationProvider) {
    $routeProvider.
		when("/", {
		    title: "Питомник Art Duviks Cat - купить котенка в Рязани пород Мейн Кун, Бенгальской, Британской и Шотландской пород",
		    description: "В питомнике Art Duviks Cat вы можете купить котят Мейн-кун, шотландцев и бенгалов.",
		    keywords: "Купить котенка в Рязани, купить котенка Мейн кун Рязань, купить шотландского котенка, купить бенгальского котенка, Арт дювикс кэт, Арт дювикс кет, Арт Дювекс",
		    templateUrl: mainPagePartialUrl + "mainPage.html",
		    controller: "MainController"
		})
		.when("/kittens", {
		    title: "Наши котята - выбрать котенка",
		    templateUrl: kittensPartialUrl + "chooseBreed.html",
		    controller: "kittensCtrl"
		})
		.when("/parent-kittens", {
		    title: "Наши кошки",
		    templateUrl: kittensPartialUrl + "parents.html",
		    controller: "kittensCtrl"
		})
		.when("/scotland-kittens", {
		    title: "Шотландские котята - выбрать котенка шотландской породы",
		    templateUrl: kittensPartialUrl + "kittens.html",
		    controller: "kittensCtrl"
		})
		.when("/mainkun-kittens", {
		    title: "Мейн-куны - выбрать котенка Мейн-кун",
		    templateUrl: kittensPartialUrl + "kittens.html",
		    controller: "kittensCtrl"
		})
		.when("/bengal-kittens", {
		    title: "Бенгальские котята - выбрать котенка",
		    templateUrl: kittensPartialUrl + "kittens.html",
		    controller: "kittensCtrl"
		})
		.when("/kitten-page/:id", {
		    title: "Информация о котенке",
		    templateUrl: kittensPartialUrl + "kitten.html",
		    controller: "kittensCtrl"
		})
		.when("/parent-kitten-page/:id", {
		    title: "Информация о кошке",
		    templateUrl: kittensPartialUrl + "kitten.html",
		    controller: "kittensCtrl"
		})
		.when("/admin-panel", {
		    title: "Панель администратора",
		    templateUrl: adminPartialsUrl + "admin/admin.html"
		})
		.when("/admin/available-kittens", {
		    title: "Панель доступных котят",
		    templateUrl: adminPartialsUrl + "admin/available-kittens.html",
		    controller: "kittensCtrl"
		})
		.when("/admin/archive-kittens", {
		    title: "Панель архивных котят",
		    templateUrl: adminPartialsUrl + "admin/available-kittens.html",
		    controller: "kittensCtrl"
		})
		.when("/admin/parents", {
		    title: "Панель родителей",
		    templateUrl: adminPartialsUrl + "admin/available-kittens.html",
		    controller: "kittensCtrl"
		})
		.when("/admin/addKitten", {
		    title: "Добавление нового котенка",
		    templateUrl: adminPartialsUrl + "admin/addKitten.html",
		    controller: "kittensCtrl"
		})
		.when("/admin/editKitten/:id", {
		    title: "Редактирование нового котенка",
		    templateUrl: adminPartialsUrl + "admin/editKitten.html",
		    controller: "kittensCtrl"
		})
		.when("/admin/kitten/modify-pictures/:id", {
		    title: "Редактирование фотографий котенка",
		    templateUrl: adminPartialsUrl + "admin/modify-kitten-pictures.html",
		    controller: "kittensCtrl"
		})

    $locationProvider.html5Mode({ enabled: true, requireBase: false });
});

artDuviksApp.config([
	'$httpProvider',
	function ($httpProvider) {
	    $httpProvider.defaults.useXDomain = true;
	    delete $httpProvider.defaults.headers.common['X-Requested-With'];
	}
]);