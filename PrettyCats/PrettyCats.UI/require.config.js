require.config({
	baseUrl: '/',
	paths: {
		'jQuery': 'node_modules/jquery/dist/jquery.min',
		'angular': 'node_modules/angular/angular',
		'angular-ui-router': 'node_modules/angular-ui-router/release/angular-ui-router'
	},
	shim: {
		'angular': {
			exports: 'angular'
		},
		'angular-ui-router': {
			deps: ['angular']
		}
	}
});

require(['app/app.js'], function (app) {
	console.log('Init app');
	app.init();
});