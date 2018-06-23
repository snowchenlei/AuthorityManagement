var absoluteUrl;
var title;

var columns = [
    { checkbox: true },
    { field: 'Id', title: 'Id', visible: false },
    { field: 'PhoneNumber', title: '手机号' },
    { field: 'Name', title: '名称' },
    { field: 'UserName', title: '用户名' },
    { field: 'Password', title: '密码' },
    { field: 'AddTime', title: '添加时间', sortable: true }
];

$(function () {
    //$('#modifyModal').modal('show');
    //1、初始化表格
    table.init(columns);
    //2、初始化Button的点击事件
    operate.init();
    //3、pannel初始化
    loadPanel();
    //4、时间初始化
    setDate();
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
    operate.add();
    operate.edit();
    operate.del();
    operate.setUserRoles();
    operate.setUserModules();
    operate.setUserElements();
    operate.EditModel = {
        Id: ko.observable(),
        Name: ko.observable(),
        UserName: ko.observable(),
        Password: ko.observable(),
        PhoneNumber: ko.observable(),
        IsCanUse: ko.observable()
    };
}
function getEmptyModel() {
    return {
        Id: ko.observable(),
        Name: ko.observable(),
        UserName: ko.observable(),
        Password: ko.observable(),
        PhoneNumber: ko.observable(),
        IsCanUse: ko.observable()
    };
}
//设置用户角色
operate.setUserRoles = function () {
    $('#btnSetUserRoles').on('click', function () {
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
//设置用户模块
operate.setUserModules = function () {
    $('#btnSetUserModules').on('click', function () {
        var arrselectedData = table.myViewModel.getSelections();
        if (!operate.check(arrselectedData)) { return; }
        loadModulePage(arrselectedData[0].Id, this);
        $("#modifyModal").modal().on("shown.bs.modal", function () {
        }).on('hidden.bs.modal', function () {
            //关闭弹出框的时候清除绑定(这个清空包括清空绑定和清空注册事件)
            ko.cleanNode(document.getElementById("modifyModal"));
        });
    });
};
//设置用户元素
operate.setUserElements = function () {
    $('#btnSetUserElements').on('click', function () {
        var arrselectedData = table.myViewModel.getSelections();
        if (!operate.check(arrselectedData)) { return; }
        loadElementsPage(arrselectedData[0].Id, this);
        //$("#modifyModal").modal().on("shown.bs.modal", function () {
        //}).on('hidden.bs.modal', function () {
        //    //关闭弹出框的时候清除绑定(这个清空包括清空绑定和清空注册事件)
        //    ko.cleanNode(document.getElementById("modifyModal"));
        //});
    });
};
//获取空绑定
//#endregion

// #region 页面加载
//加载角色设置页面
function loadRolePage(id, obj) {
    var action = $(obj).data('action');
    $.get(absoluteUrl + action, { 'userID': id }, function (data) {
        setData(data, id);
    });
}
//加载模块设置页面
function loadModulePage(id, obj) {
    var action = $(obj).data('action');
    $.get(absoluteUrl + action, { 'userID': id }, function (data) {
        setData(data, id);
    });
}
//加载元素设置页面
function loadElementsPage(id, obj) {
    var action = $(obj).data('action');
    $.get(absoluteUrl + action, { 'userID': id }, function (data) {
        setData(data, id);
    });
}
// #endregion