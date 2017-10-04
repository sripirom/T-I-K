(function () {
    'use strict';

    angular.module('courseViewer').component('courseHeading', {
        bindings: {
            course: '<'
        },
        controllerAs: 'vm',
        controller: function () {
            var vm = this;

        },
        templateUrl: 'App/course-viewer/course/course-heading.component.html'
    });
})();
