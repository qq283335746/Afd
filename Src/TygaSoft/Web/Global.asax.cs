﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using System.Web.SessionState;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TygaSoft.CustomProvider;
using TygaSoft.WebHelper;
using TygaSoft.BLL;
using TygaSoft.Model;
using TygaSoft.SysHelper;
using TygaSoft.SysException;

namespace TygaSoft.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var logCfg = new System.IO.FileInfo(HttpContext.Current.Server.MapPath("~/Log4net.config"));
            log4net.Config.XmlConfigurator.ConfigureAndWatch(logCfg);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        protected void Profile_OnMigrateAnonymous(object sender, ProfileMigrateEventArgs args)
        {
            try
            {
                AnonymousIdentificationModule.ClearAnonymousIdentifier();

                new Auth().SetMigrateAnonymous();
            }
            catch(Exception ex) {
                new CustomException(string.Format("Profile_OnMigrateAnonymous--ex：{0}", ex.Message), ex);
            }
        }
    }
}