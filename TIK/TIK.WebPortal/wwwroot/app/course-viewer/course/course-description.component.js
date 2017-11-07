(function () {
    'use strict';

    angular.module('courseViewer').component('courseDescription', {
        bindings: {
            course: '<'
        },
        controllerAs: 'vm',
        controller: function () {
            var vm = this;

        },
        templateUrl: 'App/course-viewer/course/course-description.component.html'
    });
})();
