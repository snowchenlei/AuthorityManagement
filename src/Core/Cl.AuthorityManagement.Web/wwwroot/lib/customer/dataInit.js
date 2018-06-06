//表格初始化
var table = {
    init: function (columns, detailView, height) {
        //绑定table的viewmodel
		this.myViewModel = new ko.bootstrapTableViewModel({
            url: absoluteUrl + '/Load',         //请求后台的URL（*）
			method: 'get',                      //请求方式（*）
			toolbar: '#toolbar',                //工具按钮用哪个容器
			striped: true,                      //是否显示行间隔色
			cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true,                   //是否显示分页（*）
            sortable: true,                     //是否启用排序
            sortOrder: "desc",                   //排序方式
            //导出
            showExport: true,                   //是否显示导出按钮  
            exportDataType: 'basic',     //basic', 'all', 'selected'.
            //exportTypes: ['xls', 'doc', 'json', 'csv', 'txt', 'sql',  'pdf', 'png'],
            exportOptions: {
                fileName: title,                //文件名称设置
                worksheetName: 'sheet1',        //表格工作区名称
                tableName: title,
                excelstyles: ['background-color', 'color', 'font-size', 'font-weight'],
            },
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
            columns: columns,
            detailView: detailView,             //是否显示父子表
            onExpandRow: table.onExpandRow,
            onLoadSuccess: table.onLoadSuccess, //加载成功
		});
		ko.applyBindings(this.myViewModel, document.getElementById("tb-body"));
    },
    //请求参数
    queryParams: function (params) {
        return queryParams(params);
    }
};
var childTable = {
    init: function (index, row, $detail, $cur_table, url, columns) {
        //加载从表数据
        $cur_table.bootstrapTable({
            url: url,
            method: 'get',
            queryParams: function (params) {
                return queryChildParams(params, row);
            },
            //ajaxOptions: {},
            clickToSelect: true,
            detailView: false,//父子表
            uniqueId: "Id",
            striped: true,
            columns: columns
        });
    }
}

//导出方式改变
$("#sel_exportoption").change(function () {
    $('#tb-body').bootstrapTable('refreshOptions', {
        exportDataType: $(this).val()
    });
});
//模态窗数据修改
function callBackEditPage(data, title) {
    $('#modelTitle').text(title);
    if ($('#modelForm')[0] === undefined) {
        $('#modifyContent').html(data);
    }
}

var operate = {
    init: function () {
        operateInit();
    },
    //新增
    add: function () {
        $('#btnAdd').on("click", function () {
            $('#operateType').val(0);
            loadEditPage(this);
            $("#modifyModal").modal().on("shown.bs.modal", function () {
                var oEmptyModel = getEmptyModel();
                if (window.module) {
                    module.init();
                }
                ko.utils.extend(operate.EditModel, oEmptyModel);
                //激活绑定(需要绑定的模型，[绑定的标签作用域])
                ko.applyBindings(operate.EditModel, document.getElementById("modifyModal"));
            }).on('hide.bs.modal', function () {
                destroy();
            }).on('hidden.bs.modal', function () {
                ko.cleanNode(document.getElementById("modifyModal"));
            });
        });
    },
    //编辑
    edit: function () {
        $('#btnEdit').on("click", function () {
            $('#operateType').val(1);
            var arrselectedData = table.myViewModel.getSelections();
            if (!operate.check(arrselectedData)) { return; }
            loadEditPage(this);
            //当有选择行时弹出框
            $("#modifyModal").modal().on("shown.bs.modal", function () {
                if (window.module) {
                    module.init();
                }
                //将选中该行数据有数据Model通过Mapping组件转换为viewmodel
                ko.utils.extend(operate.EditModel, ko.mapping.fromJS(arrselectedData[0]));
                ko.applyBindings(operate.EditModel, document.getElementById("modifyModal"));
                //complete();
                //operate.operateSave();
            }).on('hide.bs.modal', function () {
                destroy();
            }).on('hidden.bs.modal', function () {
                //关闭弹出框的时候清除绑定(这个清空包括清空绑定和清空注册事件)
                ko.cleanNode(document.getElementById("modifyModal"));
            });
        });
    },
    //删除
    del: function () {
        $('#btnDel').on('click', function () {
            var arrselectedData = table.myViewModel.getSelections();
            if (!operate.check(arrselectedData)) { return; }
            var action = $(this).data('action');
            bootbox.confirm({
                size: 'small',
                title: "删除",
                message: "确定要删除吗？",
                callback: function (result) {
                    if (result) {
                        $.post(absoluteUrl + action, { id: arrselectedData[0].Id }, function (data) {
                            if (data.State == 1) {
                                toastr.success(data.Message);
                                table.myViewModel.refresh();
                            } else {
                                toastr.error(data.Message);
                            }
                        });
                    }
                }
            });
        });
    },
    //数据校验
    check: function (arr) {
        if (arr.length <= 0) {
            toastr.warning("请至少选择一行数据");
            return false;
        }
        if (arr.length > 1) {
            toastr.warning("只能编辑一行数据");
            return false;
        }
        return true;
    }
}
//修改模态窗信息
function setData(data, id) {
    $('#modifyContent').html(data);
    $('#hidElementId').val(id);
}
// #region 页面操作
//加载按钮
function loadMenuHeader(moduleId) {
    $.getJSON('/Home/MenuHeader', { 'moduleId': moduleId }, function (data) {
        $('#toolbar').html(data);
    });
}
//加载修改页面
function loadEditPage(obj) {
    var action = $(obj).data('action');
    $.get(absoluteUrl + action, function (data) {
        $('#modifyContent').html(data);
    });
}
//select2解绑
function destroy() {
    $('#modifyContent select').each(function () {
        if ($(this).data('select2') != undefined) {
            $(this).data('select2').destroy();
        }
    })
}
// #endregion

// #region 数据保存
//保存数据
function saveSuccess(data) {
    if (data.State == 1) {
        toastr.success(data.Message);
        $('#close_edit').click();
        table.myViewModel.refresh();
    } else {
        if (data.Data != undefined && data.Data.length > 0) {
            var responseData = data.Data;
            for (var i = 0, max = responseData.length; i < max; i++) {
                $('#' + responseData[i]["Key"]).siblings('.text-danger').text(responseData[i]["Errors"]);
            }
        } else {
            toastr.error(data.Message);
        }
    }
}
//保存失败
function saveFailure(data) {
    toastr.error(data);
}
// #endregion