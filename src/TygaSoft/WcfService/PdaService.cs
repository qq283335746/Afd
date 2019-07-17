using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Transactions;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using TygaSoft.SysException;
using TygaSoft.SysHelper;
using TygaSoft.BLL;
using TygaSoft.Model;
using TygaSoft.WcfModel;
using TygaSoft.WebHelper;
using TygaSoft.CustomProvider;

namespace TygaSoft.WcfService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PdaService : IPda
    {
        public string GetHelloWord()
        {
            return ResResult.ResJsonString(true, "", "Hello Word");
        }

        public string ValidateUser(string platform, string deviceid, string latlng, string username,string password)
        {
            try
            {
                new CustomException(string.Format("ValidateUser--platform：{0}，deviceid：{1}，latlng：{2}，username：{3}，password：{4}", platform, deviceid, latlng, username, password));

                new Auth().SetAnonymousLoginByPda(platform, deviceid, latlng, username, password);
                return ResResult.ResJsonString(true, "", "");
            }
            catch(Exception ex)
            {
                return ResResult.ResJsonString(false, ex.Message, "");
            }
        }

        public string GetOrderProcessInfo(Guid Id)
        {
            try
            {
                var opInfo = new OrderProcess().GetModel(Id);
                if(opInfo == null) return ResResult.ResJsonString(false, MC.Data_NotExist, "");

                return ResResult.ResJsonString(true, "", JsonConvert.SerializeObject(opInfo));
            }
            catch (Exception ex)
            {
                return ResResult.ResJsonString(false, ex.Message, "");
            }
        }

        public string SaveOrderScan(string orderCode, string customerCode, string orderStep, string loginId, string deviceId, string latlng)
        {
            try
            {
                new CustomException(string.Format("SaveOrderScan--orderCode：{0}--customerCode：{1}--orderStep：{2}--loginId：{3}--deviceId：{4}--latlng：{5}", orderCode, customerCode, orderStep, loginId, deviceId, latlng));

                orderCode = orderCode.Trim();
                customerCode = customerCode.Trim();
                orderStep = orderStep.Trim();
                loginId = loginId.Trim();
                deviceId = deviceId.Trim();
                latlng = latlng.Trim();
                var ip = HttpClientHelper.GetClientIp(HttpContext.Current);
                var currTime = DateTime.Now;
                var latlngPlace = string.Empty;
                var ipPlace = string.Empty;
                var orderId = Guid.Empty;
                var staffCodeOfTake = string.Empty;
                var staffCode = string.Empty;
                var takeTime = DateTime.Parse("1754-01-01");
                var reachTime = takeTime;
                if (orderStep == "已取件")
                {
                    staffCode = loginId;
                    takeTime = currTime;
                }
                else if (orderStep == "已送达")
                {
                    staffCodeOfTake = loginId;
                    reachTime = currTime;
                }
                

                var oBll = new OrderMake();
                var opBll = new OrderProcess();
                var effect = 0;
                var oldOrderInfo = oBll.GetModel(orderCode, customerCode);
                if (oldOrderInfo != null)
                {
                    orderId = oldOrderInfo.Id;
                    if (orderStep == "已送达")
                    {
                        oldOrderInfo.StaffCodeOfTake = staffCodeOfTake;
                        oldOrderInfo.ReachTime = reachTime;
                        oBll.Update(oldOrderInfo);
                    }
                }
                else
                {
                    var userId = WebCommon.GetUserId();
                    if (userId.Equals(Guid.Empty)) throw new ArgumentException(MC.Login_NotExist);
                    var appCode = Auth.AppCode;
                    var fuInfo = new FeatureUser().GetModel(userId, "UserProfile");
                    if (fuInfo != null) appCode = fuInfo.SiteCode;

                    orderId = Guid.NewGuid();
                    oldOrderInfo = new OrderMakeInfo(orderId, appCode, userId, customerCode, orderCode, "", "", "", "", "", "", "", staffCode,staffCodeOfTake, takeTime, reachTime, "", "", 0, 0, 0, 0, 0, "", "", currTime, currTime);
                    effect = oBll.InsertByOutput(oldOrderInfo);
                }
                var opInfo = new OrderProcessInfo(Guid.Empty, orderId, loginId, orderStep, "", deviceId, latlng, latlngPlace, ip, ipPlace, currTime, currTime);
                var oldOpInfo = opBll.GetModel(orderId, orderStep);
                if (oldOpInfo != null)
                {
                    opInfo.Id = oldOpInfo.Id;
                    opInfo.Pictures = oldOpInfo.Pictures;
                    if (oldOpInfo.Ip == ip) opInfo.IpPlace = oldOpInfo.IpPlace;
                    if (oldOpInfo.Latlng == latlng) opInfo.LatlngPlace = oldOpInfo.LatlngPlace;
                    effect = opBll.Update(opInfo);
                }
                else
                {
                    opInfo.Id = Guid.NewGuid();
                    effect = opBll.InsertByOutput(opInfo);
                }
                if(string.IsNullOrWhiteSpace(opInfo.IpPlace) || string.IsNullOrWhiteSpace(opInfo.LatlngPlace))
                {
                    new RunQueueAsyn().Insert(new RunQueueInfo(EnumData.EnumRunQueue.BaiduMapRestApi.ToString(), string.Format(@"{{""Latlng"":""{0}"",""Ip"":""{1}"",""OrderProcessId"":""{2}""}}", latlng,ip, opInfo.Id), EnumData.EnumRunQueue.BaiduMapRestApi.ToString(), 0));
                }
                if(effect > 0) return ResResult.ResJsonString(true, "", opInfo.Id);
                else return ResResult.ResJsonString(false, MC.M_Save_Error, "");
            }
            catch(Exception ex)
            {
                new CustomException("SaveOrderScan--", ex);
                return ResResult.ResJsonString(false, ex.Message, "");
            }
        }
    }
}
