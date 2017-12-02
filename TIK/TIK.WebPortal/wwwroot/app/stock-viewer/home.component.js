(function () {
    'use strict';

    angular.module('stockViewer').component('home', {
        controllerAs: 'vm',
        controller: function (authenticationService) {
            var vm = this;

            vm.authenticationService = authenticationService;
        },
        templateUrl: 'app/stock-viewer/home.component.html'
    });
})();
