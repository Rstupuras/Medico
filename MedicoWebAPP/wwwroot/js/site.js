// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
var settingsForDate = {
    locale: 'en-gb',
    format: 'LL',
};
var dateFormat = "DD-MM-YYYY";
var NowDate = new Date($.now());
var numberOfDaysToAdd = 4;
var MinDate = new Date();
var settingsForTime = {
    locale: 'en-gb',
    format: 'HH:mm',
    stepping: 60,
};
MinDate.setDate(NowDate.getDate() + numberOfDaysToAdd);
var dd = MinDate.getDate();
var mm = MinDate.getMonth() + 1;
var y = MinDate.getFullYear();
var FormattedDate = dd + '-' + mm + '-' + y;
dateMin = moment(FormattedDate, dateFormat);
$('#datetimepicker4').datetimepicker(settingsForDate);
$('#datetimepicker3').datetimepicker(settingsForDate);