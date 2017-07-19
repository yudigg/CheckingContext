using CheckbookBuddy.IdentityHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace CheckbookBuddy
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                string encTicket = authCookie.Value;
                if (!String.IsNullOrEmpty(encTicket))
                {
                    var ticket = FormsAuthentication.Decrypt(encTicket);
                    var id = new UserIdentity(ticket);
                    var userRoles = Roles.GetRolesForUser(id.Name);
                    var prin = new GenericPrincipal(id, userRoles);                    
                    HttpContext.Current.User = prin;
                }
            }
        }
    }
}
