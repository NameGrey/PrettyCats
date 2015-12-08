'use strict';

var serviceId = 'datePickerService';
angular.module('bookItApp').factory(serviceId, [datePickerService]);

function datePickerService() {
    var service = {
        getDaysOfWeek: getDaysOfWeek,
        getVisibleWeeks: getVisibleWeeks,
        inValidRange: inValidRange,
        inRange: inRange,
        datePickerTemplate: "templates/datepicker.html",
        isAfter: function (model, date) {
            return model && model.unix() >= date.unix();
        },
        isAfterToday: function (model) {
            return model && !(this.isToday(model) || (model.format('YYYY-MM-DD') > moment().format('YYYY-MM-DD')));
        },
        isBefore: function(model, date) {
            return model.unix() <= date.unix();
        },
        isToday: function(date) {
            return date.format('YYYY-MM-DD') === moment().format('YYYY-MM-DD');
        },
        isSameDay: function (model, date) {
            return model && date && model.unix() == date.unix();
        },
        isInOccupied: function (occupied, date) {
            if (occupied) {
                var occ = JSON.parse(occupied);
                var d = date.format('YYYY-MM-DD');
                for (var i = 0; i < occ.length; i++) {
                    if (occ[i].start && occ[i].end) {
                        var start = moment(occ[i].start).format('YYYY-MM-DD');
                        var end = moment(occ[i].end).format('YYYY-MM-DD');
                        if (!(d >= start && d <= end)) {
                            continue;
                        } else {
                            return true;
                        }
                    }
                }
            }
            return false;
        },
        isContainBooked: function (occupied, start, end) {
            if (occupied) {
                var occ = JSON.parse(occupied);
                var st = start.format('YYYY-MM-DD');
                var ed = end.format('YYYY-MM-DD');
                for (var i = 0; i < occ.length; i++) {
                    if (occ[i].start && occ[i].end) {
                        var bookStart = moment(occ[i].start).format('YYYY-MM-DD');
                        var bookEnd = moment(occ[i].end).format('YYYY-MM-DD');
                        if (!(((st <= bookStart && ed >= bookStart) || (ed >= bookEnd && st <= bookEnd)))) {
                            continue;
                        } else {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }


    function createNewDate(year, month, day) {
        var newDate = new Date(year | 0, month | 0, day | 0, 0, 0, 0, 0);
        return moment(newDate);
    };

    function getDaysOfWeek(m) {
        m = m ? m : moment().day(0);

        var year = m.year(),
            month = m.month(),
            day = m.date(),
            days = [];

        for (var i = 0; i < 7; i++) {
            days.push(createNewDate(year, month, day));
            day++;
        }
        
        return days;
    }

    function getVisibleWeeks(m) {
        m = moment(m);
        var startYear = m.year(),
            startMonth = m.month();
        
        m.date(1);
        var day = m.day();
        if (day === 0) {
            m.date(-6);
        } else {
            m.date(1 - day);
        }

        var weeks = [];

        while (weeks.length < 6) {
            if (m.year() === startYear && m.month() > startMonth) {
                break;
            }
            weeks.push(getDaysOfWeek(m));
            m.add(7, 'd');
        }
        return weeks;
    }

    function inRange(max, min, date) {
        if (!max || !min || !date) {
            return false;
        }
        if (date >= min && date <= max) {
            return true;
        }
        return false;
    }

    function inValidRange(start, end, date) {
        if (start === false || end === false) {
            return false;
        }
        if (!inRange(start) || !inRange(end)) {
            return false;
        }

        var valid = true;
        if (start && start.isAfter(date)) {
            valid = start.isSame(date);
        }
        if (end && end.isBefore(date)) {
            valid &= end.isSame(date);
        }
        return valid;
    }

    return service;
}
