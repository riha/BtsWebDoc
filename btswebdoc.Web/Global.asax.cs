using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using btswebdoc.Shared.Exceptions;

namespace btswebdoc.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute() { ExceptionType = typeof(MissingDocumentationException), View = "MissingDocumentationError" }, 2);
            filters.Add(new HandleErrorAttribute(), 1);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Favicon",
                "favicon.ico",
                new { controller = "Assets", action = "Favicon" });

            routes.MapRoute(
                "XmlAssets",
                "Assets/{action}/{artifactid}.xml",
                new { controller = "Assets", action = "Index" });

            routes.MapRoute(
                "VersionedXmlAssets",
                "{version}/Assets/{action}/{artifactid}.xml",
                new { controller = "Assets", action = "Index" });

            routes.MapRoute(
                "JpegAssets",
                "Assets/{action}/{artifactid}.jpeg",
                new { controller = "Assets", action = "Index" });

            routes.MapRoute(
                "VersionedJpegAssets",
                "{version}/Assets/{action}/{artifactid}.jpeg",
                new { controller = "Assets", action = "Index" });

            routes.MapRoute(
                "DefaultApplication",
                "Application/{artifactid}",
                new { controller = "Application", action = "Index" });

            routes.MapRoute(
                "VersionedApplication",
                "{version}/Application/{artifactid}",
                new { controller = "Application", action = "Index" });

            routes.MapRoute(
              "CatchAllApplicationArtifact",
              "Application/{applicationName}/{action}/{*artifactid}",
              new { controller = "Application", action = "Index" }, new { action = "ReceivePort|SendPort" });

            routes.MapRoute(
              "CatchAllVersionedApplicationArtifact",
              "{version}/Application/{applicationName}/{action}/{*artifactid}",
              new { controller = "Application", action = "Index" }, new { action = "ReceivePort|SendPort" });

            routes.MapRoute(
                "ApplicationArtifact",
                "Application/{applicationName}/{action}/{artifactid}/{name}",
                new { controller = "Application", action = "Index", name = UrlParameter.Optional });

            routes.MapRoute(
                "VersionedApplicationArtifact",
                "{version}/Application/{applicationName}/{action}/{artifactid}/{name}",
                new { controller = "Application", action = "Index", name = UrlParameter.Optional });

            routes.MapRoute(
               "Host",
               "Host/{artifactid}",
               new { controller = "Host", action = "Index" });

            routes.MapRoute(
               "VersionedHost",
               "{version}/Host/{artifactid}",
               new { controller = "Host", action = "Index" });

            routes.MapRoute(
                "VersionHome",
                "{version}/{action}",
                new { controller = "Home", action = "Index" });

            routes.MapRoute(
                "Home",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = "Latest" });

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}