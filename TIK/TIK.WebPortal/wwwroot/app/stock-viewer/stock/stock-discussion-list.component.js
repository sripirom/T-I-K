(function () {
    'use strict';
    
    angular.module('stockViewer').component('stockDiscussionList', {
        bindings: {
            stockDiscussion: '<',
        },
        controllerAs: 'vm',
        controller: function () {
            var vm = this;

        },
        templateUrl: 'app/stock-viewer/stock/stock-discussion-list.component.html'
    });    
})();