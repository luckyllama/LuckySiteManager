using System.Reflection;
using System.Web.Routing;
using AttributeRouting;

[assembly: WebActivator.PreApplicationStartMethod(typeof(LuckySiteMonitor.Web.App_Start.AttributeRouting), "Start")]

namespace LuckySiteMonitor.Web.App_Start {
    public static class AttributeRouting {
        public static void RegisterRoutes(RouteCollection routes) {
            // See http://github.com/mccalltd/AttributeRouting/wiki/3.-Configuration for more options.
            // To debug routes locally using the built in ASP.NET development server, go to /routes.axd
            routes.MapAttributeRoutes(config => {
                config.ScanAssembly(Assembly.GetExecutingAssembly());
                config.UseLowercaseRoutes = true;
            });
        }

        public static void Start() {
            RegisterRoutes(RouteTable.Routes);
        }
    }
}
