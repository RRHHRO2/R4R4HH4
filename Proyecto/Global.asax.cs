using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Proyecto
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

        // Este evento se ejecuta al finalizar el procesamiento de la solicitud
        protected void Application_ReleaseRequestState(object sender, EventArgs e)
        {
            var context = HttpContext.Current;
            if (context != null && context.Response != null)
            {
                // Evitar almacenamiento en caché en el navegador
                context.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
                context.Response.Cache.SetValidUntilExpires(false);
                context.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                context.Response.Cache.SetNoStore();
            }
        }
    }
}
