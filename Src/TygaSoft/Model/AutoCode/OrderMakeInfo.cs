using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class OrderMakeInfo
    {
        public OrderMakeInfo() { }

        public OrderMakeInfo(Guid id, string appCode, Guid userId, string customerCode, string orderCode, string fromName, string fromAddress, string fromPhone, string toCity, string toName, string toAddress, string toPhone, string staffCode, string staffCodeOfTake, DateTime takeTime, DateTime reachTime, string cargoName, string serviceProduct, int pieceQty, double weight, decimal tranPrice, decimal increServicePrice, decimal totalPrice, string payWay, string remark, DateTime recordDate, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.AppCode = appCode;
            this.UserId = userId;
            this.CustomerCode = customerCode;
            this.OrderCode = orderCode;
            this.FromName = fromName;
            this.FromAddress = fromAddress;
            this.FromPhone = fromPhone;
            this.ToCity = toCity;
            this.ToName = toName;
            this.ToAddress = toAddress;
            this.ToPhone = toPhone;
            this.StaffCode = staffCode;
            this.StaffCodeOfTake = staffCodeOfTake;
            this.TakeTime = takeTime;
            this.ReachTime = reachTime;
            this.CargoName = cargoName;
            this.ServiceProduct = serviceProduct;
            this.PieceQty = pieceQty;
            this.Weight = weight;
            this.TranPrice = tranPrice;
            this.IncreServicePrice = increServicePrice;
            this.TotalPrice = totalPrice;
            this.PayWay = payWay;
            this.Remark = remark;
            this.RecordDate = recordDate;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public string AppCode { get; set; }
        public Guid UserId { get; set; }
        public string CustomerCode { get; set; }
        public string OrderCode { get; set; }
        public string FromName { get; set; }
        public string FromAddress { get; set; }
        public string FromPhone { get; set; }
        public string ToCity { get; set; }
        public string ToName { get; set; }
        public string ToAddress { get; set; }
        public string ToPhone { get; set; }
        public string StaffCode { get; set; }
        public string StaffCodeOfTake { get; set; }
        public DateTime TakeTime { get; set; }
        public DateTime ReachTime { get; set; }
        public string CargoName { get; set; }
        public string ServiceProduct { get; set; }
        public int PieceQty { get; set; }
        public double Weight { get; set; }
        public decimal TranPrice { get; set; }
        public decimal IncreServicePrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string PayWay { get; set; }
        public string Remark { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
