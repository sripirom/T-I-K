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
        templateUrl: 'app/stock-viewer/stock/stock-capitaldetail.component.html'
    });
})();
