(function() {
    var DatePicker = angular.module('datePickerApp', []);

    DatePicker.constant('datePickerConfig', {
        template: 'templates/datepicker.html',
        view: 'date',
        views: ['year', 'month', 'date', 'hours', 'minutes'],
        momentNames: {
            year: 'year',
            month: 'month',
            date: 'day',
            hours: 'hours',
            minutes: 'minutes',
        },
        viewConfig: {
            year: ['years', 'isSameYear'],
            month: ['months', 'isSameMonth'],
            hours: ['hours', 'isSameHour'],
            minutes: ['minutes', 'isSameMinutes'],
        },
        step: 3
    });

    DatePicker.filter('mFormat', function () {
        return function (m, format, tz) {
            if (!(moment.isMoment(m))) {
                return moment(m).format(format);
            }
            return tz ? moment.tz(m, tz).format(format) : m.format(format);
        };
    });

    DatePicker.directive('datePicker', ['$compile', 'datePickerConfig', 'datePickerUtils', function ($compile, datePickerConfig, datePickerUtils) {
        function link(scope, element, attrs, ngModel) {

            //Populate child scope
            function prepareViews() {
                scope.views = datePickerConfig.views.concat();
                scope.view = attrs.view || datePickerConfig.view;
                
                if (scope.views.length === 1 || scope.views.indexOf(scope.view) === -1) {
                    scope.view = scope.views[0];
                }
            }

            function getDate(name) {
                return datePickerUtils.getDate(scope, attrs, name);
            }

            datePickerUtils.setParams(attrs.timezone);

            var arrowClick = false,
                tz = scope.tz = attrs.timezone,
                createMoment = datePickerUtils.createMoment,
                eventIsForPicker = datePickerUtils.eventIsForPicker,
                step = parseInt(attrs.step || datePickerConfig.step, 10),
                partial = !!attrs.partial,
                startDate = getDate('startDate'),
                endDate = getDate('endDate'),
                pickerID = element[0].id,
                now = scope.now = createMoment(),
                selected = scope.date = createMoment(scope.model || now),
                autoclose = attrs.autoClose === 'true',
                maxDate = getDate('maxDate'),
                minDate = getDate('minDate');
            
            if (!scope.model) {
                selected.minute(Math.ceil(selected.minute() / step) * step).second(0);
            }

            scope.template = datePickerConfig.template;

            scope.watchDirectChanges = attrs.watchDirectChanges !== undefined;
            scope.callbackOnSetDate = attrs.dateChange ? datePickerUtils.findFunction(scope, attrs.dateChange) : undefined;

            prepareViews();

            scope.setView = function (nextView) {
                if (scope.views.indexOf(nextView) !== -1) {
                    scope.view = nextView;
                }
            };

            scope.selectDate = function (date) {
                if (attrs.disabled) {
                    return false;
                }
                if (isSame(scope.date, date)) {
                    date = scope.date;
                }
                date = clipDate(date);
                if (!date) {
                    return false;
                }
                scope.date = date;

                var nextView = scope.views[scope.views.indexOf(scope.view) + 1];
                if ((!nextView || partial) || scope.model) {
                    setDate(date);
                }

                if (nextView) {
                    scope.setView(nextView);
                } else if (autoclose) {
                    element.addClass('hidden');
                    scope.$emit('hidePicker');
                } else {
                    prepareViewData();
                }
            };

            function setDate(date) {
                if (date) {
                    scope.model = date;
                    if (ngModel) {
                        ngModel.$setViewValue(date.format());
                    }
                }
                scope.$emit('setDate', scope.model, scope.view);

                //This is duplicated in the new functionality.
                if (scope.callbackOnSetDate) {
                    scope.callbackOnSetDate(attrs.datePicker, scope.date);
                }
            }

            function update() {
                var view = scope.view;
                datePickerUtils.setParams(tz);

                if (scope.model && !arrowClick) {
                    scope.date = createMoment(scope.model);
                    arrowClick = false;
                }

                var date = scope.date;

                switch (view) {
                    case 'year':
                        scope.years = datePickerUtils.getVisibleYears(date);
                        break;
                    case 'month':
                        scope.months = datePickerUtils.getVisibleMonths(date);
                        break;
                    case 'date':
                        scope.weekdays = scope.weekdays || datePickerUtils.getDaysOfWeek();
                        scope.weeks = datePickerUtils.getVisibleWeeks(date);
                        break;
                    case 'hours':
                        scope.hours = datePickerUtils.getVisibleHours(date);
                        break;
                    case 'minutes':
                        scope.minutes = datePickerUtils.getVisibleMinutes(date, step);
                        break;
                }

                prepareViewData();
            }

            function watch() {
                if (scope.view !== 'date') {
                    return scope.view;
                }
                return scope.date ? scope.date.month() : null;
            }

            scope.$watch(watch, update);

            if (scope.watchDirectChanges) {
                scope.$watch('model', function () {
                    arrowClick = false;
                    update();
                });
            }

            function prepareViewData() {
                var view = scope.view,
                  date = scope.date,
                  classes = [], classList = '',
                  i, j;
                
               // datePickerUtils.setParams(tz);

                if (view === 'date') {
                    var weeks = scope.weeks, week;
                    for (i = 0; i < weeks.length; i++) {
                        week = weeks[i];
                        classes.push([]);
                        for (j = 0; j < week.length; j++) {
                            classList = '';
                            if (datePickerUtils.isSameDay(date, week[j])) {
                                classList += 'active';
                            }
                            if (isNow(week[j], view)) {
                                classList += ' now';
                            }
                            //if (week[j].month() !== date.month()) classList += ' disabled';
                            if (!inRange(week[j])) {
                                classList += ' disabled';
                            }
                            if (inValidRange(week[j])) {
                                classList += ' range';
                            }
                            classes[i].push(classList);
                        }
                    }
                } else {
                    var params = datePickerConfig.viewConfig[view],
                        dates = scope[params[0]],
                        compareFunc = params[1];

                    for (i = 0; i < dates.length; i++) {
                        classList = '';
                        if (datePickerUtils[compareFunc](date, dates[i])) {
                            classList += 'active';
                        }
                        if (isNow(dates[i], view)) {
                            classList += ' now';
                        }
                        if (!inValidRange(dates[i])) {
                            classList += ' disabled';
                        }
                        classes.push(classList);
                    }
                }
                scope.classes = classes;
            }

            scope.next = function (delta) {
                var date = moment(scope.date);
                delta = delta || 1;
                switch (scope.view) {
                    case 'year':
                        /*falls through*/
                    case 'month':
                        date.year(date.year() + delta);
                        break;
                    case 'date':
                        date.month(date.month() + delta);
                        break;
                    case 'hours':
                        /*falls through*/
                    case 'minutes':
                        date.hours(date.hours() + delta);
                        break;
                }
                date = clipDate(date);
                if (date) {
                    scope.date = date;
                    setDate(date);
                    arrowClick = true;
                    update();
                }
            };

            function inValidRange(date) {
                if (startDate === false || endDate === false) {
                    return false;
                }
                if (!inRange(startDate) || !inRange(endDate)) {
                    return false;
                }
                
                var valid = true;
                if (startDate && startDate.isAfter(date)) {
                    valid = isSame(startDate, date);
                }
                if (endDate && endDate.isBefore(date)) {
                    valid &= isSame(endDate, date);
                }
                return valid;
            }

            function inRange(date) {
                var fDate = date.format('YYYY-MM-DD');
                if (maxDate === false || minDate === false) {
                    return false;
                }
                if (fDate >= minDate.format('YYYY-MM-DD') && fDate <= maxDate.format('YYYY-MM-DD')) {
                    return true;
                }
                return false;
            }

            function isSame(date1, date2) {
                return date1.isSame(date2, datePickerConfig.momentNames[scope.view]) ? true : false;
            }

            function clipDate(date) {
                if (startDate && startDate.isAfter(date)) {
                    return startDate;
                } else if (endDate && endDate.isBefore(date)) {
                    return endDate;
                } else {
                    return date;
                }
            }

            function isNow(date, view) {
                var is = true;

                switch (view) {
                    case 'minutes':
                        is &= ~~(now.minutes() / step) === ~~(date.minutes() / step);
                        /* falls through */
                    case 'hours':
                        is &= now.hours() === date.hours();
                        /* falls through */
                    case 'date':
                        is &= now.date() === date.date();
                        /* falls through */
                    case 'month':
                        is &= now.month() === date.month();
                        /* falls through */
                    case 'year':
                        is &= now.year() === date.year();
                }
                return is;
            }

            scope.prev = function (delta) {
                return scope.next(-delta || -1);
            };
        }

        return {
            require: '?ngModel',
            template: '<div ng-include="template"></div>',
            scope: {
                //в качестве моделе использует параметры: dates.start/dates.end
                model: '=datePicker',
                after: '=?',
                before: '=?'
            },
            link: link
        };
    }]);
})();