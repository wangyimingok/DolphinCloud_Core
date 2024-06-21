/**
 * common demo
 */

layui.define(function (exports) {
    var $ = layui.$
        , layer = layui.layer
        , laytpl = layui.laytpl
        , setter = layui.setter
        , view = layui.view
        , admin = layui.admin

    //公共业务的逻辑处理可以写在此处，切换任何页面都会执行
    //……



    //退出
    admin.events.logout = function () {
        ////执行退出接口
        $.ajax({
            url: "/Account/LoginOut",
            type: 'GET',
            success: function (data) {
                if (data.responseCode == 1001) {
                    admin.exit(function () {
                        location.href = '/Account/Login';
                    });
                }
            },
            error: function (data) {
                alert(data);
                console.log(data);
            }
        });
    };

    //对外暴露的接口
    exports('common', {});
});