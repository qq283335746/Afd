using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Transactions;
using System.Web.Configuration;
using TygaSoft.WebHelper;
using TygaSoft.DBUtility;
using TygaSoft.BLL;
using TygaSoft.Model;
using TygaSoft.SysHelper;
using TygaSoft.SysException;

namespace TygaSoft.Web.Handlers
{
    public class HandlerUpload : IHttpHandler
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
                    case "UploadPdaOrderProcess":
                        UploadPdaOrderProcess(context);
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

        private void UploadPdaOrderProcess(HttpContext context)
        {
            #region 请求参数集

            var Id = Guid.Empty;
            if (context.Request.Form["Id"] != null) Guid.TryParse(context.Request.Form["Id"], out Id);
            if (Id.Equals(Guid.Empty)) throw new ArgumentException(MC.Request_Params_InvalidError);
            //string orderCode = string.Empty;
            //if (context.Request.Form["OrderCode"] != null) orderCode = context.Request.Form["OrderCode"].Trim();
            //string customerCode = string.Empty;
            //if (context.Request.Form["CustomerCode"] != null) customerCode = context.Request.Form["CustomerCode"].Trim();
            string stepName = string.Empty;
            if (context.Request.Form["OrderStep"] != null) stepName = context.Request.Form["OrderStep"].Trim();
            string staffCode = string.Empty;
            if (context.Request.Form["LoginId"] != null) staffCode = context.Request.Form["LoginId"].Trim();
            var picture = string.Empty;
            string deviceId = string.Empty;
            if (context.Request.Form["DeviceId"] != null) deviceId = context.Request.Form["DeviceId"].Trim();
            string latlng = string.Empty;
            if (context.Request.Form["Latlng"] != null) latlng = context.Request.Form["Latlng"].Trim();
            string latlngPlace = string.Empty;
            string ip = HttpClientHelper.GetClientIp(context);
            string ipPlace = string.Empty;

            #endregion

            new CustomException(string.Format("Id：{0}--stepName：{1}--staffCode：{2}--picture：{3}--deviceId：{4}--latlng：{5}--ip：{6}", Id, stepName, staffCode, picture, deviceId, latlng, ip));

            HttpFileCollection files = context.Request.Files;
            if (files.Count > 0)
            {
                ImagesHelper ih = new ImagesHelper();
                foreach (string item in files.AllKeys)
                {
                    HttpPostedFile file = files[item];
                    if (file == null || file.ContentLength == 0) continue;

                    FileValidated(file);

                    string originalUrl = UploadFilesHelper.UploadOriginalFile(file, "PdaPictures");
                    picture = originalUrl.Trim('~');

                    CreateThumbnailImage(context, ih, context.Server.MapPath(originalUrl));
                }
            }

            var pictures = new List<string>();
            //var orderId = Guid.Empty;
            //var oldOrderInfo = new OrderMake().GetModel(orderCode, customerCode);
            //if (oldOrderInfo != null) orderId = oldOrderInfo.Id;

            var opBll = new OrderProcess();
            var effect = 0;

            var currTime = DateTime.Now;
            var opInfo = new OrderProcessInfo(Id, Guid.Empty, staffCode, stepName, picture, deviceId, latlng, latlngPlace, ip, ipPlace, currTime, currTime);
            var oldOpInfo = opBll.GetModel(Id);
            if (oldOpInfo != null)
            {
                opInfo.OrderId = oldOpInfo.OrderId;
                if (!string.IsNullOrWhiteSpace(oldOpInfo.Pictures))
                {
                    pictures = oldOpInfo.Pictures.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    if (!pictures.Any(s => s.Equals(picture))) pictures.Add(picture);
                }
                else pictures.Add(picture);
                opInfo.Pictures = string.Join("|", pictures);
                if (oldOpInfo.Ip == ip) opInfo.IpPlace = oldOpInfo.IpPlace;
                if (oldOpInfo.Latlng == latlng) opInfo.LatlngPlace = oldOpInfo.LatlngPlace;
                effect = opBll.Update(opInfo);
            }
            else throw new ArgumentException(MC.Data_NotExist);

            if(effect > 0) context.Response.Write(ResResult.ResJsonString(true, "", string.Join("|", pictures)));
            else context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
        }

        #region 私有方法

        private void FileValidated(HttpPostedFile file)
        {
            int fileSize = file.ContentLength;
            int uploadFileSize = int.Parse(WebConfigurationManager.AppSettings["UploadFileSize"]);
            if (fileSize > uploadFileSize) throw new ArgumentException("文件【" + file.FileName + "】大小超出字节" + uploadFileSize + "，无法上传，请正确操作！");
            if (!UploadFilesHelper.IsFileValidated(file.InputStream, fileSize))
            {
                new CustomException("上传了非法文件--" + file.FileName);
                throw new ArgumentException("文件【" + file.FileName + "】为受限制的文件，请正确操作！");
            }
        }

        private void CreateThumbnailImage(HttpContext context, ImagesHelper ih, string originalPath)
        {
            var ext = Path.GetExtension(originalPath);
            var rndFolder = Path.GetFileNameWithoutExtension(originalPath);
            string[] platformNames = Enum.GetNames(typeof(EnumData.Platform));
            var index = 0;
            foreach (string name in platformNames)
            {
                string sizeAppend = WebConfigurationManager.AppSettings[name];
                string[] sizeArr = sizeAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < sizeArr.Length; i++)
                {
                    string bmsPath = string.Format("{0}\\{1}_{2}{3}{4}", Path.GetDirectoryName(originalPath), rndFolder, index, i+1, ext);
                    string[] wh = sizeArr[i].Split('*');

                    ih.CreateThumbnailImage(originalPath, bmsPath, int.Parse(wh[0]), int.Parse(wh[1]), "DB", ext);
                }
                index++;
            }
        }

        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}