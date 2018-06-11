// 引用自动生成的集线器代理
chat = $.connection.pushHub;
// 定义服务器端调用的客户端sendMessage来显示新消息
chat.client.showMessage = function (message) {
    // 向页面添加消息
    toastr.info(message, '推送', { positionClass: 'toast-bottom-right' })
};
// 开始连接服务器
$.connection.hub.start().done(function () {
    connected = true;
}).fail(function () {
    toastr.error(message);
});