define(['angular'], function(angular) {
	'use strict';

	function configureRoutes() {
		var adminPartialsUrl = "app/partials/";
		var artDuviksApp = angular.module("artDuviksApp");

		artDuviksApp.config(function ($stateProvider, $urlRouterProvider, $locationProvider) {
			$stateProvider
				.state({
					url: '/',
					name: 'adminPanel',
					title: 'Панель администратора',
					template: '<admin-panel-component></admin-panel-component>'
				})
				.state({
					url: '/available-kittens',
					name: 'availableKittens',
					title: 'Панель доступных котят',
					template: '<available-kittens-component></available-kittens-component>'
				})
				.state({
					url: '/archive-kittens',
					name: 'archiveKittens',
					title: 'Панель архивных котят',
					template: '<archive-kittens-component></archive-kittens-component>'
				})
				.state({
					url: '/parents',
					name: 'parents',
					title: 'Панель родителей',
					template: '<parents-component></parents-component>'
				})
				.state({
					url: '/addKitten', 
					name: 'addKitten',
					title: 'Добавление нового котенка',
					template: '<add-kitten-component></add-kitten-component>'
				})
				.state({
					url: '/addArchiveKitten',
					name: 'addArchiveKitten',
					title: 'Добавление нового котенка',
					template: '<add-archive-component></add-archive-component'
				})
				.state({
					url: '/addParent',
					name: 'addParent',
					title: 'Добавление нового родителя',
					template: '<add-parent-component></add-parent-component'
				})
				.state({
					url: '/editKitten/:id',
					name: 'editKitten',
					title: 'Редактирование нового котенка',
					template: '<edit-kitten-component></edit-kitten-component'
				})
				.state({
					url: '/kitten/modify-pictures/:id',
					name: 'modifyPictures',
					title: 'Редактирование фотографий котенка',
					template: '<modify-pictures-order-component></modify-pictures-order-component>'
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
