(function () {
    'use strict';

    angular.module('stockViewer').component('stockHeading', {
        bindings: {
            stock: '<'
        },
        controllerAs: 'vm',
        controller: function () {
            var vm = this;

        },
        templateUrl: 'app/stock-viewer/stock/stock-heading.component.html'
    });
})();
