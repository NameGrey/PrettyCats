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
        isAfterToday: function (model, date) {
            return model && !(this.isToday(model) || (model.format('YYYY-MM-DD') > date.format('YYYY-MM-DD')));
        },
        isBefore: function(model, date) {
            return model.unix() <= date.unix();
        },
        isToday: function(date) {
            return date.format('YYYY-MM-DD') === moment().format('YYYY-MM-DD');
        },
        isSameDay: function (model, date) {
            return model && date && model.unix() == date.unix();
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
