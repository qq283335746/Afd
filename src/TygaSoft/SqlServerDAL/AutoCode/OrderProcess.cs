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
    public partial class OrderProcess : IOrderProcess
    {
        #region IOrderProcess Member

        public int Insert(OrderProcessInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into OrderProcess (OrderId,StaffCode,StepName,Pictures,DeviceId,Latlng,LatlngPlace,Ip,IpPlace,RecordDate,LastUpdatedDate)
			            values
						(@OrderId,@StaffCode,@StepName,@Pictures,@DeviceId,@Latlng,@LatlngPlace,@Ip,@IpPlace,@RecordDate,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier),
new SqlParameter("@StaffCode",SqlDbType.VarChar,36),
new SqlParameter("@StepName",SqlDbType.NVarChar,20),
new SqlParameter("@Pictures",SqlDbType.NVarChar,1000),
new SqlParameter("@DeviceId",SqlDbType.VarChar,100),
new SqlParameter("@Latlng",SqlDbType.VarChar,100),
new SqlParameter("@LatlngPlace",SqlDbType.NVarChar,256),
new SqlParameter("@Ip",SqlDbType.VarChar,20),
new SqlParameter("@IpPlace",SqlDbType.NVarChar,256),
new SqlParameter("@RecordDate",SqlDbType.DateTime),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.OrderId;
            parms[1].Value = model.StaffCode;
            parms[2].Value = model.StepName;
            parms[3].Value = model.Pictures;
            parms[4].Value = model.DeviceId;
            parms[5].Value = model.Latlng;
            parms[6].Value = model.LatlngPlace;
            parms[7].Value = model.Ip;
            parms[8].Value = model.IpPlace;
            parms[9].Value = model.RecordDate;
            parms[10].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AfdDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(OrderProcessInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into OrderProcess (Id,OrderId,StaffCode,StepName,Pictures,DeviceId,Latlng,LatlngPlace,Ip,IpPlace,RecordDate,LastUpdatedDate)
			            values
						(@Id,@OrderId,@StaffCode,@StepName,@Pictures,@DeviceId,@Latlng,@LatlngPlace,@Ip,@IpPlace,@RecordDate,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier),
new SqlParameter("@StaffCode",SqlDbType.VarChar,36),
new SqlParameter("@StepName",SqlDbType.NVarChar,20),
new SqlParameter("@Pictures",SqlDbType.NVarChar,1000),
new SqlParameter("@DeviceId",SqlDbType.VarChar,100),
new SqlParameter("@Latlng",SqlDbType.VarChar,100),
new SqlParameter("@LatlngPlace",SqlDbType.NVarChar,256),
new SqlParameter("@Ip",SqlDbType.VarChar,20),
new SqlParameter("@IpPlace",SqlDbType.NVarChar,256),
new SqlParameter("@RecordDate",SqlDbType.DateTime),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.OrderId;
            parms[2].Value = model.StaffCode;
            parms[3].Value = model.StepName;
            parms[4].Value = model.Pictures;
            parms[5].Value = model.DeviceId;
            parms[6].Value = model.Latlng;
            parms[7].Value = model.LatlngPlace;
            parms[8].Value = model.Ip;
            parms[9].Value = model.IpPlace;
            parms[10].Value = model.RecordDate;
            parms[11].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AfdDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(OrderProcessInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update OrderProcess set OrderId = @OrderId,StaffCode = @StaffCode,StepName = @StepName,Pictures = @Pictures,DeviceId = @DeviceId,Latlng = @Latlng,LatlngPlace = @LatlngPlace,Ip = @Ip,IpPlace = @IpPlace,RecordDate = @RecordDate,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier),
new SqlParameter("@StaffCode",SqlDbType.VarChar,36),
new SqlParameter("@StepName",SqlDbType.NVarChar,20),
new SqlParameter("@Pictures",SqlDbType.NVarChar,1000),
new SqlParameter("@DeviceId",SqlDbType.VarChar,100),
new SqlParameter("@Latlng",SqlDbType.VarChar,100),
new SqlParameter("@LatlngPlace",SqlDbType.NVarChar,256),
new SqlParameter("@Ip",SqlDbType.VarChar,20),
new SqlParameter("@IpPlace",SqlDbType.NVarChar,256),
new SqlParameter("@RecordDate",SqlDbType.DateTime),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.OrderId;
            parms[2].Value = model.StaffCode;
            parms[3].Value = model.StepName;
            parms[4].Value = model.Pictures;
            parms[5].Value = model.DeviceId;
            parms[6].Value = model.Latlng;
            parms[7].Value = model.LatlngPlace;
            parms[8].Value = model.Ip;
            parms[9].Value = model.IpPlace;
            parms[10].Value = model.RecordDate;
            parms[11].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AfdDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from OrderProcess where Id = @Id ");
            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = id;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AfdDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public bool DeleteBatch(IList<object> list)
        {
            StringBuilder sb = new StringBuilder(500);
            ParamsHelper parms = new ParamsHelper();
            int n = 0;
            foreach (string item in list)
            {
                n++;
                sb.Append(@"delete from OrderProcess where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.AfdDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public OrderProcessInfo GetModel(Guid id)
        {
            OrderProcessInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,OrderId,StaffCode,StepName,Pictures,DeviceId,Latlng,LatlngPlace,Ip,IpPlace,RecordDate,LastUpdatedDate 
			            from OrderProcess
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

        public IList<OrderProcessInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from OrderProcess ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AfdDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<OrderProcessInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,OrderId,StaffCode,StepName,Pictures,DeviceId,Latlng,LatlngPlace,Ip,IpPlace,RecordDate,LastUpdatedDate
					  from OrderProcess ");
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

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderProcessInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           Id,OrderId,StaffCode,StepName,Pictures,DeviceId,Latlng,LatlngPlace,Ip,IpPlace,RecordDate,LastUpdatedDate
					   from OrderProcess ");
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

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderProcessInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select Id,OrderId,StaffCode,StepName,Pictures,DeviceId,Latlng,LatlngPlace,Ip,IpPlace,RecordDate,LastUpdatedDate
                        from OrderProcess ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate desc ");

            IList<OrderProcessInfo> list = new List<OrderProcessInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AfdDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderProcessInfo model = new OrderProcessInfo();
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

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderProcessInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select Id,OrderId,StaffCode,StepName,Pictures,DeviceId,Latlng,LatlngPlace,Ip,IpPlace,RecordDate,LastUpdatedDate 
			            from OrderProcess
					    order by LastUpdatedDate desc ");

            IList<OrderProcessInfo> list = new List<OrderProcessInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AfdDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderProcessInfo model = new OrderProcessInfo();
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

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
