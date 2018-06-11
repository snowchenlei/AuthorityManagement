var absoluteUrl;
var title;

var columns = [
    { checkbox: true },
    { field: 'Id', title: 'Id', visible: false },
    { field: 'Message', title: '推送信息' },
    { field: 'SourceUserName', title: '推送者姓名' },
    { field: 'TargetUserName', title: '接收者姓名' },
    { field: 'AddTime', title: '添加时间', sortable: true }
];

$(function () {
    //$('#modifyModal').modal('show');
    //1、初始化表格
    table.init(columns);
    operate.init();
    //3、pannel初始化
    loadPanel();
});

// #region bootstrap-table
//搜索
function queryParams(params) {
    return {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
        pageSize: params.limit,   //页面大小
        pageIndex: params.offset / params.limit + 1,  //页码
        sort: params.sort,
        order: params.order,
        phoneNumber: $('#txt_search_phoneNumber').val(),
        userName: $('#txt_search_userName').val(),
        startTime: DateFormat("yyyy/MM/dd HH:mm", $("#startTime").data("datetimepicker").getDate()),
        endTime: DateFormat("yyyy/MM/dd HH:mm", $("#endTime").data("datetimepicker").getDate())
    };
}
// #endregion

//#region operate
function operateInit() {
    operate.push();
}
operate.push = function () {
    var message = $('#message').val();
    $('#btnPush').click(function (data) {
        $.get(absoluteUrl + "/Push", function (data) {
            $('#modifyContent').html(data);
            $("#modifyModal").modal().on("shown.bs.modal", function () {
            }).on('hidden.bs.modal', function () {
                //关闭弹出框的时候清除绑定(这个清空包括清空绑定和清空注册事件)
                ko.cleanNode(document.getElementById("modifyModal"));
            });
        });
    });    
}

function saveSuccess(data) {
    var message = data.Data;
    if (data.State === 1) {        
        // 调用服务器端集线器的Send方法
        top.chat.server.send(message);

        toastr.success(data.Message);
        $('#close_edit').click();
        table.myViewModel.refresh();
    } else {
        if (data.Data !== undefined && data.Data.length > 0) {
            var responseData = data.Data;
            for (var i = 0, max = responseData.length; i < max; i++) {
                $('#' + responseData[i]["Key"]).siblings('.text-danger').text(responseData[i]["Errors"]);
            }
        } else {
            toastr.error(data.Message);
        }
    }
}