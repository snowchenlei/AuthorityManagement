var moduleId;
var key;
$(function () {
    tree.init();
    $('#moduleTree').parent('.modal-body').height('360px');
});
var tree = {
    init: function () {
        var setting = {
            data: {//表示tree的数据格式
                simpleData: {
                    enable: true,//表示使用简单数据模式
                    idKey: "id",//设置之后id为在简单数据模式中的父子节点关联的桥梁
                    pidKey: "pId",//设置之后pid为在简单数据模式中的父子节点关联的桥梁和id互相对应
                    rootId: "null"//pid为null的表示根节点
                }
            },
            view: {//表示tree的显示状态
                selectMulti: true//表示禁止多选
            },
            callback: {//表示tree的一些事件处理函数
                onClick: treeNode_Click,
                //onCheck: handlerCheck
            }
        }
        $.fn.zTree.init($("#moduleTree"), setting, treeJson);
    }
};
function treeNode_Click(event, treeId, treeNode) {
    if (treeNode.isParent) {
        return false;
    }
    moduleId = treeNode.id;
    var hidElementId = $('#hidElementId').val();
    var data = { 'moduleId': moduleId };
    data[key] = hidElementId;
    $.get(absoluteUrl + '/Elements', data, function (data) {
        $('#elements').html(data);
    });
}
//保存
function CallSave(firstId, secondId) {
    var data = { 'elementId': secondId, 'moduleId': moduleId };
    data[key] = firstId;
    $.post(absoluteUrl + '/ModuleElements', data, function (data) {
        if (data.State == 1) {
            toastr.success(data.Message);
            $('#close_edit').click();
            table.myViewModel.refresh();
        } else {
            toastr.error(data.Message);
        }
    });
}