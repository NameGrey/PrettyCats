
angular.module('bookItApp')
    .directive('customfilter', function() {
        return {
            restrict: 'E',
            templateUrl: 'templates/filter.html', // markup for filter
            controller: function($scope, bookingService) {
                bookingService.getCategories()
                    .success(function(data, status, headers, config) {
                        $scope.categories = data;
                    })
                    .error(catchServiceError);
            }

        };
    })
    .directive('timeslot', function() {
        return {
            restrict: 'E',
            templateUrl: 'templates/timeslot.html', // markup
            scope: {
                slot: '=', // allows data to be passed into directive from controller scope
                minDate: '=', //for constantly dates
                maxDate: '=',
                occupiedSlots: '='
            }
        };
    })
    .directive('dateformat', function() {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function(scope, element, attr, ngModel) {

                function fromUser(text) {
                    return new Date(text).toLocaleDateString("en-US");
                }

                function toUser(text) {
                    if (text) {
                        var date = new Date(text);
                        return date.toLocaleDateString("en-US");
                    }
                    return "";
                };

                ngModel.$parsers.push(fromUser);
                ngModel.$formatters.push(toUser);
            }
        };
    })
    .directive('datePicker', [
        '$compile', '$document', 'datePickerService', '$templateRequest', function ($compile, $document, datePickerService, $templateRequest) {
            function link(scope, element, attrs) {

                populatelimits();

                if (attrs.booked == 'true') {
                    element.attr('disabled', 'disabled');
                }
               
                if (datePickerService.isAfterToday(moment(scope.model))) {
                    scope.date = moment();
                    setModel(scope.date);
                } else {
                    scope.date = scope.model;
                }
                scope.weeks = datePickerService.getVisibleWeeks(scope.date);
                scope.weekdays = datePickerService.getDaysOfWeek();

                prepareViewData();

                scope.next = function (delta) {
                    var date = moment(scope.date);
                    
                    date.month(date.month() + (delta || 1));

                    if (date) {
                        scope.date = date;
                        scope.weekdays = scope.weekdays || datePickerService.getDaysOfWeek();
                        scope.weeks = datePickerService.getVisibleWeeks(date);
                        prepareViewData();
                    }
                };
                scope.prev = function () {
                    return scope.next(-1);
                };
                scope.selectDate = function (date, classes) {
                    if (validateDate(date, classes)) {
                        setModel(date);
                        prepareViewData();
                    }
                    return false;
                }

                function setModel(date) {
                    if (scope.model) {
                        scope.model = date.format('YYYY-MM-DD');
                    }
                }

                function populatelimits(date) {
                    if (date) {
                        if (attrs.name == 'start') {
                            scope.start = moment(date);
                            scope.end = moment(attrs.end);
                        } else if (attrs.name == 'end') {
                            scope.start = moment(attrs.start);
                            scope.end = moment(date);
                        }
                    } else {
                        scope.end = moment(attrs.end);
                        scope.start = moment(attrs.start);
                    }
                    scope.max = moment(attrs.max),
                    scope.min = moment(attrs.min);
                }

                function validateDate(date, classes) {
                    populatelimits(date);
                    if (classes.indexOf("booked") != -1 || classes.indexOf("disabled") != -1) {
                        return false;
                    }
                    if (attrs.name == 'start') {
                        if (scope.end && datePickerService.isBefore(scope.end, date)) {
                            return false;
                        }
                        if (datePickerService.isContainBooked(attrs.occSlots, date, scope.end)) {
                            return false;
                        }
                    } else if (attrs.name == 'end') {
                        if (scope.start && datePickerService.isAfter(scope.start, date)) {
                            return false;
                        }
                        if (datePickerService.isContainBooked(attrs.occSlots, scope.start, date)) {
                            return false;
                        }
                    }
                    return true;
                }

                function prepareViewData() {
                    var week, classes = [], classList = '', i, j;
                    
                    for (i = 0; i < scope.weeks.length; i++) {
                        week = scope.weeks[i];
                        classes.push([]);
                        for (j = 0; j < week.length; j++) {
                            classList = '';
                            if (datePickerService.isSameDay(scope.start, week[j]) || datePickerService.isSameDay(scope.end, week[j])) {
                                classList += 'limit';
                            }
                            //не разрешаем выбирать задним числом
                            if (!datePickerService.inRange(scope.max, scope.min, week[j]) || datePickerService.isAfterToday(week[j])) {
                            //if (!datePickerService.inRange(scope.max, scope.min, week[j])) {
                                classList += ' disabled';
                            }
                            if (datePickerService.inRange(scope.end, scope.start, week[j])) {
                                classList += ' range';
                            }
                            if (datePickerService.isInOccupied(attrs.occSlots, week[j])) {
                                classList += ' booked';
                            }
                            if (datePickerService.isToday(week[j])) {
                                classList += ' now';
                            }
                            classes[i].push(classList);
                        }
                    }
                    scope.classes = classes;
                }

                var picker = null;
                element.on('focus', function () {
                    populatelimits();
                    prepareViewData();
                    var pos = angular.extend(element.offset(), { height: element[0].offsetHeight });                    

                    $templateRequest(datePickerService.datePickerTemplate).then(function (html) {
                        var template = angular.element(html);
                        $document.find('body').append(template);
                        picker = $compile(template)(scope);
                        picker.css({ top: pos.top + pos.height + 5, left: pos.left, display: 'block', position: 'absolute' });

                        picker.bind('mousedown', function (evt) {
                            evt.preventDefault();
                        });
                    });
                });
                element.bind('blur', function () {                  
                    if (picker) {
                        picker.remove();
                        picker = null;
                    }
                });
            }

            return {
                restrict: 'A',
                link: link,
                scope : {
                    model: '=ngModel'
                }
            };
        }
    ]);

