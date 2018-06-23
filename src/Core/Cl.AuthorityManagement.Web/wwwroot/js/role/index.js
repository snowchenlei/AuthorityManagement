var absoluteUrl;
var title;

var columns = [
    { checkbox: true },
    { field: 'Id', title: 'Id', visible: false },
    { field: 'Name', title: '角色名称' },
    { field: 'Sort', title: '排序', sortable: true },
    { field: 'AddTime', title: '添加时间', sortable: true }
];
$(function () {
    //1、初始化表格
    table.init(columns);
    //2、初始化Button的点击事件
    operate.init();
    //3、pannel初始化
    loadPanel();
    //4、时间初始化
    setDate();
});
function queryParams(params) {
    return {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
        pageSize: params.limit,   //页面大小
        pageIndex: params.offset / params.limit + 1,  //页码
        sort: params.sort,
        order: params.order,
        roleName: $('#txt_search_roleName').val(),
        startTime: DateFormat("yyyy/MM/dd HH:mm", $("#startTime").data("datetimepicker").getDate()),
        endTime: DateFormat("yyyy/MM/dd HH:mm", $("#endTime").data("datetimepicker").getDate())
    };
}

// #region operate
function operateInit() {
    operate.add();
    operate.edit();
    operate.del();
    operate.setRoleModules();
    operate.setRoleElements();
    operate.EditModel = {
        //Id: ko.observable(),
        Name: ko.observable(),
        Sort: ko.observable()
    };
}
operate.setRoleModules = function () {
    $('#btnSetRoleModules').on('click', function () {
        var arrselectedData = table.myViewModel.getSelections();
        if (!operate.check(arrselectedData)) { return; }
        loadRolePage(arrselectedData[0].Id, this);
        $("#modifyModal").modal().on("shown.bs.modal", function () {
        }).on('hidden.bs.modal', function () {
            //关闭弹出框的时候清除绑定(这个清空包括清空绑定和清空注册事件)
            ko.cleanNode(document.getElementById("modifyModal"));
        });
    });
};
operate.setRoleElements = function () {
    $('#btnSetRoleElements').on('click', function () {
        var arrselectedData = table.myViewModel.getSelections();
        if (!operate.check(arrselectedData)) { return; }
        loadElementsPage(arrselectedData[0].Id, this);
        $("#modifyModal").modal().on("shown.bs.modal", function () {
        }).on('hidden.bs.modal', function () {
            //关闭弹出框的时候清除绑定(这个清空包括清空绑定和清空注册事件)
            ko.cleanNode(document.getElementById("modifyModal"));
        });
    });
};
//获取空绑定
function getEmptyModel() {
    return {
        //Id: ko.observable(),
        Name: ko.observable(),
        Sort: ko.observable()
    };
}
// #endregion

//加载模块设置页面
function loadRolePage(id, obj) {
    var action = $(obj).data('action');
    $.get(absoluteUrl + action, { 'roleId': id }, function (data) {
        setData(data, id);
    });
}
//加载元素设置页面
function loadElementsPage(id, obj) {
    var action = $(obj).data('action');
    $.get(absoluteUrl + action, { 'roleId': id }, function (data) {
        setData(data, id);
    });
} 