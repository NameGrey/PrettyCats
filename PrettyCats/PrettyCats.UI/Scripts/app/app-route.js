'use strict';

var artDuviksApp = angular.module("artDuviksApp");
var adminPartialsUrl = "Scripts/app/partials/";

//Configure $routeProvider service
artDuviksApp.config(function ($routeProvider, $locationProvider) {
    $routeProvider
		.when("/", {
		    title: "Панель администратора",
		    templateUrl: adminPartialsUrl + "admin.html"
		})
		.when("/available-kittens", {
		    title: "Панель доступных котят",
		    templateUrl: adminPartialsUrl + "/kittensViews/available-kittens.html"
		  //  controller: "kittensCtrl"
		})
		.when("/archive-kittens", {
		    title: "Панель архивных котят",
		    templateUrl: adminPartialsUrl + "/kittensViews/archive-kittens.html"
		   // controller: "kittensCtrl"
		})
		.when("/parents", {
		    title: "Панель родителей",
		    templateUrl: adminPartialsUrl + "/kittensViews/parents.html"
		  //  controller: "kittensCtrl"
		})
		.when("/addKitten", {
		    title: "Добавление нового котенка",
		    templateUrl: adminPartialsUrl + "addKitten.html"
		   // controller: "kittensCtrl"
		})
        .when("/addArchiveKitten", {
            title: "Добавление нового котенка",
            templateUrl: adminPartialsUrl + "addKitten.html"
           // controller: "kittensCtrl"
        })
        .when("/addParent", {
		    title: "Добавление нового родителя",
		    templateUrl: adminPartialsUrl + "addKitten.html"
		 //   controller: "kittensCtrl"
		})
		.when("/editKitten/:id", {
		    title: "Редактирование нового котенка",
		    templateUrl: adminPartialsUrl + "editKitten.html"
		  //  controller: "kittensCtrl"
		})
		.when("/kitten/modify-pictures/:id", {
		    title: "Редактирование фотографий котенка",
		    templateUrl: adminPartialsUrl + "modify-kitten-pictures.html"
		  //  controller: "kittensCtrl"
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