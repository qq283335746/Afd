using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;
using TygaSoft.IDAL;

namespace TygaSoft.DALFactory
{
    public sealed class DataAccess
    {
        private static readonly string path = ConfigurationManager.AppSettings["WebDAL"];

        #region 公共

        public static ISitePicture CreateSitePicture()
        {
            string className = path + ".SitePicture";
            return (ISitePicture)Assembly.Load(path).CreateInstance(className);
        }

        public static IFeatureUser CreateFeatureUser()
        {
            string className = path + ".FeatureUser";
            return (IFeatureUser)Assembly.Load(path).CreateInstance(className);
        }

        #endregion

        #region 订单

        public static IOrderMake CreateOrderMake()
        {
            string className = path + ".OrderMake";
            return (IOrderMake)Assembly.Load(path).CreateInstance(className);
        }

        public static IOrderProcess CreateOrderProcess()
        {
            string className = path + ".OrderProcess";
            return (IOrderProcess)Assembly.Load(path).CreateInstance(className);
        }

        #endregion

        #region 基础数据

        public static ICustomer CreateCustomer()
        {
            string className = path + ".Customer";
            return (ICustomer)Assembly.Load(path).CreateInstance(className);
        }

        #endregion

        #region 系统管理

        public static ISiteMulti CreateSiteMulti()
        {
            string className = path + ".SiteMulti";
            return (ISiteMulti)Assembly.Load(path).CreateInstance(className);
        }

        public static ISiteMenus CreateSiteMenus()
        {
            string className = path + ".SiteMenus";
            return (ISiteMenus)Assembly.Load(path).CreateInstance(className);
        }

        public static ISiteMenusAccess CreateSiteMenusAccess()
        {
            string className = path + ".SiteMenusAccess";
            return (ISiteMenusAccess)Assembly.Load(path).CreateInstance(className);
        }

        #endregion

        #region 成员资格

        public static ISiteUsers CreateSiteUsers()
        {
            string className = path + ".SiteUsers";
            return (ISiteUsers)Assembly.Load(path).CreateInstance(className);
        }
        public static ISiteMembers CreateSiteMembers()
        {
            string className = path + ".SiteMembers";
            return (ISiteMembers)Assembly.Load(path).CreateInstance(className);
        }
        public static ISiteRoles CreateSiteRoles()
        {
            string className = path + ".SiteRoles";
            return (ISiteRoles)Assembly.Load(path).CreateInstance(className);
        }
        public static IUsersInRoles CreateUsersInRoles()
        {
            string className = path + ".UsersInRoles";
            return (IUsersInRoles)Assembly.Load(path).CreateInstance(className);
        }
        public static IApplications CreateApplications()
        {
            string className = path + ".Applications";
            return (IApplications)Assembly.Load(path).CreateInstance(className);
        }

        #endregion
    }
}
