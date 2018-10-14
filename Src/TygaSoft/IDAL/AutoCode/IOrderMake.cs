using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IOrderMake
    {
        #region IOrderMake Member

        int Insert(OrderMakeInfo model);

        int InsertByOutput(OrderMakeInfo model);

        int Update(OrderMakeInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        OrderMakeInfo GetModel(Guid id);

        IList<OrderMakeInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderMakeInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderMakeInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderMakeInfo> GetList();

        #endregion
    }
}
