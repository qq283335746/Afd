<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DlgOrderProcess.aspx.cs" Inherits="TygaSoft.Web.Manages.Orders.DlgOrderProcess" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>订单状态路由</title>
</head>
<body>
    <form id="dlgOrderProcessFm" runat="server"></form>
    <table id="dgOrderProcess" class="easyui-datagrid" data-options="fit:true,fitColumns:true,rownumbers:true,singleSelect:true,border:true,striped:true">
        <thead>
            <tr>
                <th data-options="field: 'SRecordDate', width:120">操作时间</th>
                <th data-options="field: 'StaffCode', width:100">操作员工号</th>
                <th data-options="field: 'StepName', width:100">扫描类型</th>
                <th data-options="field: 'Pictures', width:100,formatter:Common.FImg">上传图片</th>
                <th data-options="field: 'LatlngPlace', width:100">GPS定位</th>
                <th data-options="field: 'IpPlace', width:100">IP定位</th>
            </tr>
        </thead>
    </table>

</body>
</html>
