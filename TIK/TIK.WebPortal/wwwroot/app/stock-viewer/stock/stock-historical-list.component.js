(function () {
    'use strict';
    
    angular.module('stockViewer').component('stockHistoricalList', {
        bindings: {
            stockHistorical: '<',
        },
        controllerAs: 'vm',
        controller: function () {
            var vm = this;

        },
        templateUrl: 'app/stock-viewer/stock/stock-historical-list.component.html'
    });    
})();