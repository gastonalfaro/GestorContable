using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Presentacion.Contingentes
{
    public class Global : System.Web.HttpApplication
    {
        void Application_Start(object sender, EventArgs e) { }
        void Application_End(object sender, EventArgs e) { }
        void Application_Error(object sender, EventArgs e) { }
        void Session_Start(object sender, EventArgs e) { }
        void Session_End(object sender, EventArgs e) { }

        // Set no cache for all the pages. This should be improved so static contents are cached.
        void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();
        }
    }
}
