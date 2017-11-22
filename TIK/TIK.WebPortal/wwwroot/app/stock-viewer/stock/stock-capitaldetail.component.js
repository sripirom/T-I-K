(function () {
    'use strict';

    angular.module('stockViewer').component('stockCapitaldetail', {
        bindings: {
            stock: '<'
        },
        controllerAs: 'vm',
        controller: function () {
            var vm = this;

        },
        templateUrl: 'App/stock-viewer/stock/stock-capitaldetail.component.html'
    });
})();
