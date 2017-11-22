(function () {
    'use strict';
    
    angular.module('stockViewer').component('stockDiscussionItem', {
        bindings: {
            commentSubmitted: '&',
            commentCanceled: '&'
        },
        controllerAs: 'vm',
        controller: function () {
            var vm = this;

            vm.commentText = '';

            vm.submit = function () {
                if (vm.commentText != '') {
                    if (vm.commentSubmitted != null) {
                        vm.commentSubmitted()(vm.commentText);
                        vm.commentText = '';
                    }
                }
                else
                    vm.cancel();
            }

            vm.cancel = function () {
                if (vm.commentCanceled != null) {
                    vm.commentCanceled()();
                    vm.commentText = '';
                }
            }
        },
        templateUrl: 'App/stock-viewer/stock/stock-discussion-item.component.html'
    });       
})();