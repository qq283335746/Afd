using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DALFactory;

namespace TygaSoft.BLL
{
    public partial class OrderMake
    {
        #region OrderMake Member

        public DataSet GetExportData(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetExportData(sqlWhere, cmdParms);
        }

        public OrderMakeInfo GetModel(string orderCode, string customerCode)
        {
            return dal.GetModel(orderCode, customerCode);
        }

        public OrderMakeInfo GetModelByJoin(Guid id)
        {
            return dal.GetModelByJoin(id);
        }

        public IList<OrderMakeInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        #endregion
    }
}
