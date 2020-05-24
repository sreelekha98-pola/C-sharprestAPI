using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;

namespace SAMPLEAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_PostAuthorizeRequest() { HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required); }

		
		protected void Session_End(object sender, EventArgs e)
		{
			foreach (String obj in this.Session)
			{
				if (Session[obj] is Unisys.Component)
				{
					try
					{
						((Unisys.Component)Session[obj]).Disconnect();
					}
					catch { }
				}
			}
		}

		protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
