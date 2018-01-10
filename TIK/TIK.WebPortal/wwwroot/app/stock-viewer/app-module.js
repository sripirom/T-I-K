(function () {
    'use strict';

    var appModule = angular.module('stockViewer', ['securityModule', 'ui.router', 'ui.bootstrap']);

    appModule.value('apiBase', window.__env.apiBaseStock);
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
                url: '/stock/{stockId}',
                resolve: {
                    stockId: function ($stateParams) {
                        return $stateParams.stockId;
                    }
                },
                template: '<stock stock-id="$resolve.stockId"></stock>'
            },
            {
                name: 'stock.historical',
                url: '/historical',
                template: '<stock-historical stock="vm.stock"></stock-historical>'
            },
            {
                name: 'stock.backtester',
                url: '/backtester',
                template: '<stock-backtester stock="vm.stock"></stock-backtester>'
            },
            {
                name: 'stock.discussion',
                url: '/discussion',
                template: '<stock-discussion stock="vm.stock" logged-in="vm.authenticationService.loggedIn"></stock-discussion>'
            }
        ];

       /* $urlRouterProvider.when('/stock/:stockId', '/stock/:stockId/stocks');*/
        $urlRouterProvider.otherwise('/');

        states.forEach(function (state) {       
            $stateProvider.state(state);
        });
    });
})();
