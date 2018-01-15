(function () {
    'use strict';

    angular.module('stockViewer').component('stockHistorical', {
        bindings: {
            stock: '<',
            loggedIn: '<'
        },
        controllerAs: 'vm',
        controller: function (authenticationService, stockService) {
            var vm = this;

            vm.stockHistorical = null;
            vm.authenticationService = authenticationService;

            vm.$onChanges = function (changes) {
                if ((changes.loggedIn != null && changes.loggedIn.currentValue != null) || 
                    (changes.stock != null && changes.stock.currentValue != null)) {
                    if (authenticationService.loggedIn && vm.stock != null)
                     {
                        stockService.getHistorical(vm.stock.symbol, vm.fromDate, vm.toDate).then(function (stockHistorical) {
                            vm.stockHistorical = stockHistorical;
                        });
                    }
                }
            };
            vm.searchEod = function()
            {
                    if (authenticationService.loggedIn && vm.stock != null)
                     {
                        stockService.getHistorical(vm.stock.symbol, vm.fromDate, vm.toDate).then(function (stockHistorical) {
                            vm.stockHistorical = stockHistorical;
                        });
                    }
                
            }

            vm.today = function() {
                vm.fromDate = new Date();
                vm.toDate = new Date();
              };
              vm.today();
            
              vm.clear = function() {
                vm.fromDate = null;
                vm.toDate = null;
              };
            
              vm.inlineOptions = {
                customClass: getDayClass,
                minDate: new Date(1900, 1, 1),
                showWeeks: true
              };
            
              vm.dateOptions = {
                dateDisabled: disabled,
                formatYear: 'yy',
                maxDate: new Date(2020, 5, 22),
                minDate: new Date(1900, 1, 1),
                startingDay: 1
              };
            
              // Disable weekend selection
              function disabled(data) {
                var date = data.date,
                  mode = data.mode;
                return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
              }
            
              vm.toggleMin = function() {
                vm.inlineOptions.minDate = vm.inlineOptions.minDate ? null : new Date();
                vm.dateOptions.minDate = vm.inlineOptions.minDate;
              };
            
              vm.toggleMin();
            
              vm.open1 = function() {
                vm.popup1.opened = true;
              };
            
              vm.open2 = function() {
                vm.popup2.opened = true;
              };
            
              vm.setDate = function(year, month, day)
               {
                vm.dt = new Date(year, month, day);
              };
            
              vm.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
              vm.format = vm.formats[0];
              vm.altInputFormats = ['M!/d!/yyyy'];
            
              vm.popup1 = {
                opened: false
              };
            
              vm.popup2 = {
                opened: false
              };
            
              var tomorrow = new Date();
              tomorrow.setDate(tomorrow.getDate() + 1);
              var afterTomorrow = new Date();
              afterTomorrow.setDate(tomorrow.getDate() + 1);

              vm.events = [
                {
                  date: tomorrow,
                  status: 'full'
                },
                {
                  date: afterTomorrow,
                  status: 'partially'
                }
              ];
            
              function getDayClass(data) {
                var date = data.date,
                  mode = data.mode;
                if (mode === 'day') {
                  var dayToCheck = new Date(date).setHours(0,0,0,0);
            
                  for (var i = 0; i < vm.events.length; i++) {
                    var currentDay = new Date(vm.events[i].date).setHours(0,0,0,0);
            
                    if (dayToCheck === currentDay) {
                      return vm.events[i].status;
                    }
                  }
                }
            
                return '';
              }

        },
        templateUrl: 'app/stock-viewer/stock/stock-historical.component.html'
    });
})();
