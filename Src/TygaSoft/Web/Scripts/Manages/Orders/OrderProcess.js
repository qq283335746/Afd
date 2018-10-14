var OrderProcess = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        this.Load(1, 10);
        var pager = $("#dgOrderProcess").datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                OrderProcess.Load(pageNumber, pageSize);
            }
        });
    },
    SelectRow: null,
    Load: function (pageIndex, pageSize,orderId) {
        var postData = '{"model":{"PageIndex": "' + pageIndex + '", "PageSize": "' + pageSize + '", "ParentId": "' + orderId + '"}}';
        var url = Common.AppName + '/Services/AfdService.svc/GetOrderProcessList';
        Common.Ajax(url, postData, 'POST', '', true, true, function (result) {
            //console.log('GetOrderProcessList--result--' + JSON.stringify(result));
            $("#dgOrderProcess").datagrid('loadData', JSON.parse(result.Data));
        });
    },
    OnSearch: function () {
        OrderProcess.Load(1, 10);
    },
    Dlg: function (orderId) {
        if ($("body").find("#dlgOrderProcess").length == 0) {
            $("body").append("<div id=\"dlgOrderProcess\" style=\"padding:10px;\"></div>");
        }
        var wh = Common.GetWh(960, 500);
        $("#dlgOrderProcess").dialog({
            title: '填写信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    OrderProcess.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgOrderProcess').dialog('close');
                }
            }],
            href: Common.AppName + '/u/yorder.html',
            onLoad: function () {
                OrderProcess.Load(1, 100, orderId);
            }
        })
        return false;
    },
    Add: function () {
        OrderProcess.SelectRow = null;
        if ($("body").find("#dlgOrderProcess").length == 0) {
            $("body").append("<div id=\"dlgOrderProcess\" style=\"padding:10px;\"></div>");
        }
        var s = '';
        var wh = Common.GetWh(780, 500);
        $("#dlgOrderProcess").dialog({
            title: '填写信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    OrderProcess.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgOrderProcess').dialog('close');
                }
            }],
            content: s
        })
        return false;
    },
    Edit: function () {
        var rows = $("#dgOrderProcess").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        OrderProcess.SelectRow = rows[0];
        if ($("body").find("#dlgOrderProcess").length == 0) {
            $("body").append("<div id=\"dlgOrderProcess\" style=\"padding:10px;\"></div>");
        }
        var s = '';
        var wh = Common.GetWh(780, 500);
        $("#dlgOrderProcess").dialog({
            title: '编辑信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    OrderProcess.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgOrderProcess').dialog('close');
                }
            }],
            content: s,
            onLoad: function () {
                OrderProcess.SetEdit(rows[0]);
            }
        })
        return false;
    },
    Del: function () {
        var rows = $("#dgOrderProcess").datagrid('getSelections');
        if (!rows || rows.length < 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        var itemAppend = "";
        for (var i = 0; i < rows.length; i++) {
            if (i > 0) itemAppend += ",";
            itemAppend += rows[i].Id;
        }
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                var postData = '{"itemAppend": "' + itemAppend + '" }';
                var url = Common.AppName + "/Services/AfdService.svc/DeleteOrderProcess";
                Common.Ajax(url, postData, "POST", "", true, true, function (result) {
                    jeasyuiFun.show("温馨提示", "保存成功！");
                    setTimeout(function () {
                        OrderProcess.Load(1, 10);
                    }, 700);
                });
            }
        });
    },
    Save: function () {
        var isValid = $('#dlgFm').form('validate');
        if (!isValid) return false;
        var orderId = $.trim($("#txtOrderId").val());
        var staffCode = $.trim($("#txtStaffCode").val());
        var stepName = $.trim($("#txtStepName").val());
        var pictures = $.trim($("#txtPictures").val());
        var deviceId = $.trim($("#txtDeviceId").val());
        var latlng = $.trim($("#txtLatlng").val());
        var latlngPlace = $.trim($("#txtLatlngPlace").val());
        var ip = $.trim($("#txtIp").val());
        var ipPlace = $.trim($("#txtIpPlace").val());
        var recordDate = $.trim($("#txtRecordDate").val());
        var lastUpdatedDate = $.trim($("#txtLastUpdatedDate").val());

        var postData = '{"model":{"OrderId":"' + orderId + '","StaffCode":"' + staffCode + '","StepName":"' + stepName + '","Pictures":"' + pictures + '","DeviceId":"' + deviceId + '","Latlng":"' + latlng + '","LatlngPlace":"' + latlngPlace + '","Ip":"' + ip + '","IpPlace":"' + ipPlace + '","RecordDate":"' + recordDate + '","LastUpdatedDate":"' + lastUpdatedDate + '"}';
        var url = Common.AppName + "/Services/AfdService.svc/SaveOrderProcess";
        Common.Ajax(url, postData, "POST", "", true, true, function (result) {
            $("#dlgOrderProcess").dialog('close');
            jeasyuiFun.show("温馨提示", "保存成功！");
            setTimeout(function () {
                OrderProcess.Load(1, 10);
            }, 700);
        });
    },
    SetEdit: function (data) {
        var contarner = $('#dlgOrderProcess');
        contarner.find('#txtOrderId').val(data.OrderId);
        contarner.find('#txtStaffCode').val(data.StaffCode);
        contarner.find('#txtStepName').val(data.StepName);
        contarner.find('#txtPictures').val(data.Pictures);
        contarner.find('#txtDeviceId').val(data.DeviceId);
        contarner.find('#txtLatlng').val(data.Latlng);
        contarner.find('#txtLatlngPlace').val(data.LatlngPlace);
        contarner.find('#txtIp').val(data.Ip);
        contarner.find('#txtIpPlace').val(data.IpPlace);
        contarner.find('#txtRecordDate').val(data.RecordDate);
        contarner.find('#txtLastUpdatedDate').val(data.LastUpdatedDate);
    }
}