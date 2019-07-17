﻿
var ListUser = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent:function(){
        $("[name=abtnUserDataPermission]").click(function () {
            ListUser.UserDataPermission();
        })
        if (!Common.IsAdmin()) {
            $('#lbtnAdd').linkbutton('disable');
            $('#lbtnDel').linkbutton('disable');
            $('#lbtnResetPsw').linkbutton('disable');
            $('#lbtnEditAuth').linkbutton('disable');
            $('#lbtnEditProfile').linkbutton('disable');
        }
    },
    InitData:function(){
        this.Grid(sPageIndex, sPageSize);
        this.InitSearchItem();
    },
    InitSearchItem: function () {
        var keyword = $.trim(ListUser.GetMyData($("#myDataForSearch")).keyword);
        if (keyword != "") {
            $("#txtKeyword").textbox('setValue', keyword);
        }
    },
    GetMyData: function (obj) {
        var myData = obj.html();
        return eval("(" + myData + ")");
    },
    Grid: function (pageIndex, pageSize) {
        var pager = $('#bindT').datagrid('getPager');
        $(pager).pagination({
            total: sTotalRecord,
            pageNumber: sPageIndex,
            pageSize: sPageSize,
            onSelectPage: function (pageNumber, pageSize) {
                if (sQueryStr.length == 0) {
                    window.location = "?pageIndex=" + pageNumber + "&pageSize=" + pageSize + "";
                }
                else {
                    window.location = "?" + sQueryStr + "&pageIndex=" + pageNumber + "&pageSize=" + pageSize + "";
                }
            }
        });
    },
    Search: function () {
        window.location = "?keyword=" + $("#txtKeyword").textbox('getValue') + "";
    },
    OnIsLockedOut: function (h) {
        try {
            var currObj = $(h);
            if (currObj.text() == "正常") {
                return false;
            }
            var userName = $.trim(currObj.attr("code"));
            $.messager.confirm('温馨提醒', '确定要执行此操作吗？', function (r) {
                if (r) {
                    var sData = '{"userName":"' + userName + '"}';
                    var url = Common.AppName + "/Services/SecurityService.svc/SaveIsLockedOut";
                    Common.Ajax(url, sData, 'POST', '', true, true, function (result) {
                        if (result.Data == "1") {
                            currObj.text("已锁定");
                            jeasyuiFun.show("温馨提醒", "操作成功");
                        }
                        else if (result.Data == "0") {
                            currObj.text("正常");
                            jeasyuiFun.show("温馨提醒", "操作成功");
                        }
                        else {
                            $.messager.alert('错误提醒', msg, 'error');
                        }
                    });
                }
            })
        }
        catch (e) {
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
    },
    OnIsApproved: function (h) {
        try {
            var currObj = $(h);
            $.messager.confirm('温馨提醒', '确定要执行此操作吗？', function (r) {
                if (r) {
                    var sData = '{"userName":"' + currObj.attr("code") + '"}';
                    var url = Common.AppName + "/Services/SecurityService.svc/SaveIsApproved";
                    Common.Ajax(url, sData, 'POST', '', true, true, function (result) {
                        if (result.Data == "1") {
                            currObj.text("启用");
                            jeasyuiFun.show("温馨提醒", "操作成功");
                        }
                        else if (result.Data == "0") {
                            currObj.text("禁用");
                            jeasyuiFun.show("温馨提醒", "操作成功");
                        }
                        else {
                            $.messager.alert('错误提醒', msg, 'error');
                        }
                    })
                }
            })
        }
        catch (e) {
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
    },
    Add: function () {
        window.location = "tmember.html";
    },
    Edit: function () {

    },
    Del: function () {
        try {
            var cbl = $('#bindT').datagrid("getSelections");
            if (!cbl || cbl.length != 1) {
                $.messager.alert('错误提醒', '请选择一行且仅一行数据进行操作', 'error');
                return false;
            }
            $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
                if (r) {
                    var userName = cbl[0].f0;
                    var sData = '{"userName":"' + userName + '"}';
                    $.ajax({
                        url: Common.AppName + "/Services/SecurityService.svc/DelUser",
                        type: "post",
                        contentType: "application/json; charset=utf-8",
                        data: sData,
                        beforeSend: function () {
                            $.messager.progress({ title: '请稍等', msg: '正在执行...' });
                        },
                        complete: function () {
                            $.messager.progress('close');
                        },
                        success: function (result) {
                            if (result.ResCode != 1000) {
                                $.messager.alert('系统提示', result.Msg, 'info');
                                return false;
                            }
                            jeasyuiFun.show("温馨提示", "操作成功！");
                            setTimeout(function () {
                                window.location.reload();
                            }, 1000);
                        }
                    });
                }
            });
        }
        catch (e) {
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
    },
    Save: function () {

    },
    SaveRole: function () {

    },
    EditRole: function (userName) {
        $("#lbUserName").text(userName);
        this.BindRole();
    },
    BindRole: function () {
        $("#dgRole").datagrid('loadData', ListUser.GetMyData($("#myDataForRole")));
    },
    RoleFormatter: function (value, row, index) {
        var isInRole = row.IsInRole == "True";
        if (isInRole) {
            return "<input type=\"checkbox\" checked=\"checked\" value=\"" + value + "\" onclick=\"ListUser.CbIsInRole(this)\" />" + value;
        }
        return "<input type=\"checkbox\" value=\"" + value + "\" onclick=\"ListUser.CbIsInRole(this)\" />" + value;
    },
    CbIsInRole: function (h) {
        try {
            var $_obj = $(h);
            var userName = $.trim($("#lbUserName").text());
            var roleName = $_obj.val();
            var isInRole = $_obj.is(":checked");

            var sData = '{"userName":"' + userName + '","roleName":"' + roleName + '","isInRole":"' + isInRole + '"}';
            $.ajax({
                url: Common.AppName + "/Services/SecurityService.svc/SaveUserInRole",
                type: "post",
                contentType: "application/json; charset=utf-8",
                data: sData,
                beforeSend: function () {
                    $.messager.progress({ title: '请稍等', msg: '正在执行...' });
                },
                complete: function () {
                    $.messager.progress('close');
                },
                success: function (result) {
                    if (result.ResCode != 1000) {
                        $.messager.alert('系统提示', result.Msg, 'info');
                        return false;
                    }
                }
            });
        }
        catch (e) {
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
    },
    OnRoleLoadSuccess: function (data) {
        try {
            var userName = $("#lbUserName").text();
            var dg = $('#dgRole');
            var rows = dg.datagrid('getRows');
            if (rows && rows.length > 0) {
                $.ajax({
                    url: Common.AppName + "/Services/SecurityService.svc/GetRolesForUser",
                    type: "post",
                    contentType: "application/json; charset=utf-8",
                    data: '{"userName":"' + userName + '"}',
                    beforeSend: function () {
                        $.messager.progress({ title: '请稍等', msg: '正在执行...' });
                    },
                    complete: function () {
                        $.messager.progress('close');
                    },
                    success: function (result) {
                        if (result.ResCode != 1000) {
                            $.messager.alert('系统提示', result.Msg, 'info');
                            return false;
                        }
                        var roles = result.Data;
                        if (roles.length > 0) {
                            for (var i = 0; i < rows.length; i++) {
                                if (roles.indexOf(rows[i].RoleName) > -1) {
                                    dg.datagrid('updateRow', {
                                        index: i,
                                        row: {
                                            IsInRole: 'True'
                                        }
                                    });
                                }
                            }
                        }
                    }
                });
            }
        }
        catch (e) {
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
    },
    ResetPassword: function () {
        var rows = $('#bindT').datagrid("getSelections");
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提醒', '请选择一行且仅一行数据进行操作', 'error');
            return false;
        }
        $.messager.confirm('温馨提醒', '确定要重置该用户的密码吗？', function (r) {
            if (r) {
                var username = rows[0].f0;

                $.ajax({
                    url: Common.AppName + "/Services/SecurityService.svc/ResetPassword",
                    type: "post",
                    contentType: "application/json; charset=utf-8",
                    data: '{"username":"' + username + '"}',
                    beforeSend: function () {
                        $.messager.progress({ title: '请稍等', msg: '正在执行...' });
                    },
                    complete: function () {
                        $.messager.progress('close');
                    },
                    success: function (result) {

                        if (result.ResCode != 1000) {
                            $.messager.alert('系统提示', result.Msg, 'info');
                            return false;
                        }

                        $.messager.alert('温馨提示', "新密码：" + result.Data + "", 'info');
                    }
                });
            }
        });
    },
    UserAccess: function () {
        var rows = $('#bindT').datagrid("getSelections");
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提醒', '请选择一行且仅一行数据进行操作', 'error');
            return false;
        }
        window.location = "../u/rolemenu.html?denyUser=" + rows[0].f1 + "";
    },
    SetUserProfile: function () {
        var rows = $("#bindT").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        var s = '';
        s += '<div class="row"><span class="rl w100">所属站点：</span><div class="fl">';
        s += '<input id="cbgSiteMulti" class="easyui-combogrid txt w400" data-options="mode:\'remote\',idField:\'Id\',textField:\'Named\',editable:false" />';
        s += '</div></div>';
        s += '<span class="clr"></span>';
        var wh = Common.GetWh(600, 300);
        var dlgId = "dlgAddUserProfile";
        if ($("body").find("#" + dlgId + "").length == 0) {
            $("body").append("<div id=\"" + dlgId + "\" style=\"padding:10px;\"></div>");
        }
        var dlg = $("#" + dlgId + "");
        dlg.dialog({
            title: '用户个性化设置',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                text: '确定', iconCls: 'icon-save', handler: function () {
                    var appId = $('#cbgSiteMulti').combogrid('getValues');
                    var postData = '{"model":{"FeatureId": "' + appId + '", "UserName": "' + rows[0].f0 + '", "TypeName": "UserProfile"}}';
                    //console.log(JSON.stringify(postData));
                    //return false;
                    var url = Common.AppName + "/Services/AfdService.svc/SaveFeatureUser";
                    Common.Ajax(url, postData, 'POST', '', true, true, function (result) {
                        console.log('SaveFeatureUser--' + JSON.stringify(result));
                        jeasyuiFun.show("温馨提示", "操作成功！");
                        dlg.dialog('close');
                    })
                }
            }, {
                text: '取消', iconCls: 'icon-cancel', handler: function () {
                    dlg.dialog('close');
                }
            }],
            content: s,
            onOpen: function () {
                var data = { "username": "" + rows[0].f0 + "", "typeName": "UserProfile" };
                var url = Common.AppName + "/Services/AfdService.svc/GetFeatureUserInfo";
                Common.Ajax(url, data, 'GET', '', true, true, function (result) {
                    console.log('GetFeatureUserInfo--' + JSON.stringify(result));
                    var jData = JSON.parse(result.Data);
                    if ($.trim(jData.FeatureId) != '') {
                        SiteMulti.Cbg('cbgSiteMulti', true, [jData.FeatureId]);
                    }
                    else SiteMulti.Cbg('cbgSiteMulti', true, null);
                })
            }
        })
    },
    CallBackByProfile: function (data) {
        //console.log(JSON.stringify(data));
        $('#imgSiteLogo').attr('src', data[0].Src);
        $('#imgSiteLogo').attr('code', data[0].Id);
    }
}