(function () {
    'use strict';

    angular.module('stockViewer').component('stockDiscussion', {
        bindings: {
            stock: '<',
            loggedIn: '<'
        },
        controllerAs: 'vm',
        controller: function ($scope, authenticationService, stockService) 
        {
            var vm = this;

            vm.stockDiscussion = null;
            vm.authenticationService = authenticationService;
            vm.commentEntryVisible = false;

            vm.hubConnection= null;

            vm.$onInit = function () {

                let transportType = signalR.TransportType[getParameterByName('transport')] || signalR.TransportType.WebSockets;
                let logger = new signalR.ConsoleLogger(signalR.LogLevel.Information);
                let http = new signalR.HttpConnection(window.__env.socketBase + '/stockDiscussion', { transport: transportType, logger: logger  });
                vm.hubConnection = new signalR.HubConnection(http);

                var isConnected = false;
                function invoke(method, groupName) {
                    if (!isConnected) {
                        return;
                    }

                    vm.hubConnection.invoke(method, groupName)
                            .then(result => {
                                console.log("invocation completed successfully: " + (result === null ? '(null)' : result));

                            })
                            .catch(err => {
                              console.log('Error invocation JoinGroup')
                            });
                }

                vm.hubConnection.on('addStockDiscussion', (stockId, discussionItem) => 
                {
                    vm.stockDiscussion.push(discussionItem);
                    vm.commentEntryVisible = false;
                    $scope.$digest(); 
                });

                vm.hubConnection.start()
                .then(() => {
                    isConnected = true;
                    console.log('Hub connection started');
                    invoke('JoinGroup', vm.stock.stockId.toString());
                })
                .catch(err => {
                    console.log('Error while establishing connection')
                });

            }

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
                    stockService.addStockDiscussionItem(authenticationService.userName, vm.stock.stockId, commentText)
                    .then(function (data)
                     {
                       console.log("result = " + data); 
                       // vm.stockDiscussion.push(discussionItem);
                       // vm.commentEntryVisible = false;
                    });
                }
            }

            vm.commentCanceled = function () {
                vm.commentEntryVisible = false;
            }

            function getParameterByName(name, url) {
                if (!url) {
                    url = window.location.href;
                }
                name = name.replace(/[\[\]]/g, "\\$&");
                var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                    results = regex.exec(url);
                if (!results) return null;
                if (!results[2]) return '';
                return decodeURIComponent(results[2].replace(/\+/g, " "));
            };
        },
        templateUrl: 'app/stock-viewer/stock/stock-discussion.component.html'
    });
})();
