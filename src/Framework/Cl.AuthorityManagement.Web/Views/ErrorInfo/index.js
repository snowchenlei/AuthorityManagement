var absoluteUrl;
var title;
var applications;

var columns = [
    { checkbox: true },
    { field: 'Id', title: 'Id', visible: false },
    { field: 'Head', title: '消息头', width: '12%', formatter: textFormatter },
    { field: 'Controller', title: '控制器' },
    { field: 'Action', title: '方法' },
    { field: 'HttpMethod', title: '请求方式' },
    { field: 'IP', title: 'IP', formatter: ipFormatter },
    { field: 'RequestMessage', title: '请求信息', width: '15%', formatter: jsonFormatter },
    { field: 'Exception', title: '异常信息', width: '15%', formatter: textFormatter },
    { field: 'InnerException', title: '内部异常信息', width: '15%', formatter: textFormatter },
    { field: 'Description', title: '描述信息', width: '15%', formatter: textFormatter, visible: false },
    { field: 'ApplicationType', title: '日志类型', formatter: applicationFormatter },
    { field: 'AddTime', title: '添加时间', sortable: true }
];
$(function () {
    //1、搜索模块初始化
    module.initSearch();
    //1、初始化表格
    table.init(columns);
    //3、pannel初始化
    loadPanel();
});

var module = {
    initSearch: function () {
        this.initSearchApplication();
    },
    initSearchApplication: function () {
        $('#sel_search_application').select2({
            language: "zh-CN",// 指定语言为中文，国际化才起效
            placeholder: '请选择',
            width: '100%',
            data: applications
        });
    }
}
// #region bootstrap-table
//搜索
function queryParams(params) {
    return {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
        pageSize: params.limit,   //页面大小
        pageIndex: params.offset / params.limit + 1,  //页码
        sort: params.sort,
        order: params.order,
        controller: $('#txt_search_controller').val(),
        action: $('#txt_search_action').val(),
        application: $('#sel_search_application').val(),
        requestMessage: $('#txt_search_requestMessage').val(),
        exception: $('#txt_search_exception').val(),
        httpMethod: $('#txt_search_httpMethod').val()
    };
}
        // #endregion