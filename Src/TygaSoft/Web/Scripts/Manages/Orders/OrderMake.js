var OrderMake = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        OrderMake.SetForm();
        setTimeout(function () {
            OrderMake.Load(1, 10);
            var pager = $("#dg").datagrid('getPager');
            pager.pagination({
                onSelectPage: function (pageNumber, pageSize) {
                    OrderMake.Load(pageNumber, pageSize);
                }
            });
        })
    },
    SelectRow: null,
    Load: function (pageIndex, pageSize) {
        var jSearchItem = OrderMake.GetSearchItem();
        //var keyword = $("#txtKeyword").textbox('getValue');
        var postData = '{"model":{"PageIndex": "' + pageIndex + '", "PageSize": "' + pageSize + '","OrderCode": "' + jSearchItem.OrderCode + '", "CustomerCode": "' + jSearchItem.CustomerCode + '", "StaffCode": "' + jSearchItem.StaffCode + '", "PayWay": "' + jSearchItem.PayWay + '", "ServiceProduct": "' + jSearchItem.ServiceProduct + '", "StartDate": "' + jSearchItem.StartDate + '", "EndDate": "' + jSearchItem.EndDate + '"}}';
        var url = Common.AppName + '/Services/AfdService.svc/GetOrderMakeList';
        Common.Ajax(url, postData, 'POST', '', true, true, function (result) {
            //console.log('GetOrderMakeList--result--' + JSON.stringify(result));
            $("#dg").datagrid('loadData', JSON.parse(result.Data));
        });
    },
    OnSearch: function () {
        OrderMake.Load(1, 10);
    },
    GetSearchItem: function () {
        var sOrderCode = $.trim($('#txtSOrderCode').val());
        var sCustomerCode = $.trim($('#txtSCustomerCode').val());
        var sStaffCode = $.trim($('#txtSStaffCode').val());
        var sPayWay = $.trim($('#cbbSPayWay').combobox('getValue')).replace('请选择','');
        var sServiceProduct = $.trim($('#txtSServiceProduct').val());
        var sStartDate = $.trim($('#txtSStartDate').datebox('getValue'));
        var sEndDate = $.trim($('#txtSEndDate').datebox('getValue'));
        var data = { "OrderCode": sOrderCode, "CustomerCode": sCustomerCode, "StaffCode": sStaffCode, "PayWay": sPayWay, "ServiceProduct": sServiceProduct, "StartDate": sStartDate, "EndDate": sEndDate };
        //console.log(JSON.stringify(data));
        return data;
    },
    Add: function () {
        OrderMake.SelectRow = null;
        if ($("body").find("#dlgOrderMake").length == 0) {
            $("body").append("<div id=\"dlgOrderMake\" style=\"padding:10px;\"></div>");
        }
        var s = '';
        var wh = Common.GetWh(720, 550);
        $("#dlgOrderMake").dialog({
            title: '填写信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    OrderMake.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgOrderMake').dialog('close');
                }
            }],
            href: Common.AppName + '/u/torder.html',
            onLoad: function () {
                var contarner = $('#dlgOrderMake');
                contarner.find('#hId').val("");
                OrderMake.CbbPayWay('cbbPayWay', '');
            }
        })
        return false;
    },
    Edit: function () {
        var rows = $("#dg").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        OrderMake.SelectRow = rows[0];
        if ($("body").find("#dlgOrderMake").length == 0) {
            $("body").append("<div id=\"dlgOrderMake\" style=\"padding:10px;\"></div>");
        }
        var wh = Common.GetWh(720, 550);
        $("#dlgOrderMake").dialog({
            title: '编辑信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    OrderMake.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgOrderMake').dialog('close');
                }
            }],
            href: Common.AppName + '/u/torder.html',
            onLoad: function () {
                OrderMake.SetEdit(rows[0]);
            }
        })
        return false;
    },
    Del: function () {
        var rows = $("#dg").datagrid('getSelections');
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
                var url = Common.AppName + "/Services/AfdService.svc/DeleteOrderMake";
                Common.Ajax(url, postData, "POST", "", true, true, function (result) {
                    jeasyuiFun.show("温馨提示", "保存成功！");
                    setTimeout(function () {
                        OrderMake.Load(1, 10);
                    }, 700);
                });
            }
        });
    },
    Save: function () {
        var isValid = $('#dlgFm').form('validate');
        if (!isValid) return false;
        var id = $.trim($("#hId").val());
        var customerCode = $.trim($("#txtCustomerCode").val());
        var orderCode = $.trim($("#txtOrderCode").val());
        var fromName = $.trim($("#txtFromName").val());
        var fromAddress = $.trim($("#txtFromAddress").val());
        var fromPhone = $.trim($("#txtFromPhone").val());
        var toCity = $.trim($("#txtToCity").val());
        var toName = $.trim($("#txtToName").val());
        var toAddress = $.trim($("#txtToAddress").val());
        var toPhone = $.trim($("#txtToPhone").val());
        var staffCode = $.trim($("#txtStaffCode").val());
        var staffCodeOfTake = $.trim($("#txtStaffCodeOfTake").val());
        var takeTime = $.trim($("#txtTakeTime").datetimebox('getValue'));
        var reachTime = $.trim($("#txtReachTime").datetimebox('getValue'));
        var cargoName = $.trim($("#txtCargoName").val());
        var serviceProduct = $.trim($("#txtServiceProduct").val());
        var pieceQty = $.trim($("#txtPieceQty").val());
        var weight = $.trim($("#txtWeight").val());
        var tranPrice = $.trim($("#txtTranPrice").val());
        var increServicePrice = $.trim($("#txtIncreServicePrice").val());
        var totalPrice = $.trim($("#txtTotalPrice").val());
        var payWay = $.trim($("#cbbPayWay").combobox('getValue'));
        var remark = $.trim($("#txtRemark").val());

        var postData = '{"model":{"Id":"' + id + '","CustomerCode":"' + customerCode + '","OrderCode":"' + orderCode + '","FromName":"' + fromName + '","FromAddress":"' + fromAddress + '","FromPhone":"' + fromPhone + '","ToCity":"' + toCity + '","ToName":"' + toName + '","ToAddress":"' + toAddress + '","ToPhone":"' + toPhone + '","StaffCode":"' + staffCode + '","StaffCodeOfTake":"' + staffCodeOfTake + '","TakeTime":"' + takeTime + '","ReachTime":"' + reachTime + '","CargoName":"' + cargoName + '","ServiceProduct":"' + serviceProduct + '","PieceQty":"' + pieceQty + '","Weight":"' + weight + '","TranPrice":"' + tranPrice + '","IncreServicePrice":"' + increServicePrice + '","TotalPrice":"' + totalPrice + '","PayWay":"' + payWay + '","Remark":"' + remark + '"}';
        var url = Common.AppName + "/Services/AfdService.svc/SaveOrderMake";
        Common.Ajax(url, postData, "POST", "", true, true, function (result) {
            $("#dlgOrderMake").dialog('close');
            jeasyuiFun.show("温馨提示", "保存成功！");
            setTimeout(function () {
                OrderMake.Load(1, 10);
            }, 700);
        });
    },
    SetForm:function(){
        OrderMake.CbbPayWay('cbbSPayWay', '');
    },
    SetEdit: function (data) {
        //console.log('SetEdit--' + JSON.stringify(data));
        var contarner = $('#dlgOrderMake');
        contarner.find('#hId').val(data.Id);
        contarner.find('#txtUserId').val(data.UserId);
        contarner.find('#txtCustomerCode').val(data.CustomerCode);
        contarner.find('#txtOrderCode').val(data.OrderCode);
        contarner.find('#txtFromName').val(data.FromName);
        contarner.find('#txtFromAddress').val(data.FromAddress);
        contarner.find('#txtFromPhone').val(data.FromPhone);
        contarner.find('#txtToCity').val(data.ToCity);
        contarner.find('#txtToName').val(data.ToName);
        contarner.find('#txtToAddress').val(data.ToAddress);
        contarner.find('#txtToPhone').val(data.ToPhone);
        contarner.find('#txtStaffCode').val(data.StaffCode);
        contarner.find('#txtStaffCodeOfTake').val(data.StaffCodeOfTake);
        var sTakeTime = data.STakeTime.indexOf('1754-01-01') > -1 ? '' : data.STakeTime;
        contarner.find('#txtTakeTime').datetimebox('setValue', sTakeTime);
        var sReachTime = data.SReachTime.indexOf('1754-01-01') > -1 ? '' : data.SReachTime;
        contarner.find('#txtReachTime').datetimebox('setValue', sReachTime);
        contarner.find('#txtCargoName').val(data.CargoName);
        contarner.find('#txtServiceProduct').val(data.ServiceProduct);
        contarner.find('#txtPieceQty').val(data.PieceQty);
        contarner.find('#txtWeight').val(data.Weight);
        contarner.find('#txtTranPrice').val(data.TranPrice);
        contarner.find('#txtIncreServicePrice').val(data.IncreServicePrice);
        contarner.find('#txtTotalPrice').val(data.TotalPrice);
        OrderMake.CbbPayWay('cbbPayWay', data.PayWay);
        contarner.find('#txtRemark').textbox('setValue', data.Remark);

    },
    OnExport: function () {
        $.messager.confirm('提示', '确定要导出数据吗？', function (r) {
            if (r) {
                var jSearchItem = OrderMake.GetSearchItem();
                var s = 'OrderCode=' + jSearchItem.OrderCode + '&CustomerCode=' + jSearchItem.CustomerCode + '&StaffCode=' + jSearchItem.StaffCode + '&PayWay=' + jSearchItem.PayWay + '&ServiceProduct=' + jSearchItem.ServiceProduct + '&StartDate=' + jSearchItem.StartDate + '&EndDate=' + jSearchItem.EndDate + '';
                window.open(Common.AppName + "/h/content.html?ReqName=ExportOrderMake&" + s + "");
            }
        })
    },
    DlgOrderProcess: function () {
        var rows = $("#dg").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        OrderProcess.Dlg(rows[0].Id);
    },
    ViewPic: function (t) {
        var $this = $(t);
        var value = $.trim($this.attr('code'));
        var picItems = value.split('|');
        var picLen = picItems.length;
        var wh = Common.GetWh(780, 590);
        var imgWidth = parseInt(wh[0] - 50);
        var imgHeight = parseInt(wh[1] - 210);
        var s = '';
        for (var i = 0; i < picLen; i++) {
            s += '<div class="row-fl mr10">';
            s += '<img src="' + Common.AppName + '' + picItems[i] + '" alt="图片" width="100" height="100" onclick="OrderMake.OnViewPicSelect(this)" />';
            s += '</div>';
        }
        s += '<span class="clr"></span>';
        s += '<div style="text-align:center;">';
        s += '<img id="imgCurr" src="' + Common.AppName + '' + picItems[0] + '" alt="" width="' + imgWidth + '" height="' + imgHeight + '" />';
        s += '</div>';
        if ($("body").find("#dlgViewPic").length == 0) {
            $("body").append("<div id=\"dlgViewPic\" style=\"padding:10px;\"></div>");
        }
        $("#dlgViewPic").dialog({
            title: '查看图片',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-tip',
            buttons: [{
                id: 'btnCancelView', text: '关闭', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgViewPic').dialog('close');
                }
            }],
            content: s,
            onOpen: function () {
            }
        })
    },
    OnViewPicSelect: function (t) {
        $("#imgCurr").attr("src", $(t).attr("src"));
    },
    CbbPayWay: function (cbbId, v) {
        var cbb = $('#' + cbbId + '');
        var jData = [{ "Name": "现金" }, { "Name": "到付" }, { "Name": "协议结算" }];
        if (cbbId == 'cbbSPayWay') jData.splice(0, 0, { "Name": "请选择" });

        cbb.combobox({
            valueField: 'Name',
            textField: 'Name',
            data: jData,
            onLoadSuccess: function () {
                if (v != "") {
                    cbb.combobox('setValue', v);
                }
                else {
                    cbb.combobox('select', jData[0].Name);
                }
            }
        });
    }
}