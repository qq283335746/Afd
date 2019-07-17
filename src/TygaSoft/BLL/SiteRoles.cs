﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace TygaSoft.BLL
{
    public partial class SiteRoles
    {
        #region SiteRoles Member

        public Guid[] GetAspnetRoleIds(string appName, string[] names)
        {
            var sqlIn = new StringBuilder(300);
            foreach(var item in names)
            {
                sqlIn.AppendFormat("'{0}',", item);
            }
            var sqlWhere = string.Format("and RoleName in ({0}) ", sqlIn.ToString().Trim(','));
            return dal.GetAspnetList(appName,sqlWhere, null).Select(m => m.Id).ToArray();
        }

        public SiteRolesInfo GetAspnetModel(string appName, string name)
        {
            return dal.GetAspnetModel(appName, name);
        }

        public List<SiteRolesInfo> GetAspnetList(string appName, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetAspnetList(appName,sqlWhere, cmdParms);
        }

        public SiteRolesInfo GetModel(string name)
        {
            return dal.GetModel(name);
        }

        public int UpdateAspnetRoles(SiteRolesInfo model)
        {
            return dal.UpdateAspnetRoles(model);
        }

        #endregion
    }
}
