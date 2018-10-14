using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class OrderProcessInfo
    {
        public OrderProcessInfo() { }

        public OrderProcessInfo(Guid id, Guid orderId, string staffCode, string stepName, string pictures, string deviceId, string latlng, string latlngPlace, string ip, string ipPlace, DateTime recordDate, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.OrderId = orderId;
            this.StaffCode = staffCode;
            this.StepName = stepName;
            this.Pictures = pictures;
            this.DeviceId = deviceId;
            this.Latlng = latlng;
            this.LatlngPlace = latlngPlace;
            this.Ip = ip;
            this.IpPlace = ipPlace;
            this.RecordDate = recordDate;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public string StaffCode { get; set; }
        public string StepName { get; set; }
        public string Pictures { get; set; }
        public string DeviceId { get; set; }
        public string Latlng { get; set; }
        public string LatlngPlace { get; set; }
        public string Ip { get; set; }
        public string IpPlace { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
