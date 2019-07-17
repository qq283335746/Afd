using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Transactions;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TygaSoft.SysException;
using TygaSoft.SysHelper;
using TygaSoft.DBUtility;
using TygaSoft.Model;
using TygaSoft.WcfModel;
using TygaSoft.BLL;
using TygaSoft.WebHelper;
using TygaSoft.CustomProvider;

namespace TygaSoft.WcfService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AfdService: IAfd
    {
        #region 订单管理

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetOrderMakeList(OrderMakeModel model)
        {
            try
            {
                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;
                var bll = new OrderMake();
                StringBuilder sqlWhere = null;
                ParamsHelper parms = null;

                #region 构造查询条件

                new Auth().CreateSearchItem(ref sqlWhere, ref parms);

                if (!string.IsNullOrWhiteSpace(model.OrderCode))
                {
                    if (sqlWhere == null) sqlWhere = new StringBuilder(500);
                    if (parms == null) parms = new ParamsHelper();
                    sqlWhere.Append("and OrderCode like @OrderCode ");
                    var parm = new SqlParameter("@OrderCode", SqlDbType.VarChar, 36);
                    parm.Value = "%" + model.OrderCode + "%";
                    parms.Add(parm);
                }
                if (!string.IsNullOrWhiteSpace(model.CustomerCode))
                {
                    if (sqlWhere == null) sqlWhere = new StringBuilder(400);
                    if (parms == null) parms = new ParamsHelper();
                    sqlWhere.Append("and CustomerCode like @CustomerCode ");
                    var parm = new SqlParameter("@CustomerCode", SqlDbType.VarChar, 36);
                    parm.Value = "%" + model.CustomerCode + "%";
                    parms.Add(parm);
                }
                if (!string.IsNullOrWhiteSpace(model.StaffCode))
                {
                    if (sqlWhere == null) sqlWhere = new StringBuilder(300);
                    if (parms == null) parms = new ParamsHelper();
                    sqlWhere.Append("and StaffCode like @StaffCode ");
                    var parm = new SqlParameter("@StaffCode", SqlDbType.VarChar, 36);
                    parm.Value = "%" + model.StaffCode + "%";
                    parms.Add(parm);
                }
                if (!string.IsNullOrWhiteSpace(model.PayWay))
                {
                    if (sqlWhere == null) sqlWhere = new StringBuilder(200);
                    if (parms == null) parms = new ParamsHelper();
                    sqlWhere.Append("and PayWay like @PayWay ");
                    var parm = new SqlParameter("@PayWay", SqlDbType.VarChar, 36);
                    parm.Value = "%" + model.PayWay + "%";
                    parms.Add(parm);
                }
                if (!string.IsNullOrWhiteSpace(model.ServiceProduct))
                {
                    if (sqlWhere == null) sqlWhere = new StringBuilder(100);
                    if (parms == null) parms = new ParamsHelper();
                    sqlWhere.Append("and ServiceProduct like @ServiceProduct ");
                    var parm = new SqlParameter("@ServiceProduct", SqlDbType.VarChar, 36);
                    parm.Value = "%" + model.ServiceProduct + "%";
                    parms.Add(parm);
                }

                #region 开始与结束日期段

                DateTime startDate = DateTime.MinValue;
                DateTime endDate = DateTime.MinValue;
                if (!string.IsNullOrWhiteSpace(model.StartDate)) DateTime.TryParse(model.StartDate, out startDate);
                if (!string.IsNullOrWhiteSpace(model.EndDate)) DateTime.TryParse(model.EndDate, out endDate);
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

                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());

                return ResResult.Response(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveOrderMake(OrderMakeFmModel model)
        {
            try
            {
                Guid Id = Guid.Empty;
                if (model.Id != null) Guid.TryParse(model.Id.ToString(), out Id);
                DateTime takeTime = DateTime.MinValue;
                if (model.TakeTime != null) DateTime.TryParse(model.TakeTime, out takeTime);
                if (takeTime == DateTime.MinValue) takeTime = DateTime.Parse("1754-01-01");
                DateTime reachTime = DateTime.MinValue;
                if (!string.IsNullOrWhiteSpace(model.ReachTime)) DateTime.TryParse(model.ReachTime, out reachTime);
                if (reachTime == DateTime.MinValue) reachTime = DateTime.Parse("1754-01-01");
                int pieceQty = 0;
                if (!string.IsNullOrWhiteSpace(model.PieceQty)) int.TryParse(model.PieceQty, out pieceQty);
                double weight = 0d;
                if (!string.IsNullOrWhiteSpace(model.Weight)) double.TryParse(model.Weight, out weight);
                decimal tranPrice = 0;
                if (!string.IsNullOrWhiteSpace(model.TranPrice)) decimal.TryParse(model.TranPrice, out tranPrice);
                decimal increServicePrice = 0;
                if (!string.IsNullOrWhiteSpace(model.IncreServicePrice)) decimal.TryParse(model.IncreServicePrice, out increServicePrice);
                decimal totalPrice = 0;
                if (!string.IsNullOrWhiteSpace(model.TotalPrice)) decimal.TryParse(model.TotalPrice, out totalPrice);
                var currTime = DateTime.Now;
                var userId = WebCommon.GetUserId();

                CustomProfileCommon Profile = new CustomProfileCommon();
                var upi = JsonConvert.DeserializeObject<UserProfileInfo>(Profile.UserInfo);

                var modelInfo = new OrderMakeInfo(Id,upi.SiteCode, userId, model.CustomerCode, model.OrderCode, model.FromName, model.FromAddress, model.FromPhone, model.ToCity, model.ToName, model.ToAddress, model.ToPhone, model.StaffCode,model.StaffCodeOfTake, takeTime, reachTime, model.CargoName, model.ServiceProduct, pieceQty, weight, tranPrice, increServicePrice, totalPrice, model.PayWay, model.Remark, currTime, currTime);

                var bll = new OrderMake();
                int effect = -1;

                if (Id.Equals(Guid.Empty))
                {
                    effect = bll.Insert(modelInfo);
                }
                else
                {
                    var oldInfo = bll.GetModel(Id);
                    modelInfo.RecordDate = oldInfo.RecordDate;
                    effect = bll.Update(modelInfo);
                }
                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, "");

                return ResResult.Response(true, "", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteOrderMake(string itemAppend)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.删除, true);

                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");
                var items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                var bll = new OrderMake();
                var opBll = new OrderProcess();
                var effect = 0;

                foreach(var item in items)
                {
                    var orderId = Guid.Parse(item);
                    effect += bll.Delete(orderId);
                    effect += opBll.DeleteByOrder(orderId);
                }

                return ResResult.Response(true, MC.M_Save_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel ExportOrderMake(OrderMakeModel model)
        {
            try
            {
                return ResResult.Response(true, MC.M_Save_Ok, "");
            }
            catch(Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetOrderProcessList(ListModel model)
        {
            try
            {
                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;
                var bll = new OrderProcess();
                StringBuilder sqlWhere = null;
                ParamsHelper parms = null;
                var orderId = Guid.Empty;
                if(model.ParentId != null && Guid.TryParse(model.ParentId.ToString(),out orderId))
                {
                    sqlWhere = new StringBuilder(100);
                    parms = new ParamsHelper();
                    sqlWhere.Append("and OrderId = @OrderId ");
                    var parm = new SqlParameter("@OrderId", SqlDbType.UniqueIdentifier);
                    parm.Value = orderId;
                    parms.Add(parm);
                }
                if(!string.IsNullOrWhiteSpace(model.Keyword))
                {
                    sqlWhere = new StringBuilder(50);
                    parms = new ParamsHelper();
                    sqlWhere.Append("and o.OrderCode = @OrderCode ");
                    var parm = new SqlParameter("@OrderCode", SqlDbType.VarChar,20);
                    parm.Value = model.Keyword;
                    parms.Add(parm);
                }

                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null: parms.ToArray());

                return ResResult.Response(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }


        #endregion

        #region 基础数据

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetCustomerList(ListModel model)
        {
            try
            {
                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;
                var bll = new Customer();
                StringBuilder sqlWhere = null;
                ParamsHelper parms = null;

                new Auth().CreateSearchItem(ref sqlWhere, ref parms);

                if (!string.IsNullOrWhiteSpace(model.Keyword))
                {
                    parms = new ParamsHelper();
                    sqlWhere = new StringBuilder("and (Coded like @Keyword or Named like @Keyword) ");
                    var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 50);
                    parm.Value = "%" + model.Keyword + "%";
                    parms.Add(parm);
                }

                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());

                return ResResult.Response(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveCustomer(CustomerFmModel model)
        {
            try
            {
                Guid Id = Guid.Empty;
                if (model.Id != null) Guid.TryParse(model.Id.ToString(), out Id);
                DateTime cooperateTime = DateTime.MinValue;
                if (model.CooperateTime != null) DateTime.TryParse(model.CooperateTime, out cooperateTime);
                if (cooperateTime == DateTime.MinValue) cooperateTime = DateTime.Parse("1754-01-01");
                DateTime agreementTimeout = DateTime.MinValue;
                if (model.AgreementTimeout != null) DateTime.TryParse(model.AgreementTimeout, out agreementTimeout);
                if (agreementTimeout == DateTime.MinValue) agreementTimeout = DateTime.Parse("1754-01-01");
                decimal joinPrice = 0;
                if (model.JoinPrice != null) decimal.TryParse(model.JoinPrice, out joinPrice);
                string discountAbout = string.Empty;
                if (model.DiscountAbout != null) discountAbout = model.DiscountAbout.Trim();
                var currTime = DateTime.Now;

                CustomProfileCommon Profile = new CustomProfileCommon();
                var upi = JsonConvert.DeserializeObject<UserProfileInfo>(Profile.UserInfo);

                var modelInfo = new CustomerInfo(Id, upi.SiteCode, WebCommon.GetUserId(), model.Coded, model.Named, model.ShortName, model.ContactMan, model.ContactPhone, model.TelPhone, model.Fax, model.PostCode, model.Address, model.CityName, model.TradeName, cooperateTime, agreementTimeout, joinPrice, discountAbout, model.PayWay, model.StaffCode, model.Remark, currTime, currTime);

                var bll = new Customer();
                int effect = -1;

                if (Id.Equals(Guid.Empty))
                {
                    effect = bll.Insert(modelInfo);
                }
                else
                {
                    effect = bll.Update(modelInfo);
                }
                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, "");

                return ResResult.Response(true, "", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteCustomer(string itemAppend)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.删除, true);

                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");
                var items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                var bll = new Customer();

                if (!bll.DeleteBatch((IList<object>)items.ToList<object>()))
                {
                    return ResResult.Response(false, MC.M_Save_Error, "");
                }

                return ResResult.Response(true, MC.M_Save_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region 系统管理

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ResResultModel GetFeatureUserInfo(string username,string typeName)
        {
            try
            {
                var bll = new FeatureUser();
                var fuInfo = bll.GetModel(SecurityService.GetUserId(username), typeName);
                if (fuInfo == null) fuInfo = new FeatureUserInfo();
                return ResResult.Response(true, "", JsonConvert.SerializeObject(fuInfo));
            }
            catch(Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveFeatureUser(FeatureUserFmModel model)
        {
            try
            {
                var featureId = Guid.Empty;
                if (!string.IsNullOrWhiteSpace(model.FeatureId)) Guid.TryParse(model.FeatureId, out featureId);
                var userId = SecurityService.GetUserId(model.UserName);

                var fuBll = new FeatureUser();
                fuBll.DoFeatureUser(userId, featureId, model.TypeName, true);

                return ResResult.Response(true, "", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region 图片、文件管理

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetSitePictureList(ListModel model)
        {
            try
            {
                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;
                var bll = new SitePicture();

                var list = bll.GetCbbList(model.PageIndex, model.PageSize, out totalRecord, model.Keyword);
                return ResResult.Response(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteSitePicture(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");
                var items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                var bll = new SitePicture();
                if (!bll.DeleteBatch((IList<object>)items.ToList<object>()))
                {
                    return ResResult.Response(false, MC.M_Save_Error, "");
                }

                return ResResult.Response(true, MC.M_Save_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion
    }
}
