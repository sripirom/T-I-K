(function () {
    'use strict';
    
    angular.module('stockViewer').factory('stockService', function (apiBase, $http, authenticationService) {

        var self = this;

        self.getAllstocks = function () {
            return $http.get(apiBase + 'stocks')
                .then(function (result) {
                    return result.data;
                });
        }

        self.getStock = function (stockId) {
            return $http.get(apiBase + 'stock/' + stockId + '/info')
                .then(function (result) {
                    return result.data;
                });
        }

        self.getStockDiscussion = function (stockId) {
            var accessToken = authenticationService.getAccessToken();
            return $http( {
                url: apiBase + 'stock/' + stockId + '/discussion',
                method: 'GET',
                headers: { 'Authorization': 'Bearer ' + accessToken }
            })
            .then(function (result) {
                return result.data;
            });
        }

        self.addStockDiscussionItem = function (userName, stockId, comment) {
            var accessToken = authenticationService.getAccessToken();
            if (accessToken != '') {
                var discussionItemModel = {
                    UserName: userName,
                    StockId: stockId,
                    Comment: comment
                };
                return $http({
                    url: apiBase + 'stock/' + stockId + '/discussion',
                    method: 'POST',
                    headers: { 'Authorization': 'Bearer ' + accessToken },
                    data: discussionItemModel
                })
                .then(function (result) {
                    return result.data;
                });
            }
        }

        self.getHistorical = function (stockId) {
            var accessToken = authenticationService.getAccessToken();
            return $http( {
                url: apiBase + 'stock/' + stockId + '/historical',
                method: 'GET',
                headers: { 'Authorization': 'Bearer ' + accessToken }
            })
            .then(function (result) {
                return result.data;
            });
        }

        self.timeFormat = function (hours, minutes, seconds) {
            while (seconds > 59) {
                minutes++;
                seconds -= 60;
            }
            while (minutes > 59) {
                hours++;
                minutes -= 60;
            }
                
            var timeString = '';

            if (hours > 0)
                timeString += hours.toString() + 'h ';
            timeString += minutes.toString() + 'm ';
            timeString += seconds.toString() + 's';

            return timeString;
        }




        

        
        return this;
    });
})();