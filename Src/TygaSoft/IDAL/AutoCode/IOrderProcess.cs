using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IOrderProcess
    {
        #region IOrderProcess Member

        int Insert(OrderProcessInfo model);

        int InsertByOutput(OrderProcessInfo model);

        int Update(OrderProcessInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        OrderProcessInfo GetModel(Guid id);

        IList<OrderProcessInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderProcessInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderProcessInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderProcessInfo> GetList();

        #endregion
    }
}
