(function () {
    'use strict';

    angular.module('stockViewer').component('stockHistorical', {
        bindings: {
            stock: '<',
            loggedIn: '<'
        },
        controllerAs: 'vm',
        controller: function (authenticationService, stockService) {
            var vm = this;

            vm.stockHistorical = null;
            vm.authenticationService = authenticationService;

            vm.$onChanges = function (changes) {
                if ((changes.loggedIn != null && changes.loggedIn.currentValue != null) || 
                    (changes.stock != null && changes.stock.currentValue != null)) {
                    if (authenticationService.loggedIn && vm.stock != null) {
                        stockService.getHistorical(vm.stock.stockId).then(function (stockHistorical) {
                            vm.stockHistorical = stockHistorical;
                        });
                    }
                }
            }

        },
        templateUrl: 'App/stock-viewer/stock/stock-historical.component.html'
    });
})();
