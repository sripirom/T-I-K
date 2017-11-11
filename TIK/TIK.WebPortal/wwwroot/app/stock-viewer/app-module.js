(function () {
    'use strict';

    var appModule = angular.module('stockViewer', ['securityModule', 'ui.router']);

    appModule.value('apiBase', 'http://localhost:5000/api/stockViewer/');
    appModule.value('componentBorders', true);

    appModule.run(function (componentBorders) {
        if (componentBorders) {
            if (appModule._invokeQueue) {
                appModule._invokeQueue.forEach(function (item) {
                    if (item[1] == 'component') {
                        var componentName = item[2][0];
                        var componentProperties = item[2][1];
                        if (componentProperties.templateUrl) {
                            var templateUrl = componentProperties.templateUrl;
                            delete componentProperties.templateUrl;
                            componentProperties.template = '<div class="component-borders"><b>' + componentName + '</b><div ng-include="\'' + templateUrl + '\'"></div></div>';
                        }
                        else {
                            var template = '<div class="component-borders">' + componentName + '<div>' + componentProperties.template + '</div></div>';
                            componentProperties.template = template;
                        }
                    }
                });
            }
        }
    });

    appModule.config(function ($stateProvider, $urlRouterProvider) {
        var states = [
            {
                name: 'home',
                url: '',
                template: '<home></home>'
            },
            {
                name: 'home2',
                url: '/',
                template: '<home></home>'
            },        {
                name: 'stocks',
                url: '/stocks',
                template: '<stock-list></stock-list>'
            },
            {
                name: 'stock',
                url: '/stock/{symbol}',
                resolve: {
                    symbol: function ($stateParams) {
                        return $stateParams.symbol;
                    }
                },
                template: '<stock stock-symbol="$resolve.symbol"></stock>'
            }
        ];

        $urlRouterProvider.when('/stock/:symbol', '/stock/:symbol/stocks');
        $urlRouterProvider.otherwise('/');

        states.forEach(function (state) {       
            $stateProvider.state(state);
        });
    });
})();
