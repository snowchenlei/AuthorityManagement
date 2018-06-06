var absoluteUrl;
var title;

var columns = [
    { checkbox: true },
    { field: 'Id', title: 'Id', visible: false },
    { field: 'UserName', title: '用户名' },
    { field: 'IP', title: 'IP', formatter: ipFormatter },
    { field: 'OS', title: '系统' },
    { field: 'Device', title: '设备' },
    { field: 'UserAgent', title: '设备标识' },
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
        userName: $('#txt_search_userName').val()
    };
}
// #endregion