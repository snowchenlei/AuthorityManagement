﻿loadTianqi();
function loadTianqi() {
    $('#tianqi').attr('src', 'http://tianqiapi.com/api.php?style=ta&skin=pitaya&fontsize=16&paddingtop=10&color=FFF');
}
function logout() {
    $.post('/Account/Logout', function (data) {
        if (data.State == 1) {
            window.location.href = "/Account/Login";
        } else {
            toastr.error(data.Message);
        }
    });
}
function fullScreen() {
    $('body').fullScreen();
}
$(window).resize(function () {
    ChangeIFrame();
});
setInterval(function () {
    var date = new Date();
    $('#date').text(DateFormat("yyyy年MM月dd日", date));
    $('#now').text(DateFormat(" 星期w HH:mm:ss", date));
}, 1000);

function ChangeIFrame() {
    var height = getHeight();
    $('.contentFrame').css('height', height);
}
ChangeIFrame();

//获取主体内容(iframe)的高度
function getHeight() {
    return document.documentElement.clientHeight
        - (parseInt($('.footer').css('height'))
            + parseInt($('#navbar').css('height'))
            + parseInt($('#tabHeader').css('height')))
        - 5;
}

//参数设置，若用默认值可以省略以下面代
toastr.options = {
    closeButton: true, //是否显示关闭按钮
    debug: false, //是否使用debug模式
    progressBar: true,//是否显示进度条
    positionClass: "toast-top-center",//弹出窗的位置
    showDuration: "300",//显示的动画时间
    hideDuration: "1000",//消失的动画时间
    timeOut: "2000", //展现时间
    extendedTimeOut: "1000",//加长展示时间
    showEasing: "swing",//显示时的动画缓冲方式
    hideEasing: "linear",//消失时的动画缓冲方式
    showMethod: "fadeIn",//显示时的动画方式
    hideMethod: "fadeOut" //消失时的动画方式
}

// #region 导航栏辅助
//删除index页面输出缓存
$('.remove-cache').click(function () {
    $.get('/api/cache', function (data) {
        debugger
        if (data.State == 1) {
            toastr.success(data.Message);
        } else {
            toastr.error(data.Message);
        }
    });
});
//重载页面
$('.reload-page').click(function () {
    document.getElementById($('#tabHeader > .active:first').data('id')).children[0].contentWindow.location.reload(true);
});
//关闭其它标签
$('.close-other').click(function () {
    $('#tabHeader li:first').nextAll(':not(.active)').each(function () {
        if (!$(this).hasClass('active')) {
            $('#' + $(this).data('id')).remove();
            $(this).remove();
        }
    });
});
//关闭所有标签
$('.close-all').click(function () {
    $('#tabHeader li:first').nextAll().each(function () {
        $('#' + $(this).data('id')).remove();
        $(this).remove();
    });
    $('#tabHeader li:first a').click();
});
// #endregion

// #region 导航列表
SetNavs();
//刷新列表
$('#sidebarRefresh').click(function () {
    SetNavs();
});

//设置导航
function SetNavs() {
    $.getJSON('/Home/Navs', function (data) {
        if (data.State = 1) {
            $('#navList').html(data.Data);
            ChangeNav();
        }
    });
}
//点击导航
function ChangeNav() {
    $('.nav-list .deepNav').click(function () {
        $('li').removeClass('active');
        $(this).parent('li').addClass('active');
        $(this).parents('.open').addClass('active');
        addTabs({ "id": $(this).data('id'), "title": $(this).data('name'), "close": true, "url": $(this).attr('href'), "height": getHeight() });
    });
}
// #endregion