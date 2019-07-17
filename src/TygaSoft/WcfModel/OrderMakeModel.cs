using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "OrderMakeModel")]
    public class OrderMakeModel
    {
        [DataMember]
        public int PageIndex { get; set; }

        [DataMember]
        public int PageSize { get; set; }

        [DataMember]
        public string OrderCode { get; set; }

        [DataMember]
        public string CustomerCode { get; set; }

        [DataMember]
        public string StartDate { get; set; }

        [DataMember]
        public string EndDate { get; set; }

        [DataMember]
        public string StaffCode { get; set; }

        [DataMember]
        public string PayWay { get; set; }

        [DataMember]
        public string ServiceProduct { get; set; }
    }
}
