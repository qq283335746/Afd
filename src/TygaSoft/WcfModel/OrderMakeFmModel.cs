using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "OrderMakeFmModel")]
    public class OrderMakeFmModel
    {
        [DataMember]
        public object Id { get; set; }

        [DataMember]
        public string CustomerCode { get; set; }

        [DataMember]
        public string OrderCode { get; set; }

        [DataMember]
        public string FromName { get; set; }

        [DataMember]
        public string FromAddress { get; set; }

        [DataMember]
        public string FromPhone { get; set; }

        [DataMember]
        public string ToCity { get; set; }

        [DataMember]
        public string ToName { get; set; }

        [DataMember]
        public string ToAddress { get; set; }

        [DataMember]
        public string ToPhone { get; set; }

        [DataMember]
        public string StaffCode { get; set; }

        [DataMember]
        public string StaffCodeOfTake { get; set; }

        [DataMember]
        public string TakeTime { get; set; }

        [DataMember]
        public string ReachTime { get; set; }

        [DataMember]
        public string CargoName { get; set; }

        [DataMember]
        public string ServiceProduct { get; set; }

        [DataMember]
        public string PieceQty { get; set; }

        [DataMember]
        public string Weight { get; set; }

        [DataMember]
        public string TranPrice { get; set; }

        [DataMember]
        public string IncreServicePrice { get; set; }

        [DataMember]
        public string TotalPrice { get; set; }

        [DataMember]
        public string PayWay { get; set; }

        [DataMember]
        public string Remark { get; set; }
    }
}
