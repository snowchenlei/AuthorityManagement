﻿//格式化日期 DateFormat('yyyy_MM_dd hh:mm:ss:SS 星期w 第q季度')  
function DateFormat(format, date) {
    if (!date) {
        date = new Date();
    }
    var Week = ['日', '一', '二', '三', '四', '五', '六'];
    var o = {
        "y+": date.getYear(), //year  
        "M+": date.getMonth() + 1, //month   
        "d+": date.getDate(), //day   
        "h+": date.getHours(), //hour   
        "H+": date.getHours(), //hour  
        "m+": date.getMinutes(), //minute   
        "s+": date.getSeconds(), //second   
        "q+": Math.floor((date.getMonth() + 3) / 3), //quarter   
        "S": date.getMilliseconds(), //millisecond   
        "w": Week[date.getDay()]
    }
    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (date.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length === 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
}

//时间间隔
function DateAdd(type, value) {
    var date = new Date();
    switch (type) {
        case "y":
            date.setYear(date.getFullYear() + value);
            date = new Date(date.getTime());
            break;
        case "M":
            date.setMonth(date.getMonth() + value);
            date = new Date(date.getTime());
            break;
        case "d":
            date.setDate(date.getDate() + value);
            date = new Date(date.getTime());
            break;
        case "h":
            date.setHours(date.getHours() + value);
            date = new Date(date.getTime());
            break;
        case "m":
            date.setMinutes(date.getMinutes() + value);
            date = new Date(date.getTime());
            break;
    }
    return date;
}