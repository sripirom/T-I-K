(function () {
    'use strict';

    angular.module('stockViewer').component('stockBacktester', {
        bindings: {
            stock: '<'
        },
        controllerAs: 'vm',
        controller: function () {
            var vm = this;

        },
        templateUrl: 'app/stock-viewer/stock/stock-backtester.component.html'
    });
})();
