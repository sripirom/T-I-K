(function () {
    'use strict';

    angular.module('stockViewer').component('stockList', {
        controllerAs: 'vm',
        controller: function (stockService) {
            var vm = this;

            vm.stocks = null;

            vm.$onInit = function () {
                stockService.getAllstocks().then(function (stocks) {
                    vm.stocks = stocks;
                });
            }
        },
        templateUrl: 'app/stock-viewer/stock/stock-list.component.html'
    });
})();
