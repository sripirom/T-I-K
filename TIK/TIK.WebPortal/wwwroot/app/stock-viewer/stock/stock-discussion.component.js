(function () {
    'use strict';

    angular.module('stockViewer').component('stockDiscussion', {
        bindings: {
            stock: '<',
            loggedIn: '<'
        },
        controllerAs: 'vm',
        controller: function (authenticationService, stockService) {
            var vm = this;

            vm.stockDiscussion = null;
            vm.authenticationService = authenticationService;
            vm.commentEntryVisible = false;

            vm.$onChanges = function (changes) {
                if ((changes.loggedIn != null && changes.loggedIn.currentValue != null) || 
                    (changes.stock != null && changes.stock.currentValue != null)) {
                    if (authenticationService.loggedIn && vm.stock != null) {
                        stockService.getStockDiscussion(vm.stock.stockId).then(function (stockDiscussion) {
                            vm.stockDiscussion = stockDiscussion;
                        });
                    }
                }
            }

            vm.showCommentEntry = function () {
                vm.commentEntryVisible = true;
            }

            vm.commentSubmitted = function (commentText) {
                if (authenticationService.loggedIn) {
                    stockService.addStockDiscussionItem(authenticationService.userName, vm.stock.stockId, commentText).then(function (discussionItem) {
                        vm.stockDiscussion.push(discussionItem);
                        vm.commentEntryVisible = false;
                    });
                }
            }

            vm.commentCanceled = function () {
                vm.commentEntryVisible = false;
            }
        },
        templateUrl: 'App/stock-viewer/stock/stock-discussion.component.html'
    });
})();
