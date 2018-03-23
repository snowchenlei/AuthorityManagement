//表格初始化
var table = {
    init: function (url, columns, height, exportOptions) {
        //绑定table的viewmodel
		this.myViewModel = new ko.bootstrapTableViewModel({
			url: url,         //请求后台的URL（*）
			method: 'get',                      //请求方式（*）
			toolbar: '#toolbar',                //工具按钮用哪个容器
			striped: true,                      //是否显示行间隔色
			cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true,                   //是否显示分页（*）
            sortable: true,                     //是否启用排序
            sortOrder: "asc",                   //排序方式
            //导出
            showExport: true,                   //是否显示导出按钮  
            exportDataType: 'basic',     //basic', 'all', 'selected'.
            //exportTypes: ['xls', 'doc', 'json', 'csv', 'txt', 'sql',  'pdf', 'png'],
            exportOptions: exportOptions,

			queryParams: table.queryParams,     //传递参数（*）
			sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
			pageNumber: 1,                       //初始化加载第一页，默认第一页
			pageSize: 10,                       //每页的记录行数（*）
            pageList: [10, 25, 50, 100],        //可供选择的每页的行数（*）
            search: false,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
			//strictSearch: true,               //设置为 true启用全匹配搜索，否则为模糊搜索。
			showColumns: true,                  //是否显示所有的列
            showRefresh: true,                  //是否显示刷新按钮
            singleSelect: false,                 //单选
			minimumCountColumns: 2,             //最少允许的列数
			clickToSelect: true,                //是否启用点击选中行
			height: height,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
			uniqueId: "Id",                     //每一行的唯一标识，一般为主键列
			showToggle: true,                    //是否显示详细视图和列表视图的切换按钮
			cardView: false,                    //是否显示详细视图
			detailView: false,                   //是否显示父子表
			columns: columns
		});
		ko.applyBindings(this.myViewModel, document.getElementById("tb-body"));
    },
    //请求参数
    queryParams: function (params) {
        return queryParams(params);
    }
};
//导出方式改变
$("#sel_exportoption").change(function () {
    $('#tb-body').bootstrapTable('refreshOptions', {
        exportDataType: $(this).val()
    });
});
//加载按钮
function loadMenuHeader(moduleId) {
    $.getJSON('/Home/MenuHeader', { 'moduleId': moduleId }, function (data) {
        $('#toolbar').html(data);
    });
}
//模态窗数据修改
function callBackEditPage(data, title) {
    $('#modelTitle').text(title);
    if ($('#modelForm')[0] === undefined) {
        $('#modifyContent').html(data);
    }
}