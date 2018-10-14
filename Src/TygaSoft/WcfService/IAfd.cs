using System;
using System.ServiceModel;
using TygaSoft.WcfModel;

namespace TygaSoft.WcfService
{
    [ServiceContract(Namespace = "http://TygaSoft.Services.AfdService")]
    public interface IAfd
    {
        #region 订单管理

        [OperationContract(Name = "GetOrderMakeList")]
        ResResultModel GetOrderMakeList(OrderMakeModel model);

        [OperationContract(Name = "SaveOrderMake")]
        ResResultModel SaveOrderMake(OrderMakeFmModel model);

        [OperationContract(Name = "DeleteOrderMake")]
        ResResultModel DeleteOrderMake(string itemAppend);

        [OperationContract(Name = "GetOrderProcessList")]
        ResResultModel GetOrderProcessList(ListModel model);

        #endregion

        #region 基础数据

        [OperationContract(Name = "GetCustomerList")]
        ResResultModel GetCustomerList(ListModel model);

        [OperationContract(Name = "SaveCustomer")]
        ResResultModel SaveCustomer(CustomerFmModel model);

        [OperationContract(Name = "DeleteCustomer")]
        ResResultModel DeleteCustomer(string itemAppend);

        #endregion

        #region 系统管理

        [OperationContract(Name = "GetFeatureUserInfo")]
        ResResultModel GetFeatureUserInfo(string username, string typeName);

        [OperationContract(Name = "SaveFeatureUser")]
        ResResultModel SaveFeatureUser(FeatureUserFmModel model);

        #endregion

        #region 图片、文件管理

        [OperationContract(Name = "GetSitePictureList")]
        ResResultModel GetSitePictureList(ListModel model);

        [OperationContract(Name = "DeleteSitePicture")]
        ResResultModel DeleteSitePicture(string itemAppend);

        #endregion

    }
}
