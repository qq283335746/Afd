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
    public partial class OrderMake : IOrderMake
    {
        #region IOrderMake Member

        public int Insert(OrderMakeInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into OrderMake (AppCode,UserId,CustomerCode,OrderCode,FromName,FromAddress,FromPhone,ToCity,ToName,ToAddress,ToPhone,StaffCode,StaffCodeOfTake,TakeTime,ReachTime,CargoName,ServiceProduct,PieceQty,Weight,TranPrice,IncreServicePrice,TotalPrice,PayWay,Remark,RecordDate,LastUpdatedDate)
			            values
						(@AppCode,@UserId,@CustomerCode,@OrderCode,@FromName,@FromAddress,@FromPhone,@ToCity,@ToName,@ToAddress,@ToPhone,@StaffCode,@StaffCodeOfTake,@TakeTime,@ReachTime,@CargoName,@ServiceProduct,@PieceQty,@Weight,@TranPrice,@IncreServicePrice,@TotalPrice,@PayWay,@Remark,@RecordDate,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@AppCode",SqlDbType.Char,6),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@CustomerCode",SqlDbType.VarChar,36),
new SqlParameter("@OrderCode",SqlDbType.VarChar,36),
new SqlParameter("@FromName",SqlDbType.NVarChar,50),
new SqlParameter("@FromAddress",SqlDbType.NVarChar,256),
new SqlParameter("@FromPhone",SqlDbType.VarChar,20),
new SqlParameter("@ToCity",SqlDbType.NVarChar,50),
new SqlParameter("@ToName",SqlDbType.NVarChar,50),
new SqlParameter("@ToAddress",SqlDbType.NVarChar,256),
new SqlParameter("@ToPhone",SqlDbType.VarChar,20),
new SqlParameter("@StaffCode",SqlDbType.VarChar,36),
new SqlParameter("@StaffCodeOfTake",SqlDbType.VarChar,36),
new SqlParameter("@TakeTime",SqlDbType.DateTime),
new SqlParameter("@ReachTime",SqlDbType.DateTime),
new SqlParameter("@CargoName",SqlDbType.NVarChar,256),
new SqlParameter("@ServiceProduct",SqlDbType.NVarChar,256),
new SqlParameter("@PieceQty",SqlDbType.Int),
new SqlParameter("@Weight",SqlDbType.Float),
new SqlParameter("@TranPrice",SqlDbType.Decimal),
new SqlParameter("@IncreServicePrice",SqlDbType.Decimal),
new SqlParameter("@TotalPrice",SqlDbType.Decimal),
new SqlParameter("@PayWay",SqlDbType.NVarChar,30),
new SqlParameter("@Remark",SqlDbType.NVarChar,256),
new SqlParameter("@RecordDate",SqlDbType.DateTime),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.AppCode;
            parms[1].Value = model.UserId;
            parms[2].Value = model.CustomerCode;
            parms[3].Value = model.OrderCode;
            parms[4].Value = model.FromName;
            parms[5].Value = model.FromAddress;
            parms[6].Value = model.FromPhone;
            parms[7].Value = model.ToCity;
            parms[8].Value = model.ToName;
            parms[9].Value = model.ToAddress;
            parms[10].Value = model.ToPhone;
            parms[11].Value = model.StaffCode;
            parms[12].Value = model.StaffCodeOfTake;
            parms[13].Value = model.TakeTime;
            parms[14].Value = model.ReachTime;
            parms[15].Value = model.CargoName;
            parms[16].Value = model.ServiceProduct;
            parms[17].Value = model.PieceQty;
            parms[18].Value = model.Weight;
            parms[19].Value = model.TranPrice;
            parms[20].Value = model.IncreServicePrice;
            parms[21].Value = model.TotalPrice;
            parms[22].Value = model.PayWay;
            parms[23].Value = model.Remark;
            parms[24].Value = model.RecordDate;
            parms[25].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AfdDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(OrderMakeInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into OrderMake (Id,AppCode,UserId,CustomerCode,OrderCode,FromName,FromAddress,FromPhone,ToCity,ToName,ToAddress,ToPhone,StaffCode,StaffCodeOfTake,TakeTime,ReachTime,CargoName,ServiceProduct,PieceQty,Weight,TranPrice,IncreServicePrice,TotalPrice,PayWay,Remark,RecordDate,LastUpdatedDate)
			            values
						(@Id,@AppCode,@UserId,@CustomerCode,@OrderCode,@FromName,@FromAddress,@FromPhone,@ToCity,@ToName,@ToAddress,@ToPhone,@StaffCode,@StaffCodeOfTake,@TakeTime,@ReachTime,@CargoName,@ServiceProduct,@PieceQty,@Weight,@TranPrice,@IncreServicePrice,@TotalPrice,@PayWay,@Remark,@RecordDate,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@AppCode",SqlDbType.Char,6),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@CustomerCode",SqlDbType.VarChar,36),
new SqlParameter("@OrderCode",SqlDbType.VarChar,36),
new SqlParameter("@FromName",SqlDbType.NVarChar,50),
new SqlParameter("@FromAddress",SqlDbType.NVarChar,256),
new SqlParameter("@FromPhone",SqlDbType.VarChar,20),
new SqlParameter("@ToCity",SqlDbType.NVarChar,50),
new SqlParameter("@ToName",SqlDbType.NVarChar,50),
new SqlParameter("@ToAddress",SqlDbType.NVarChar,256),
new SqlParameter("@ToPhone",SqlDbType.VarChar,20),
new SqlParameter("@StaffCode",SqlDbType.VarChar,36),
new SqlParameter("@StaffCodeOfTake",SqlDbType.VarChar,36),
new SqlParameter("@TakeTime",SqlDbType.DateTime),
new SqlParameter("@ReachTime",SqlDbType.DateTime),
new SqlParameter("@CargoName",SqlDbType.NVarChar,256),
new SqlParameter("@ServiceProduct",SqlDbType.NVarChar,256),
new SqlParameter("@PieceQty",SqlDbType.Int),
new SqlParameter("@Weight",SqlDbType.Float),
new SqlParameter("@TranPrice",SqlDbType.Decimal),
new SqlParameter("@IncreServicePrice",SqlDbType.Decimal),
new SqlParameter("@TotalPrice",SqlDbType.Decimal),
new SqlParameter("@PayWay",SqlDbType.NVarChar,30),
new SqlParameter("@Remark",SqlDbType.NVarChar,256),
new SqlParameter("@RecordDate",SqlDbType.DateTime),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.AppCode;
            parms[2].Value = model.UserId;
            parms[3].Value = model.CustomerCode;
            parms[4].Value = model.OrderCode;
            parms[5].Value = model.FromName;
            parms[6].Value = model.FromAddress;
            parms[7].Value = model.FromPhone;
            parms[8].Value = model.ToCity;
            parms[9].Value = model.ToName;
            parms[10].Value = model.ToAddress;
            parms[11].Value = model.ToPhone;
            parms[12].Value = model.StaffCode;
            parms[13].Value = model.StaffCodeOfTake;
            parms[14].Value = model.TakeTime;
            parms[15].Value = model.ReachTime;
            parms[16].Value = model.CargoName;
            parms[17].Value = model.ServiceProduct;
            parms[18].Value = model.PieceQty;
            parms[19].Value = model.Weight;
            parms[20].Value = model.TranPrice;
            parms[21].Value = model.IncreServicePrice;
            parms[22].Value = model.TotalPrice;
            parms[23].Value = model.PayWay;
            parms[24].Value = model.Remark;
            parms[25].Value = model.RecordDate;
            parms[26].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AfdDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(OrderMakeInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update OrderMake set AppCode = @AppCode,UserId = @UserId,CustomerCode = @CustomerCode,OrderCode = @OrderCode,FromName = @FromName,FromAddress = @FromAddress,FromPhone = @FromPhone,ToCity = @ToCity,ToName = @ToName,ToAddress = @ToAddress,ToPhone = @ToPhone,StaffCode = @StaffCode,StaffCodeOfTake = @StaffCodeOfTake,TakeTime = @TakeTime,ReachTime = @ReachTime,CargoName = @CargoName,ServiceProduct = @ServiceProduct,PieceQty = @PieceQty,Weight = @Weight,TranPrice = @TranPrice,IncreServicePrice = @IncreServicePrice,TotalPrice = @TotalPrice,PayWay = @PayWay,Remark = @Remark,RecordDate = @RecordDate,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@AppCode",SqlDbType.Char,6),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@CustomerCode",SqlDbType.VarChar,36),
new SqlParameter("@OrderCode",SqlDbType.VarChar,36),
new SqlParameter("@FromName",SqlDbType.NVarChar,50),
new SqlParameter("@FromAddress",SqlDbType.NVarChar,256),
new SqlParameter("@FromPhone",SqlDbType.VarChar,20),
new SqlParameter("@ToCity",SqlDbType.NVarChar,50),
new SqlParameter("@ToName",SqlDbType.NVarChar,50),
new SqlParameter("@ToAddress",SqlDbType.NVarChar,256),
new SqlParameter("@ToPhone",SqlDbType.VarChar,20),
new SqlParameter("@StaffCode",SqlDbType.VarChar,36),
new SqlParameter("@StaffCodeOfTake",SqlDbType.VarChar,36),
new SqlParameter("@TakeTime",SqlDbType.DateTime),
new SqlParameter("@ReachTime",SqlDbType.DateTime),
new SqlParameter("@CargoName",SqlDbType.NVarChar,256),
new SqlParameter("@ServiceProduct",SqlDbType.NVarChar,256),
new SqlParameter("@PieceQty",SqlDbType.Int),
new SqlParameter("@Weight",SqlDbType.Float),
new SqlParameter("@TranPrice",SqlDbType.Decimal),
new SqlParameter("@IncreServicePrice",SqlDbType.Decimal),
new SqlParameter("@TotalPrice",SqlDbType.Decimal),
new SqlParameter("@PayWay",SqlDbType.NVarChar,30),
new SqlParameter("@Remark",SqlDbType.NVarChar,256),
new SqlParameter("@RecordDate",SqlDbType.DateTime),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.AppCode;
            parms[2].Value = model.UserId;
            parms[3].Value = model.CustomerCode;
            parms[4].Value = model.OrderCode;
            parms[5].Value = model.FromName;
            parms[6].Value = model.FromAddress;
            parms[7].Value = model.FromPhone;
            parms[8].Value = model.ToCity;
            parms[9].Value = model.ToName;
            parms[10].Value = model.ToAddress;
            parms[11].Value = model.ToPhone;
            parms[12].Value = model.StaffCode;
            parms[13].Value = model.StaffCodeOfTake;
            parms[14].Value = model.TakeTime;
            parms[15].Value = model.ReachTime;
            parms[16].Value = model.CargoName;
            parms[17].Value = model.ServiceProduct;
            parms[18].Value = model.PieceQty;
            parms[19].Value = model.Weight;
            parms[20].Value = model.TranPrice;
            parms[21].Value = model.IncreServicePrice;
            parms[22].Value = model.TotalPrice;
            parms[23].Value = model.PayWay;
            parms[24].Value = model.Remark;
            parms[25].Value = model.RecordDate;
            parms[26].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AfdDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from OrderMake where Id = @Id ");
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
                sb.Append(@"delete from OrderMake where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.AfdDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public OrderMakeInfo GetModel(Guid id)
        {
            OrderMakeInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,AppCode,UserId,CustomerCode,OrderCode,FromName,FromAddress,FromPhone,ToCity,ToName,ToAddress,ToPhone,StaffCode,StaffCodeOfTake,TakeTime,ReachTime,CargoName,ServiceProduct,PieceQty,Weight,TranPrice,IncreServicePrice,TotalPrice,PayWay,Remark,RecordDate,LastUpdatedDate 
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
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
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
                        model.RecordDate = reader.IsDBNull(25) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(25);
                        model.LastUpdatedDate = reader.IsDBNull(26) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(26);
                    }
                }
            }

            return model;
        }

        public IList<OrderMakeInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
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
			          Id,AppCode,UserId,CustomerCode,OrderCode,FromName,FromAddress,FromPhone,ToCity,ToName,ToAddress,ToPhone,StaffCode,StaffCodeOfTake,TakeTime,ReachTime,CargoName,ServiceProduct,PieceQty,Weight,TranPrice,IncreServicePrice,TotalPrice,PayWay,Remark,RecordDate,LastUpdatedDate
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
                        model.AppCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.UserId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.CustomerCode = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.OrderCode = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.FromName = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.FromAddress = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.FromPhone = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.ToCity = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.ToName = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                        model.ToAddress = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                        model.ToPhone = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                        model.StaffCode = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                        model.StaffCodeOfTake = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
                        model.TakeTime = reader.IsDBNull(15) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(15);
                        model.ReachTime = reader.IsDBNull(16) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(16);
                        model.CargoName = reader.IsDBNull(17) ? string.Empty : reader.GetString(17);
                        model.ServiceProduct = reader.IsDBNull(18) ? string.Empty : reader.GetString(18);
                        model.PieceQty = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);
                        model.Weight = reader.IsDBNull(20) ? 0D : reader.GetDouble(20);
                        model.TranPrice = reader.IsDBNull(21) ? 0 : reader.GetDecimal(21);
                        model.IncreServicePrice = reader.IsDBNull(22) ? 0 : reader.GetDecimal(22);
                        model.TotalPrice = reader.IsDBNull(23) ? 0 : reader.GetDecimal(23);
                        model.PayWay = reader.IsDBNull(24) ? string.Empty : reader.GetString(24);
                        model.Remark = reader.IsDBNull(25) ? string.Empty : reader.GetString(25);
                        model.RecordDate = reader.IsDBNull(26) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(26);
                        model.LastUpdatedDate = reader.IsDBNull(27) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(27);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderMakeInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           Id,AppCode,UserId,CustomerCode,OrderCode,FromName,FromAddress,FromPhone,ToCity,ToName,ToAddress,ToPhone,StaffCode,StaffCodeOfTake,TakeTime,ReachTime,CargoName,ServiceProduct,PieceQty,Weight,TranPrice,IncreServicePrice,TotalPrice,PayWay,Remark,RecordDate,LastUpdatedDate
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
                        model.AppCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.UserId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.CustomerCode = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.OrderCode = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.FromName = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.FromAddress = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.FromPhone = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.ToCity = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.ToName = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                        model.ToAddress = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                        model.ToPhone = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                        model.StaffCode = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                        model.StaffCodeOfTake = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
                        model.TakeTime = reader.IsDBNull(15) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(15);
                        model.ReachTime = reader.IsDBNull(16) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(16);
                        model.CargoName = reader.IsDBNull(17) ? string.Empty : reader.GetString(17);
                        model.ServiceProduct = reader.IsDBNull(18) ? string.Empty : reader.GetString(18);
                        model.PieceQty = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);
                        model.Weight = reader.IsDBNull(20) ? 0D : reader.GetDouble(20);
                        model.TranPrice = reader.IsDBNull(21) ? 0 : reader.GetDecimal(21);
                        model.IncreServicePrice = reader.IsDBNull(22) ? 0 : reader.GetDecimal(22);
                        model.TotalPrice = reader.IsDBNull(23) ? 0 : reader.GetDecimal(23);
                        model.PayWay = reader.IsDBNull(24) ? string.Empty : reader.GetString(24);
                        model.Remark = reader.IsDBNull(25) ? string.Empty : reader.GetString(25);
                        model.RecordDate = reader.IsDBNull(26) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(26);
                        model.LastUpdatedDate = reader.IsDBNull(27) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(27);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderMakeInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select Id,AppCode,UserId,CustomerCode,OrderCode,FromName,FromAddress,FromPhone,ToCity,ToName,ToAddress,ToPhone,StaffCode,StaffCodeOfTake,TakeTime,ReachTime,CargoName,ServiceProduct,PieceQty,Weight,TranPrice,IncreServicePrice,TotalPrice,PayWay,Remark,RecordDate,LastUpdatedDate
                        from OrderMake ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate desc ");

            IList<OrderMakeInfo> list = new List<OrderMakeInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AfdDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderMakeInfo model = new OrderMakeInfo();
                        model.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
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
                        model.RecordDate = reader.IsDBNull(25) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(25);
                        model.LastUpdatedDate = reader.IsDBNull(26) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(26);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderMakeInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select Id,AppCode,UserId,CustomerCode,OrderCode,FromName,FromAddress,FromPhone,ToCity,ToName,ToAddress,ToPhone,StaffCode,StaffCodeOfTake,TakeTime,ReachTime,CargoName,ServiceProduct,PieceQty,Weight,TranPrice,IncreServicePrice,TotalPrice,PayWay,Remark,RecordDate,LastUpdatedDate 
			            from OrderMake
					    order by LastUpdatedDate desc ");

            IList<OrderMakeInfo> list = new List<OrderMakeInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AfdDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderMakeInfo model = new OrderMakeInfo();
                        model.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
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
                        model.RecordDate = reader.IsDBNull(25) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(25);
                        model.LastUpdatedDate = reader.IsDBNull(26) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(26);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
