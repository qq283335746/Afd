using System;

namespace TygaSoft.Model
{
    [Serializable]
    public class PdaOrderScanFmInfo
    {
        public string Barcode { get; set; }

        public string CustomerCode { get; set; }

        public string OrderStep { get; set; }

        public string LoginId { get; set; }

        public string DeviceId { get; set; }

        public string Latlng { get; set; }

        public string Ip { get; set; }
    }
}
