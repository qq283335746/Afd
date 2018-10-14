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

        IList<OrderProcessInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        OrderProcessInfo GetModel(Guid orderId, string orderStep);

        OrderProcessInfo GetModelByJoin(string orderCode, string customerCode, string orderStep, string staffCode, string deviceId);

        int DeleteByOrder(Guid orderId);

        #endregion
    }
}
