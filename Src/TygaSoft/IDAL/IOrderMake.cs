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

        DataSet GetExportData(string sqlWhere, params SqlParameter[] cmdParms);

        OrderMakeInfo GetModel(string orderCode, string customerCode);

        OrderMakeInfo GetModelByJoin(Guid id);

        IList<OrderMakeInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        #endregion
    }
}
