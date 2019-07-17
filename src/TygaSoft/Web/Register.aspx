﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="TygaSoft.Web.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>注册</title>
    <link href="~/Styles/Main.css" rel="stylesheet" type="text/css" />
    <link href="~/Scripts/plugins/Jeasyui15/themes/bootstrap/easyui.css" rel="stylesheet" type="text/css" />
    <link href="~/Scripts/plugins/Jeasyui15/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/ui-v1.0.1.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/Login.css" rel="stylesheet" type="text/css" />
    <script src="/Afd/Scripts/plugins/Jeasyui15/jquery.min.js" type="text/javascript"></script>
    <script src="/Afd/Scripts/Plugins/Jeasyui15/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/Afd/Scripts/Plugins/Jeasyui15/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="/Afd/Scripts/JeasyuiHelper.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="w login">
        <div class="main">
            <div class="loginl">
	            <img src="/Afd/Images/loginl.jpg" alt="" />
            </div>
            <div class="loginr">
                
                <h1>注册</h1>
                <div id="loginInfo" class="content">
                    <div class="control-group">
			            <div class="warning clearfix" style="display:none;">
				            <span class="warning-ico"></span>
				            <span class="warning-con">用户名不能为空</span>
			            </div>
		            </div>
                    <div class="control-group">
                        <label class="placeholder" style="position: absolute; left: 5px; top: 0px; color: rgb(119, 119, 119); height: 40px; line-height: 40px; display:block;">用户名</label>
                        <input type="text" id="txtUserName" name="txtUserName" tabindex="1" maxlength="50" class="ui-input-h40" placeholder="" autocomplete="off" value="" />
                    </div>
                    <div class="control-group">
                        <label class="placeholder" style="position:absolute;left:5px;top:0px;color:#777777;height:40px;line-height:40px;display: block;">密码</label>
                        <input type="password" id="txtPsw" name="txtPsw" tabindex="2" maxlength="30" class="ui-input-h40" placeholder="" autocomplete="off" />
                    </div>
                    <div class="control-group">
                        <label class="placeholder" style="position:absolute;left:5px;top:0px;color:#777777;height:40px;line-height:40px;display: block;">确认密码</label>
                        <input type="password" id="txtCfmPsw" name="txtPsw" tabindex="3" maxlength="30" class="ui-input-h40" placeholder="" autocomplete="off" />
                    </div>
                    <div class="control-group">
                        <label class="placeholder" style="position:absolute;left:5px;top:0px;color:#777777;height:40px;line-height:40px;display: block;">验证码</label>
                        <input type="text" id="txtVc" name="txtVc" maxlength="4" tabindex="4" class="ui-input-h40" placeholder="" autocomplete="off" style="width:100px;" />
                        <img border="0" id="imgCode" src="/Afd/h/vc.html?vcType=login" alt="看不清，单击换一张" onclick="this.src='/Afd/h/vc.html?vcType=login&abc='+Math.random()" style="vertical-align:middle;line-height: 38px;height: 38px; width:100px; margin-bottom:3px;" />
                    </div>
                    <div class="control-group">
                        <input type="submit" class="ui-submit-blue" id="btn-submit" value="注册" />
                    </div>
                </div>
                
            </div>
        </div>
    </div>
    
    <span class="clr"></span>
    <div class="footer">
        <div class="footerMain">
            <span>©  2016-2017</span>
        </div>
    </div>

    <asp:Literal runat="server" ID="ltrMyData"></asp:Literal>

    </form>

    <script type="text/javascript" src="/Afd/Scripts/Login.js"></script>
    <script type="text/javascript">
        $(function () {
            Login.Init();
        })
    </script>
</body>

</html>
