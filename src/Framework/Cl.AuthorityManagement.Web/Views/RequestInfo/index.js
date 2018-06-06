var absoluteUrl;
var title;

var columns = [
    { checkbox: true },
    { field: 'Id', title: 'Id', visible: false },
    { field: 'Url', title: '请求地址', width: '20%' },
    { field: 'Head', title: '消息头' },
    { field: 'RequestMessage', title: '请求信息', width: '30%', formatter: jsonFormatter },
    { field: 'ResponseMessage', title: '响应信息', width: '30%', formatter: jsonFormatter },
    { field: 'AddTime', title: '添加时间', sortable: true }
];
$(function () {
    //1、初始化表格
    table.init(columns);
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
        header: $('#txt_search_head').val(),
        requestMessage: $('#txt_search_requestMessage').val()
    };
}
        // #endregion