using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DBUtility;

namespace TygaSoft.SqlServerDAL
{
    public partial class OrderMake
    {
        #region IOrderMake Member

        public DataSet GetExportData(string sqlWhere, params SqlParameter[] cmdParms)
        {
            var sb = new StringBuilder(1000);
            sb.Append(@"select o.OrderCode '单号',o.CustomerCode '客户编码',o.StaffCode '配送员工号',o.ServiceProduct '服务产品',o.PayWay '付款方式',o.FromName '寄方客户',o.FromAddress '寄方地址',o.FromPhone '寄方联系电话',o.ToCity '目的地',o.ToName '收货人',o.ToAddress '收货地址',o.ToPhone '收货人联系电话'
                          ,o.TakeTime '取件时间',o.ReachTime '送达时间',o.CargoName '配送物品',o.PieceQty '件数',o.Weight '重量',o.TranPrice '应收运费',o.IncreServicePrice '增值服务费',o.TotalPrice '费用合计',o.Remark '备注'
					      from OrderMake o
                         ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by RecordDate desc ");
            return SqlHelper.ExecuteDataset(SqlHelper.AfdDbConnString, CommandType.Text, sb.ToString(), cmdParms);
        }

        public OrderMakeInfo GetModel(string orderCode,string customerCode)
        {
            OrderMakeInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,CustomerCode,OrderCode,FromName,FromAddress,FromPhone,ToCity,ToName,ToAddress,ToPhone,StaffCode,StaffCodeOfTake,TakeTime,ReachTime,CargoName,ServiceProduct,PieceQty,Weight,TranPrice,IncreServicePrice,TotalPrice,PayWay,Remark,RecordDate,LastUpdatedDate 
			            from OrderMake
						where OrderCode = @OrderCode ");
            SqlParameter[] parms = {
                                     new SqlParameter("@OrderCode",SqlDbType.VarChar,36)
                                   };
            parms[0].Value = orderCode;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AfdDbConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new OrderMakeInfo();
                        model.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.UserId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.CustomerCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.OrderCode = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        model.FromName = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.FromAddress = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.FromPhone = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.ToCity = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.ToName = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.ToAddress = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.ToPhone = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                        model.StaffCode = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                        model.StaffCodeOfTake = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                        model.TakeTime = reader.IsDBNull(13) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(13);
                        model.ReachTime = reader.IsDBNull(14) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(14);
                        model.CargoName = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                        model.ServiceProduct = reader.IsDBNull(16) ? string.Empty : reader.GetString(16);
                        model.PieceQty = reader.IsDBNull(17) ? 0 : reader.GetInt32(17);
                        model.Weight = reader.IsDBNull(18) ? 0D : reader.GetDouble(18);
                        model.TranPrice = reader.IsDBNull(19) ? 0 : reader.GetDecimal(19);
                        model.IncreServicePrice = reader.IsDBNull(20) ? 0 : reader.GetDecimal(20);
                        model.TotalPrice = reader.IsDBNull(21) ? 0 : reader.GetDecimal(21);
                        model.PayWay = reader.IsDBNull(22) ? string.Empty : reader.GetString(22);
                        model.Remark = reader.IsDBNull(23) ? string.Empty : reader.GetString(23);
                        model.RecordDate = reader.IsDBNull(24) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(24);
                        model.LastUpdatedDate = reader.IsDBNull(25) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(25);
                    }
                }
            }

            return model;
        }

        public OrderMakeInfo GetModelByJoin(Guid id)
        {
            OrderMakeInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,CustomerCode,OrderCode,FromName,FromAddress,FromPhone,ToCity,ToName,ToAddress,ToPhone,StaffCode,StaffCodeOfTake,TakeTime,ReachTime,CargoName,ServiceProduct,PieceQty,Weight,TranPrice,IncreServicePrice,TotalPrice,PayWay,Remark,RecordDate,LastUpdatedDate 
			            from OrderMake
						where Id = @Id ");
            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = id;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AfdDbConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new OrderMakeInfo();
                        model.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.UserId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.CustomerCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.OrderCode = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        model.FromName = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.FromAddress = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.FromPhone = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.ToCity = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.ToName = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.ToAddress = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.ToPhone = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                        model.StaffCode = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                        model.StaffCodeOfTake = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                        model.TakeTime = reader.IsDBNull(13) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(13);
                        model.ReachTime = reader.IsDBNull(14) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(14);
                        model.CargoName = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                        model.ServiceProduct = reader.IsDBNull(16) ? string.Empty : reader.GetString(16);
                        model.PieceQty = reader.IsDBNull(17) ? 0 : reader.GetInt32(17);
                        model.Weight = reader.IsDBNull(18) ? 0D : reader.GetDouble(18);
                        model.TranPrice = reader.IsDBNull(19) ? 0 : reader.GetDecimal(19);
                        model.IncreServicePrice = reader.IsDBNull(20) ? 0 : reader.GetDecimal(20);
                        model.TotalPrice = reader.IsDBNull(21) ? 0 : reader.GetDecimal(21);
                        model.PayWay = reader.IsDBNull(22) ? string.Empty : reader.GetString(22);
                        model.Remark = reader.IsDBNull(23) ? string.Empty : reader.GetString(23);
                        model.RecordDate = reader.IsDBNull(24) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(24);
                        model.LastUpdatedDate = reader.IsDBNull(25) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(25);

                        model.STakeTime = model.TakeTime.ToString("yyyy-MM-dd") == "1754-01-01" ? "" : model.TakeTime.ToString("yyyy-MM-dd HH:mm:ss");
                        model.SReachTime = model.ReachTime.ToString("yyyy-MM-dd") == "1754-01-01" ? "" : model.ReachTime.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
            }

            return model;
        }

        public IList<OrderMakeInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from OrderMake ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AfdDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<OrderMakeInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,UserId,CustomerCode,OrderCode,FromName,FromAddress,FromPhone,ToCity,ToName,ToAddress,ToPhone,StaffCode,StaffCodeOfTake,TakeTime,ReachTime,CargoName,ServiceProduct,PieceQty,Weight,TranPrice,IncreServicePrice,TotalPrice,PayWay,Remark,RecordDate,LastUpdatedDate
					  from OrderMake ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<OrderMakeInfo> list = new List<OrderMakeInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AfdDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderMakeInfo model = new OrderMakeInfo();
                        model.Id = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.CustomerCode = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        model.OrderCode = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.FromName = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.FromAddress = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.FromPhone = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.ToCity = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.ToName = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.ToAddress = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                        model.ToPhone = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                        model.StaffCode = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                        model.StaffCodeOfTake = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                        model.TakeTime = reader.IsDBNull(14) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(14);
                        model.ReachTime = reader.IsDBNull(15) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(15);
                        model.CargoName = reader.IsDBNull(16) ? string.Empty : reader.GetString(16);
                        model.ServiceProduct = reader.IsDBNull(17) ? string.Empty : reader.GetString(17);
                        model.PieceQty = reader.IsDBNull(18) ? 0 : reader.GetInt32(18);
                        model.Weight = reader.IsDBNull(19) ? 0D : reader.GetDouble(19);
                        model.TranPrice = reader.IsDBNull(20) ? 0 : reader.GetDecimal(20);
                        model.IncreServicePrice = reader.IsDBNull(21) ? 0 : reader.GetDecimal(21);
                        model.TotalPrice = reader.IsDBNull(22) ? 0 : reader.GetDecimal(22);
                        model.PayWay = reader.IsDBNull(23) ? string.Empty : reader.GetString(23);
                        model.Remark = reader.IsDBNull(24) ? string.Empty : reader.GetString(24);

                        model.STakeTime = model.TakeTime.ToString("yyyy-MM-dd") == "1754-01-01" ? "" : model.TakeTime.ToString("yyyy-MM-dd HH:mm:ss");
                        model.SReachTime = model.ReachTime.ToString("yyyy-MM-dd") == "1754-01-01" ? "" : model.ReachTime.ToString("yyyy-MM-dd HH:mm:ss");

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
