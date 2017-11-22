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

        self.getstock = function (stockId) {
            return $http.get(apiBase + 'stock/' + stockId + '/info')
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

        self.getstockDiscussion = function (stockId) {
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

        self.addstockDiscussionItem = function (userName, stockId, comment) {
            var accessToken = authenticationService.getAccessToken();
            if (accessToken != '') {
                var discussionItemModel = {
                    UserName: userName,
                    stockId: stockId,
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
        
        self.updateRecentlyViewedstock = function (userName, stockId) {
            var accessToken = authenticationService.getAccessToken();
            if (accessToken != '') {
                var recentModel = {
                    UserName: userName,
                    stockId: stockId
                };
                return $http({
                    url: apiBase + 'stocks/recent',
                    method: 'POST',
                    headers: { 'Authorization': 'Bearer ' + accessToken },
                    data: recentModel
                })
                .then(function (result) {

                });
            }
        }

        self.getRecentlyViewedstocks = function (userName) {
            var accessToken = authenticationService.getAccessToken();
            if (accessToken != '') {
                return $http({
                    url: apiBase + 'stocks/recent/' + userName + '/get',
                    method: 'GET',
                    headers: { 'Authorization': 'Bearer ' + accessToken }
                })
                .then(function (result) {
                    return result.data;
                });
            }
        }

        self.clearRecentlyViewedList = function (userName) {
            var accessToken = authenticationService.getAccessToken();
            if (accessToken != '') {
                return $http({
                    url: apiBase + 'stocks/recent/' + userName + '/clear',
                    method: 'GET',
                    headers: { 'Authorization': 'Bearer ' + accessToken }
                })
                .then(function (result) {

                });
            }
        }
        
        return this;
    });
})();