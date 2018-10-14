using System;
using System.IO;
using System.ServiceModel;
using TygaSoft.WcfModel;

namespace TygaSoft.WcfService
{
    [ServiceContract(Namespace = "http://TygaSoft.Services.PdaService")]
    public interface IPda
    {
        [OperationContract(Name = "GetHelloWord")]
        string GetHelloWord();

        [OperationContract(Name = "ValidateUser")]
        string ValidateUser(string platform, string deviceid, string latlng, string username, string password);

        [OperationContract(Name = "GetOrderProcessInfo")]
        string GetOrderProcessInfo(Guid Id);

        [OperationContract(Name = "SaveOrderScan")]
        string SaveOrderScan(string orderCode, string customerCode, string orderStep, string loginId, string deviceId, string latlng);
    }
}
