(function () {
    'use strict';

    angular.module('stockViewer').component('userStatus', {
        controllerAs: 'vm',
        controller: function (userAccountService, authenticationService) {
            var vm = this;

            var getUser = function (userName) {
                userAccountService.getUser(userName).then(function (user) {
                    vm.userFullName = user.firstName + ' ' + user.lastName;
                });
            }
            
            vm.$onInit = function () {
                if (authenticationService.loggedIn) {
                    getUser(authenticationService.userName);
                }
            }
            
            vm.postRegister = function (registerResponse, callback) {
                userAccountService.addUser(registerResponse).then(function (user) {
                    if (callback != null)
                        callback();
                })
            }

            vm.postLogin = function (loginResponse) {
                getUser(loginResponse);
            }

 
        },
        templateUrl: 'app/stock-viewer/user-status.component.html'
    });
})();
