(function () {
    'use strict';

    var appModule = angular.module('transactionMonitor', ['securityModule', 'ui.router']);

    appModule.value('apiBase', ''http://localhost:5001/api/transactionmonitor/');
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
                name: 'bulk',
                url: '/bulk',
                template: '<bulk-list></bulk-list>'
            },
            {
                name: 'single',
                url: '/single',
                template: '<single-list></single-list>'
            }
        ];

        $urlRouterProvider.when('/bulk/:bulkId', '/author/:authorId/courses');
        $urlRouterProvider.when('/single/:singleId', '/course/:courseId/modules');
        $urlRouterProvider.otherwise('/');

        states.forEach(function (state) {       
            $stateProvider.state(state);
        });
    });
})();
