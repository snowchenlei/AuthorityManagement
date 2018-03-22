function setDate() {
    $('#startTime')
        .datetimepicker()
        .on('changeDate', function (e) {
            $('#endTime').datetimepicker('setStartDate', e.date);
        });
    $('#endTime')
        .datetimepicker()
        .on('changeDate', function (e) {
            $('#startTime').datetimepicker('setEndDate', e.date);
        });
}