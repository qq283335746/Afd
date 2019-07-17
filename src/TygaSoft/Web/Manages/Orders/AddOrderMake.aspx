<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddOrderMake.aspx.cs" Inherits="TygaSoft.Web.Manages.Orders.AddOrderMake" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>新建/编辑订单信息</title>
</head>
<body>
    <form id="dlgFm" runat="server">
        <div class="row-fl"><span class="rl w100"><span class="cr">*</span> 订单号：</span><div class="fl"><input tabindex="1" id ="txtOrderCode" class="easyui-validatebox txt w200" data-options="required:true" /></div></div> 
        <div class="row-fl"><span class="rl w100"> 客户编码：</span><div class="fl"><input tabindex="2" id ="txtCustomerCode" class="txt w200" /></div></div> 
        <div class="row-fl"><span class="rl w100"> 寄方客户：</span><div class="fl"><input tabindex="3" id ="txtFromName" class="txt w200" /></div></div> 
        <div class="row-fl"><span class="rl w100"> 寄方地址：</span><div class="fl"><input tabindex="4" id ="txtFromAddress" class="txt w200" /></div></div> 
        <div class="row-fl"><span class="rl w100"> 寄方电话：</span><div class="fl"><input tabindex="5" id ="txtFromPhone" class="easyui-validatebox txt w200" data-options="validType:'phone'" /></div></div> 
        <div class="row-fl"><span class="rl w100"> 目的地：</span><div class="fl"><input tabindex="6" id ="txtToCity" class="txt w200" /></div></div> 
        <div class="row-fl"><span class="rl w100"> 收货人：</span><div class="fl"><input tabindex="7" id ="txtToName" class="txt w200" /></div></div> 
        <div class="row-fl"><span class="rl w100"> 收货地址：</span><div class="fl"><input tabindex="8" id ="txtToAddress" class="txt w200" /></div></div> 
        <div class="row-fl"><span class="rl w100"> 收方电话：</span><div class="fl"><input tabindex="9" id ="txtToPhone" class="easyui-validatebox txt w200" data-options="validType:'phone'" /></div></div> 
        <div class="row-fl"><span class="rl w100"> 配送员工号：</span><div class="fl"><input tabindex="10" id ="txtStaffCode" class="txt w200" /></div></div> 
        <div class="row-fl"><span class="rl w100"> 收货员工号：</span><div class="fl"><input tabindex="11" id ="txtStaffCodeOfTake" class="txt w200" /></div></div> 
        <div class="row-fl"><span class="rl w100"> 取件时间：</span><div class="fl"><input tabindex="12" id ="txtTakeTime" class="easyui-datetimebox txt w200" style="height:20px;" /></div></div> 
        <div class="row-fl"><span class="rl w100"> 送达时间：</span><div class="fl"><input tabindex="13" id ="txtReachTime" class="easyui-datetimebox txt w200" style="height:20px;" /></div></div> 
        <div class="row-fl"><span class="rl w100"> 配送物品：</span><div class="fl"><input tabindex="14" id ="txtCargoName" class="txt w200" /></div></div> 
        <div class="row-fl"><span class="rl w100"> 服务产品：</span><div class="fl"><input tabindex="15" id ="txtServiceProduct" class="txt w200" /></div></div> 
        <div class="row-fl"><span class="rl w100"> 件数：</span><div class="fl"><input tabindex="16" id ="txtPieceQty" class="txt w200" /></div></div> 
        <div class="row-fl"><span class="rl w100"> 重量：</span><div class="fl"><input tabindex="17" id ="txtWeight" class="txt w200" /></div></div> 
        <div class="row-fl"><span class="rl w100"> 应收运费：</span><div class="fl"><input tabindex="18" id ="txtTranPrice" class="txt w200" /></div></div> 
        <div class="row-fl"><span class="rl w100"> 增值服务费：</span><div class="fl"><input tabindex="19" id ="txtIncreServicePrice" class="txt w200" /></div></div> 
        <div class="row-fl"><span class="rl w100"> 费用合计：</span><div class="fl"><input tabindex="20" id ="txtTotalPrice" class="txt w200" /></div></div> 
        <div class="row-fl"><span class="rl w100"> 付款方式：</span><div class="fl"><input tabindex="21" id ="cbbPayWay" class="txt w200" /></div></div> 
        <input type="hidden" id="hId" />
        <span class="clr"></span>
        <div class="row mt10">
            <span class="rl w100">备注：</span>
            <div class="fl">
                <input tabindex="22" id="txtRemark" class="easyui-textbox txt" data-options="multiline:true" style="width:516px;" />
            </div>
        </div>
    </form>

</body>
</html>
