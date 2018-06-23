var absoluteUrl;
var title;
var modules;

var columns = [
    { checkbox: true },
    { field: 'Id', title: 'Id', visible: false },
    { field: 'Name', title: '模块名称' },
    { field: 'Url', title: '请求地址' },
    { field: 'Sort', title: '排序', sortable: true },
    { field: 'IconName', title: '图标名称', formatter: iconFormatter },
    { field: 'ParentName', title: '父模块名称' },
    { field: 'ParentID', title: '父模块Id', visible: false },
    { field: 'AddTime', title: '添加时间', sortable: true }
];
$(function () {
    //1、搜索模块初始化
    module.initSearch();
    //1、初始化表格
    table.init(columns);
    //2.初始化Button的点击事件
    operate.init();
    //3、pannel初始化
    loadPanel();
    //4、时间初始化
    setDate();
});

// #region operate
function operateInit() {
    operate.add();
    operate.edit();
    operate.del();
    operate.setModuleElements();
    operate.EditModel = {
        Id: ko.observable()
        ,Name: ko.observable()
        ,Url: ko.observable()
        ,Sort: ko.observable()
        ,IconName: ko.observable()
        ,ParentName: ko.observable()
        ,ParentID: ko.observableArray()
    };
};
operate.setModuleElements = function () {
    $('#btnSetModuleElements').on('click', function () {
        var arrselectedData = table.myViewModel.getSelections();
        if (!operate.check(arrselectedData)) { return; }
        loadElementPage(arrselectedData[0].Id, this);
        $("#modifyModal").modal().on("shown.bs.modal", function () {
        }).on('hidden.bs.modal', function () {
            //关闭弹出框的时候清除绑定(这个清空包括清空绑定和清空注册事件)
            ko.cleanNode(document.getElementById("modifyModal"));
        });
    })
};
//获取空绑定
function getEmptyModel() {
    return {
        Id: ko.observable()
        ,Name: ko.observable()
        ,Url: ko.observable()
        ,Sort: ko.observable()
        ,IconName: ko.observable()
        ,ParentName: ko.observable()
        ,ParentID: ko.observableArray()
    };
}
// #endregion

var module = {
    init: function () {
        module.initParents();
    },
    initSearch: function () {
        module.initSearchMosules();
    },
    initParents: function () {
        $('#ParentId').select2({
            language: "zh-CN",// 指定语言为中文，国际化才起效
            //placeholder: '请选择',
            width: '100%',
            data: modules
        });
    },
    initSearchMosules: function () {
        $('#sel_search_module').select2({
            language: "zh-CN",// 指定语言为中文，国际化才起效
            //placeholder: '请选择',
            width: '100%',
            data: modules
        });
    }
}
//搜索
function queryParams(params) {
    return {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
        pageSize: params.limit,   //页面大小
        pageIndex: params.offset / params.limit + 1,  //页码
        sort: params.sort,
        order: params.order,
        moduleName: $('#txt_search_moduleName').val(),
        parentId: $('#sel_search_module').val(),
        startTime: DateFormat("yyyy/MM/dd HH:mm", $("#startTime").data("datetimepicker").getDate()),
        endTime: DateFormat("yyyy/MM/dd HH:mm", $("#endTime").data("datetimepicker").getDate())
    };
}
//加载角色设置页面
function loadElementPage(id, obj) {
    var action = $(obj).data('action');
    $.get(absoluteUrl + action, { 'moduleId': id }, function (data) {
        setData(data, id);
    });
}