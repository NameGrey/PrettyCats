define(['angular'], function(angular) {
	'use strict';

	function configureRoutes() {
		var adminPartialsUrl = "app/partials/";
		var artDuviksApp = angular.module("artDuviksApp");

		//Configure $routeProvider service
		artDuviksApp.config(function ($stateProvider, $urlRouterProvider, $locationProvider) {
			$stateProvider
				.state({
					url: '/',
					name: 'adminPanel',
					title: 'Панель администратора',
					templateUrl: adminPartialsUrl + 'admin.html'
				})
				.state({
					url: '/available-kittens',
					name: 'availableKittens',
					title: 'Панель доступных котят',
					templateUrl: adminPartialsUrl + '/kittensViews/available-kittens.html'
				})
				.state({
					url: '/archive-kittens',
					name: 'archiveKittens',
					title: 'Панель архивных котят',
					templateUrl: adminPartialsUrl + '/kittensViews/archive-kittens.html'
				})
				.state({
					url: '/parents',
					name: 'parents',
					title: 'Панель родителей',
					templateUrl: adminPartialsUrl + '/kittensViews/parents.html'
				})
				.state({
					url: '/addKitten', 
					name: 'addKitten',
					title: 'Добавление нового котенка',
					templateUrl: adminPartialsUrl + 'addKitten.html'
				})
				.state({
					url: '/addArchiveKitten',
					name: 'addArchiveKitten',
					title: 'Добавление нового котенка',
					templateUrl: adminPartialsUrl + 'addKitten.html'
				})
				.state({
					url: '/addParent',
					name: 'addParent',
					title: 'Добавление нового родителя',
					templateUrl: adminPartialsUrl + 'addKitten.html'
				})
				.state({
					url: '/editKitten/:id',
					name: 'editKitten',
					title: 'Редактирование нового котенка',
					templateUrl: adminPartialsUrl + 'editKitten.html'
				})
				.state({
					url: '/kitten/modify-pictures/:id',
					name: 'modifyPictures',
					title: 'Редактирование фотографий котенка',
					templateUrl: adminPartialsUrl + 'modify-kitten-pictures.html'
				});

			$urlRouterProvider.otherwise("/");

			$locationProvider.html5Mode({ enabled: true, requireBase: false });
		});
	}

	function configureHttpHeaders() {
		var artDuviksApp = angular.module("artDuviksApp");

		artDuviksApp.config([
			'$httpProvider',
			function ($httpProvider) {
				$httpProvider.defaults.useXDomain = true;
				delete $httpProvider.defaults.headers.common['X-Requested-With'];
			}
		]);
	}

	return {
		configureRoutes: configureRoutes,
		configureHttpHeaders: configureHttpHeaders
	}
});
