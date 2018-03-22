var addTabs = function (options) {
    //var rand = Math.random().toString();
    //var id = rand.substring(rand.indexOf('.') + 1);
    var url = window.location.protocol + '//' + window.location.host;
    options.url = url + options.url;
    id = "tab_" + options.id;
    $(".active").removeClass("active");
    //如果TAB不存在，创建一个新的TAB
    if (!$("#" + id)[0]) {
        //固定TAB中IFRAME高度
        mainHeight = options.height;//$(document.body).height() - 90;
        //创建新TAB的title
        title = '<li role="presentation" id="tab_' + id + '" data-id="' + id + '"><a href="#' + id + '" aria-controls="' + id + '" role="tab" data-toggle="tab">' + options.title;
        //是否允许关闭
        if (options.close) {
            title += ' <i class="glyphicon glyphicon-remove" style="cursor: pointer;" tabclose="' + id + '"></i>';
        }
        title += '</a></li>';
        //是否指定TAB内容
        if (options.content) {
            content = '<div role="tabpanel" class="tab-pane" id="' + id + '">' + options.content + '</div>';
        } else {//没有内容，使用IFRAME打开链接
            content = '<div role="tabpanel" class="tab-pane" id="' + id + '"><iframe src="' + options.url + '" class="contentFrame" width="100%" id="' + id + '" name="' + id + '" height="' + mainHeight +
                '" frameborder="no" border="0" marginwidth="0" marginheight="0" scrolling="yes" allowtransparency="yes"></iframe></div>';
        }
        //加入TABS
        $(".nav-tabs").append(title);
        $(".tab-content").append(content);
    }
    //激活TAB
    $("#tab_" + id).addClass('active');
    $("#" + id).addClass("active");
};
var closeTab = function (id) {
    //如果关闭的是当前激活的TAB，激活他的前一个TAB
    if ($("li.active").attr('id') == "tab_" + id) {
        var $prev = $("#tab_" + id).prev();
        if ($prev.length > 0) {
            $prev.addClass('active');
            $("#" + $prev.data('id')).prev().addClass('active');
        } else {
            var $last = $("#tab_" + id).parent().find('>li:last');
            $last.addClass('active');
            $("#tab_" + id).prev().addClass('active');
            $("#" + $last.data('id')).addClass('active');
        }        
    }
    //关闭TAB
    $("#tab_" + id).remove();
    $("#" + id).remove();
};
$(function () {
    mainHeight = $(document.body).height() - 45;
    $('.main-left,.main-right').height(mainHeight);
    $("[addtabs]").click(function () {
        addTabs({ id: $(this).attr("id"), title: $(this).attr('title'), close: true });
    });

    $(".nav-tabs").on("click", "[tabclose]", function (e) {
        id = $(this).attr("tabclose");
        closeTab(id);
    });
});
