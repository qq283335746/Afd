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
    public partial class OrderProcess
    {
        #region IOrderProcess Member

        public IList<OrderProcessInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from OrderProcess op
                        left join OrderMake o on o.Id = op.OrderId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AfdDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<OrderProcessInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by op.RecordDate) as RowNumber,
			          op.Id,op.OrderId,op.StaffCode,op.StepName,op.Pictures,op.DeviceId,op.Latlng,op.LatlngPlace,op.Ip,op.IpPlace,op.RecordDate,op.LastUpdatedDate
                      from OrderProcess op
                      left join OrderMake o on o.Id = op.OrderId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<OrderProcessInfo> list = new List<OrderProcessInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AfdDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderProcessInfo model = new OrderProcessInfo();
                        model.Id = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.OrderId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.StaffCode = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        model.StepName = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.Pictures = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.DeviceId = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.Latlng = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.LatlngPlace = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.Ip = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.IpPlace = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                        model.RecordDate = reader.IsDBNull(11) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(11);
                        model.LastUpdatedDate = reader.IsDBNull(12) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(12);

                        model.SRecordDate = model.RecordDate.ToString("yyyy-MM-dd") == "1754-01-01" ? "" : model.RecordDate.ToString("yyyy-MM-dd HH:mm:ss");

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public OrderProcessInfo GetModel(Guid orderId, string orderStep)
        {
            OrderProcessInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 op.Id,op.OrderId,op.StaffCode,op.StepName,op.Pictures,op.DeviceId,op.Latlng,op.LatlngPlace,op.Ip,op.IpPlace,op.RecordDate,op.LastUpdatedDate 
                        from OrderProcess op
						where op.OrderId = @OrderId and op.StepName = @StepName ");
            SqlParameter[] parms = {
                                     new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier),
                                     new SqlParameter("@StepName",SqlDbType.NVarChar,20)
                                   };
            parms[0].Value = orderId;
            parms[1].Value = orderStep;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AfdDbConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new OrderProcessInfo();
                        model.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.OrderId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.StaffCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.StepName = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        model.Pictures = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.DeviceId = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.Latlng = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.LatlngPlace = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.Ip = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.IpPlace = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.RecordDate = reader.IsDBNull(10) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(10);
                        model.LastUpdatedDate = reader.IsDBNull(11) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(11);
                    }
                }
            }

            return model;
        }

        public OrderProcessInfo GetModelByJoin(string orderCode, string customerCode, string orderStep, string staffCode, string deviceId)
        {
            OrderProcessInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 op.Id,op.OrderId,op.StaffCode,op.StepName,op.Pictures,op.DeviceId,op.Latlng,op.LatlngPlace,op.Ip,op.IpPlace,op.RecordDate,op.LastUpdatedDate 
                        from OrderProcess op
                        left join OrderMake o on o.Id = op.OrderId
						where o.OrderCode = @OrderCode and o.CustomerCode = @CustomerCode and op.StepName = @StepName and op.StaffCode = @StaffCode and op.DeviceId = @DeviceId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@OrderCode",SqlDbType.VarChar,36),
                                     new SqlParameter("@CustomerCode",SqlDbType.VarChar,36),
                                     new SqlParameter("@StepName",SqlDbType.VarChar,36),
                                     new SqlParameter("@StaffCode",SqlDbType.VarChar,36),
                                     new SqlParameter("@DeviceId",SqlDbType.VarChar,100)
                                   };
            parms[0].Value = orderCode;
            parms[1].Value = customerCode;
            parms[2].Value = orderStep;
            parms[3].Value = staffCode;
            parms[4].Value = deviceId;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AfdDbConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new OrderProcessInfo();
                        model.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.OrderId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.StaffCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.StepName = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        model.Pictures = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.DeviceId = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.Latlng = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.LatlngPlace = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.Ip = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.IpPlace = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.RecordDate = reader.IsDBNull(10) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(10);
                        model.LastUpdatedDate = reader.IsDBNull(11) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(11);
                    }
                }
            }

            return model;
        }

        public int DeleteByOrder(Guid orderId)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from OrderProcess where OrderId = @OrderId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = orderId;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AfdDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        #endregion
    }
}
