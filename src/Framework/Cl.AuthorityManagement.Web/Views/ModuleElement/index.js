var absoluteUrl;
var title;
var modules

var columns = [
    { checkbox: true },
    { field: 'Id', title: 'Id', visible: false },
    { field: 'Name', title: '元素名称' },
    { field: 'DomId', title: '元素Id' },
    { field: 'Attr', title: '附加属性' },
    { field: 'Class', title: '元素样式' },
    { field: 'Icon', title: '元素图标', formatter: iconFormatter },
    { field: 'Remark', title: '备注' },
    { field: 'Script', title: '事件名称' },
    { field: 'Type', title: '元素类型' },
    { field: 'Action', title: '方法名' },
    { field: 'Sort', title: '排序', sortable: true },
    { field: 'AddTime', title: '添加时间', sortable: true }
];
$(function () {
    //$('#modifyModal').modal('show');
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
    operate.EditModel = {
        Id: ko.observable(),
        Name: ko.observable(),
        DomId: ko.observable(),
        Attr: ko.observable(),
        Class: ko.observable(),
        Icon: ko.observable(),
        Remark: ko.observable(),
        Action: ko.observable(),
        Script: ko.observable(),
        Type: ko.observable(),
        Sort: ko.observable()
    };
};
//获取空绑定
function getEmptyModel() {
    return {
        Id: ko.observable(),
        Name: ko.observable(),
        DomId: ko.observable(),
        Attr: ko.observable(),
        Class: ko.observable(),
        Icon: ko.observable(),
        Remark: ko.observable(),
        Action: ko.observable(),
        Script: ko.observable(),
        Type: ko.observable(),
        Sort: ko.observable()
    };
}
// #endregion
//搜索
function queryParams(params) {
    return {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
        pageSize: params.limit,   //页面大小
        pageIndex: params.offset / params.limit + 1,  //页码
        sort: params.sort,
        order: params.order,
        name: $('#txt_search_name').val(),
        startTime: DateFormat("yyyy/MM/dd HH:mm", $("#startTime").data("datetimepicker").getDate()),
        endTime: DateFormat("yyyy/MM/dd HH:mm", $("#endTime").data("datetimepicker").getDate())
    };
}