using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Pipelines;
using System.Web.Routing;
using Sitecore.Mvc.Configuration;
using System.Web.Mvc;
using Sitecore.Mvc.Presentation;
using Sitecore.Mvc.Extensions;
using System.Web.Optimization;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace SitecoreMobile.Pipelines.Loaders
{
    public class InitRoutes 
    {
        public virtual void Process(PipelineArgs args)
        {
            RegisterRoutes(RouteTable.Routes, args);

            RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add( new QueryStringMapping("type", "json", new MediaTypeHeaderValue("application/json"))); 
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.MediaTypeMappings.Add( new QueryStringMapping("type", "xml", new MediaTypeHeaderValue("application/xml")));

        }

        private void RegisterBundles(BundleCollection bundleCollection)
        {

            bundleCollection.Add(
               new StyleBundle("~/bundles/standardstyle").Include(
                   "~/Content/Site.css",
                   "~/Content/jqmSymIco.css",
                   "~/Content/jqmStandard.css"
                   ));

            bundleCollection.Add(
                new ScriptBundle("~/bundles/standardscript").Include());

            bundleCollection.Add(
               new StyleBundle("~/bundles/symnastyle").Include(
                   "~/Content/Site.css",
                   "~/Content/jqmSymIco.css",
                   "~/Content/jqmSymNA.css"
                   ));

            bundleCollection.Add(
                new ScriptBundle("~/bundles/symnascript").Include());


            bundleCollection.Add(
               new StyleBundle("~/bundles/symeustyle").Include(
                   "~/Content/Site.css",
                   "~/Content/jqmSymIco.css",
                   "~/Content/jqmSymEU.css"
                   ));

            bundleCollection.Add(
                new ScriptBundle("~/bundles/symeuscript").Include());

            bundleCollection.Add(
                new StyleBundle("~/bundles/scmobilestyle01").Include(
                    "~/Content/jquery.mobile.structure-1.4.0.css",
                    "~/Content/jquery.mobile.icons-1.4.0.min.css",
                    "~/Content/SitecoreMobile.css",
                    "~/Content/camera.css" 
                    ));

            bundleCollection.Add(
                new ScriptBundle("~/bundles/scmobilescript01").Include(
                    "~/Scripts/jquery-1.7.1.min.js",
                    "~/Scripts/jqmSitecoreNet.js",
                    "~/Scripts/modernizr-2.5.3.js",
                    "~/Scripts/jquery.mobile-1.4.0.js",
                    "~/Scripts/jquery.easing.1.3.js",
                    "~/Scripts/jquery.hoverIntent.minified.js",
                    "~/Scripts/camera.js"));


            bundleCollection.Add(
                new ScriptBundle("~/bundles/editingextensions").Include(
                    "~/Scripts/SitecoreMobileEditingExtensions.js"));

        }

        protected virtual void RegisterRoutes(System.Web.Routing.RouteCollection routes, PipelineArgs args)
        {
            MapSearchController(routes);
        }

        private static void MapSearchController(System.Web.Routing.RouteCollection routes)
        {
            System.Web.Mvc.RouteCollectionExtensions.MapRoute(routes, "SearchRoute", "search/{action}/{*scItemPath}",
                new
                {
                    controller = "Search",
                    scKeysToIgnore = new string[] { }
                },
                new
                {
                }
            );
            System.Web.Mvc.RouteCollectionExtensions.MapRoute(routes, "Test", "test/{action}/{*scItemPath}",
                new
                {
                    controller = "Test",
                    scKeysToIgnore = new string[] { }
                },
                new
                {
                }
            );
            System.Web.Mvc.RouteCollectionExtensions.MapRoute(routes, "Mobile", "-/mobile/{controller}/{action}/",
                new
                {
                    scKeysToIgnore = new string[] { }
                },
                new
                {
                }
            );
            System.Web.Mvc.RouteCollectionExtensions.MapRoute(routes, "Experience", "-/experience/{action}/{identifier}/{*scItemPath}",
                new
                {
                    controller = "Experience",
                    scItemPath = "Profile/ExperienceAction",
                    scKeysToIgnore = new string[] { }
                },
                new
                {
                }
            );


            GlobalConfiguration.Configuration.Routes.MapHttpRoute("ExperienceApi", "-/expapi/{action}/{identifier}",
                new {
                    controller = "ExperienceApi",
                    identifier = RouteParameter.Optional
                });

            GlobalConfiguration.Configuration.Routes.MapHttpRoute("Report", "-/report/{action}",
                new
                {
                    controller = "Report"
                });


        }

        public class isValidId : IRouteConstraint
        {
            public bool Match(
                HttpContextBase httpContext,
                Route route,
                string parameterName,
                RouteValueDictionary values,
                RouteDirection routeDirection)
            {
                string str = values["id"] as string;
                if (String.IsNullOrEmpty(str))
                {
                    return false;
                }
                else
                {
                    // run better check for id existance
                    return true;
                }
            }
        }



        public class IsContentUrlRestraint : IRouteConstraint
        {
            public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
            {
                if (routeDirection == RouteDirection.IncomingRequest)
                {
                    string str = httpContext.Items[(object)"sc::IsContentUrl"] as string;
                    if (str == null)
                        return false;
                    else
                        return StringExtensions.ToBool(str);
                }
                else
                {
                    return true;
                }
            }
        }

    }
}