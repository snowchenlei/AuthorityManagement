//格式化
//邮箱格式化
function emailFormatter(value, row, index) {
    return "<a href='mailto:" + value + "' title='单击发送邮件'>" + value + "</a>";
}
//手机号格式
function phoneFormatter(value, row, index) {
    return "<a href='tel:" + value + "' title='单击拨打电话'>" + value + "</a>";
}
//图标格式化
function iconFormatter(value, row, index) {
    return "<i class='glyphicon " + value + "'></i>";
}
//应用格式化
function applicationFormatter(value, row, index) {
    for (var i = 0; i < applications.length; i++) {
        if (applications[i].id == value) {
            return applications[i].text;
        }
    }
}
//JSON格式化
function jsonFormatter(value, row, index) {
    return '<a href="#" class="wi" onclick=showJson(this) title=\'' + value + '\'>' + value + '</a>';
}
//文本格式化
function textFormatter(value, row, index) {
    if (value != null) {
        value = value.replace(/'/g, '"');
        return '<a href="#" class="wi" onclick=showText(this) title=\'' + value + '\'>' + value + '</a>';
    }
}
//IP格式化
function ipFormatter(value, row, index) {
    if (ipRegex.test(value)) {
        return '<a href="#" class="wi" onclick=showLocation("' + value + '") title=\'' + value + '\'>' + value + '</a>';
    } else {
        return value;
    }
}