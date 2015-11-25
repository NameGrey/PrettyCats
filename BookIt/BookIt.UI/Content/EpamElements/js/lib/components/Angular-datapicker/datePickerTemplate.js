'use strict';

(function (){
    var DatePicker = angular.module('datePickerApp');

    //Шаблон календаря
    DatePicker.constant('templateConfig', {
        template: function (attrs, id) {
            return '' +
                '<div ' +
                (id ? 'id="' + id + '" ' : '') +
                'date-picker="' + attrs.ngModel + '" ' +
                (attrs.view ? 'view="' + attrs.view + '" ' : '') +
                (attrs.end ? 'end-date="' + attrs.end + '" ' : '') +
                (attrs.start ? 'start-date="' + attrs.start + '" ' : '') +
                (attrs.max ? 'max-date="' + attrs.max + '" ' : '') +
                (attrs.min ? 'min-date="' + attrs.min + '" ' : '') +
                (attrs.autoClose ? 'auto-close="' + attrs.autoClose + '" ' : '') +
                (attrs.template ? 'template="' + attrs.template + '" ' : '') +
                (attrs.partial ? 'partial="' + attrs.partial + '" ' : '') +
                (attrs.step ? 'step="' + attrs.step + '" ' : '') +
                (attrs.onSetDate ? 'date-change="' + attrs.onSetDate + '" ' : '') +
                (attrs.ngModel ? 'ng-model="' + attrs.ngModel + '" ' : '') +
                'class="date-picker-date-time"></div>';
        },
        format: 'YYYY-MM-DD',
        //format: 'YYYY-MM-DD HH:mm',
        //views: ['date', 'year', 'month'],
        views: ['date', 'year', 'month'],
        autoClose: false,
        position: 'relative'
    });

    function randomName() {
        return 'picker' + Math.random().toString().substr(2);
    }

    DatePicker.directive('datePickerRange', ['$compile', '$document', '$filter', 'templateConfig', '$parse', 'datePickerUtils', function ($compile, $document, $filter, templateConfig, $parse, datePickerUtils) {
        function link(scope, element, attrs, ngModel) {
            var dateFilter = $filter('mFormat');
            var body = $document.find('body');

            var format = attrs.format || templateConfig.format,
                views = $parse(attrs.views)(scope) || templateConfig.views.concat(),
                view = attrs.view || views[0],
                index = views.indexOf(view),
                picker = null,
                pickerName = element[0].name,
                timezone = attrs.timezone || false,
                dateChange = null,
                shownOnce = false,
                template = null,
                pickerIDs = [randomName(), randomName()];

            if (index === -1) {
                views.splice(index, 1);
            }

            function formatter(value) {
                return dateFilter(value, format, timezone);
            }

            function parser(viewValue) {
                if (viewValue.length === format.length) {
                    return viewValue;
                }
                return undefined;
            }

            ngModel.$formatters.push(formatter);
            ngModel.$parsers.unshift(parser);

            function getTemplate(attrs, id, model) {
                return templateConfig.template(angular.extend(attrs, {
                    ngModel: model
                }), id);
            }

            function clear() {
                if (picker) {
                    picker.remove();
                    picker = null;
                }
            }

            element.on('focus', function () {
                scope.start = element.attr("start");
                scope.end = element.attr("end");

                //todo: create auto detection of vertical orientation and change class "datepicker-orient-top" / "datepicker-orient-bottom" in template
                if (pickerName === "start") {
                    template = getTemplate(attrs, pickerIDs[0], element.attr("ng-model").toString());
                } else {
                    template = getTemplate(attrs, pickerIDs[1], element.attr("ng-model").toString());
                }

                //If the picker has already been shown before then we shouldn't be binding to events, as these events are already bound to in this scope.
                if (!shownOnce) {
                    scope.$on('setDate', function (event, date) {
                        if (dateChange) {
                            dateChange(attrs.ngModel, date);
                        }
                    });

                    scope.$on('hidePicker', function () {
                        element.triggerHandler('blur');
                    });

                    scope.$on('$destroy', clear);

                    shownOnce = true;
                }

                picker = $compile(template)(scope);
                var pos = angular.extend(element.offset(), { height: element[0].offsetHeight });
                picker.css({ top: pos.top + pos.height, left: pos.left, display: 'block', position: 'absolute' });
                body.append(picker);

                picker.bind('mousedown', function(evt) {
                    evt.preventDefault();
                });
            });
            element.bind('blur', clear);
        }

        return {
            require: 'ngModel',
            link: link
        }
    }]);
}());
