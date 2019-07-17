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
    public partial class OrderProcess
    {
        #region OrderProcess Member

        public IList<OrderProcessInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public OrderProcessInfo GetModel(Guid orderId, string orderStep)
        {
            return dal.GetModel(orderId, orderStep);
        }

        public OrderProcessInfo GetModelByJoin(string orderCode, string customerCode, string orderStep, string staffCode, string deviceId)
        {
            return dal.GetModelByJoin(orderCode, customerCode, orderStep, staffCode, deviceId);
        }

        public int DeleteByOrder(Guid orderId)
        {
            return dal.DeleteByOrder(orderId);
        }

        #endregion
    }
}
