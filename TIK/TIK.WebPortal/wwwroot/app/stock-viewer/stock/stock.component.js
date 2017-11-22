(function () {
    'use strict';

    angular.module('stockViewer').component('stock', {
        bindings: {
            stockId: '<'
        },
        controllerAs: 'vm',
        controller: function (stockService, authenticationService) {
            var vm = this;

            vm.stock = null;
            vm.authenticationService = authenticationService;

            vm.$onInit = function () {
               
                /*if (vm.stockId)
                 {
                  */
                    stockService.getStock(vm.stockId).then(function (stock) {
                        vm.stock = stock;
                        /*
                        if (authenticationService.loggedIn) {
                            stockService.updateRecentlyViewedstock(authenticationService.userName, vm.stock.stockId).then(function (recentItem) {
                            });
                        }
                        */
                    }); 

                /*}*/

            }
        },
        templateUrl: 'App/stock-viewer/stock/stock.component.html'
    });
})();
