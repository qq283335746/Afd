<%@ Page Title="订单管理" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListOrderMake.aspx.cs" Inherits="TygaSoft.Web.Manages.Orders.ListOrderMake" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="dgToolbar">
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" onclick="OrderMake.Add()"><span>新增</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="OrderMake.Edit()"><span>编辑</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="OrderMake.Del()"><span>删除</span></a>
        <a class="easyui-menubutton" data-options="menu:'#mmExcel',iconCls:'icon-edit'">导入/导出</a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-tip'" onclick="OrderMake.DlgOrderProcess()"><span>订单状态路由</span></a>
        <div id="mmExcel" style="width:150px;">
            <div onclick="OrderMake.OnExport()">导出</div>
        </div>
        <div id="searchFm" class="search_box" style="height:55px;">
            <ul class="ul_h">
                <li>
                    订单号：<input id ="txtSOrderCode" class="txt w100" />
                </li>
                <li>
                    客户编码：<input id ="txtSCustomerCode" class="txt w100" />
                </li>
                <li>
                    配送员工号：<input id ="txtSStaffCode" class="txt w100" />
                </li>
                <li>
                    支付方式：<input id ="cbbSPayWay" class="txt w100" />
                </li>
                <li>
                    服务产品：<input id ="txtSServiceProduct" class="txt w100" />
                </li>
            </ul>
            <span class="clr"></span>
            <ul class="ul_h mt5">
                <li>
                    开始日期：<input id ="txtSStartDate" class="easyui-datebox txt w100" />
                </li>
                <li>
                    结束日期：<input id ="txtSEndDate" class="easyui-datebox txt w100" />
                </li>
                <li>
                    <a class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="OrderMake.OnSearch()"><span>查询</span></a>
                </li>
            </ul>
            <span class="clr"></span>
        </div>
    </div>
    <table id="dg" class="easyui-datagrid" data-options="fit:true,fitColumns:false,pagination:true,rownumbers:true,singleSelect:true,border:true,striped:true,toolbar:'#dgToolbar'">
        <thead>
            <tr>
		        <th data-options="field: 'Id', checkbox: true"></th> 
                <th data-options="field: 'OrderCode', width:120">单号</th> 
                <th data-options="field: 'CustomerCode', width:120">客户编码</th> 
                <th data-options="field: 'StaffCode', width:100">配送员工号</th> 
                <th data-options="field: 'StaffCodeOfTake', width:100">收货员工号</th> 
                <th data-options="field: 'ServiceProduct', width:100">服务产品</th> 
                <th data-options="field: 'PayWay', width:100">付款方式</th> 
                <th data-options="field: 'FromName', width:100">寄方客户</th> 
                <th data-options="field: 'FromAddress', width:100">寄方地址</th> 
                <th data-options="field: 'FromPhone', width:120">寄方联系电话</th> 
                <th data-options="field: 'ToCity', width:100">目的地</th> 
                <th data-options="field: 'ToName', width:100">收货人</th> 
                <th data-options="field: 'ToAddress', width:100">收货地址</th> 
                <th data-options="field: 'ToPhone', width:120">收货人联系电话</th> 
                <th data-options="field: 'STakeTime', width:150">取件时间</th> 
                <th data-options="field: 'SReachTime', width:150">送达时间</th> 
                <th data-options="field: 'CargoName', width:100">配送物品</th> 
                <th data-options="field: 'PieceQty', width:100">件数</th> 
                <th data-options="field: 'Weight', width:100">重量</th> 
                <th data-options="field: 'TranPrice', width:100">应收运费</th> 
                <th data-options="field: 'IncreServicePrice', width:100">增值服务费</th> 
                <th data-options="field: 'TotalPrice', width:100">费用合计</th> 
                <th data-options="field: 'Remark', width:100">备注</th> 
            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/afd/Scripts/Manages/Orders/OrderProcess.js"></script>
    <script type="text/javascript" src="/afd/Scripts/Manages/Orders/OrderMake.js"></script>
    <script type="text/javascript">
        $(function () {
            OrderMake.Init();
        })
    </script>

</asp:Content>
