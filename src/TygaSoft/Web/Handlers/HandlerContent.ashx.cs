using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using TygaSoft.BLL;
using TygaSoft.DBUtility;
using TygaSoft.Model;
using TygaSoft.SysHelper;
using TygaSoft.WebHelper;
using TygaSoft.CustomProvider;

namespace TygaSoft.Web.Handlers
{
    /// <summary>
    /// HandlerContent 的摘要说明
    /// </summary>
    public class HandlerContent : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json; charset=utf-8";
            try
            {
                string reqName = "";
                switch (context.Request.HttpMethod.ToUpper())
                {
                    case "GET":
                        reqName = context.Request.QueryString["ReqName"];
                        break;
                    case "POST":
                        reqName = context.Request.Form["ReqName"];
                        break;
                    default:
                        break;
                }
                if (string.IsNullOrWhiteSpace(reqName)) return;
                reqName = reqName.Trim();

                switch (reqName)
                {
                    case "SaveMenuAccess":
                        SaveMenuAccess(context);
                        break;
                    case "ExportOrderMake":
                        ExportOrderMake(context);
                        break;
                    case "ExportCustomer":
                        ExportCustomer(context);
                        break;
                    default:
                        throw new ArgumentException(MC.Request_Params_InvalidError);
                }
            }
            catch (Exception ex)
            {
                context.Response.Write(ResResult.ResJsonString(false, ex.Message, ""));
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void ExportOrderMake(HttpContext context)
        {
            var bll = new OrderMake();

            StringBuilder sqlWhere = null;
            ParamsHelper parms = null;

            #region 构造查询条件

            var sOrderCode = context.Request.QueryString["OrderCode"];
            var sCustomerCode = context.Request.QueryString["CustomerCode"];
            var sStaffCode = context.Request.QueryString["StaffCode"];
            var sPayWay = context.Request.QueryString["PayWay"];
            var sServiceProduct = context.Request.QueryString["ServiceProduct"];
            var sStartDate = context.Request.QueryString["StartDate"];
            var sEndDate = context.Request.QueryString["EndDate"];

            if (!string.IsNullOrWhiteSpace(sOrderCode))
            {
                if (sqlWhere == null) sqlWhere = new StringBuilder(500);
                if (parms == null) parms = new ParamsHelper();
                sqlWhere.Append("and OrderCode like @OrderCode ");
                var parm = new SqlParameter("@OrderCode", SqlDbType.VarChar, 36);
                parm.Value = parm.Value = "%" + sOrderCode + "%";
                parms.Add(parm);
            }
            if (!string.IsNullOrWhiteSpace(sCustomerCode))
            {
                if (sqlWhere == null) sqlWhere = new StringBuilder(400);
                if (parms == null) parms = new ParamsHelper();
                sqlWhere.Append("and CustomerCode like @CustomerCode ");
                var parm = new SqlParameter("@CustomerCode", SqlDbType.VarChar, 36);
                parm.Value = parm.Value = "%" + sCustomerCode + "%";
                parms.Add(parm);
            }
            if (!string.IsNullOrWhiteSpace(sStaffCode))
            {
                if (sqlWhere == null) sqlWhere = new StringBuilder(300);
                if (parms == null) parms = new ParamsHelper();
                sqlWhere.Append("and StaffCode like @StaffCode ");
                var parm = new SqlParameter("@StaffCode", SqlDbType.VarChar, 36);
                parm.Value = parm.Value = "%" + sStaffCode + "%";
                parms.Add(parm);
            }
            if (!string.IsNullOrWhiteSpace(sPayWay))
            {
                if (sqlWhere == null) sqlWhere = new StringBuilder(200);
                if (parms == null) parms = new ParamsHelper();
                sqlWhere.Append("and PayWay like @PayWay ");
                var parm = new SqlParameter("@PayWay", SqlDbType.VarChar, 36);
                parm.Value = parm.Value = "%" + sPayWay + "%";
                parms.Add(parm);
            }
            if (!string.IsNullOrWhiteSpace(sServiceProduct))
            {
                if (sqlWhere == null) sqlWhere = new StringBuilder(100);
                if (parms == null) parms = new ParamsHelper();
                sqlWhere.Append("and ServiceProduct like @ServiceProduct ");
                var parm = new SqlParameter("@ServiceProduct", SqlDbType.VarChar, 36);
                parm.Value = parm.Value = "%" + sServiceProduct + "%";
                parms.Add(parm);
            }

            #region 开始与结束日期段

            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MinValue;
            if (!string.IsNullOrWhiteSpace(sStartDate)) DateTime.TryParse(sStartDate, out startDate);
            if (!string.IsNullOrWhiteSpace(sEndDate)) DateTime.TryParse(sEndDate, out endDate);
            if (startDate != DateTime.MinValue && endDate != DateTime.MinValue)
            {
                if (sqlWhere == null) sqlWhere = new StringBuilder(300);
                if (parms == null) parms = new ParamsHelper();

                sqlWhere.Append(@"and (RecordDate between @StartDate and @EndDate) ");
                var parm = new SqlParameter("@StartDate", SqlDbType.DateTime);
                parm.Value = startDate;
                parms.Add(parm);
                parm = new SqlParameter("@EndDate", SqlDbType.DateTime);
                parm.Value = DateTime.Parse(endDate.ToString("yyyy-MM-dd") + " 23:59:59");
                parms.Add(parm);
            }
            else
            {
                if (startDate != DateTime.MinValue)
                {
                    if (sqlWhere == null) sqlWhere = new StringBuilder(300);
                    if (parms == null) parms = new ParamsHelper();

                    sqlWhere.Append(@"and (RecordDate >= @StartDate) ");
                    var parm = new SqlParameter("@StartDate", SqlDbType.DateTime);
                    parm.Value = startDate;
                    parms.Add(parm);
                }
                if (endDate != DateTime.MinValue)
                {
                    if (sqlWhere == null) sqlWhere = new StringBuilder(300);
                    if (parms == null) parms = new ParamsHelper();

                    sqlWhere.Append(@"and (RecordDate <= @EndDate) ");
                    var parm = new SqlParameter("@EndDate", SqlDbType.DateTime);
                    parm.Value = DateTime.Parse(endDate.ToString("yyyy-MM-dd") + " 23:59:59");
                    parms.Add(parm);
                }
            }

            #endregion

            #endregion

            var ds = bll.GetExportData(sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());
            var dt = ds.Tables[0];
            OpenXmlHelper.Export(context, dt);
        }

        private void ExportCustomer(HttpContext context)
        {
            var bll = new Customer();

            StringBuilder sqlWhere = null;
            ParamsHelper parms = null;

            #region 构造查询条件

            var sKeyword = context.Request.QueryString["Keyword"];

            if (!string.IsNullOrWhiteSpace(sKeyword))
            {
                if (sqlWhere == null) sqlWhere = new StringBuilder(100);
                if (parms == null) parms = new ParamsHelper();
                sqlWhere.Append("and (c.Coded like @Keyword or c.Named like @Keyword) ");
                var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 50);
                parm.Value = parm.Value = "%" + sKeyword + "%";
                parms.Add(parm);
            }

            #endregion

            var ds = bll.GetExportData(sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());
            var dt = ds.Tables[0];
            OpenXmlHelper.Export(context, dt);
        }

        #region 系统管理

        private void SaveMenuAccess(HttpContext context)
        {
            if (!(HttpContext.Current.User.IsInRole("Administrators") || HttpContext.Current.User.IsInRole("System"))) throw new ArgumentException(MC.Role_InvalidError);

            var sRoleName = context.Request.Form["RoleName"];
            var sUserName = context.Request.Form["UserName"];
            var sMenuItemJson = context.Request.Form["MenuItemJson"];

            if (string.IsNullOrWhiteSpace(sMenuItemJson)) throw new ArgumentException(MC.Request_Params_InvalidError);
            sMenuItemJson = HttpUtility.UrlDecode(sMenuItemJson);
            if (string.IsNullOrWhiteSpace(sRoleName) && string.IsNullOrWhiteSpace(sUserName)) throw new ArgumentException(MC.Request_Params_InvalidError);
            List<SiteMenusAccessItemInfo> list = JsonConvert.DeserializeObject<List<SiteMenusAccessItemInfo>>(sMenuItemJson);
            var accessId = Guid.Empty;
            var isRole = !string.IsNullOrWhiteSpace(sRoleName);
            var accessType = isRole ? "Roles" : "Users";
            if (isRole)
            {
                if (sRoleName.ToLower() == "administrators") throw new ArgumentException(MC.GetString(MC.Params_SaveRoleAccessError, sRoleName));

                var roleBll = new SiteRoles();
                accessId = roleBll.GetAspnetModel(Membership.ApplicationName, sRoleName).Id;
            }
            else
            {
                if (Roles.GetRolesForUser(sUserName).Contains("administrators")) throw new ArgumentException(MC.GetString(MC.Params_SaveUserAccessError, sUserName));

                accessId = Guid.Parse(Membership.GetUser(sUserName).ProviderUserKey.ToString());
            }
            var menuBll = new SiteMenus();
            var maBll = new SiteMenusAccess();
            List<SiteMenusAccessItemInfo> maitems = null;
            var appId = new Applications().GetAspnetAppId(Membership.ApplicationName);
            var menusAccessInfo = maBll.GetModel(appId,accessId);
            if (menusAccessInfo != null) maitems = JsonConvert.DeserializeObject<List<SiteMenusAccessItemInfo>>(menusAccessInfo.OperationAccess);
            else maitems = new List<SiteMenusAccessItemInfo>();

            foreach (var item in list)
            {
                var menuId = Guid.Parse(item.MenuId.ToString());

                var itemIndex = maitems.FindIndex(m => m.MenuId.Equals(menuId));
                if (itemIndex > -1) maitems[itemIndex] = item;
                else maitems.Add(item);
            }

            if (menusAccessInfo != null)
            {
                menusAccessInfo.OperationAccess = JsonConvert.SerializeObject(maitems);
                maBll.Update(menusAccessInfo);
            }
            else
            {
                menusAccessInfo = new SiteMenusAccessInfo(appId,accessId, JsonConvert.SerializeObject(maitems), accessType);
                maBll.Insert(menusAccessInfo);
            }

            context.Response.Write(ResResult.ResJsonString(true, "", ""));
        }

        #endregion
    }
}