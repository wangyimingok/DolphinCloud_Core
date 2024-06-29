/**
 * user demo    
 */

layui.define(['table', 'dropdown', 'form'], function (exports) {
    var table = layui.table;
    var dropdown = layui.dropdown;
    var $ = layui.$;
    var form = layui.form;


    // 创建渲染实例
    table.render({
        elem: '#userTable',
        //url: '@Url.Action("GetUserPaginationDataList", "User")',
        url: '/User/GetUserPaginationDataList',
        defaultToolbar: ['filter', 'exports', 'print', {
            title: '帮助',
            layEvent: 'LAYTABLE_TIPS',
            icon: 'layui-icon-tips'
        }],
        method: "POST",
        contentType: "application/json",
        headers: { 'X-CSRF-TOKEN': $('#RequestVerificationToken').val() },
        cellMinWidth: 80,
        toolbar: '#userToolbar',
        page: { //支持传入 laypage 组件的所有参数（某些参数除外，如：jump/elem） - 详见文档
            layout: ['limit', 'count', 'prev', 'page', 'next', 'skip'] //自定义分页布局
        },
        cols: [[
            { field: 'userID', title: '主键', sort: true },
            { field: 'userName', title: '用户名', sort: true, align: 'center' },
            { field: 'realName', title: '真实姓名', sort: true, align: 'center' },
            { field: 'mobileNumber', title: '手机号码', sort: true, align: 'center' },
            { field: 'eMailAddress', title: '邮箱地址', sort: true, align: 'center' },
            {
                field: 'status', title: '状态', sort: true, align: 'center', templet: function (d) {
                    if (d.status == '0') {
                        return ' <span class="layui-badge layui-bg-gray">未知</span>';
                    } else if (d.status == '1') {
                        return ' <span class="layui-badge layui-bg-green">正常</span>';
                    } else if (d.status == '2') {
                        return ' <span class="layui-badge">禁用</span>';
                    }
                }
            },
            { field: 'createBy', title: '创建人', sort: true, align: 'center' },
            { field: 'createDateTime', title: '创建时间', sort: true, align: 'center' },
            { field: 'lastModifyBy', title: '最后修改人', sort: true, align: 'center' },
            { field: 'lastModifyDate', title: '最后修改时间', sort: true, align: 'center' },
            { fixed: 'right', title: '操作', width: 260, minWidth: 125, toolbar: '#userBar' }
        ]],
        response: {
            statusCode: 200 //重新规定成功的状态码为 200，table 组件默认为 0
        },
        done: function () {
            var id = this.id;
        }
        , error: function (res, msg) {
            console.log(res, msg)
        }
    });

    //触发单元格工具事件
    table.on('tool(userTable)', function (obj) { // 双击 toolDouble
        if (obj.event === 'del') {
            layer.confirm('真的删除行么', {
                btn: ['确定', '关闭'] //按钮
            }, function (index) {
                obj.del();
                $.ajax({
                    //url: "@Url.Action("DeleteUser", "User")",
                    url: '/User/DeleteUser',
                    type: 'POST',
                    dataType: "JSON",
                    data: JSON.stringify(obj.data),
                    contentType: "application/json;charset=utf-8",
                    headers: { 'X-CSRF-TOKEN': $('#RequestVerificationToken').val() },
                    success: function (data) {
                        if (data.responseCode == 200) {
                            layer.msg(data.message, { icon: 1, offset: 'auto', time: 2000 }, function () {
                                // debugger;
                                //同时关闭上一级弹出层
                                var index = parent.layer.getFrameIndex(window.name); // 获取当前 iframe 层的索引
                                parent.layer.close(index); // 关闭当前 iframe 弹层
                                //回调重新刷新表格
                                table.reload('userTable');
                            });
                            //return;
                        }
                        else {
                            layer.msg(data.message, { icon: 2, offset: 'auto', time: 5000 });
                            //return;
                        }
                    },
                    error: function (data) {
                        alert(data);
                        console.log(data);
                    }
                });
                layer.close(index);
            });
        } else if (obj.event === 'edit') {
            layer.open({
                title: '编辑用户信息',
                shadeClose: true,
                shade: false,
                maxmin: true, //开启最大化最小化按钮
                type: 2,
                area: ['400px', '500px'],
                content: '/User/Edit?UserID=' + obj.data.userID,
                //content: '@Url.Action("Edit", "User")?UserID=' + obj.data.userID,
                end: function () {
                    //回调重新刷新表格
                    table.reload('userTable');
                }
            });
        } else if (obj.event === 'configRole') {
            layer.open({
                title: '配置用户角色',
                shadeClose: true,
                shade: false,
                maxmin: true, //开启最大化最小化按钮
                type: 2,
                area: ['400px', '500px'],
                content:'/User/ConfigRole' + obj.data.userID,
                //content: '@Url.Action("ConfigRole", "User")?UserID=' + obj.data.userID,
                end: function () {
                    //回调重新刷新表格
                    table.reload('userTable');
                }
            });
        }

    });
    // 工具栏事件
    table.on('toolbar(userTable)', function (obj) {
        var id = obj.config.id;
        var checkStatus = table.checkStatus(id);
        var othis = lay(this);
        var searchKey = $(UserName).val();
        switch (obj.event) {
            case 'createUser':
                layer.open({
                    title: '创建新用户',
                    shadeClose: true,
                    shade: false,
                    maxmin: true, //开启最大化最小化按钮
                    type: 2,
                    area: ['400px', '500px'],
                    content:'/User/Create',
                    //content: '@Url.Action("Create", "User")',
                    end: function () {
                        //回调重新刷新表格
                        table.reload('userTable');
                    }
                });
            case 'search':
                //执行重载
                table.reload('userTable', {
                    page: {
                        curr: 1, //重新从第 1 页开始
                    }
                    , where: {
                        searchKey
                    }
                });
                //对搜索框重新赋值
                $(UserName).val(searchKey);
                break;
        };
    });
    //对外暴露的接口
    exports('userManage', {});
});